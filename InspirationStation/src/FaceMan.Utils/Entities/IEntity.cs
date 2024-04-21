namespace FaceMan.Utils.Entities;

/// <summary>
/// 实体接口
/// </summary>
/// <typeparam name="TPrimaryKey"> 主键类型 </typeparam>
public interface IEntity<TPrimaryKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    TPrimaryKey Id { get; set; }
}