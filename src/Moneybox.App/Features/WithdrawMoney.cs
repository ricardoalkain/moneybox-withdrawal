using System;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            var account = this.accountRepository.GetAccountById(fromAccountId);

            account.Withdraw(amount);

            this.accountRepository.Update(account);

            // Only notify user if the operation was successful (no exception thrown)
            if (account.Balance < Account.LowFundsWarningValue)
            {
                this.notificationService.NotifyFundsLow(account.User.Email);
            }
        }
    }
}
