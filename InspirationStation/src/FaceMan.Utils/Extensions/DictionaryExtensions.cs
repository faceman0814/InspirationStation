namespace FaceManUtils.Extensions;

public static class DictionaryExtensions
  {
    /// <summary>
    /// 从字典中尝试获取指定键的值，并将其转换为指定类型 T。
    /// </summary>
    /// <typeparam name="T">值的类型</typeparam>
    /// <param name="dictionary">集合对象</param>
    /// <param name="key">Key</param>
    /// <param name="value">键的值（如果键不存在，则为默认值）</param>
    /// <returns>如果字典中确实存在键，则为 True</returns>
    public static bool TryGetValue<T>(
      this IDictionary<string, object> dictionary,
      string key,
      out T value)
    {
      object obj1;
      if (dictionary.TryGetValue(key, out obj1) && obj1 is T obj2)
      {
        value = obj2;
        return true;
      }
      value = default (T);
      return false;
    }

    /// <summary>
    /// 从字典中获取指定键的值，如果找不到，则返回默认值。
    /// </summary>
    /// <param name="dictionary">要检查和获取的字典</param>
    /// <param name="key">查找值的键</param>
    /// <typeparam name="TKey">密钥的类型</typeparam>
    /// <typeparam name="TValue">值的类型</typeparam>
    /// <returns>如果找到，则为值，如果找不到，则为默认值。</returns>
    public static TValue GetOrDefault<TKey, TValue>(
      this IDictionary<TKey, TValue> dictionary,
      TKey key)
    {
      TValue obj;
      return !dictionary.TryGetValue(key, out obj) ? default (TValue) : obj;
    }

    /// <summary>
    /// 此方法用于尝试获取字典中的值 （如果该值确实存在），如果找不到则使用指定的工厂方法创建值并添加到字典中它并返回。
    /// </summary>
    /// <param name="dictionary">要检查和获取的字典</param>
    /// <param name="key">查找值的键</param>
    /// <param name="factory">用于创建值（如果在字典中找不到）的工厂方法</param>
    /// <typeparam name="TKey">密钥的类型</typeparam>
    /// <typeparam name="TValue">值的类型</typeparam>
    /// <returns>如果找到，则为值，如果找不到，则为默认值。</returns>
    public static TValue GetOrAdd<TKey, TValue>(
      this IDictionary<TKey, TValue> dictionary,
      TKey key,
      Func<TKey, TValue> factory)
    {
      TValue obj;
      return dictionary.TryGetValue(key, out obj) ? obj : (dictionary[key] = factory(key));
    }

    /// <summary>
    /// 此方法用于尝试获取字典中的值 （如果该值确实存在），如果找不到则使用指定的工厂方法创建值并添加到字典中它并返回。
    /// </summary>
    /// <param name="dictionary">要检查和获取的字典</param>
    /// <param name="key">查找值的键</param>
    /// <param name="factory">用于创建值（如果在字典中找不到）的工厂方法</param>
    /// <typeparam name="TKey">密钥的类型</typeparam>
    /// <typeparam name="TValue">值的类型</typeparam>
    /// <returns>如果找到值，则为值，如果找不到，则为默认值.</returns>
    public static TValue GetOrAdd<TKey, TValue>(
      this IDictionary<TKey, TValue> dictionary,
      TKey key,
      Func<TValue> factory)
    {
      return dictionary.GetOrAdd<TKey, TValue>(key, (Func<TKey, TValue>) (k => factory()));
    }
  }