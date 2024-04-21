using System;
using FaceManUtils.Timing;

namespace FaceManUtils.Entities;

public abstract class CreationAuditedEntity<TPrimaryKey> :Entity<TPrimaryKey>, ICreationAudited
{
    public virtual DateTime CreationTime { get; set; }

    public virtual required string CreatorUserId { get; set; }

    protected CreationAuditedEntity() => this.CreationTime = Clock.Now;
}