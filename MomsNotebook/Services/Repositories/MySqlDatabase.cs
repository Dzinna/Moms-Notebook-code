using MomsNotebook.Models.Database;
using MomsNotebook.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MomsNotebook.Services.Repositories
{
    public class MySqlDatabase : IMySqlDatabase, IDisposable
    {
        public bool Active { get; set; }

        private MySqlConnection Connection { get; set; }
        private bool Disposed { get; set; }

        public void GetMySqlConnection()
        {
            if (Connection == null)
            {
                Connection = new MySqlConnection("Server=localhost;User ID=root;Password=test;Database=momsdatabase");
                Connection.Open();
                Active = true;
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
            {
                return;
            }

            if (disposing)
            {
                Connection.Close();
                Connection.Dispose();
            }

            Disposed = true;
        }

        public void CreateLogin(string email, string password)
        {
            var salt = GetSalt();
            var hashedPassword = GetSha256Hash(password);
            var hashedPasswordWithSalt = GetSha256Hash(hashedPassword + salt);

            var query = $@"INSERT INTO users
                                      (uuid,
                                       email, 
                                       password, 
                                       salt, 
                                       registeredon) 
                           VALUES ('{Guid.NewGuid()}',
                                   '{email}',
                                   '{hashedPasswordWithSalt}',
                                   '{salt}',
                                   '{DateTime.UtcNow.FormatMySqlDateTime()}');";

            using (var command = new MySqlCommand(query, Connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public bool CheckEmailExistsInDatabase(string email)
        {
            var emailExists = false;

            using (var reader = CheckEmailExists(email))
            {
                if (reader.Read() && reader.GetString("email").Trim() == email.Trim())
                {
                    emailExists = true;
                }
            }

            return emailExists;
        }

        public bool CheckLoginSuccess(string email, string password)
        {
            var loginChecked = false;
            var sqlPass = string.Empty;
            var sqlSalt = string.Empty;

            var query = $@"SELECT uuid,
                                  email, 
                                  password, 
                                  salt, 
                                  registeredon, 
                                  lastlogin 
                           FROM   users 
                           WHERE  email = '{email}';";

            var updateQuery = $@"UPDATE users 
                                 SET lastlogin = '{DateTime.UtcNow.FormatMySqlDateTime()}'
                                 WHERE email = '{email}'";

            using (var command = new MySqlCommand(query, Connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sqlPass = reader.GetString("password").Trim();
                        sqlSalt = reader.GetString("salt").Trim();
                        GlobalProperties.Uuid = reader.GetGuid("uuid").ToString();
                    }
                }
            }

            var hashedPassword = GetSha256Hash(password);
            var hash = GetSha256Hash(hashedPassword + sqlSalt);

            if (hash == sqlPass)
            {
                loginChecked = true;

                GlobalProperties.UserConnected = loginChecked;
                GlobalProperties.UserEmail = email;

                // if login successful update last login time and set user connected
                using (var command = new MySqlCommand(updateQuery, Connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            return loginChecked;
        }

        public bool CheckLoginExists(string email)
        {
            var exists = false;

            using (var reader = CheckEmailExists(email))
            {
                if (reader.Read())
                {
                    exists = true;
                }
            }

            return exists;
        }

        public IList<Info> GetDataFromInfoTable()
        {
            var data = new List<Info>();

            var query = $@"SELECT name,
                                  weblink
                           FROM   info";

            using (var command = new MySqlCommand(query, Connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        data.Add(
                            new Info
                            {
                                Key = $"{Guid.NewGuid()}",
                                Name = reader.GetString("name"),
                                Weblink = reader.GetString("weblink")
                            });
                    }
                }
            }

            return data;
        }

        private MySqlDataReader CheckEmailExists(string email)
        {
            var query = $@"SELECT email, 
                                  password, 
                                  salt, 
                                  registeredon, 
                                  lastlogin
                           FROM   users 
                           WHERE  email = '{email}';";

            using (var command = new MySqlCommand(query, Connection))
            {
                var reader = command.ExecuteReader();
                return reader;
            }
        }

        private static string GetSalt()
        {
            return GetSha256Hash(Guid.NewGuid().ToString());
        }

        // https://stackoverflow.com/questions/16999361/obtain-sha-256-string-of-a-string/17001289
        private static string GetSha256Hash(string value)
        {
            var Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                var enc = Encoding.UTF8;
                var result = hash.ComputeHash(enc.GetBytes(value));

                foreach (var b in result)
                {
                    Sb.Append(b.ToString("x2"));
                }
            }

            return Sb.ToString();
        }
    }
}
