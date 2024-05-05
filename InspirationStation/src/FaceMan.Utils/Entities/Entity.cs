using System.ComponentModel.DataAnnotations;
using System.Reflection;
using FaceManUtils.Entities;

namespace FaceMan.Utils.Entities;

/// <summary>
/// 表示具有唯一标识符的实体。
/// </summary>
/// <typeparam name="TPrimaryKey"> 实体的主键类型。</typeparam>
public class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
{
    [MaxLength(32)]
    [Key]
    public virtual required TPrimaryKey Id { get; set; }

    public virtual bool IsTransient()
    {
        if (EqualityComparer<TPrimaryKey>.Default.Equals(this.Id, default (TPrimaryKey)))
            return true;
        if (typeof (TPrimaryKey) == typeof (int))
            return Convert.ToInt32((object) this.Id) <= 0;
        return typeof (TPrimaryKey) == typeof (long) && Convert.ToInt64((object) this.Id) <= 0L;
    }
    /// <summary>
    /// 重写Equals方法，比较两个实体是否相等。
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public virtual bool EntityEquals(object? obj)
    {
        // 如果对象为空或不是Entity<TPrimaryKey>类型，则返回false。
        if (obj == null || !(obj is Entity<TPrimaryKey>))
            return false;
        // 如果当前实例等于输入对象，则返回true。
        if (this == obj)
            return true;
        // 尝试将输入对象转换为Entity<TPrimaryKey>类型，并比较两个实例的类型和Id属性是否相等，最终返回比较结果。
        var entity = (Entity<TPrimaryKey>)obj;
        var type1 = this.GetType();
        var type2 = entity.GetType();
        // 类型不相等，则返回false。
        return (type1.GetTypeInfo().IsAssignableFrom(type2) ||
                type2.GetTypeInfo().IsAssignableFrom(type1)) &&
               this.Id!.Equals((object)entity.Id!);
    }
    
    /// <summary>
    /// 重写GetHashCode方法，返回实体的哈希值。（哈希码是一个整数值，它代表了对象在内存中的唯一标识。）
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return this.Id!.GetHashCode();
    }
}