using System;

namespace Moneybox.App
{
    public class Account
    {
        public const decimal PayInLimit = 4000m;
        public const decimal PayInLimitWarningValue = 500m;
        public const decimal LowFundsWarningValue = 500m;

        public Guid Id { get; protected set; }

        public User User { get; protected set; }

        public decimal Balance { get; protected set; }

        public decimal Withdrawn { get; protected set; }

        public decimal PaidIn { get; protected set; }

        // This protected constructor and above set methods allow persistence/serialization to
        // work fine while it keeps the Account values read-only for other non-inherited classes
        protected Account()
        {
        }

        public void Deposit(decimal amount)
        {
            var paidIn = this.PaidIn + amount;

            if (paidIn > Account.PayInLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            this.Balance += amount;
            this.PaidIn += amount;
        }

        public void Withdraw(decimal amount)
        {
            var finalBalance = this.Balance - amount;

            if (finalBalance < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

            this.Balance -= amount;
            this.Withdrawn -= amount;
        }
    }
}
