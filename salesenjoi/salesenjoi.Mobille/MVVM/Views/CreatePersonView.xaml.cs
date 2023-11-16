using salesenjoi.Mobille.MVVM.ViewModels;
using salesenjoi.Mobille.Services;

namespace salesenjoi.Mobille.MVVM.Views;

public partial class CreatePersonView : ContentPage
{
	public CreatePersonView()
	{
		InitializeComponent();
		var serviceProvaider = new ServiceCollection()
			.AddSingleton<IApiService, ApiService>()
			.BuildServiceProvider();
		var _apiService = serviceProvaider.GetRequiredService<IApiService>();
        BindingContext = new CreatePersonViewModel(_apiService, this.Navigation);
	}
}