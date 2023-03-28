namespace BookKeeper.Views;

public partial class DetailPage : ContentPage
{

    public DetailPage(RecordDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
