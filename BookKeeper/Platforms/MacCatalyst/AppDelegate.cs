﻿using Foundation;
using SQLitePCL;

namespace BookKeeper;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp()
	{
        raw.SetProvider(new SQLite3Provider_e_sqlite3());
        return MauiProgram.CreateMauiApp();
    }
}

