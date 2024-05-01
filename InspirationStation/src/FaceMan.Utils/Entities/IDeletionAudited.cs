namespace FaceMan.Utils.Entities;

/// <summary>
/// 删除审计接口
/// </summary>
public interface IDeletionAudited
{
    /// <summary>
    /// 是否删除
    /// </summary>
    bool IsDeleted { get; set; }
    /// <summary>
    /// 删除者Id
    /// </summary>
    string? DeleterUserId { get; set; }
    /// <summary>
    /// 删除时间
    /// </summary>
    DateTime? DeletionTime { get; set; }
}