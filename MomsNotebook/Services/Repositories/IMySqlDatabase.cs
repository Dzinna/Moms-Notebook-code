using MomsNotebook.Models.Database;
using System.Collections.Generic;

namespace MomsNotebook.Services.Repositories
{
    public interface IMySqlDatabase
    {
        bool Active { get; set; }
        void GetMySqlConnection();
        void CreateLogin(string email, string password);
        bool CheckLoginSuccess(string email, string password);
        bool CheckLoginExists(string email);
        bool CheckEmailExistsInDatabase(string email);
        IList<Info> GetDataFromInfoTable();
        void Dispose();
    }
}
