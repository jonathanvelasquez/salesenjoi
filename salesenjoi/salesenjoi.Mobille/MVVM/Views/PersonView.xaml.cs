using salesenjoi.Mobille.MVVM.ViewModels;
using salesenjoi.Mobille.Services;

namespace salesenjoi.Mobille.MVVM.Views;

public partial class PersonView : ContentPage
{
	public PersonView()
	{
		InitializeComponent();
        var serviceProvider = new ServiceCollection()
          .AddSingleton<IApiService, ApiService>()
          .BuildServiceProvider();

        var _apiService = serviceProvider.GetRequiredService<IApiService>();
        BindingContext = new PersonViewModel(this.Navigation, _apiService);
	}
}