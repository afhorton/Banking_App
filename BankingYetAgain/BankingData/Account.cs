using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingData
{
    public class Account // turn class from 'internal' to 'public'
    {
        // private data
        private static int nextAccountNo = 100;
        private int accountNo;
        private decimal balance;

        // read-only properties
        public int AccountNo { get { return accountNo; }}
        public decimal Balance { get { return balance; }}

        // constructor 
        public Account(decimal initBalance)
        {
            accountNo = nextAccountNo;
            nextAccountNo++;
            if(initBalance < 0)
            {
                initBalance = 0;
            }
            balance = initBalance;
        }

        // public methods
        public void Deposit (decimal amount)
        {
            if(amount > 0)
            {
                balance += amount;
            }
        }

        /// <summary>
        /// withdraw amount from the account
        /// </summary>
        /// <param name="amount">amount to withdraw</param>
        /// <returns>true if success, or false if NSF</returns>
        public bool Withdraw (decimal amount)
        {
            bool result = true;
            if (amount > 0)
            {
                if(balance >= amount)
                {
                    balance -= amount;
                } else //NSF
                {
                    result = false;
                }
            }
            return result;
        }

        // ToString() method for display
        public override string ToString()
        {
            return $"{accountNo.ToString()}: {balance.ToString("c")}.";
        }
    }
}
