using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.FileOperations
{
    public class FileOperations
    {
        public static string Add(IFormFile imageFile)
        {



            try
            {
                string path = @"C:\Users\armağan\source\repos\RentACarProject\CarImages\";
                string guidImageName = GuidCreator.Create(imageFile.FileName);
                if (imageFile.Length > 0)
                {

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }


                    using (FileStream fileStream = System.IO.File.Create(path + guidImageName))
                    {
                        imageFile.CopyTo(fileStream);
                        fileStream.Flush();
                    }

                }
                return path + guidImageName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
                

            
        }

        public static void DeleteFile(string filePath)
        {
            if (File.Exists(@filePath))
            {
                File.Delete(@filePath);
            }
        }
    }
}
        
    

