using salesenjoi.Mobille.MVVM.ViewModels;

namespace salesenjoi.Mobille.MVVM.Views;

public partial class PersonView : ContentPage
{
	public PersonView()
	{
		InitializeComponent();
		BindingContext = new PersonViewModel(this.Navigation);
	}
}