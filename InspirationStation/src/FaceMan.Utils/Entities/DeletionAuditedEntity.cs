using System;

namespace FaceManUtils.Entities;

public class DeletionAuditedEntity<TPrimaryKey> : IDeletionAudited
{
    public bool IsDeleted { get; set; }
    public string DeleterUserId { get; set; }
    public DateTime? DeletionTime { get; set; }
}