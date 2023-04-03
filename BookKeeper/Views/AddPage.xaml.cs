namespace BookKeeper.Views;

public partial class AddPage : ContentPage
{
	public AddPage(AddViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
