using System.Threading.Tasks;

namespace MomsNotebook.Services.Ftp
{
    public interface IFtpSyncService
    {
        bool Active { get; set; }
        Task ConnectToFTPSAsync();
        Task UploadFileAsync(string uuid);
        Task DownloadFileAsync(string uuid);
        void Dispose();
        Task<bool> FileExistsAsync(string uuid);
    }
}
