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
        /// <summary>
        /// Metoda tworząca nowy plik w folderze, oraz dodaje pierwszy event startowy
        /// </summary>
        /// <param name="date">Data eventu</param>
        /// <param name="userModel">Dane użytkownika</param>
        /// <returns>True/False w zależności od powodzenia metody</returns>
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
        /// <summary>
        /// Dodaje nową linie do istniejącego już pliku.
        /// </summary>
        /// <param name="date">Data eventu</param>
        /// <param name="statusId">Status eventu</param>
        /// <param name="userModel">Dane użytkownika</param>
        /// <returns>True/False w zależności od powodzenia metody</returns>
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
                    File.WriteAllText(FileFolderCore.ReturnFilePath(date, userModel), Cryptography.Encrypt(formatedText));
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
        /// <summary>
        /// Zwraca listę wszystkich dostępnych akcji czasów użytkownika.
        /// </summary>
        /// <param name="userModel">Dane uzytkownika (imię, nazwisko)</param>
        /// <returns>Listę akcji.</returns>
        public List<Model.UserWorkTime> GetUserWorkTime(UserModel userModel)
        {
            List<UserWorkTime> userWorkTimes = new List<UserWorkTime>();
            var userTimeEvents = FileFolderCore.GetDayWorkTimes(userModel).OrderBy(s => s.date).ToArray();
            for (int i = 0; i < userTimeEvents.Count()-1; i++)
            {
                TimeCheckpointModel currItem = userTimeEvents[i];
                TimeCheckpointModel nextItem = userTimeEvents[i + 1];
                if (currItem.status == TimeCheckpoinStatus.Start && nextItem.status == TimeCheckpoinStatus.Break)
                {
                    var singleDate = nextItem.date - currItem.date;
                    if (userWorkTimes.Any(s => s.DayStamp == currItem.date.Date))
                    {
                        userWorkTimes.FirstOrDefault(s => s.DayStamp == currItem.date.Date).DataWork += singleDate;

                    }
                    else
                    {
                        userWorkTimes.Add(new UserWorkTime()
                        {
                            DataWork = singleDate,
                            DayStamp = currItem.date.Date
                        });
                    }

                }
            }
            return userWorkTimes;
        }
    }
}
