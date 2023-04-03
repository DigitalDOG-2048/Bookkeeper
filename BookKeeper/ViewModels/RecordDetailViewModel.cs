using BookKeeper.Services;
using CommunityToolkit.Mvvm.Input;
namespace BookKeeper.ViewModels;

[QueryProperty("Record", "Record")]
public partial class RecordDetailViewModel : BaseViewModel
{
    RecordService recordService;

    public RecordDetailViewModel(RecordService recordService)
	{
        Title = "Record Detail";

        this.recordService = recordService;
    }

	[ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AccountBookName))]
    Record record;

    public string AccountBookName => recordService.GetAccountBook(record.AccountBookID).AccountBookName;

    [RelayCommand]
    async Task EditAsync()
    {
        // todo

        //SaveRecordAsync
    }

    [RelayCommand]
    async Task DeleteAsync()
    {
        bool answer = await Shell.Current.DisplayAlert(
            "Reminder",
            "Are you sure you want to delete it?",
            "Yes",
            "Cancel");

        if (answer)
        {
            await recordService.DeleteRecordByIdAsync(record.ID);

            await Shell.Current.GoToAsync("..", true);
        }
    }
}

