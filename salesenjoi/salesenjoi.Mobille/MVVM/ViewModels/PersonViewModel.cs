using salesenjoi.Mobille.MVVM.Model;
using salesenjoi.Mobille.MVVM.Views;
using salesenjoi.Mobille.Responses;
using salesenjoi.Mobille.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace salesenjoi.Mobille.MVVM.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        private ObservableCollection<Person> _people;
        private readonly IApiService _apiService;
        private static PersonViewModel _instance;

        public event PropertyChangedEventHandler PropertyChanged;

        public PersonViewModel(INavigation navigation, IApiService apiService)
        {
            _instance = this;   
            _navigation = navigation;
            _apiService = apiService;
            LoadPersonAsync();
        }

        public ObservableCollection<Person> People
        {
            get { return _people; }
            set
            {
                if (_people != value)
                {
                    _people = value;
                    OnPropertyChanged(nameof(People));
                }
            }
        }

        public ICommand NewPersonCommand => new Command(async () => await ExecuteNewPersonCommand());

        public static PersonViewModel GetInstance()
        {
            return _instance;
        }
        private async Task ExecuteNewPersonCommand()
        {
            await _navigation.PushAsync(new CreatePersonView());
        }
        public async void LoadPersonAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No tiene internet", "Aceptar");
                return;
            }

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<Person>(url, "/api", "/people");
            await Task.Delay(2000);


            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            People = new ObservableCollection<Person>((List<Person>)response.Result);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
