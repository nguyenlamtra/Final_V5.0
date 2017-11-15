using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStoreApi.Helper
{
    public class FileHelper
    {
        public static void DeleteFile(string filePath, string fileName)
        {
            if (System.IO.File.Exists(Path.Combine(filePath, fileName)))
            {
                try
                {
                    System.IO.File.Delete(Path.Combine(filePath, fileName));
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static async Task AddFileAsync(string filePath, string base64String, string fileName)
        {
            try
            {
                byte[] image = Convert.FromBase64String(base64String);
                using (var stream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                {
                    await stream.WriteAsync(image, 0, image.Length);
                    stream.Flush();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }
    }
}
