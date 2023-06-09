namespace ShopApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
	protected override Window CreateWindow(IActivationState activationState)
	{
		var window = base.CreateWindow(activationState);

		const int newWidth = 1150;
        const int newHight = 600;

		window.X = 170;
		window.Y = 130;

		window.Width = newWidth;
		window.Height = newHight;

		window.MinimumHeight = newHight;
		window.MinimumWidth = newWidth;

        return window;
    }
}
