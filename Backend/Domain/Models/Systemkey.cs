using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Systemkey
{
    /// <summary>
    /// ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID Cha
    /// </summary>
    public uint? ParentId { get; set; }

    public string? CodeKey { get; set; }

    /// <summary>
    /// Giá trị
    /// </summary>
    public int? CodeValue { get; set; }

    public string? Description { get; set; }

    /// <summary>
    /// Sắp xếp theo
    /// </summary>
    public uint? SortOrder { get; set; }

    /// <summary>
    /// Trạng thái : 0.Chưa xóa 1.Đã xóa
    /// </summary>
    public ulong? IsDelete { get; set; }

    public Guid? CreateBy { get; set; }

    public Guid? UpdateBy { get; set; }

    /// <summary>
    /// Thời gian tạo
    /// </summary>
    public DateTime? CreateAt { get; set; }

    /// <summary>
    /// Lần cuối cập nhật
    /// </summary>
    public DateTime? LastUpdate { get; set; }
}
