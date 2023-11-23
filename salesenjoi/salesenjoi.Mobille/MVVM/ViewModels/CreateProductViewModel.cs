using Android.Webkit;
using salesenjoi.Mobille.MVVM.Models;
using salesenjoi.Mobille.Responses;
using salesenjoi.Mobille.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace salesenjoi.Mobille.MVVM.ViewModels
{
    
    public class CreateProductViewModel : INotifyPropertyChanged
    {
        private readonly IApiService _apiService;
        private readonly INavigation _navigation;
        private ObservableCollection<Category> _categories;
        private string _name;
        private string _description;
        private int _price;
        private bool _isToggled;
        private Category _selectedCategory;

        public CreateProductViewModel(IApiService apiService, INavigation navigation)
        {
            _apiService = apiService;
            _navigation = navigation;
            LoadCategoriesAsync();
        }       

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public int Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        public bool IsToggled
        {
            get { return _isToggled; }
            set
            {
                if (_isToggled != value)
                {
                    _isToggled = value;
                    OnPropertyChanged(nameof(IsToggled));
                }
            }
        }

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    OnPropertyChanged(nameof(Categories));
                }
            }
        }
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged(nameof(SelectedCategory));
                }
            }
        }

        public ICommand NewProductCommand => new Command(async () => await ExecuteNewProductCommand());

        private async Task ExecuteNewProductCommand()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No tiene internet", "Aceptar");
                return;
            }

            Product product = new Product
            {
                name = Name,
                description = Description,
                price = Price,
                isActive = IsToggled,
                CategoryId = SelectedCategory.id,
                category = new Category {id = SelectedCategory.id, name = SelectedCategory.name }
            };

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.PostAsync(url, "/api", "/products", product);
            await Task.Delay(2000);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            await App.Current.MainPage.DisplayAlert("OK","El producto se ha registrado correctamente","Aceptar");
            await _navigation.PopAsync();
        }


        private async void LoadCategoriesAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No tiene internet", "Aceptar");
                return;
            }

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<Category>(url, "/api", "/categories");
            await Task.Delay(2000);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            Categories = new ObservableCollection<Category>((List<Category>)response.Result);
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
