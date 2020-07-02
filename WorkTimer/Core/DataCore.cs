using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTimer.Model;

namespace WorkTimer.Core
{
    public class DataCore
    {
        public bool GenerateNewFile(DateTime date, UserModel userModel)
        {
            string fileContent = $"{date.ToString()},1;";
            string filePath = FileFolderCore.ReturnFilePath(date, userModel);


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
                        byte[] title = new UTF8Encoding(true).GetBytes(Cryptography.Encrypt(fileContent));
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
        public bool AppendNewLine(DateTime date, int statusId, UserModel userModel)
        {
            try
            {
                if (FileFolderCore.FileExist(date, userModel))
                {
                    string newElement = $"{date},{statusId};";
                    string allText = File.ReadAllText(FileFolderCore.ReturnFilePath(date, userModel));
                    string formatedText = Cryptography.Decrypt(allText);
                    formatedText += newElement;
                    File.WriteAllText(FileFolderCore.ReturnFilePath(date, userModel), formatedText);
                    return true;
                }
                else
                {
                    if (statusId == (int)TimeCheckpoinStatus.Start)
                    {
                        if (GenerateNewFile(date, userModel))
                            return true;
                        else
                            return false;
                    }
                }
                return false;
            }
            catch (Exception er)
            {
                Console.WriteLine(er.ToString());
                return false;
            }
        }

        public List<Model.UserWorkTime> GetUserWorkTime(UserModel userModel)
        {
            var temp = FileFolderCore.GetDayWorkTimes(userModel);
            return new List<UserWorkTime>();
        }
    }
}
