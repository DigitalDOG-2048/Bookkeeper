using CommunityToolkit.Mvvm.Input;
namespace BookKeeper.ViewModels;

[QueryProperty("Record", "Record")]
public partial class RecordDetailViewModel : BaseViewModel
{
	public RecordDetailViewModel()
	{
        Title = "Record Detail";
    }

	[ObservableProperty]
	Record record;

    [RelayCommand]
    async void Edit()
    {
        DateTime dateTime = record.DateTime;
    }

    [RelayCommand]
    private void Delete(Record record)
    {

    }

}

