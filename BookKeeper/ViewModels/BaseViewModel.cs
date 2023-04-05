namespace BookKeeper.ViewModels;

public partial class BaseViewModel : ObservableObject
{
	public BaseViewModel()
	{
	}

    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    [ObservableProperty]
    string title;

    public bool IsNotBusy => !IsBusy;

    public static void showErrorAlert(string msg)
    {
        Shell.Current.DisplayAlert("Reminder", msg, "OK");
    }
}


