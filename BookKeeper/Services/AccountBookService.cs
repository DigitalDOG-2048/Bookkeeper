namespace BookKeeper.Services;

public class AccountBookService
{
	public Task<AccountBookModel> GetAccountBookList()
	{
		return Task.FromResult(new AccountBookModel());
	}
}

