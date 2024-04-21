using System.Reflection;

namespace FaceMan.Utils.Extensions;

public static class TypeExtensions
{
    public static Assembly GetAssembly(this Type type) => type.GetTypeInfo().Assembly;

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