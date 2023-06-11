using Letzte_Zeugen.Models;
using Microsoft.CodeAnalysis;
using System.IO;

namespace Letzte_Zeugen.Helpers
{
    /*
     * Simple helper class for storing images to the filesystem.
     */
    public class StoreHelper
    {
        /*
         * Default directory where images will be stored.
         */
        public static readonly string DEFAULTDIR = "./database/image-data";

        public static string GetProjectPath(string projectID)
        {
            return Path.Combine(DEFAULTDIR, projectID);
        }

        public static void SaveModellImage(List<IFormFile> imageData, long projectID, string subfolder)
        {

            if (imageData != null)
            {
                foreach (IFormFile file in imageData)
                {
					using (var ms = new MemoryStream())
					{
						file.CopyTo(ms);
						byte[] fileBytes = ms.ToArray();
						//saves image in Storage
						string path = Path.Combine(DEFAULTDIR, projectID.ToString(), subfolder, file.FileName);
						SaveImage(fileBytes, path);
					}
				}
            }

        }

        private static void SaveImage(byte[] imagedata, string path)
        {
            string absolutePath = Path.GetFullPath(path);

            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));
            }
            File.WriteAllBytes(absolutePath, imagedata);
        }
    }
}
