using FaceMan.Utils.Extensions;
using FaceManUtils.Extensions;

namespace Core.Helpers;

public static class WebContentDirectoryFinder
{
    public static string CalculateContentRootFolder()
    {
        var coreAssemblyDirectoryPath = Path.GetDirectoryName(typeof(CoreModule).GetAssembly().Location);
        if (coreAssemblyDirectoryPath == null)
        {
            throw new Exception("找不到程序集Core !");
        }

        var directoryInfo = new DirectoryInfo(coreAssemblyDirectoryPath);
        while (!DirectoryContains(directoryInfo.FullName, "InspirationStation.sln"))
        {
            if (directoryInfo.Parent == null)
            {
                throw new Exception("找不到项目根目录!");
            }

            directoryInfo = directoryInfo.Parent;
        }

        var webHostFolder = Path.Combine(directoryInfo.FullName, "src", "Host");
        if (Directory.Exists(webHostFolder))
        {
            Console.WriteLine("当前使用Host 项目启动");
            return webHostFolder;
        }
        throw new Exception("无法找到web项目的根文件夹!");
    }

    private static bool DirectoryContains(string directory, string fileName)
    {
        return Directory.GetFiles(directory).Any(filePath => string.Equals(Path.GetFileName(filePath), fileName));
    }
}