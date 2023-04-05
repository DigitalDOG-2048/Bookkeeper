using AndroidX.Lifecycle;

namespace BookKeeper.Views;

public partial class SearchPage : ContentPage
{
    private SearchViewModel _viewModel;

    public SearchPage(SearchViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.GetAccountBookListCommand.Execute(null);
    }
}
