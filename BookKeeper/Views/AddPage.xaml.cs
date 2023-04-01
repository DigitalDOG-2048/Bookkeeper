namespace BookKeeper.Views;

public partial class AddPage : ContentPage
{
	public AddPage(AddViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    //void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    //{
    //    string oldText = e.OldTextValue;
    //    string newText = e.NewTextValue;
    //    //string myText = RemarksEntry.Text;
    //}

    //void OnEntryCompleted(object sender, EventArgs e)
    //{
    //    string text = ((Entry)sender).Text;
    //}
}
