using salesenjoi.Mobille.MVVM.ViewModels;

namespace salesenjoi.Mobille.MVVM.Views;

public partial class ProductView : ContentPage
{
	public ProductView()
	{
		InitializeComponent();
        BindingContext = new ProductViewModel(this.Navigation);
    }
}