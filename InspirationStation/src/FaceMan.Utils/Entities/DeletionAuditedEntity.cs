using System;
using FaceMan.Utils.Entities;

namespace FaceManUtils.Entities;

/// <summary>
/// 此类可用作需要存储删除信息的实体的基类。
/// </summary>
/// <typeparam name="TPrimaryKey"></typeparam>
public class DeletionAuditedEntity<TPrimaryKey> : IDeletionAudited
{
    public bool IsDeleted { get; set; }
    public string DeleterUserId { get; set; }
    public DateTime? DeletionTime { get; set; }
}