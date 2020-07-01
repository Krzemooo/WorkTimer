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

        public static List<TimeCheckpointModel> GetDayWorkTimes(DateTime date, UserModel userModel)
        {
            try
            {
                DataCore dataCore = new DataCore();
                string[] WorkTimes = Directory.GetFiles(ReturnFileFolder());
                if (WorkTimes.Contains(ReturnFilePath(date, userModel)))
                {
                    List<TimeCheckpointModel> timeCheckPoints = new List<TimeCheckpointModel>();
                    string[] textLine = File.ReadAllLines(ReturnFilePath(date, userModel));
                    foreach (string line in textLine)
                    {
                        string[] lineElemtns = line.Remove(line.Length - 1).Split(',');
                        timeCheckPoints.Add(new TimeCheckpointModel()
                        {
                            date = DateTime.Parse(lineElemtns[0]),
                            status = (TimeCheckpoinStatus)int.Parse(lineElemtns[1])
                        });
                    }
                    return timeCheckPoints;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.ToString());
                return new List<TimeCheckpointModel>();
            }
        }

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
