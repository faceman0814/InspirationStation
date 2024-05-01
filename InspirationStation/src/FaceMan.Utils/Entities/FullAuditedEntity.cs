using FaceMan.Utils.Entities;

namespace FaceManUtils.Entities;
using System;

/// <summary>
///  此类用于定义完全审核的实体。
/// </summary>
/// <typeparam name="TPrimaryKey"></typeparam>
[Serializable]
public abstract class FullAuditedEntity<TPrimaryKey> 
    : CreationAuditedEntity<TPrimaryKey>,
        IDeletionAudited,
        IModificationAudited
{
    public bool IsDeleted { get; set; }=false;
    public string? DeleterUserId { get; set; }
    public DateTime? DeletionTime { get; set; }
    public string? LastModifierUserId { get; set; }
    public DateTime? LastModificationTime { get; set; }
}