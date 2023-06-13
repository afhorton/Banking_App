using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankingData;

namespace BankingYetAgain
{

    public partial class frmBanking : Form
    {
        List<Account> accounts;
        public frmBanking()
        {
            InitializeComponent();
        }

        // "hardcode" three accounts
        private void frmBanking_Load(object sender, EventArgs e)
        {
            accounts = new List<Account>()
            {
                new Account(200),
                new Account(500),
                new Account(100)
            };
            DisplayAccounts();
        }

        // display accounts in the list box
        private void DisplayAccounts()
        {
            lstAccounts.Items.Clear();
            foreach (Account a in accounts)
            {
                lstAccounts.Items.Add(a);
            }
        }

        private void GetAmountAndAccount(out decimal amount, out Account acct)
        {
            amount = 0;
            acct = null; // no account
            if (Validator.IsPresent(txtAmount) &&
                Validator.IsNonNegativeDecimal(txtAmount)) // valid data
            {
                // which account
                if (lstAccounts.SelectedIndex == -1)
                {
                    MessageBox.Show("You need to select an account");
                    return;
                } else // there is account
                {
                    int selectedIndex = lstAccounts.SelectedIndex; // 0 or more
                    acct = accounts[selectedIndex];
                    // get the amount
                    amount = Convert.ToDecimal(txtAmount.Text);
                }
            }
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            decimal amount = 0;
            Account selectedAccount = null;
            GetAmountAndAccount (out amount, out selectedAccount);
            if (amount > 0 && 
                selectedAccount != null)
            {
                // deposit to selected account
                selectedAccount.Deposit(amount);
                DisplayAccounts();
            }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            decimal amount = 0;
            Account selectedAccount = null;
            GetAmountAndAccount(out amount, out selectedAccount);
            if (amount > 0 &&
                selectedAccount != null)
            {
                bool success = selectedAccount.Withdraw(amount);
                if (success)
                {
                    DisplayAccounts();
                }
                else // NSF
                {
                    MessageBox.Show($"NSF: cannot withdraw {amount.ToString("c")}" +
                        $" from account with balance {selectedAccount.Balance.ToString("c")}");
                }
            }
        }
    }
}
