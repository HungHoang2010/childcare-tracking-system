using System;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure.Persistence;

public partial class ChildcateDbContext : DbContext
{
    public ChildcateDbContext()
    {
    }

    public ChildcateDbContext(DbContextOptions<ChildcateDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcademicYear> AcademicYears { get; set; }

    public virtual DbSet<AdministrativeRegion> AdministrativeRegions { get; set; }

    public virtual DbSet<AdministrativeUnit> AdministrativeUnits { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<DriverDocument> DriverDocuments { get; set; }

    public virtual DbSet<DriverProfile> DriverProfiles { get; set; }

    public virtual DbSet<Logactivity> Logactivities { get; set; }

    public virtual DbSet<Logerror> Logerrors { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<ParentProfile> ParentProfiles { get; set; }

    public virtual DbSet<ParentStudent> ParentStudents { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Rolespermission> Rolespermissions { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<RouteStop> RouteStops { get; set; }

    public virtual DbSet<School> Schools { get; set; }

    public virtual DbSet<SchoolAttendance> SchoolAttendances { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentTransportAssignment> StudentTransportAssignments { get; set; }

    public virtual DbSet<Systemkey> Systemkeys { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<TripStudent> TripStudents { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserOtp> UserOtps { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<UserSchool> UserSchools { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleAttendance> VehicleAttendances { get; set; }

    public virtual DbSet<VehicleDriver> VehicleDrivers { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=childcate_db;user=root;password=Database@12345", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.46-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AcademicYear>(entity =>
        {
            entity.HasKey(e => e.AcademicYearId).HasName("PRIMARY");

            entity.ToTable("academic_years");

            entity.HasIndex(e => e.SchoolId, "IX_AcademicYears_SchoolID");

            entity.HasIndex(e => new { e.SchoolId, e.YearCode }, "UX_AcademicYears_School_YearCode").IsUnique();

            entity.Property(e => e.AcademicYearId).HasColumnName("AcademicYearID");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsCurrent)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);
            entity.Property(e => e.YearCode).HasMaxLength(20);
            entity.Property(e => e.YearName).HasMaxLength(50);

            entity.HasOne(d => d.School).WithMany(p => p.AcademicYears)
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AcademicYears_School");
        });

        modelBuilder.Entity<AdministrativeRegion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("administrative_regions");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CodeName)
                .HasMaxLength(255)
                .HasColumnName("code_name");
            entity.Property(e => e.CodeNameEn)
                .HasMaxLength(255)
                .HasColumnName("code_name_en");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NameEn)
                .HasMaxLength(255)
                .HasColumnName("name_en");
        });

        modelBuilder.Entity<AdministrativeUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("administrative_units");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CodeName)
                .HasMaxLength(255)
                .HasColumnName("code_name");
            entity.Property(e => e.CodeNameEn)
                .HasMaxLength(255)
                .HasColumnName("code_name_en");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.FullNameEn)
                .HasMaxLength(255)
                .HasColumnName("full_name_en");
            entity.Property(e => e.ShortName)
                .HasMaxLength(255)
                .HasColumnName("short_name");
            entity.Property(e => e.ShortNameEn)
                .HasMaxLength(255)
                .HasColumnName("short_name_en");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PRIMARY");

            entity.ToTable("classes");

            entity.HasIndex(e => e.AcademicYearId, "IX_Classes_AcademicYearID");

            entity.HasIndex(e => new { e.AcademicYearId, e.ClassName }, "UX_Classes_AcademicYear_ClassName").IsUnique();

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.AcademicYearId).HasColumnName("AcademicYearID");
            entity.Property(e => e.ClassName)
                .HasMaxLength(12)
                .IsFixedLength();
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);

