namespace FaceManUtils.Entities;
using System;
[Serializable]
public abstract class FullAuditedEntity<TPrimaryKey> 
    : CreationAuditedEntity<TPrimaryKey>,
        IDeletionAudited,
        IModificationAudited
{
    public bool IsDeleted { get; set; }
    public string DeleterUserId { get; set; }
    public DateTime? DeletionTime { get; set; }
    public string LastModifierUserId { get; set; }
    public DateTime? LastModificationTime { get; set; }
}