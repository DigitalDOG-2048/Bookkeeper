namespace BookKeeper.Views;

public partial class EditPage : ContentPage
{
	public EditPage(EditViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
