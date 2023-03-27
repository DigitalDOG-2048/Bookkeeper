namespace BookKeeper.Views;

public partial class MainPage : ContentPage
{
	public MainPage(RecordsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}


