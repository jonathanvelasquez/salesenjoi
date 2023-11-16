using salesenjoi.Mobille.MVVM.Model;
using salesenjoi.Mobille.Responses;
using salesenjoi.Mobille.Services;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace salesenjoi.Mobille.MVVM.ViewModels
{
    public class CreatePersonViewModel : INotifyPropertyChanged
    {
        private string _document;
        private string _firtName;
        private string _lastName;
        private string _addres;
        private string _phone;
        private string _email;
        private bool _isRunning;
        private bool _isEnabled;
        private readonly IApiService _apiService;
        private readonly INavigation _navigation;

        public CreatePersonViewModel(IApiService apiService, INavigation navigation)
        {
            IsEnabled = true;
            _apiService = apiService;
            _navigation = navigation;
        }

        public string Document
        {
            get { return _document; }
            set
            {
                if (_document != value)
                {
                    _document = value;
                    OnPropertyChanged(nameof(Document));
                    
                }

            }
        }

        public string FirtName
        {
            get { return _firtName; }
            set
            {
                if (_firtName != value)
                {
                    _firtName = value;
                    OnPropertyChanged(nameof(FirtName));

                }

            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));

                }

            }
        }

        public string Addres
        {
            get { return _addres; }
            set
            {
                if (_addres != value)
                {
                    _addres = value;
                    OnPropertyChanged(nameof(Addres));

                }

            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged(nameof(Phone));

                }

            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));

                }

            }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    OnPropertyChanged(nameof(IsRunning));

                }

            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged(nameof(IsEnabled));

                }

            }
        }

        public ICommand SavePersonCommand => new Command(async () => await ExecuteSavePersonCommand());

        private async Task ExecuteSavePersonCommand()
        {
            bool isValid = await ValidateDateAsync();
            if (!isValid) 
            {
                return;
            }


            IsRunning = true;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "NO tienes internet, porfavor enciende el internet de tu movil", "Aceptar");
                return;
            }

            Person person = new Person 
            { 
                Document = Document,
                FirstName = FirtName,
                LastName = LastName,
                Addres = Addres,    
                Phone = Phone,  
                Email = Email,  
            
            };

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.PostAsync(url,"/api","/people", person);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message,"Aceptar");
                return;
            }

            await App.Current.MainPage.DisplayAlert("Ok","La persona se ha registrado correctamente","Aceptar");
            await _navigation.PopAsync();

        }

        private async Task<bool> ValidateDateAsync()
        {
            if (string.IsNullOrEmpty(Document))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debes de ingresar un documento es obligatorio.", "Aceptar");
                return false;   
            }

            if (string.IsNullOrEmpty(FirtName))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debes de ingresar un nombre es obligatorio.", "Aceptar");
                return false;
            }

            return true;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
