using MomsNotebook.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPageMaster : ContentPage
    {
        public ListView ListView;

        public RootPageMaster()
        {
            InitializeComponent();

            BindingContext = new RootPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class RootPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<RootPageMasterMenuItem> MenuItems { get; set; }

            public RootPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<RootPageMasterMenuItem>(new[]
                {
                    new RootPageMasterMenuItem { Id = 0, Title = "APIE", TargetType = typeof(About)},
                    new RootPageMasterMenuItem { Id = 1, Title = "SINCHRONIZUOTI", TargetType = typeof(Synchronize) },
                    new RootPageMasterMenuItem { Id = 2, Title = "PRISIJUNGTI", TargetType = typeof(Login) },
                    new RootPageMasterMenuItem { Id = 3, Title = "IŠJUNGTI", TargetType = typeof(Exit) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}