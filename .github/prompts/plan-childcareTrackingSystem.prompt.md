## Plan: MVP Theo Dõi Học Sinh

Xây dựng hệ thống theo dõi học sinh đi học gồm hai ứng dụng:

- **Frontend:** ReactJS + Vite, giao diện responsive cho nhà trường và phụ huynh.
- **Backend:** C# + ASP.NET Core Web API, Entity Framework Core và SQL Server.
- **Xác thực:** ASP.NET Core Identity hoặc JWT Bearer với hai vai trò `SchoolStaff` và `Parent`.
- **MVP:** dữ liệu thật qua API, chưa tích hợp GPS thật; trạng thái xe có thể được cập nhật thủ công để kiểm thử nghiệp vụ.

**Kiến trúc tổng thể**

```text
ReactJS/Vite
	-> API client (fetch hoặc Axios)
	-> ASP.NET Core Web API
			-> Application services
			-> Entity Framework Core
			-> SQL Server
			-> JWT authentication and role authorization
```

Frontend không truy cập database trực tiếp. Mọi dữ liệu học sinh, xe và điểm danh phải đi qua Backend API. Trong giai đoạn đầu có thể dùng mock service ở Frontend, nhưng mock service phải có cùng interface và kiểu dữ liệu với API thật.

**Steps**

### 1. Chốt yêu cầu và quy tắc nghiệp vụ

**Mục tiêu:** xác định chính xác hệ thống phải lưu và cập nhật điều gì trước khi tạo database hoặc component.

- Xác định hai vai trò:
  - `SchoolStaff`: xem toàn trường, quản lý học sinh, xe, tuyến và điểm danh.
  - `Parent`: chỉ xem học sinh được liên kết với tài khoản phụ huynh.
- Xác định trạng thái xe: `Scheduled`, `PickingUp`, `OnRoute`, `Arrived`, `Delayed`, `Completed`.
- Xác định trạng thái học sinh trên xe: `NotBoarded`, `Boarded`, `DroppedOff`, `Absent`.
- Xác định trạng thái điểm danh tại trường: `Present`, `Absent`, `Late`, `Excused`.
- Quy định chuyển trạng thái:
  - Học sinh chỉ được đánh dấu `Boarded` nếu chuyến xe đang hoạt động.
  - Học sinh có thể `Present` tại trường sau khi đã `DroppedOff`, hoặc được nhân viên xác nhận thủ công.
  - Mọi thay đổi điểm danh phải lưu người thực hiện và thời điểm cập nhật.
- Chốt các màn hình MVP: dashboard nhà trường, quản lý điểm danh, quản lý trạng thái chuyến xe, dashboard phụ huynh, chi tiết hành trình và lịch sử điểm danh.

**Đầu ra:** tài liệu nghiệp vụ ngắn, danh sách trạng thái, bảng quyền và wireframe đơn giản cho hai vai trò.

### 2. Thiết kế Backend C# và cấu trúc solution

Tạo solution tại `Backend/` theo kiến trúc nhiều project để giữ Controller mỏng và nghiệp vụ có thể kiểm thử độc lập:

```text
Backend/
	ChildcareTracking.sln
	src/
		ChildcareTracking.Api/
			Controllers/ Middleware/ Extensions/ Program.cs appsettings.json
		ChildcareTracking.Application/
			DTOs/ Interfaces/ Services/ Validators/
		ChildcareTracking.Domain/
			Entities/ Enums/ Exceptions/
		ChildcareTracking.Infrastructure/
			Persistence/ Repositories/ Identity/
	tests/
		ChildcareTracking.UnitTests/
		ChildcareTracking.IntegrationTests/
```

Trách nhiệm:

- `Domain`: entity, enum và quy tắc cốt lõi; không phụ thuộc ASP.NET Core.
- `Application`: DTO, interface và use case như cập nhật điểm danh, lấy dashboard.
- `Infrastructure`: EF Core, SQL Server, migration, repository, Identity và JWT implementation.
- `Api`: HTTP endpoint, authentication middleware, validation response và dependency injection.

