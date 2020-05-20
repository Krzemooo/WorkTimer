using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimer.Core
{
    public class DataCore
    {
        public bool GenerateNewFile(DateTime date)
        {
            string fileContent = $"{date.ToString()},1;{Environment.NewLine}";
            string filePath = FileFolderCore.ReturnFilePath(date);


            try
            {
                if (File.Exists(filePath))
                {
                    return false;
                }
                else
                {
                    using (FileStream fs = File.Create(filePath))
                    {
                        byte[] title = new UTF8Encoding(true).GetBytes(fileContent);
                        fs.Write(title, 0, title.Length);
                    }
                    return true;
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                return false;
            }
        }
        public bool AppendNewLine(DateTime date, int statusId)
        {
            try
            {
                File.AppendAllText(FileFolderCore.ReturnFilePath(date), $"{date},{statusId};{Environment.NewLine}");
                return true;

            }
            catch (Exception er)
            {
                Console.WriteLine(er.ToString());
                return false;
            }
        }
    }
}
