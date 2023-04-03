﻿using BookKeeper.Data;

namespace BookKeeper;

public partial class App : Application
{
	public static RecordDatabase RecordDatabase { get; private set; }

	public App(RecordDatabase recordDatabase)
	{
		InitializeComponent();
		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTUzODA1NEAzMjMxMmUzMTJlMzMzNUhPSFNyelp4UFNONVY4ZGJIK0tSQU9BMytCcjBRM0dBd2lZZS9MeU11SkU9");

		MainPage = new AppShell();

        RecordDatabase = recordDatabase;
	}
}