**Đầu ra:** solution build được, CORS cho Frontend, Swagger/OpenAPI và `GET /api/health`.

### 3. Thiết kế database và Entity Framework Core

#### 3.1. Các bảng hiện có

Database hiện tại đã có các bảng nền tảng sau:

| Bảng                     | Công dụng                                                                                            | Quan hệ chính                                                                                                    |
| ------------------------ | ---------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------- |
| `administrative_regions` | Lưu vùng hoặc khu vực hành chính cấp cao hơn để phục vụ tra cứu địa chỉ.                             | Có thể liên kết với `provinces`.                                                                                 |
| `provinces`              | Lưu tỉnh hoặc thành phố trực thuộc trung ương.                                                       | Thuộc `administrative_regions`; có nhiều `wards`.                                                                |
| `wards`                  | Lưu phường, xã hoặc thị trấn.                                                                        | Thuộc `provinces`; được `schools` sử dụng để lưu địa chỉ.                                                        |
| `administrative_units`   | Lưu danh mục hoặc loại đơn vị hành chính dùng chung nếu hệ thống cần phân loại các cấp hành chính.   | Có thể được tham chiếu bởi các bảng địa chỉ; cần xác định rõ vai trò để tránh trùng nghĩa với các bảng địa giới. |
| `schools`                | Lưu thông tin từng trường học trong hệ thống multi-tenant. Mỗi trường là một tenant riêng.           | Liên kết với địa chỉ, người dùng và toàn bộ dữ liệu nghiệp vụ của trường.                                        |
| `roles`                  | Lưu các vai trò hệ thống, ví dụ `PlatformAdmin`, `SchoolAdmin`, `SchoolStaff`, `Parent` và `Driver`. | Liên kết với `permissions` qua `rolespermission` và với người dùng qua bảng liên kết tài khoản-trường.           |
| `permissions`            | Lưu các quyền nhỏ trong hệ thống, ví dụ xem học sinh, cập nhật điểm danh hoặc quản lý chuyến xe.     | Liên kết với `roles` qua `rolespermission`.                                                                      |
| `rolespermission`        | Bảng trung gian nhiều-nhiều giữa vai trò và quyền.                                                   | Nên chuẩn hóa tên thành `role_permissions` nếu dự án dùng snake_case.                                            |

Các ràng buộc nên có cho nhóm bảng hiện tại:

- `schools.code` là duy nhất.
- `schools.slug` là duy nhất và dùng cho URL của từng trường.
- `schools.ward_id` phải tham chiếu tới `wards.id`.
- `wards.province_id` phải tham chiếu tới `provinces.id`.
- `rolespermission` có khóa chính kết hợp gồm `role_id` và `permission_id`.
- Không xóa cứng trường, vai trò hoặc quyền đang được sử dụng; dùng trạng thái `is_active` khi phù hợp.

#### 3.2. Các bảng tài khoản và phân quyền cần bổ sung

Để phục vụ nhiều trường, cần bổ sung các bảng sau:

| Bảng                                        | Công dụng                                                                                                                                                  |
| ------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `users` hoặc các bảng ASP.NET Core Identity | Lưu thông tin xác thực như tên đăng nhập, email, mật khẩu đã hash, trạng thái khóa và thời điểm đăng nhập. Không lưu dữ liệu hồ sơ cá nhân chi tiết ở đây. |
| `user_profiles`                             | Lưu hồ sơ cá nhân dùng chung cho mọi loại người dùng như họ tên, số định danh, ngày sinh, giới tính, số điện thoại, địa chỉ và ảnh đại diện.               |
| `user_schools`                              | Liên kết người dùng với trường. Đây là bảng tenant membership, giúp một người có thể thuộc một hoặc nhiều trường.                                          |
| `user_roles` hoặc bảng Identity tương ứng   | Liên kết người dùng với vai trò. Nếu vai trò khác nhau theo từng trường, nên lưu `role_id` trong `user_schools` hoặc tạo membership role riêng.            |
| `parent_profiles`                           | Lưu thông tin nghiệp vụ của phụ huynh sau khi tài khoản được tạo.                                                                                          |
| `driver_profiles`                           | Lưu thông tin tài xế nếu tài xế cần đăng nhập hoặc được quản lý riêng.                                                                                     |