            entity.HasOne(d => d.AcademicYear).WithMany(p => p.Classes)
                .HasForeignKey(d => d.AcademicYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AcademicYearID_Classes");
        });

        modelBuilder.Entity<DriverDocument>(entity =>
        {
            entity.HasKey(e => e.DriverDocumentId).HasName("PRIMARY");

            entity.ToTable("driver_documents");

            entity.HasIndex(e => e.DocumentType, "IX_DriverDocuments_DocumentType");

            entity.HasIndex(e => e.DriverUserId, "IX_DriverDocuments_DriverUserID");

            entity.HasIndex(e => e.VerifiedBy, "IX_DriverDocuments_VerifiedBy");

            entity.Property(e => e.DriverDocumentId).HasColumnName("DriverDocumentID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.DocumentType).HasMaxLength(50);
            entity.Property(e => e.DocumentVersion).HasDefaultValueSql("'1'");
            entity.Property(e => e.DriverUserId).HasColumnName("DriverUserID");
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsVerified)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.MimeType).HasMaxLength(100);
            entity.Property(e => e.VerificationNote).HasMaxLength(500);
            entity.Property(e => e.VerifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.DriverUser).WithMany(p => p.DriverDocuments)
                .HasForeignKey(d => d.DriverUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DriverDocuments_DriverProfile");

            entity.HasOne(d => d.VerifiedByNavigation).WithMany(p => p.DriverDocuments)
                .HasForeignKey(d => d.VerifiedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_DriverDocuments_VerifiedBy");
        });

        modelBuilder.Entity<DriverProfile>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("driver_profiles");

            entity.HasIndex(e => e.VerifiedBy, "IX_DriverProfiles_VerifiedBy");

            entity.HasIndex(e => e.LicenseNumber, "UX_DriverProfiles_LicenseNumber").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("UserID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsVerified)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.LicenseNumber).HasMaxLength(50);
            entity.Property(e => e.LicenseType).HasMaxLength(50);
            entity.Property(e => e.VerificationNote).HasMaxLength(500);
            entity.Property(e => e.VerifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithOne(p => p.DriverProfileUser)
                .HasForeignKey<DriverProfile>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DriverProfiles_User");

            entity.HasOne(d => d.VerifiedByNavigation).WithMany(p => p.DriverProfileVerifiedByNavigations)
                .HasForeignKey(d => d.VerifiedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_DriverProfiles_VerifiedBy");
        });

        modelBuilder.Entity<Logactivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("logactivity");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Ip)
                .HasMaxLength(40)
                .HasColumnName("IP");
            entity.Property(e => e.LogContent).HasMaxLength(10000);
            entity.Property(e => e.ObjectGuid)
                .HasMaxLength(38)
                .IsFixedLength()
                .HasColumnName("ObjectGUID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<Logerror>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("logerror");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy).HasMaxLength(36);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.LogContent).HasMaxLength(10000);
            entity.Property(e => e.ProcessContent).HasMaxLength(4000);
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PRIMARY");

            entity.ToTable("notifications");

            entity.HasIndex(e => e.CreatedDate, "IX_Notifications_CreatedDate");

            entity.HasIndex(e => new { e.ReferenceId, e.ReferenceType }, "IX_Notifications_Reference");

            entity.HasIndex(e => e.SchoolId, "IX_Notifications_SchoolID");

            entity.HasIndex(e => new { e.UserId, e.IsRead }, "IX_Notifications_UserID_IsRead");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpiresAt).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsRead)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Message).HasColumnType("text");
            entity.Property(e => e.NotificationType).HasMaxLength(50);
            entity.Property(e => e.Priority)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Normal'");
            entity.Property(e => e.ReadAt).HasColumnType("datetime");
            entity.Property(e => e.ReferenceId).HasColumnName("ReferenceID");
            entity.Property(e => e.ReferenceType).HasMaxLength(100);
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.School).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_School");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_User");
        });

        modelBuilder.Entity<ParentProfile>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("parent_profiles");

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("UserID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.EmergencyContactName).HasMaxLength(255);
            entity.Property(e => e.EmergencyContactPhone).HasMaxLength(20);
            entity.Property(e => e.EmergencyContactRelationship).HasMaxLength(50);
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Occupation).HasMaxLength(150);

            entity.HasOne(d => d.User).WithOne(p => p.ParentProfile)
                .HasForeignKey<ParentProfile>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParentProfiles_User");
        });

        modelBuilder.Entity<ParentStudent>(entity =>
        {
            entity.HasKey(e => e.ParentStudentId).HasName("PRIMARY");

            entity.ToTable("parent_students");

            entity.HasIndex(e => e.StudentId, "IX_ParentStudents_StudentID");

            entity.HasIndex(e => new { e.ParentUserId, e.StudentId }, "UX_ParentStudents_Parent_Student").IsUnique();

            entity.Property(e => e.ParentStudentId).HasColumnName("ParentStudentID");
            entity.Property(e => e.CanReceiveNotification)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsPrimaryContact)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.ParentUserId).HasColumnName("ParentUserID");
            entity.Property(e => e.Relationship).HasMaxLength(50);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.ParentUser).WithMany(p => p.ParentStudents)
                .HasForeignKey(d => d.ParentUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParentStudents_Parent");

            entity.HasOne(d => d.Student).WithMany(p => p.ParentStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParentStudents_Student");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PRIMARY");

            entity.ToTable("permissions");

            entity.Property(e => e.PermissionId).HasColumnName("PermissionID");
            entity.Property(e => e.CreateBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IsAccepted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.PermissionDescription).HasMaxLength(255);
            entity.Property(e => e.PermissionName).HasMaxLength(255);
            entity.Property(e => e.UpdateBy).HasMaxLength(36);
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PRIMARY");

            entity.ToTable("provinces");

            entity.HasIndex(e => e.AdministrativeUnitId, "idx_provinces_unit");

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.AdministrativeUnitId).HasColumnName("administrative_unit_id");
            entity.Property(e => e.CodeName)
                .HasMaxLength(255)
                .HasColumnName("code_name");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.FullNameEn)
                .HasMaxLength(255)
                .HasColumnName("full_name_en");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NameEn)
                .HasMaxLength(255)
                .HasColumnName("name_en");
            entity.Property(e => e.PostalCodePrefix)
                .HasMaxLength(255)
                .HasColumnName("postal_code_prefix");

            entity.HasOne(d => d.AdministrativeUnit).WithMany(p => p.Provinces)
                .HasForeignKey(d => d.AdministrativeUnitId)
                .HasConstraintName("provinces_administrative_unit_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.CreateBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IsAccepted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.RoleDescription).HasMaxLength(255);
            entity.Property(e => e.RoleName).HasMaxLength(255);
            entity.Property(e => e.UpdateBy).HasMaxLength(36);
        });

        modelBuilder.Entity<Rolespermission>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.PermissionId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("rolespermission");

            entity.HasIndex(e => e.PermissionId, "idx_permission_id");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.PermissionId).HasColumnName("PermissionID");
            entity.Property(e => e.CreateBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(36);

            entity.HasOne(d => d.Permission).WithMany(p => p.Rolespermissions)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("fk_rp_permission");

            entity.HasOne(d => d.Role).WithMany(p => p.Rolespermissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("fk_rp_role");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.RouteId).HasName("PRIMARY");

            entity.ToTable("routes");

            entity.HasIndex(e => e.SchoolId, "IX_Routes_SchoolID");

            entity.HasIndex(e => new { e.SchoolId, e.RouteCode }, "UX_Routes_School_Code").IsUnique();

            entity.Property(e => e.RouteId).HasColumnName("RouteID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.RouteCode).HasMaxLength(50);
            entity.Property(e => e.RouteName).HasMaxLength(150);
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

            entity.HasOne(d => d.School).WithMany(p => p.Routes)
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Routes_School");
        });

        modelBuilder.Entity<RouteStop>(entity =>
        {
            entity.HasKey(e => e.RouteStopId).HasName("PRIMARY");

            entity.ToTable("route_stops");

            entity.HasIndex(e => e.RouteId, "IX_RouteStops_RouteID");

            entity.HasIndex(e => e.WardCode, "IX_RouteStops_WardID");

            entity.HasIndex(e => new { e.RouteId, e.StopCode }, "UX_RouteStops_Route_Code").IsUnique();

            entity.HasIndex(e => new { e.RouteId, e.StopOrder }, "UX_RouteStops_Route_Order").IsUnique();

            entity.Property(e => e.RouteStopId).HasColumnName("RouteStopID");
            entity.Property(e => e.AddressDetail).HasMaxLength(500);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpectedArrivalTime).HasColumnType("time");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Latitude).HasPrecision(10, 7);
            entity.Property(e => e.Longitude).HasPrecision(10, 7);
            entity.Property(e => e.RouteId).HasColumnName("RouteID");
            entity.Property(e => e.StopCode).HasMaxLength(50);
            entity.Property(e => e.StopName).HasMaxLength(150);
            entity.Property(e => e.StopType).HasMaxLength(20);
            entity.Property(e => e.WardCode).HasMaxLength(20);

            entity.HasOne(d => d.Route).WithMany(p => p.RouteStops)
                .HasForeignKey(d => d.RouteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RouteStops_Route");

            entity.HasOne(d => d.WardCodeNavigation).WithMany(p => p.RouteStops)
                .HasForeignKey(d => d.WardCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RouteStops_Ward");
        });

        modelBuilder.Entity<School>(entity =>
        {
            entity.HasKey(e => e.SchoolId).HasName("PRIMARY");

            entity.ToTable("schools");

            entity.HasIndex(e => e.WardId, "fk_schools_wards");

            entity.HasIndex(e => new { e.ProvinceId, e.WardId }, "idx_province_ward");

            entity.HasIndex(e => e.SchoolCode, "idx_school_code").IsUnique();

            entity.HasIndex(e => e.Slug, "idx_school_slug").IsUnique();

            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.AddressDetail).HasMaxLength(500);
            entity.Property(e => e.CreateBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Latitude).HasPrecision(10, 7);
            entity.Property(e => e.LogoUrl).HasMaxLength(500);
            entity.Property(e => e.Longitude).HasPrecision(10, 7);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.PrimaryColor).HasMaxLength(20);
            entity.Property(e => e.ProvinceId).HasMaxLength(20);
            entity.Property(e => e.SchoolCode).HasMaxLength(50);
            entity.Property(e => e.SchoolType).HasMaxLength(50);
            entity.Property(e => e.SecondaryColor).HasMaxLength(20);
            entity.Property(e => e.ShortName).HasMaxLength(100);
            entity.Property(e => e.Slug).HasMaxLength(150);
            entity.Property(e => e.TimeZone).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(36);
            entity.Property(e => e.WardId).HasMaxLength(20);
            entity.Property(e => e.Website).HasMaxLength(255);

            entity.HasOne(d => d.Province).WithMany(p => p.Schools)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_schools_provinces");

            entity.HasOne(d => d.Ward).WithMany(p => p.Schools)
                .HasForeignKey(d => d.WardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_schools_wards");
        });

        modelBuilder.Entity<SchoolAttendance>(entity =>
        {
            entity.HasKey(e => e.SchoolAttendanceId).HasName("PRIMARY");

            entity.ToTable("school_attendances");

            entity.HasIndex(e => e.AcademicYearId, "IX_SchoolAttendances_AcademicYearID");

            entity.HasIndex(e => e.CheckedBy, "IX_SchoolAttendances_CheckedBy");

            entity.HasIndex(e => e.AttendanceDate, "IX_SchoolAttendances_Date");

            entity.HasIndex(e => e.StudentId, "IX_SchoolAttendances_StudentID");

            entity.HasIndex(e => new { e.SchoolId, e.StudentId, e.AttendanceDate }, "UX_SchoolAttendances_Student_Date").IsUnique();

            entity.Property(e => e.SchoolAttendanceId).HasColumnName("SchoolAttendanceID");
            entity.Property(e => e.AcademicYearId).HasColumnName("AcademicYearID");
            entity.Property(e => e.AttendanceStatus).HasMaxLength(30);
            entity.Property(e => e.CheckedInAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.AcademicYear).WithMany(p => p.SchoolAttendances)
                .HasForeignKey(d => d.AcademicYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SchoolAttendances_AcademicYear");

            entity.HasOne(d => d.CheckedByNavigation).WithMany(p => p.SchoolAttendances)
                .HasForeignKey(d => d.CheckedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_SchoolAttendances_CheckedBy");

            entity.HasOne(d => d.School).WithMany(p => p.SchoolAttendances)
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SchoolAttendances_School");

            entity.HasOne(d => d.Student).WithMany(p => p.SchoolAttendances)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SchoolAttendances_Student");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PRIMARY");

            entity.ToTable("students");

            entity.HasIndex(e => e.ClassId, "IX_Students_ClassID");

            entity.HasIndex(e => new { e.ClassId, e.StudentCode }, "UX_Students_StudentCode").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.AvatarUrl).HasMaxLength(500);
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.StudentCode).HasMaxLength(50);
            entity.Property(e => e.StudentName).HasMaxLength(255);
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassID_students");
        });

        modelBuilder.Entity<StudentTransportAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PRIMARY");

            entity.ToTable("student_transport_assignments");

            entity.HasIndex(e => e.RouteStopId, "IX_TransportAssignments_RouteStopID");

            entity.HasIndex(e => e.StudentId, "IX_TransportAssignments_StudentID");

            entity.HasIndex(e => new { e.StudentId, e.RouteStopId, e.StartDate }, "UX_StudentTransportAssignments_Active").IsUnique();

            entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.RouteStopId).HasColumnName("RouteStopID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.TransportType).HasMaxLength(20);

            entity.HasOne(d => d.RouteStop).WithMany(p => p.StudentTransportAssignments)
                .HasForeignKey(d => d.RouteStopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TransportAssignments_RouteStop");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentTransportAssignments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TransportAssignments_Student");
        });

        modelBuilder.Entity<Systemkey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("systemkey");

            entity.HasIndex(e => new { e.ParentId, e.CodeValue }, "Index_CodeValue");

            entity.HasIndex(e => e.Id, "Index_Id");

            entity.HasIndex(e => e.CodeKey, "idx_codekey").IsUnique();

            entity.Property(e => e.Id)
                .HasComment("ID")
                .HasColumnName("ID");
            entity.Property(e => e.CodeKey).HasMaxLength(128);
            entity.Property(e => e.CodeValue)
                .HasDefaultValueSql("'0'")
                .HasComment("Giá trị");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Thời gian tạo")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsDelete)
                .HasDefaultValueSql("b'0'")
                .HasComment("Trạng thái : 0.Chưa xóa 1.Đã xóa")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasComment("Lần cuối cập nhật")
                .HasColumnType("datetime");
            entity.Property(e => e.ParentId)
                .HasComment("ID Cha")
                .HasColumnName("ParentID");
            entity.Property(e => e.SortOrder).HasComment("Sắp xếp theo");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.TripId).HasName("PRIMARY");

            entity.ToTable("trips");

            entity.HasIndex(e => e.RouteId, "FK_Trips_Route");

            entity.HasIndex(e => e.DriverUserId, "IX_Trips_DriverUserID");

            entity.HasIndex(e => e.TripDate, "IX_Trips_TripDate");

            entity.HasIndex(e => e.VehicleId, "IX_Trips_VehicleID");

            entity.HasIndex(e => new { e.SchoolId, e.RouteId, e.TripDate, e.TripType }, "UX_Trips_School_Route_Date_Type").IsUnique();

            entity.Property(e => e.TripId).HasColumnName("TripID");
            entity.Property(e => e.ActualArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.ActualStartTime).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.DriverUserId).HasColumnName("DriverUserID");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.RouteId).HasColumnName("RouteID");
            entity.Property(e => e.ScheduledArrivalTime).HasColumnType("time");
            entity.Property(e => e.ScheduledStartTime).HasColumnType("time");
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.TripType).HasMaxLength(20);
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.DriverUser).WithMany(p => p.Trips)
                .HasForeignKey(d => d.DriverUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Trips_Driver");

            entity.HasOne(d => d.Route).WithMany(p => p.Trips)
                .HasForeignKey(d => d.RouteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trips_Route");

            entity.HasOne(d => d.School).WithMany(p => p.Trips)
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trips_School");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Trips)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trips_Vehicle");
        });

        modelBuilder.Entity<TripStudent>(entity =>
        {
            entity.HasKey(e => e.TripStudentId).HasName("PRIMARY");

            entity.ToTable("trip_students");

            entity.HasIndex(e => e.RouteStopId, "IX_TripStudents_RouteStopID");

            entity.HasIndex(e => e.StudentId, "IX_TripStudents_StudentID");

            entity.HasIndex(e => new { e.TripId, e.StudentId }, "UX_TripStudents_Trip_Student").IsUnique();

            entity.Property(e => e.TripStudentId).HasColumnName("TripStudentID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsExpected)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.RouteStopId).HasColumnName("RouteStopID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.TripId).HasColumnName("TripID");

            entity.HasOne(d => d.RouteStop).WithMany(p => p.TripStudents)
                .HasForeignKey(d => d.RouteStopId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_TripStudents_RouteStop");

            entity.HasOne(d => d.Student).WithMany(p => p.TripStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TripStudents_Student");

            entity.HasOne(d => d.Trip).WithMany(p => p.TripStudents)
                .HasForeignKey(d => d.TripId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TripStudents_Trip");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UX_Users_Email").IsUnique();

            entity.HasIndex(e => e.UserName, "UX_Users_UserName").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreateBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsVerified)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.LockoutEndDate).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(512);
            entity.Property(e => e.UpdateBy).HasMaxLength(36);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        modelBuilder.Entity<UserOtp>(entity =>
        {
            entity.HasKey(e => e.Otpid).HasName("PRIMARY");

            entity.ToTable("user_otps");

            entity.HasIndex(e => new { e.Email, e.OtpType }, "IX_UserOTPs_Email_Type");

            entity.HasIndex(e => e.ExpiredDate, "IX_UserOTPs_ExpiredDate");

            entity.HasIndex(e => e.IsUsed, "IX_UserOTPs_IsUsed");

            entity.Property(e => e.Otpid).HasColumnName("OTPID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsUsed)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.OtpHash)
                .HasMaxLength(64)
                .IsFixedLength();
            entity.Property(e => e.OtpType).HasMaxLength(50);
            entity.Property(e => e.UsedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user_profiles");

            entity.HasIndex(e => e.Phone, "IX_UserProfiles_Phone");

            entity.HasIndex(e => e.ResidentNumber, "UX_UserProfiles_ResidentNumber").IsUnique();

            entity.HasIndex(e => e.WardId, "fk_user_profile_ward");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.AddressDetail).HasMaxLength(500);
            entity.Property(e => e.CreateBy).HasMaxLength(36);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Gender).HasColumnType("bit(1)");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .HasColumnName("ImageURL");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.ResidentNumber)
                .HasMaxLength(12)
                .IsFixedLength();
            entity.Property(e => e.UpdateBy).HasMaxLength(36);
            entity.Property(e => e.WardId)
                .HasMaxLength(20)
                .HasColumnName("WardID");

            entity.HasOne(d => d.Ward).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.WardId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_user_profile_ward");
        });

        modelBuilder.Entity<UserSchool>(entity =>
        {
            entity.HasKey(e => e.UserSchoolId).HasName("PRIMARY");

            entity.ToTable("user_schools");

            entity.HasIndex(e => e.RoleId, "IX_UserSchools_RoleID");

            entity.HasIndex(e => e.SchoolId, "IX_UserSchools_SchoolID");

            entity.HasIndex(e => e.UserId, "IX_UserSchools_UserID");

            entity.HasIndex(e => new { e.UserId, e.SchoolId, e.RoleId }, "UX_UserSchools_User_School_Role").IsUnique();

            entity.Property(e => e.UserSchoolId).HasColumnName("UserSchoolID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Role).WithMany(p => p.UserSchools)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserSchools_Role");

            entity.HasOne(d => d.School).WithMany(p => p.UserSchools)
                .HasForeignKey(d => d.SchoolId)
                .HasConstraintName("FK_UserSchools_School");

            entity.HasOne(d => d.User).WithMany(p => p.UserSchools)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserSchools_User");
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PRIMARY");

            entity.ToTable("user_tokens");

            entity.HasIndex(e => e.ExpiredDate, "IX_UserTokens_ExpiredDate");

            entity.HasIndex(e => e.UserId, "IX_UserTokens_UserID");

            entity.HasIndex(e => new { e.UserId, e.IsRevoked }, "IX_UserTokens_User_Revoked");

            entity.HasIndex(e => e.TokenHash, "UX_UserTokens_TokenHash").IsUnique();

            entity.Property(e => e.TokenId).HasColumnName("TokenID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.DeviceName).HasMaxLength(255);
            entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(45)
                .HasColumnName("IPAddress");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsRememberMe)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsRevoked)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.RevokedDate).HasColumnType("datetime");
            entity.Property(e => e.TokenHash)
                .HasMaxLength(64)
                .IsFixedLength();
            entity.Property(e => e.TokenType).HasMaxLength(30);
            entity.Property(e => e.UserAgent).HasMaxLength(500);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UserTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserTokens_User");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PRIMARY");

            entity.ToTable("vehicles");

            entity.HasIndex(e => e.SchoolId, "IX_Vehicles_SchoolID");

            entity.HasIndex(e => new { e.SchoolId, e.VehicleCode }, "UX_Vehicles_School_Code").IsUnique();

            entity.HasIndex(e => new { e.SchoolId, e.PlateNumber }, "UX_Vehicles_School_Plate").IsUnique();

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.ManufactureYear).HasColumnType("year");
            entity.Property(e => e.PlateNumber).HasMaxLength(20);
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.VehicleCode).HasMaxLength(50);
            entity.Property(e => e.VehicleName).HasMaxLength(100);
            entity.Property(e => e.VehicleType).HasMaxLength(50);

            entity.HasOne(d => d.School).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehicles_School");
        });

        modelBuilder.Entity<VehicleAttendance>(entity =>
        {
            entity.HasKey(e => e.VehicleAttendanceId).HasName("PRIMARY");

            entity.ToTable("vehicle_attendances");

            entity.HasIndex(e => e.UpdatedBy, "IX_VehicleAttendances_UpdatedBy");

            entity.HasIndex(e => e.TripStudentId, "UX_VehicleAttendances_TripStudent").IsUnique();

            entity.Property(e => e.VehicleAttendanceId).HasColumnName("VehicleAttendanceID");
            entity.Property(e => e.BoardedAt).HasColumnType("datetime");
            entity.Property(e => e.BoardingStatus).HasMaxLength(30);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.DroppedOffAt).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.TripStudentId).HasColumnName("TripStudentID");

            entity.HasOne(d => d.TripStudent).WithOne(p => p.VehicleAttendance)
                .HasForeignKey<VehicleAttendance>(d => d.TripStudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VehicleAttendances_TripStudent");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.VehicleAttendances)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_VehicleAttendances_UpdatedBy");
        });

        modelBuilder.Entity<VehicleDriver>(entity =>
        {
            entity.HasKey(e => e.VehicleDriverId).HasName("PRIMARY");

            entity.ToTable("vehicle_drivers");

            entity.HasIndex(e => e.DriverUserId, "IX_VehicleDrivers_DriverUserID");

            entity.HasIndex(e => e.VehicleId, "IX_VehicleDrivers_VehicleID");

            entity.Property(e => e.VehicleDriverId).HasColumnName("VehicleDriverID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.DriverUserId).HasColumnName("DriverUserID");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsPrimary)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.DriverUser).WithMany(p => p.VehicleDrivers)
                .HasForeignKey(d => d.DriverUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VehicleDrivers_Driver");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleDrivers)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VehicleDrivers_Vehicle");
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PRIMARY");

            entity.ToTable("wards");

            entity.HasIndex(e => e.ProvinceCode, "idx_wards_province");

            entity.HasIndex(e => e.AdministrativeUnitId, "idx_wards_unit");

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.AdministrativeUnitId).HasColumnName("administrative_unit_id");
            entity.Property(e => e.CodeName)
                .HasMaxLength(255)
                .HasColumnName("code_name");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.FullNameEn)
                .HasMaxLength(255)
                .HasColumnName("full_name_en");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NameEn)
                .HasMaxLength(255)
                .HasColumnName("name_en");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .HasColumnName("postal_code");
            entity.Property(e => e.ProvinceCode)
                .HasMaxLength(20)
                .HasColumnName("province_code");

            entity.HasOne(d => d.AdministrativeUnit).WithMany(p => p.Wards)
                .HasForeignKey(d => d.AdministrativeUnitId)
                .HasConstraintName("wards_administrative_unit_id_fkey");

            entity.HasOne(d => d.ProvinceCodeNavigation).WithMany(p => p.Wards)
                .HasForeignKey(d => d.ProvinceCode)
                .HasConstraintName("wards_province_code_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
