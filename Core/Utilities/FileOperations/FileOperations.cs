using Core.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.FileOperations
{
    public class FileOperations
    {

        public static string Add(IFormFile imageFile,string defaultPath,string defaultFile)
        {



                
                if (imageFile !=null && imageFile.Length > 0)
                {
                string path = @"wwwroot/"+defaultPath;
                string guidImageName = GuidCreator.Create(imageFile.FileName);
                if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }


                    using (FileStream fileStream = System.IO.File.Create(path + guidImageName))
                    {
                        imageFile.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                return defaultPath + guidImageName;
            }
            else
            {
                return defaultPath+defaultFile;
            }
            



        }

        public static void DeleteFile(string filePath,IResult defaulFileExist)
        {
            if (!defaulFileExist.Success)
            {
                if (File.Exists(@"wwwroot/" + filePath))
                {
                    File.Delete(@"wwwroot/" + filePath);
                }
            }
            
        }
    }
}