`user_profiles` có quan hệ một-một với `users` và nên có tối thiểu:

```text
id
user_id
resident_number
full_name
date_of_birth
gender
phone
ward_id
address_detail
image_url
created_at
updated_at
```

`resident_number` là dữ liệu nhạy cảm, phải giới hạn quyền truy cập và không hiển thị đầy đủ cho phụ huynh hoặc nhân viên thông thường. Không lưu `resident_number`, `date_of_birth` hoặc `full_name` lặp lại trong `parent_profiles` và `driver_profiles`.

Các bảng profile theo vai trò chỉ lưu dữ liệu đặc thù:

- `parent_profiles`: nghề nghiệp, người liên hệ khẩn cấp hoặc thông tin liên lạc bổ sung.
- `driver_profiles`: số giấy phép lái xe, loại giấy phép, ngày hết hạn và trạng thái làm việc.

`user_schools` nên có tối thiểu:

```text
id
user_id
school_id
role_id
is_active
created_at
updated_at
```

Mọi truy vấn nghiệp vụ phải xác định `school_id` từ membership hoặc JWT, không tin `school_id` tùy ý do Frontend gửi lên.

#### 3.3. Các bảng nghiệp vụ cần bổ sung

| Nhóm             | Bảng                            | Công dụng                                                                                                    |
| ---------------- | ------------------------------- | ------------------------------------------------------------------------------------------------------------ |
| Năm học và lớp   | `academic_years`                | Lưu năm học, ví dụ `2026-2027`, và trạng thái đang hoạt động.                                                |
| Năm học và lớp   | `classes`                       | Lưu lớp thuộc trường và năm học, ví dụ lớp `5A1`.                                                            |
| Học sinh         | `students`                      | Lưu mã học sinh, họ tên, ngày sinh, giới tính, lớp, trường và trạng thái hoạt động.                          |
| Học sinh         | `parent_students`               | Liên kết phụ huynh với học sinh; cho phép một phụ huynh có nhiều con và một học sinh có nhiều người giám hộ. |
| Xe đưa đón       | `vehicles`                      | Lưu xe thuộc trường, biển số, sức chứa, mã xe và trạng thái hoạt động.                                       |
| Xe đưa đón       | `drivers`                       | Lưu tài xế, số điện thoại, giấy phép và trạng thái làm việc.                                                 |
| Tuyến xe         | `routes`                        | Lưu tuyến xe thuộc trường.                                                                                   |
| Tuyến xe         | `route_stops`                   | Lưu các điểm đón/trả, thứ tự và thời gian dự kiến trên một tuyến.                                            |
| Tuyến xe         | `student_transport_assignments` | Gán học sinh vào tuyến và điểm đón cụ thể.                                                                   |
| Chuyến xe        | `trips`                         | Lưu chuyến xe theo ngày, tuyến, xe, loại chuyến sáng/chiều và trạng thái.                                    |
| Chuyến xe        | `trip_students`                 | Xác định học sinh thực tế thuộc từng chuyến xe trong ngày.                                                   |
| Điểm danh xe     | `vehicle_attendances`           | Lưu học sinh đã lên xe, xuống xe, vắng trên xe và thời điểm cập nhật.                                        |
| Điểm danh trường | `school_attendances`            | Lưu trạng thái có mặt, vắng, đi trễ hoặc có phép tại trường.                                                 |
| Lịch sử          | `audit_logs`                    | Lưu ai đã thay đổi dữ liệu nào, giá trị cũ, giá trị mới và thời điểm thay đổi.                               |
| Thông báo        | `notifications`                 | Lưu thông báo cho phụ huynh hoặc nhân viên khi xe trễ, học sinh vắng hoặc có sự kiện quan trọng.             |

Các bảng nghiệp vụ thuộc một trường phải có `school_id`, bao gồm `classes`, `students`, `vehicles`, `drivers`, `routes`, `trips`, `vehicle_attendances` và `school_attendances`.

