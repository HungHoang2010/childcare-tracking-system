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
