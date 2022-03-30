using System.IO;
using System.Threading.Tasks;

namespace CraftsApi.Helpers
{
    public class FileHelper
    {
        public static async Task Write(string fileName, string content)
        {
            await File.WriteAllTextAsync(fileName, content);
        }
    }
}