#### 3.4. Quan hệ database chính

```text
administrative_regions
	└── provinces
				└── wards
							└── schools

schools
	├── user_schools ── users ── roles ── permissions
	├── academic_years ── classes ── students
	├── parent_profiles ── parent_students ── students
	├── vehicles ── trips ── vehicle_attendances
	├── routes ── route_stops ── student_transport_assignments
	├── trips ── trip_students ── students
	└── school_attendances ── students
```

#### 3.5. Thứ tự tạo bảng tiếp theo

Sau các bảng hiện tại, nên tạo theo thứ tự:

1. `users` hoặc ASP.NET Core Identity tables.
2. `user_profiles`.
3. `user_schools`.
4. `academic_years`.
5. `classes`.
6. `students`.
7. `parent_profiles` và `parent_students`.
8. `driver_profiles` và `vehicles`.
9. `routes` và `route_stops`.
10. `student_transport_assignments`.
11. `trips` và `trip_students`.
12. `vehicle_attendances`.
13. `school_attendances`.
14. `audit_logs` và `notifications`.

Tạo các entity tối thiểu:

- `User`, `ParentProfile`, `Student`, `ParentStudent`.
- `Vehicle`, `Route`, `RouteStop`, `StudentTransportAssignment`.
- `Trip`, `VehicleAttendance`, `SchoolAttendance`, `AuditLog`.

Chi tiết chính:

- `Student`: mã học sinh, họ tên, lớp và trạng thái hoạt động.
- `Vehicle`: biển số, tên xe, sức chứa và người phụ trách.
- `Trip`: ngày chạy, tuyến, xe, trạng thái và thời gian cập nhật.
- `VehicleAttendance`: học sinh lên/xuống xe, trạng thái và thời điểm.
- `SchoolAttendance`: trạng thái `Present`, `Absent`, `Late`, `Excused`, lý do và người xác nhận.
- `AuditLog`: lưu lịch sử thay đổi trạng thái quan trọng.

Quy tắc kỹ thuật:

- Dùng khóa chính `Guid` hoặc `int` nhất quán trong toàn hệ thống.
- Có `CreatedAt`, `UpdatedAt` và `IsActive` cho entity phù hợp.
- Dùng EF Core migration, không tạo bảng thủ công ngoài migration.
- Tạo index cho mã học sinh, ngày chuyến xe và các khóa tra cứu điểm danh.
- Không lưu mật khẩu dạng plain text.
- Seed development gồm tài khoản trường, tài khoản phụ huynh, học sinh, xe, tuyến và điểm danh mẫu.

**Đầu ra:** ERD, entity/configuration, migration đầu tiên, seed data và lệnh chạy database local.

### 4. Xây dựng xác thực và phân quyền Backend

- Tạo `POST /api/auth/login`, `GET /api/auth/me` và refresh token nếu cần.
- JWT chứa `sub`, `email`, `role` và user id.
- Bảo vệ endpoint bằng `[Authorize]` và policy theo vai trò.
- `SchoolStaff` được xem dữ liệu toàn trường và cập nhật dữ liệu quản lý.
- `Parent` chỉ được truy cập học sinh thuộc quan hệ `ParentStudent` của chính tài khoản đó.
- Backend phải kiểm tra quyền sở hữu, không tin `studentId` do phụ huynh gửi lên.
- Chuẩn hóa lỗi gồm mã lỗi, thông báo, validation errors và correlation id.

**Đầu ra:** login hoạt động trên Swagger, request thiếu token bị từ chối, phụ huynh không đọc được dữ liệu của học sinh khác.

### 5. Xây dựng API nghiệp vụ Backend

API nhà trường:

- `GET /api/school/dashboard?date=...`
- `GET /api/students`
- `GET /api/vehicles`
- `POST /api/trips`
- `PATCH /api/trips/{id}/status`
- `GET /api/attendance?date=...`
- `PUT /api/students/{studentId}/vehicle-attendance`
- `PUT /api/students/{studentId}/school-attendance`

API phụ huynh:

