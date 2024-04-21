using System.Reflection;

namespace FaceMan.Utils.Extensions;

/// <summary>
/// 类型扩展方法。
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    ///  获取给定类型的程序集。
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static Assembly GetAssembly(this Type type) => type.GetTypeInfo().Assembly;

    /// <summary>
    /// 获取给定类型中具有指定名称的方法。
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="pParametersCount"></param>
    /// <param name="pGenericArgumentsCount"></param>
    /// <returns></returns>
    public static MethodInfo GetMethod(
        this Type type,
        string methodName,
        int pParametersCount = 0,
        int pGenericArgumentsCount = 0)
    {
        return ((IEnumerable<MethodInfo>) type.GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == methodName)).ToList<MethodInfo>().Select(m => new
        {
            Method = m,
            Params = m.GetParameters(),
            Args = m.GetGenericArguments()
        }).Where(x => x.Params.Length == pParametersCount && x.Args.Length == pGenericArgumentsCount).Select(x => x.Method).First<MethodInfo>();
    }
}