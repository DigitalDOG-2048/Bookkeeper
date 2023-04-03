namespace BookKeeper.Views;

public partial class RecordsPage : ContentPage
{
    private RecordsViewModel _viewModel;
    public RecordsPage(RecordsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetRecordsCommand.Execute(null);
    }
}


