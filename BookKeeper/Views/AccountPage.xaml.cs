using AndroidX.Lifecycle;
using Xamarin.Google.Crypto.Tink.Mac;

namespace BookKeeper.Views;

public partial class AccountPage : ContentPage
{
    private AccountViewModel _viewModel;
    public AccountPage(AccountViewModel viewModel)
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

    //void OnCollectionViewSelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    //{
    //    var currentID = (e.CurrentSelection.FirstOrDefault() as AccountBook)?.ID;
    //    if (currentID == null)
    //        currentID = 0;
    //    MessagingCenter.Send<AccountPage, int>(this, "CurrentAccountBookID", (int)currentID);
    //}

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        //var selectedID = 0;
        //if (collectionView.SelectedItem != null)
        //{
        //    AccountBook accountBook = (AccountBook) collectionView.SelectedItem;
        //    selectedID = accountBook.ID;
        //}
        //MessagingCenter.Send<AccountPage, int>(this, "CurrentAccountBookID", selectedID);
    }
}
