
using Letzte_Zeugen.Models;
using Microsoft.CodeAnalysis;
using System.IO;

namespace backend.Helpers
{
    /*
     * Simple helper class for storing images to the filesystem.
     */
    public class StoreHelper
    {
        /*
         * Default directory where images will be stored.
         */
        private static readonly string DEFAULTDIR = "...\\database\\image-data\\";

        public static string GetProjectPath(string projectID)
        {
            return Path.Combine(DEFAULTDIR, projectID);
        }

        public static void SaveModellImage(IFormFile imageData, long projectID)
        {

            if (imageData != null)
            {
                
                using (var ms = new MemoryStream())
                {
                    imageData.CopyTo(ms);
                    byte[] fileBytes = ms.ToArray();
                    //saves image in Storage
                    string path = Path.Combine(DEFAULTDIR, projectID.ToString(),"Bilder", imageData.Name);
                    SaveImage(fileBytes, path);
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
