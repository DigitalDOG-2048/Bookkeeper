﻿namespace BookKeeper;
using BookKeeper.Views;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(RecordsPage), typeof(RecordsPage));
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
    }
}

