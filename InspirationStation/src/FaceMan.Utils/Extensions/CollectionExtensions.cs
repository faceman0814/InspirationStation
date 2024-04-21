namespace FaceManUtils.Extensions;

public static class CollectionExtensions
{
    /// <summary>
    /// 检查任何给定的集合对象是否为 null 或没有项。
    /// </summary>
    public static bool IsNullOrEmpty<T>(this ICollection<T> source)
    {
        return source == null || source.Count <= 0;
    }

    /// <summary>
    /// 如果集合中尚不存在项，则将项添加到集合中。
    /// </summary>
    /// <param name="source">来源</param>
    /// <param name="item">要检查和添加的项目</param>
    /// <typeparam name="T">集合中项目的类型</typeparam>
    /// <returns>如果添加，则返回 True，如果未添加，则返回 False。</returns>
    public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
    {
        if (source == null)
            throw new ArgumentNullException(nameof (source));
        if (source.Contains(item))
            return false;
        source.Add(item);
        return true;
    }
}