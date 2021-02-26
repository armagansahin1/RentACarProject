using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities
{
    public class GuidCreator
    {
        public static string Create(string fileName)
        {
            int startValue = fileName.LastIndexOf(".");
            string fileType = fileName.Substring(startValue);
            string GuidKey = Guid.NewGuid().ToString();
            return GuidKey+fileType;
        }
    }
}