- `GET /api/parent/children`
- `GET /api/parent/children/{studentId}/today`
- `GET /api/parent/children/{studentId}/attendance-history`

Quy tắc API:

- Dùng DTO, không trả trực tiếp entity EF Core.
- Có phân trang cho danh sách học sinh và lịch sử.
- Validate ở Application layer bằng FluentValidation hoặc cơ chế tương đương.
- Tài liệu hóa request/response trong Swagger.
- Lưu UTC trong database và chuyển sang giờ địa phương ở Frontend.

**Đầu ra:** OpenAPI contract, controller/service/repository và test các trường hợp quyền, dữ liệu trùng và chuyển trạng thái không hợp lệ.

### 6. Chia cấu trúc Frontend ReactJS

Tổ chức lại `Frontend/src/` như sau:

```text
src/
	app/ App.jsx routes.jsx providers.jsx
	components/
		layout/ AppShell.jsx Sidebar.jsx Topbar.jsx MobileNav.jsx
		common/ Button.jsx Modal.jsx EmptyState.jsx LoadingState.jsx StatusBadge.jsx Toast.jsx
		dashboard/ StatCard.jsx VehicleOverview.jsx AlertList.jsx
		attendance/ AttendanceTable.jsx AttendanceFilters.jsx AttendanceActionMenu.jsx
		parent/ ChildSelector.jsx TripStatusCard.jsx JourneyTimeline.jsx AttendanceHistory.jsx
	features/
		auth/ authApi.js authStorage.js useAuth.js
		school/ schoolApi.js useSchoolDashboard.js
		parent/ parentApi.js useParentOverview.js
		attendance/ attendanceApi.js useAttendance.js
	services/ apiClient.js mockApi.js
	data/ mockData.js
	hooks/ useToast.js useDebounce.js
	utils/ formatDate.js statusLabels.js
	styles/ tokens.css
```

Nguyên tắc Frontend:

- `components` chỉ hiển thị và phát event; không gọi API trực tiếp.
- `features` chứa API và state theo nghiệp vụ.
- `services/apiClient.js` cấu hình base URL, token, timeout và lỗi chung.
- `mockApi.js` có cùng interface với API thật để chuyển môi trường mà không sửa component.
- Dùng React Router khi số màn hình tăng; chưa cần Redux nếu hooks/context đáp ứng MVP.
- Tạo `ProtectedRoute` và `RoleRoute` để kiểm tra đăng nhập, vai trò.
- Mọi màn hình phải có loading, error, empty và success state.
- Dùng `VITE_API_BASE_URL`, không hard-code URL Backend.

**Đầu ra:** app shell React, route theo vai trò, API client có mock fallback và không còn code template Vite.

### 7. Dựng dashboard nhà trường

- Header: ngày hiện tại, tài khoản và đăng xuất.
- KPI: tổng học sinh, đã đến trường, đang trên xe, vắng/chưa cập nhật, đi trễ.
- Khu vực xe: tuyến, tài xế, số học sinh, trạng thái và thời gian cập nhật.
- Cho phép cập nhật `OnRoute`, `Arrived`, `Delayed`, `Completed`.
- Bảng điểm danh gồm họ tên, lớp, tuyến, trạng thái trên xe, trạng thái tại trường, thời gian và người cập nhật.
- Filter theo lớp, tuyến, trạng thái; tìm kiếm theo tên hoặc mã học sinh.
- Cảnh báo riêng cho xe trễ, học sinh chưa lên xe và học sinh chưa có điểm danh.

**Đầu ra:** nhân viên nhìn thấy và cập nhật toàn bộ tình hình trong ngày từ dashboard.

### 8. Dựng cổng phụ huynh

- Chỉ gọi API `/api/parent/*`, không dùng API tổng hợp của nhà trường.
- Nếu có nhiều con, hiển thị bộ chọn học sinh.
- Thẻ trạng thái: chưa bắt đầu, đã lên xe, đang trên đường, đã đến trường, trễ hoặc chưa có dữ liệu.
- Timeline hiển thị giờ dự kiến và thực tế: rời điểm đón, lên xe, đến trường, điểm danh.
- Lịch sử chỉ hiển thị dữ liệu của học sinh đang chọn.
- Không hiển thị danh sách hoặc thông tin nhận diện của học sinh khác.

