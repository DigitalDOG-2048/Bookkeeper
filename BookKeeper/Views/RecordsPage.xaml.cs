using CommunityToolkit.Mvvm.Messaging;
using static Java.Util.Concurrent.Flow;

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

        _viewModel.GetAccountBookListCommand.Execute(null);
        _viewModel.GetRecordsCommand.Execute(null);
    }
}
