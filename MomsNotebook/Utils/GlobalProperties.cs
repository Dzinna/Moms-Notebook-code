using MomsNotebook.Services.Repositories;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace MomsNotebook.Utils
{
    public static class GlobalProperties
    {
        /// <summary>
        /// Database filename directory path.
        /// </summary>
        public static string DatabasePath { get; } = Path.Combine(Environment.
              GetFolderPath(Environment.SpecialFolder.Personal), SqlLiteDatabase.DatabaseName);

        /// <summary>
        /// Value for checking if database file exists in users device.
        /// </summary>
        public static bool DatabaseFileExists => File.Exists(DatabasePath);

        /// <summary>
        /// From MySQL database check if user is connected.
        /// </summary>
        public static bool UserConnected { get; set; } = false;

        /// <summary>
        /// From MySQL database logged in users email.
        /// </summary>
        public static string UserEmail { get; set; }

        /// <summary>
        /// From MySQL database logged in users UUID for synchronization file storage.
        /// </summary>
        public static string Uuid { get; set; }

        /// <summary>
        /// Memory parameter for if users lost his device and connecting with new one.
        /// </summary>
        public static string NewDeviceSync => "NewDeviceSync";

        /// <summary>
        /// Check validity of inserted email, regexp value taken from: http://www.regular-expressions.info/email.html
        /// </summary>
        private static string EmailRegex { get; } = @"\A[a-z0-9!#$%&'*+/=?^_‘{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_‘{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\z";

        public static bool IsEmailValid => CheckEmailWithRegex();

        private static bool CheckEmailWithRegex()
        {
            if (UserEmail != null)
            {
                var match = Regex.Match(UserEmail, EmailRegex, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
