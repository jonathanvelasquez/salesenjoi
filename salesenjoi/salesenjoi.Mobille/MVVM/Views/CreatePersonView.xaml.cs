using salesenjoi.Mobille.MVVM.ViewModels;

namespace salesenjoi.Mobille.MVVM.Views;

public partial class CreatePersonView : ContentPage
{
	public CreatePersonView()
	{
		InitializeComponent();
		BindingContext = new CreatePersonViewModel();
	}
}