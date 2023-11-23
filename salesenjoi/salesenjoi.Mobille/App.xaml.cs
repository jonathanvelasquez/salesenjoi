using salesenjoi.Mobille.MVVM.Views;

namespace salesenjoi.Mobille
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TabbedView());
        }
    }
}