﻿using System;
using System.Collections.ObjectModel;
using BookKeeper.Services;
using BookKeeper.Views;

namespace BookKeeper.ViewModels;

public partial class RecordsViewModel : BaseViewModel
{
    RecordService recordService;
    public ObservableCollection<RecordModel> Records { get; set; } = new();
    public ObservableCollection<AccountBookModel> AccountBookList { get; set; } = new();

    public RecordsViewModel(RecordService recordService)
	{
        Title = "BOOKKEEPER";
        this.recordService = recordService;
        GetAccountBookList();
        GetRecordsAsync();
    }

    async void GetAccountBookList()
    {
        var response = await recordService.GetAccountBookList();
        if (response?.Count > 0)
        {
            foreach (var accountBook in response)
                AccountBookList.Add(accountBook);
        }
    }

    [RelayCommand]
    async Task GetRecordsAsync()
	{
		if (IsBusy)
			return;

        try
        {
            IsBusy = true;
            var records = await recordService.GetRecords();

            if (Records.Count != 0)
                Records.Clear();

            foreach (var transction in records)
                Records.Add(transction);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
	}

    [RelayCommand]
    async Task Add()
    {

    }

    [RelayCommand]
    void Delete(RecordModel record)
    {
        if (Records.Contains(record))
        {
            Records.Remove(record);
        }
    }

    [RelayCommand]
    async Task Tap(RecordModel record)
    {
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?RecordModel={record}");
    }
}
