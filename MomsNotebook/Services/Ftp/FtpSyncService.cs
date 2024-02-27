using FluentFTP;
using MomsNotebook.Services.Repositories;
using MomsNotebook.Utils;
using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace MomsNotebook.Services.Ftp
{
    public class FtpSyncService : IFtpSyncService, IDisposable
    {
        public bool Active { get; set; }
        private FtpClient Connection { get; set; }
        private bool Disposed { get; set; }
        public async Task ConnectToFTPSAsync()
        {
            var token = new CancellationToken();
            var conn = new FtpClient("ip_address", "", "");

            conn.EncryptionMode = FtpEncryptionMode.Implicit;

            // Allows to use certificate provided by the server
            conn.ValidateAnyCertificate = true;
            conn.SslProtocols = SslProtocols.Tls12;

            await conn.ConnectAsync(token);

            Connection = conn;

            Active = true;
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
                Connection.Disconnect();
                Connection.Dispose();
            }

            Disposed = true;
            Active = false;
        }

        public async Task UploadFileAsync(string uuid)
        {
            var folderName = uuid.Replace('-', '_');

            await Connection.CreateDirectoryAsync($"/{folderName}", true);

            await Connection.UploadFileAsync(
                GlobalProperties.DatabasePath,
                $"/{folderName}/{SqlLiteDatabase.DatabaseName}",
                FtpRemoteExists.Overwrite);
        }

        public async Task DownloadFileAsync(string uuid)
        {
            var folderName = uuid.Replace('-', '_');

            await Connection.DownloadFileAsync(
                $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/{SqlLiteDatabase.DatabaseName}",
                $"/{folderName}/{SqlLiteDatabase.DatabaseName}",
                FtpLocalExists.Overwrite);
        }

        public async Task<bool> FileExistsAsync(string uuid)
        {
            var folderName = uuid.Replace('-', '_');

            return await Connection.FileExistsAsync($"/{folderName}/{SqlLiteDatabase.DatabaseName}");
        }
    }
}
