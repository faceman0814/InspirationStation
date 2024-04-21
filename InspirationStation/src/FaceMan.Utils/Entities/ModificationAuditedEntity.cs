using System;

namespace FaceManUtils.Entities;

public class ModificationAuditedEntity<TPrimaryKey> 
    : CreationAuditedEntity<TPrimaryKey>, IModificationAudited
{
    public string LastModifierUserId { get; set; }
    public DateTime? LastModificationTime { get; set; }
}