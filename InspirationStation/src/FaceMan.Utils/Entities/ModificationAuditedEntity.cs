using FaceManUtils.Entities;

namespace FaceMan.Utils.Entities;

/// <summary>
/// 此类用于存储实体的修改审核信息。
/// </summary>
/// <typeparam name="TPrimaryKey"></typeparam>
public class ModificationAuditedEntity<TPrimaryKey>
    : CreationAuditedEntity<TPrimaryKey>, IModificationAudited
{
    public string LastModifierUserId { get; set; }
    public DateTime? LastModificationTime { get; set; }
}