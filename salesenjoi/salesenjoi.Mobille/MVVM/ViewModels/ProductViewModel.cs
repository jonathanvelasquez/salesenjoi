using salesenjoi.Mobille.MVVM.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace salesenjoi.Mobille.MVVM.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private readonly INavigation _navigation;

        public ProductViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        public ICommand NewProductCommand => new Command(async () => await ExecuteNewProductCommand());

        private async Task ExecuteNewProductCommand()
        {
           await _navigation.PushAsync(new CreateProductView());
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
