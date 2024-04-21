using System;
using FaceMan.Utils.Entities;
using FaceMan.Utils.Timing;

namespace FaceManUtils.Entities;

/// <summary>
/// 此类用于向实体添加创建审核属性。
/// </summary>
/// <typeparam name="TPrimaryKey"></typeparam>
public abstract class CreationAuditedEntity<TPrimaryKey> :Entity<TPrimaryKey>, ICreationAudited
{
    public virtual DateTime CreationTime { get; set; }

    public virtual required string CreatorUserId { get; set; }

    protected CreationAuditedEntity() => this.CreationTime = Clock.Now;
}