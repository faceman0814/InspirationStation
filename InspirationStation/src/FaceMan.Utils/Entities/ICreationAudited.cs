namespace FaceMan.Utils.Entities;

/// <summary>
/// 创建审计接口
/// </summary>
public interface ICreationAudited
{
    /// <summary>
    /// 创建者ID
    /// </summary>
    string CreatorUserId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    DateTime CreationTime { get; set; }
}