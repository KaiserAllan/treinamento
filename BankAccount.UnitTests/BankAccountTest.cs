using System;
using BankAccountApp;

namespace BankAccountTests
{
    using System;
    using BankAccountApp;
    using Xunit;

    namespace BankAccountTests
    {
        public class BankAccountTest
        {
            [Fact]
            public void Credit_ShouldIncreaseBalance()
            {
                // Arrange
                var account = new BankAccount("12345", 1000, "John Doe", "Savings", DateTime.Now);

                // Act
                account.Credit(500);

                // Assert
                Assert.Equal(1500, account.Balance);
            }

            [Fact]
            public void Debit_ShouldDecreaseBalance_WhenSufficientFunds()
            {
                // Arrange
                var account = new BankAccount("12345", 1000, "John Doe", "Savings", DateTime.Now);

                // Act
                account.Debit(500);

                // Assert
                Assert.Equal(500, account.Balance);
            }

            [Fact]
            public void Debit_ShouldThrowException_WhenInsufficientFunds()
            {
                // Arrange
                var account = new BankAccount("12345", 1000, "John Doe", "Savings", DateTime.Now);

                // Act & Assert
                Assert.Throws<Exception>(() => account.Debit(1500));
            }

            [Fact]
            public void Transfer_ShouldTransferAmount_WhenSufficientFunds()
            {
                // Arrange
                var accountFrom = new BankAccount("12345", 1000, "John Doe", "Savings", DateTime.Now);
                var accountTo = new BankAccount("67890", 500, "Jane Doe", "Savings", DateTime.Now);

                // Act
                accountFrom.Transfer(accountTo, 500);

                // Assert
                Assert.Equal(500, accountFrom.Balance);
                Assert.Equal(1000, accountTo.Balance);
            }

            [Fact]
            public void Transfer_ShouldThrowException_WhenInsufficientFunds()
            {
                // Arrange
                var accountFrom = new BankAccount("12345", 1000, "John Doe", "Savings", DateTime.Now);
                var accountTo = new BankAccount("67890", 500, "Jane Doe", "Savings", DateTime.Now);

                // Act & Assert
                Assert.Throws<Exception>(() => accountFrom.Transfer(accountTo, 1500));
            }

            [Fact]
            public void Transfer_ShouldThrowException_WhenAmountExceedsLimitForDifferentOwners()
            {
                // Arrange
                var accountFrom = new BankAccount("12345", 1000, "John Doe", "Savings", DateTime.Now);
                var accountTo = new BankAccount("67890", 500, "Jane Doe", "Savings", DateTime.Now);

                // Act & Assert
                Assert.Throws<Exception>(() => accountFrom.Transfer(accountTo, 600));
            }

            [Fact]
            public void GetBalance_ShouldReturnCorrectBalance()
            {
                // Arrange
                var account = new BankAccount("12345", 1000, "John Doe", "Savings", DateTime.Now);

                // Act
                double balance = account.GetBalance();

                // Assert
                Assert.Equal(1000, balance);
            }

            [Fact]
            public void Credit_ShouldThrowException_WhenAmountIsNegative()
            {
                // Arrange
                var account = new BankAccount("12345", 1000, "John Doe", "Savings", DateTime.Now);

                // Act & Assert
                Assert.Throws<ArgumentException>(() => account.Credit(-500));
            }

            [Fact]
            public void Debit_ShouldThrowException_WhenAmountIsNegative()
            {
                // Arrange
                var account = new BankAccount("12345", 1000, "John Doe", "Savings", DateTime.Now);

                // Act & Assert
                Assert.Throws<ArgumentException>(() => account.Debit(-500));
            }

            [Fact]
            public void Transfer_ShouldThrowException_WhenAmountIsNegative()
            {
                // Arrange
                var accountFrom = new BankAccount("12345", 1000, "John Doe", "Savings", DateTime.Now);
                var accountTo = new BankAccount("67890", 500, "Jane Doe", "Savings", DateTime.Now);

                // Act & Assert
                Assert.Throws<ArgumentException>(() => accountFrom.Transfer(accountTo, -500));
            }

            [Fact]
            public void PrintStatement_ShouldPrintCorrectStatement()
            {
                // Arrange
                var account = new BankAccount("12345", 1000, "John Doe", "Savings", DateTime.Now);
                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);

                    // Act
                    account.PrintStatement();

                    // Assert
                    var expectedOutput = $"Account Number: 12345, Balance: 1000{Environment.NewLine}";
                    Assert.Equal(expectedOutput, sw.ToString());
                }
            }

            [Fact]
            public void AccountType_ShouldNotBeChecking()
            {
                // Arrange, Act & Assert
                Assert.Throws<ArgumentException>(() => new BankAccount("12345", 1000, "John Doe", "Checking", DateTime.Now));
            }

            [Fact]
            public void AccountOpeningDate_ShouldNotExceed80Years()
            {
                // Arrange
                var invalidDate = DateTime.Now.AddYears(-81);

                // Act & Assert
                Assert.Throws<ArgumentException>(() => new BankAccount("12345", 1000, "John Doe", "Savings", invalidDate));
            }

            [Fact]
            public void AccountNumber_ShouldContainOnlyNumbers()
            {
                // Arrange, Act & Assert
                Assert.Throws<ArgumentException>(() => new BankAccount("12345A", 1000, "John Doe", "Savings", DateTime.Now));
            }

            [Fact]
            public void InitialBalance_ShouldNotBeNegative()
            {
                // Arrange, Act & Assert
                Assert.Throws<ArgumentException>(() => new BankAccount("12345", -100, "John Doe", "Savings", DateTime.Now));
            }
            [Fact]
            public void Transfer_ShouldThrowException_WhenAccountNamesAreSame()
            {
                // Arrange
                var accountFrom = new BankAccount("12345", 1000, "John Doe", "Savings", DateTime.Now);
                var accountTo = new BankAccount("67890", 500, "John Doe", "Savings", DateTime.Now);

                // Act & Assert
                Assert.Throws<Exception>(() => accountFrom.Transfer(accountTo, 500));
            }

            [Fact]
            public void AccountOpeningDate_ShouldNotFutureDate()
            {
                // Arrange
                var invalidDate = DateTime.Now.AddDays(1);

                // Act & Assert
                Assert.Throws<ArgumentException>(() => new BankAccount("12345", 1000, "John Doe", "Savings", invalidDate));
            }
            
        }
    }
}