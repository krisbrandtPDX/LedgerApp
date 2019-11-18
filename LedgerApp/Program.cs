using LedgerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LedgerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramUI.Notify("Hello and welcome to Kris's version of the Ledger App");
            UpdateLedger();
        }

        private static void UpdateLedger()
        {
            Ledger ledger = new Ledger();
            string ledgerJson = LedgerAPI.GetLedger();
            if (ledgerJson != "")
            {
                ledger = JsonSerializer.Deserialize<Ledger>(ledgerJson);
            }
            ledger = UpdateUsers(ledger);
            ledgerJson = JsonSerializer.Serialize(ledger);
            LedgerAPI.PostLedger(ledgerJson);
        }

        private static Ledger UpdateUsers(Ledger ledger)
        {
            string prompt = "Enter user name to login: ";
            string userName = ProgramUI.Prompt(prompt);

            while (userName != "")
            {
                var user = ledger.Users.Where(u => u.Name == userName).FirstOrDefault();
                if (user == null)
                {
                    if (ProgramUI.Prompt("User not found, create now? <Y>/N ", "Y") == "Y")
                    {
                        user = new User() { Name = userName };
                        ledger.Users.Add(user);
                        ProgramUI.Notify("User created successfully.");
                    }
                }

                if(user != null)
                { 
                    user = UpdateAccounts(user);
                }
                userName = ProgramUI.Prompt(prompt);
            }
            return ledger;
        }


        private static User UpdateAccounts(User user)
        {
            ProgramUI.Notify(string.Format("Welcome, {0:G}", user.Name));
            string prompt = "Enter account name: ";
            string accountName = ProgramUI.Prompt(prompt);

            while (accountName != "")
            {
                var account = user.Accounts.Where(a => a.Name == accountName).FirstOrDefault();
                if (account == null)
                {
                    if (ProgramUI.Prompt("Account not found, create now? <Y>/N ", "Y") == "Y")
                    {
                        account = new Account() { Name = accountName };
                        user.Accounts.Add(account);
                        ProgramUI.Notify("Account created successfully");
                    }
                }
               
                if(account != null)
                {
                    account = UpdateTransactions(account);
                }
                accountName = ProgramUI.Prompt(prompt);
            }
            return user;
        }

        private static Account UpdateTransactions(Account account)
        {
            ProgramUI.Notify(string.Format("You are accessing account {0:G}", account.Name));
            string prompt = "Select option - 1:Balance 2:Deposit 3:Withdrawal 4:Transactions";
            string menuChoice = ProgramUI.Prompt(prompt);

            while (menuChoice != "")
            {
                switch (menuChoice)
                {
                    case "1":
                        double balance = account.Transactions.Sum(trx => trx.Amount);
                        ProgramUI.Notify(string.Format("Current Balance of Account {0:G}: {1:C}", account.Name, balance));
                        break;
                    case "2":
                        string depositEntered = ProgramUI.Prompt("Please enter deposit amount: ");
                        if (double.TryParse(depositEntered, out double depositAmount))
                        {
                            Transaction trx = new Transaction() { Amount = depositAmount };
                            account.Transactions.Add(trx);
                            ProgramUI.Notify(string.Format("Deposit recorded at {0:G}", trx.Timestamp));
                        }
                        break;
                    case "3":
                        string withdrawalEntered = ProgramUI.Prompt("Please enter withdrawal amount: ");
                        if (double.TryParse(withdrawalEntered, out double withdrawalAmount))
                        {
                            Transaction trx = new Transaction() { Amount = withdrawalAmount * -1 };
                            account.Transactions.Add(trx);
                            ProgramUI.Notify(string.Format("Withdrawal recorded at {0:G}", trx.Timestamp));
                        }
                        break;
                    case "4":
                        ProgramUI.Notify(string.Format("Transaction history for account {0:G}", account.Name));
                        foreach (Transaction t in account.Transactions)
                        {
                            ProgramUI.Notify(string.Format("{0:G} : {1:C} ", t.Timestamp, t.Amount));
                        }
                        ProgramUI.Notify(string.Format("End of transactions"));
                        break;
                    default:
                        menuChoice = "";
                        break;
                }
                menuChoice = ProgramUI.Prompt(prompt);
            }
            return account;
        }
    }
}