**Đầu ra:** phụ huynh biết con đã lên xe chưa, xe đã đến trường chưa và hôm nay con được điểm danh thế nào.

### 9. Kết nối Frontend với Backend

Thay mock API bằng API thật theo thứ tự:

1. Login và user profile.
2. Parent children và trạng thái hôm nay.
3. School dashboard.
4. Attendance update.
5. Vehicle/trip update.

Cấu hình CORS đúng origin. Khi token hết hạn, xóa session và chuyển về login. Hiển thị lỗi thân thiện cho timeout, `401`, `403`, `404` và `500`. Khi API loading, giữ kích thước layout ổn định.

**Đầu ra:** hai vai trò dùng dữ liệu từ SQL Server qua API, không còn phụ thuộc mock data trong môi trường tích hợp.

### 10. Kiểm thử, tài liệu và chạy local

Backend:

- `dotnet restore`, `dotnet build`, `dotnet ef database update`, `dotnet test`.
- Test quyền phụ huynh, chuyển trạng thái không hợp lệ, duplicate attendance và dữ liệu không tồn tại.
- Dùng Swagger để kiểm tra endpoint thủ công.

Frontend:

- `npm install`, `npm run lint`, `npm run build`, `npm run dev`.
- Kiểm tra login, đổi vai trò, filter, update attendance, update trip, logout và lỗi API trên desktop/mobile.

README phải có yêu cầu Node.js, .NET SDK, SQL Server, cách chạy Backend, migration, seed data, cách chạy Frontend, `VITE_API_BASE_URL`, tài khoản development và endpoint chính.

**Cấu trúc repository sau khi hoàn thiện**

```text
childcare-tracking-system/
	Backend/
		ChildcareTracking.sln
		src/
		tests/
	Frontend/
		src/
		public/
		package.json
	README.md
```

**Verification**

1. Backend build thành công bằng `dotnet build`.
2. Backend test thành công bằng `dotnet test`.
3. Database tạo được bằng EF Core migration và seed data xuất hiện đúng.
4. Frontend lint và build thành công bằng `npm run lint` và `npm run build`.
5. Đăng nhập đúng vai trò; request không có token bị chặn.
6. Phụ huynh không thể xem hoặc cập nhật học sinh ngoài quyền được liên kết.
7. Nhân viên trường xem được dashboard và cập nhật điểm danh/chuyến xe.
8. Kiểm tra loading, empty, error, delayed và dữ liệu chưa cập nhật.
9. Kiểm tra responsive, đặc biệt bảng điểm danh dài.
10. Kiểm tra timezone, audit log và lịch sử thay đổi điểm danh.

**Decisions**

- Frontend dùng ReactJS + Vite và JavaScript JSX ở giai đoạn đầu; có thể chuyển sang TypeScript sau khi API contract ổn định.
- Backend dùng C# + ASP.NET Core Web API, EF Core và SQL Server.
- API dùng REST, DTO và JWT Bearer.
- MVP có hai vai trò: nhà trường và phụ huynh.
- Điểm danh có hai lớp: lên/xuống xe và có mặt tại trường.
- Trạng thái xe được cập nhật thủ công trong MVP; chưa coi đây là GPS tracking thật.
- Không để Frontend truy cập database trực tiếp.
- Chưa làm thông báo push/SMS, thanh toán, báo cáo xuất file, quản lý lương tài xế hoặc GPS thật.

**Further Considerations**

1. Sau MVP, chốt nguồn GPS, tần suất cập nhật, bản đồ, quyền riêng tư và cơ chế lưu lịch sử vị trí.
2. Cân nhắc SignalR để đẩy trạng thái xe và điểm danh theo thời gian thực thay vì polling.
3. Trước production cần rate limit, secret management, HTTPS, backup database, audit policy và logging tập trung.
4. Khi nghiệp vụ ổn định, chuyển Frontend sang TypeScript để giảm lỗi giữa DTO Backend và API client.
