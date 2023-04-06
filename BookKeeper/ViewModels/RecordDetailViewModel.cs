using BookKeeper.Services;
using CommunityToolkit.Mvvm.Input;
namespace BookKeeper.ViewModels;

[QueryProperty("Record", "Record")]
[QueryProperty("AccountBookName", "AccountBookName")]
public partial class RecordDetailViewModel : BaseViewModel
{
    RecordService recordService;
    AccountBookService accountBookService;

    public RecordDetailViewModel(RecordService recordService, AccountBookService accountBookService)
	{
        Title = "Record Detail";

        this.recordService = recordService;
        this.accountBookService = accountBookService;
    }

	[ObservableProperty]
    Record record;

    [ObservableProperty]
    string accountBookName;

    [RelayCommand]
    async Task EditAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(EditPage)}", false,
            new Dictionary<string, object>
            {
                {"Record", record}
            });
    }

    [RelayCommand]
    async Task DeleteAsync()
    // alert message display https://learn.microsoft.com/zh-cn/dotnet/maui/user-interface/pop-ups?view=net-maui-7.0
    {
        bool answer = await Shell.Current.DisplayAlert(
            "Reminder",
            "Are you sure you want to delete it?",
            "Yes",
            "Cancel");

        if (answer)
        {
            await recordService.DeleteRecordByIDAsync(record.ID);

            await Shell.Current.GoToAsync("..", true);
        }
    }
}

