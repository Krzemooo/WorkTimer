using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTimer.Model;

namespace WorkTimer.Core
{
    public static class FileFolderCore
    {
        /// <summary>
        /// Metoda zwracająca ścieżkę do pliku, z uzględnieniem jego nazwy
        /// </summary>
        /// <param name="date">Data akcji</param>
        /// <param name="userModel">Dane użytkownika</param>
        /// <returns></returns>
        public static string ReturnFilePath(DateTime date, UserModel userModel)
        {
            return Path.Combine(FileFolderCore.ReturnFileFolder(), date.ToShortDateString().Replace('.', '_') + '_' + userModel.Name + '_' + userModel.Surname);
        }
        private static string ReturnFileFolder()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string specificFolder = Path.Combine(folder, "WorkTimer");

            if (!Directory.Exists(specificFolder))
                Directory.CreateDirectory(specificFolder);

            return specificFolder;
        }
        /// <summary>
        /// Metoda zwracająca listę wszstkich eventów, jakie miały miejsce w historii, dla danego użytkownika.
        /// </summary>
        /// <param name="userModel">Dane użytkownika</param>
        /// <returns></returns>
        public static List<TimeCheckpointModel> GetDayWorkTimes(UserModel userModel)
        {
            try
            {
                DataCore dataCore = new DataCore();
                List<TimeCheckpointModel> timeCheckPoints = new List<TimeCheckpointModel>();
                string[] workTimesFiles = Directory.GetFiles(ReturnFileFolder());
                foreach (string file in workTimesFiles)
                {
                    if (file.EndsWith($"{userModel.Name}_{userModel.Surname}"))
                    {
                        string textHashed = File.ReadAllText(file);
                        string decryptedText = Cryptography.Decrypt(textHashed);
                        string[] textLine = decryptedText.Remove(decryptedText.Length - 1).Split(';');
                        foreach (string line in textLine)
                        {
                            string[] timeElements = line.Split(',');
                            timeCheckPoints.Add(new TimeCheckpointModel()
                            {
                                date = DateTime.Parse(timeElements[0]),
                                status = (TimeCheckpoinStatus)int.Parse(timeElements[1])
                            });

                        }
                    }
                }
                return timeCheckPoints;
            }
            catch (Exception er)
            {
                Console.WriteLine(er.ToString());
                return null;
            }
        }
        /// <summary>
        /// Metoda sprawdzająca istnienie pliku, wg danych.
        /// </summary>
        /// <param name="date">Data akcji</param>
        /// <param name="userModel">Dane użytkownika</param>
        /// <returns></returns>
        public static bool FileExist(DateTime date, UserModel userModel)
        {
            string[] WorkTimes = Directory.GetFiles(ReturnFileFolder());
            if (WorkTimes.Contains(ReturnFilePath(date, userModel)))
                return true;
            else
                return false;
        }
    }
}
