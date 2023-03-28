namespace BookKeeper.Views;

public partial class RecordsPage : ContentPage
{
    public RecordsPage(RecordsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}


