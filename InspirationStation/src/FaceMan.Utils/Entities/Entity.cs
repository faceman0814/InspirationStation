using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FaceManUtils.Entities;

public class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
{
    [MaxLength(32)]
    [Key]
    public virtual required TPrimaryKey Id { get; set; }

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
        return (type1.GetTypeInfo().IsAssignableFrom(type2) || type2.GetTypeInfo().IsAssignableFrom(type1)) &&
               this.Id!.Equals((object)entity.Id!);

    }

    public override string ToString()
    {
        // 返回当前对象的类型名称和标识符的字符串表示形式
        return $"[{(object)this.GetType().Name} {(object)Id!}]";
    }
}