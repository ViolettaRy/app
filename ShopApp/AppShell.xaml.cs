using ShopApp.Views;

namespace ShopApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute("AnimalPage", typeof(AnimalPage));
        Routing.RegisterRoute("ProviderPage", typeof(ProviderPage));
        Routing.RegisterRoute("MainPage", typeof(CategoryPage));
        Routing.RegisterRoute("ProductPage", typeof(ProductPage));
        Routing.RegisterRoute("StatisticPage", typeof(StatisticPage));
        Routing.RegisterRoute("MarkPage", typeof(StatisticPage));
        Routing.RegisterRoute("TemplatePage", typeof(TemplatePage));
        Routing.RegisterRoute("RevenuePage", typeof(RevenuePage));
        Routing.RegisterRoute("RevisionPage", typeof(RevisionPage));
    }
}
