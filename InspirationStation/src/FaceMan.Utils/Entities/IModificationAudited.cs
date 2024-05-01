namespace FaceMan.Utils.Entities;

/// <summary>
/// 修改审计接口
/// </summary>
public interface IModificationAudited
{
    /// <summary>
    /// 修改者ID
    /// </summary>
    string? LastModifierUserId { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    DateTime? LastModificationTime { get; set; }
}