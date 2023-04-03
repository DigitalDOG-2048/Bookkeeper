using AndroidX.Lifecycle;

namespace BookKeeper.Views;

public partial class SummaryPage : ContentPage
{
    private SummaryViewModel _viewModel;

    public SummaryPage(SummaryViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
		BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetBalanceListCommand.Execute(null);
    }
}
