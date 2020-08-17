using System.IO;
using System.Reflection;

namespace Hymnal.Resources
{
    public interface IAssets
    {
        Stream GetResourceStream(Assembly assembly, string resourceFileName);
        string GetResourceString(Assembly assembly, string resourceFileName);
        string GetResourceString(string resourceFileName);
    }
}
