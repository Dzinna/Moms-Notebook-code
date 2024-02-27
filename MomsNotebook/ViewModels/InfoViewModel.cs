using MomsNotebook.Models.Database;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MomsNotebook.Services;
using Xamarin.Forms;

namespace MomsNotebook.ViewModels
{
    public class InfoViewModel : BaseViewModel
    {
        public ObservableCollection<Info> InfoCollection { get; set; } = new ObservableCollection<Info>();
        public Command PopulateListCommand { get; set; }
        public InfoViewModel()
        {
            PopulateListCommand = new Command(async () => await LoadInfo());
        }

        private async Task LoadInfo()
        {
            InfoCollection.Clear();

            var infoData = await Database.ReadAllInfo();

            foreach (var info in infoData)
            {
                InfoCollection.Add(info);
            }
        }
    }
}
