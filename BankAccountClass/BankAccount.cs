using System;

namespace BankAccountApp
{
    public class BankAccount
    {
        public string AccountNumber { get; }
        public double Balance { get; private set; }
        public string AccountHolderName { get; }
        public string AccountType { get; }
        public DateTime DateOpened { get; }

        public BankAccount(string accountNumber, double initialBalance, string accountHolderName, string accountType, DateTime dateOpened)
        {
            if (accountType == "Checking")
            {
                throw new ArgumentException("Account type 'Checking' is not allowed.");
            }

            if (dateOpened < DateTime.Now.AddYears(-80))
            {
                throw new ArgumentException("Account cannot be opened more than 80 years ago.");
            }

            if (dateOpened > DateTime.Now)
            {
                throw new ArgumentException("Date opened cannot be in the future.");
            }

            if (!long.TryParse(accountNumber, out _))
            {
                throw new ArgumentException("Account number must contain only numbers.");
            }
            if (initialBalance < 0)
            {
                throw new ArgumentException("Initial balance cannot be negative.");
            }
            AccountNumber = accountNumber;
            Balance = initialBalance;
            AccountHolderName = accountHolderName;
            AccountType = accountType;
            DateOpened = dateOpened;
        }

        public void Credit(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount to be credited cannot be negative.");
            }
            Balance += amount;
        }

        public void Debit(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount to be debited cannot be negative.");
            }

            if (Balance >= amount)
            {
                Balance -= amount;
            }
            else
            {
                throw new Exception("Insufficient balance for debit.");
            }
        }

        public double GetBalance()
        {
            return Balance; // Math.Round(balance, 2);
        }

        public void Transfer(BankAccount toAccount, double amount)
        {
            if (Balance >= amount)
            {
                if (AccountHolderName != toAccount.AccountHolderName && amount > 500)
                {
                    throw new Exception("Transfer amount exceeds maximum limit for different account owners.");
                }

                if (AccountHolderName == toAccount.AccountHolderName)
                {
                    throw new Exception("Tranferencia não permitida para mesma conta");
                }

                Debit(amount);
                toAccount.Credit(amount);
            }
            else
            {
                throw new Exception("Insufficient balance for transfer.");
            }
        }

        public void PrintStatement()
        {
            Console.WriteLine($"Account Number: {AccountNumber}, Balance: {Balance}");
            // Add code here to print recent transactions
        }

        public double CalculateInterest(double interestRate)
        {
            return Balance * interestRate;
        }
    }
}