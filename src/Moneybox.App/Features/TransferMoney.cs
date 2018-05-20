using System;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = this.accountRepository.GetAccountById(fromAccountId);
            var to = this.accountRepository.GetAccountById(toAccountId);

            from.Withdraw(amount);
            to.Deposit(amount);

            this.accountRepository.Update(from);
            this.accountRepository.Update(to);

            // Only notify user if the operation was successful (no exception thrown)

            if (Account.PayInLimit - to.PaidIn < Account.PayInLimitWarningValue)
            {
                this.notificationService.NotifyApproachingPayInLimit(to.User.Email);
            }

            if (from.Balance < Account.LowFundsWarningValue)
            {
                this.notificationService.NotifyFundsLow(from.User.Email);
            }
        }
    }
}
