using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace FaceManUtils.Extensions;

 public static class StringExtensions
  {
    /// <summary>
    /// 如果给定字符串的末尾不以 char 结尾，则将 char 添加到该字符串的末尾。
    /// </summary>
    public static string EnsureEndsWith(this string str, char c)
    {
      return str.EnsureEndsWith(c, StringComparison.Ordinal);
    }

    /// <summary>
    /// 如果给定字符串的末尾不以 char 结尾，则将 char 添加到该字符串的末尾。
    /// </summary>
    public static string EnsureEndsWith(this string str, char c, StringComparison comparisonType)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      return str.EndsWith(c.ToString(), comparisonType) ? str : str + c.ToString();
    }

    /// <summary>
    /// 如果给定字符串的末尾不以 char 结尾，则将 char 添加到该字符串的末尾。
    /// </summary>
    public static string EnsureEndsWith(
      this string str,
      char c,
      bool ignoreCase,
      CultureInfo culture)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      return str.EndsWith(c.ToString((IFormatProvider) culture), ignoreCase, culture) ? str : str + c.ToString();
    }

    /// <summary>
    /// 如果给定字符串的开头不以 char 开头，则将 char 添加到给定字符串的开头。
    /// </summary>
    public static string EnsureStartsWith(this string str, char c)
    {
      return str.EnsureStartsWith(c, StringComparison.Ordinal);
    }

    /// <summary>
    /// 如果给定字符串的开头不以 char 开头，则将 char 添加到给定字符串的开头。
    /// </summary>
    public static string EnsureStartsWith(this string str, char c, StringComparison comparisonType)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      return str.StartsWith(c.ToString(), comparisonType) ? str : c.ToString() + str;
    }

    /// <summary>
    /// 如果给定字符串的开头不以 char 开头，则将 char 添加到给定字符串的开头。
    /// </summary>
    public static string EnsureStartsWith(
      this string str,
      char c,
      bool ignoreCase,
      CultureInfo culture)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      return str.StartsWith(c.ToString((IFormatProvider) culture), ignoreCase, culture) ? str : c.ToString() + str;
    }

    /// <summary>
    /// 指示此字符串是 null 还是 System.String.Empty 字符串。
    /// </summary>
    public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

    /// <summary>
    /// 指示此字符串是空、空还是仅由空格字符组成。
    /// </summary>
    public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);

    /// <summary>
    /// 从字符串的开头获取字符串的子字符串。
    /// </summary>
    /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="str" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">Thrown if <paramref name="len" /> is bigger that string's length</exception>
    public static string Left(this string str, int len)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      if (str.Length < len)
        throw new ArgumentException("len argument can not be bigger than given string's length!");
      return str.Substring(0, len);
    }

    /// <summary>
    /// 将字符串中的行尾转换为 <see cref="P:System.Environment.NewLine" />.
    /// </summary>
    public static string NormalizeLineEndings(this string str)
    {
      return str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
    }

    /// <summary>获取字符串中 char 出现的第 n 次的索引.</summary>
    /// <param name="str">要搜索的源字符串</param>
    /// <param name="c">要搜索的字符 <paramref name="str" /></param>
    /// <param name="n">发生次数计数</param>
    public static int NthIndexOf(this string str, char c, int n)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      int num = 0;
      for (int index = 0; index < str.Length; ++index)
      {
        if ((int) str[index] == (int) c && ++num == n)
          return index;
      }
      return -1;
    }

    /// <summary>
    /// 从给定字符串的末尾删除给定后缀的第一次出现。排序很重要。如果其中一个postFixes匹配，则不会测试其他postFixes。
    /// </summary>
    /// <param name="str">字符串。</param>
    /// <param name="postFixes">一个或多个后缀</param>
    /// <returns>修改后的字符串或相同的字符串（如果它没有任何给定的后缀）</returns>
    public static string RemovePostFix(this string str, params string[] postFixes)
    {
      if (str == null)
        return (string) null;
      if (string.IsNullOrEmpty(str))
        return string.Empty;
      if (((ICollection<string>) postFixes).IsNullOrEmpty<string>())
        return str;
      foreach (string postFix in postFixes)
      {
        if (str.EndsWith(postFix))
          return str.Left(str.Length - postFix.Length);
      }
      return str;
    }

    /// <summary>
    /// 从给定字符串的开头删除给定前缀的第一次出现。
    /// 排序很重要。如果其中一个 preFix 匹配，则不会测试其他 preFix。
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="preFixes">一个或多个前缀.</param>
    /// <returns>修改后的字符串或相同的字符串（如果它没有任何给定的前缀）</returns>
    public static string RemovePreFix(this string str, params string[] preFixes)
    {
      if (str == null)
        return (string) null;
      if (string.IsNullOrEmpty(str))
        return string.Empty;
      if (((ICollection<string>) preFixes).IsNullOrEmpty<string>())
        return str;
      foreach (string preFix in preFixes)
      {
        if (str.StartsWith(preFix))
          return str.Right(str.Length - preFix.Length);
      }
      return str;
    }

    /// <summary>从字符串末尾获取字符串的子字符串。</summary>
    /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="str" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">Thrown if <paramref name="len" /> is bigger that string's length</exception>
    public static string Right(this string str, int len)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      if (str.Length < len)
        throw new ArgumentException("len argument can not be bigger than given string's length!");
      return str.Substring(str.Length - len, len);
    }

    /// <summary>
    /// 使用字符串。通过给定分隔符拆分给定字符串的 Split 方法。
    /// </summary>
    public static string[] Split(this string str, string separator)
    {
      return str.Split(new string[1]{ separator }, StringSplitOptions.None);
    }

    /// <summary>
    /// 使用字符串。通过给定分隔符拆分给定字符串的 Split 方法。
    /// </summary>
    public static string[] Split(this string str, string separator, StringSplitOptions options)
    {
      return str.Split(new string[1]{ separator }, options);
    }

    /// <summary>
    /// 使用字符串。拆分给定字符串的拆分方法 <see cref="P:System.Environment.NewLine" />.
    /// </summary>
    public static string[] SplitToLines(this string str) => str.Split(Environment.NewLine);

    /// <summary>
    /// 使用字符串。拆分给定字符串的拆分方法 <see cref="P:System.Environment.NewLine" />.
    /// </summary>
    public static string[] SplitToLines(this string str, StringSplitOptions options)
    {
      return str.Split(Environment.NewLine, options);
    }

    /// <summary>将 PascalCase 字符串转换为 camelCase 字符串。</summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="invariantCulture">固定文化</param>
    /// <returns>骆驼字符串的大小写</returns>
    public static string ToCamelCase(this string str, bool invariantCulture = true)
    {
      if (string.IsNullOrWhiteSpace(str))
        return str;
      if (str.Length != 1)
        return (invariantCulture ? char.ToLowerInvariant(str[0]) : char.ToLower(str[0])).ToString() + str.Substring(1);
      return !invariantCulture ? str.ToLower() : str.ToLowerInvariant();
    }

    /// <summary>
    /// 在指定的区域性中将 PascalCase 字符串转换为 camelCase 字符串。
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="culture">提供特定于区域性的大小写规则的对象</param>
    /// <returns>骆驼字符串的大小写</returns>
    public static string ToCamelCase(this string str, CultureInfo culture)
    {
      if (string.IsNullOrWhiteSpace(str))
        return str;
      return str.Length == 1 ? str.ToLower(culture) : char.ToLower(str[0], culture).ToString() + str.Substring(1);
    }

    /// <summary>
    /// 将给定的 PascalCase/camelCase 字符串转换为句子（通过按空格拆分单词）。
    /// 示例：“ThisIsSampleSentence”转换为“This is a sample sentence”。
    /// </summary>
    /// <param name="str">要转换的字符串.</param>
    /// <param name="invariantCulture">固定文化</param>
    public static string ToSentenceCase(this string str, bool invariantCulture = false)
    {
      return string.IsNullOrWhiteSpace(str) ? str : Regex.Replace(str, "[a-z][A-Z]", (MatchEvaluator) (m =>
      {
        char ch = m.Value[0];
        string str1 = ch.ToString();
        ch = invariantCulture ? char.ToLowerInvariant(m.Value[1]) : char.ToLower(m.Value[1]);
        string str2 = ch.ToString();
        return str1 + " " + str2;
      }));
    }

    /// <summary>
    /// 将给定的 PascalCase/camelCase 字符串转换为句子（通过按空格拆分单词）。
    /// 示例：“ThisIsSampleSentence”转换为“This is a sample sentence”。
    /// </summary>
    /// <param name="str">要转换的字符串.</param>
    /// <param name="culture">提供特定于区域性的大小写规则的对象。</param>
    public static string ToSentenceCase(this string str, CultureInfo culture)
    {
      return string.IsNullOrWhiteSpace(str) ? str : Regex.Replace(str, "[a-z][A-Z]", (MatchEvaluator) (m =>
      {
        char lower = m.Value[0];
        string str1 = lower.ToString();
        lower = char.ToLower(m.Value[1], culture);
        string str2 = lower.ToString();
        return str1 + " " + str2;
      }));
    }

    /// <summary>将字符串转换为枚举值。</summary>
    /// <typeparam name="T">枚举的类型</typeparam>
    /// <param name="value">要转换的字符串值</param>
    /// <returns>返回枚举对象</returns>
    public static T ToEnum<T>(this string value) where T : struct
    {
      return value != null ? (T) Enum.Parse(typeof (T), value) : throw new ArgumentNullException(nameof (value));
    }

    /// <summary>将字符串转换为枚举值。</summary>
    /// <typeparam name="T">枚举的类型</typeparam>
    /// <param name="value">要转换的字符串值</param>
    /// <param name="ignoreCase">忽略大小写</param>
    /// <returns>返回枚举对象</returns>
    public static T ToEnum<T>(this string value, bool ignoreCase) where T : struct
    {
      return value != null ? (T) Enum.Parse(typeof (T), value, ignoreCase) : throw new ArgumentNullException(nameof (value));
    }

    /// <summary>
    /// 将字符串转换为 MD5 哈希值。
    /// </summary>
    /// <param name="str"> 要进行哈希处理的字符串。 </param>
    /// <returns></returns>
    public static string ToMd5(this string str)
    {
      using (MD5 md5 = MD5.Create())
      {
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        byte[] hash = md5.ComputeHash(bytes);
        StringBuilder stringBuilder = new StringBuilder();
        foreach (byte num in hash)
          stringBuilder.Append(num.ToString("X2"));
        return stringBuilder.ToString();
      }
    }

    /// <summary>将 camelCase 字符串转换为 PascalCase 字符串.</summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="invariantCulture">固定文化</param>
    /// <returns>字符串的 PascalCase</returns>
    public static string ToPascalCase(this string str, bool invariantCulture = true)
    {
      if (string.IsNullOrWhiteSpace(str))
        return str;
      if (str.Length != 1)
        return (invariantCulture ? char.ToUpperInvariant(str[0]) : char.ToUpper(str[0])).ToString() + str.Substring(1);
      return !invariantCulture ? str.ToUpper() : str.ToUpperInvariant();
    }

    /// <summary>
    /// 将指定区域性中的 camelCase 字符串转换为 PascalCase 字符串。
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="culture">提供特定于区域性的大小写规则的对象</param>
    /// <returns>字符串的 PascalCase</returns>
    public static string ToPascalCase(this string str, CultureInfo culture)
    {
      if (string.IsNullOrWhiteSpace(str))
        return str;
      return str.Length == 1 ? str.ToUpper(culture) : char.ToUpper(str[0], culture).ToString() + str.Substring(1);
    }

    /// <summary>
    /// 如果字符串超过最大长度，则从字符串的开头获取字符串的子字符串。
    /// </summary>
    /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="str" /> is null</exception>
    public static string Truncate(this string str, int maxLength)
    {
      if (str == null)
        return (string) null;
      return str.Length <= maxLength ? str : str.Left(maxLength);
    }

    /// <summary>
    /// 如果字符串超过最大长度，则从字符串的开头获取字符串的子字符串。
    /// 它添加了一个“...”postfix 添加到字符串的末尾（如果字符串被截断）。
    /// 返回的字符串不能长于 maxLength。
    /// </summary>
    /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="str" /> is null</exception>
    public static string TruncateWithPostfix(this string str, int maxLength)
    {
      return str.TruncateWithPostfix(maxLength, "...");
    }

    /// <summary>
    /// 如果字符串超过最大长度，则从字符串的开头获取字符串的子字符串。
    /// 如果字符串被截断，它会将给定的 <paramref name=“postfix” /> 添加到字符串的末尾。
    /// 返回的字符串不能长于 maxLength。
    /// </summary>
    /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="str" /> is null</exception>
    public static string TruncateWithPostfix(this string str, int maxLength, string postfix)
    {
      if (str == null)
        return (string) null;
      if (string.IsNullOrEmpty(str) || maxLength == 0)
        return string.Empty;
      if (str.Length <= maxLength)
        return str;
      return maxLength <= postfix.Length ? postfix.Left(maxLength) : str.Left(maxLength - postfix.Length) + postfix;
    }
  }