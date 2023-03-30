namespace BookKeeper;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTUzODA1NEAzMjMxMmUzMTJlMzMzNUhPSFNyelp4UFNONVY4ZGJIK0tSQU9BMytCcjBRM0dBd2lZZS9MeU11SkU9");

		MainPage = new AppShell();
	}
}

