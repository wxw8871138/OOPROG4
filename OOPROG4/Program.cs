using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPROG4
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer a = new Customer("wang", "clementi", "xxx", new DateTime(1993, 9, 18));
            Customer b = new Customer("zhang", "ISS", "xxf", new DateTime(1999, 2, 8));
            Customer c = new Customer("pan", "home", "xxz", new DateTime(1993, 11, 1));
            SavingAccount x = new SavingAccount("000-000-001", a, 2000);
            CurrentAccount y = new CurrentAccount("000-000-002", b, 20);
            OverDraftAccount z = new OverDraftAccount("000-000-003", c, 50000);

            Console.WriteLine("Before creditinterest:");
            Console.WriteLine(x.ToString());
            Console.WriteLine(y.ToString());
            Console.WriteLine(z.ToString());

            x.CreditInterest();
            y.CreditInterest();
            z.CreditInterest();
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("After creditinterest:");
            Console.WriteLine(x.ToString());
            Console.WriteLine(y.ToString());
            Console.WriteLine(z.ToString());

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("After pan withdraw 100000 & creditinterest:");
            z.Withdraw(100000);
            z.CreditInterest();
            Console.WriteLine(z.ToString());
            
        }
    }
    class Customer
    {
        private string name;
        private string address;
        private string passportNo;
        private DateTime dateOfBirth;

        public Customer()
        {

        }
        public Customer(string name, string address, string passportNo, DateTime dateOfBirth)
        {
            this.name = name;
            this.address = address;
            this.passportNo = passportNo;
            this.dateOfBirth = dateOfBirth;
        }

        public int GetAge()
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            return age;
        }
        public string GetName()
        {
            return name;
        }

        public string GetAddress()
        {
            return address;
        }
    }
    class BankAccount3
    {
        string number;
        Customer holder;
        double balance;
        double interestRate = 0;

        public BankAccount3(string number, Customer holder, double balance)
        {
            this.number = number;
            this.holder = holder;
            this.balance = balance;
        }
        public bool Withdraw(double amount)
        {
            if (amount <= balance)
            {
                balance = balance = amount;
                return true;
            }
            else
            {
                Console.Error.WriteLine("Withdraw for {0} is unseccessful.No enough balance", holder.GetName());
                return false;
            }
        }
        public void Deposit(double amount)
        {
            balance = balance + amount;
        }

        public bool TransferTo(double amount, BankAccount3 another)
        {
            if (Withdraw(amount))
            {
                another.Deposit(amount);
                return true;
            }
            else
            {
                Console.Error.WriteLine("Translate for {0} is unseccessful.No enough balance", holder.GetName());
                return false;
            }
        }

        protected double CalculateInterest()
        {
            double insterest;
            insterest = Balance * interestRate;
            return insterest;
        }

        public void CreditInterest()
        {
            balance = balance + CalculateInterest();
        }

        protected double Balance
        {
            get
            {
                return balance;
            }
            set
            {
                balance = value;
            }
        }

        public override string ToString()
        {
            string info;
            info = string.Format("number={0},holder={1},balance={2}", number, holder.GetName(), balance);
            return info;
        }

    }
    class SavingAccount : BankAccount3
    {
        double interestRate = 0.01;

        public SavingAccount(string number, Customer holder, double balance) : base(number, holder, balance)
        {
        }

        private new double CalculateInterest()
        {
            double interest;
            interest = Balance * interestRate;
            return interest;
        }

        public new void CreditInterest()
        {
            Balance = Balance + CalculateInterest();
        }
    }

    class CurrentAccount : BankAccount3
    {
        double interestRate = 0.0025;

        public CurrentAccount(string number, Customer holder, double balance) : base(number, holder, balance)
        {
        }

        private new double CalculateInterest()
        {
            double interest;
            interest = Balance * interestRate;
            return interest;
        }

        public new void CreditInterest()
        {
            Balance = Balance + CalculateInterest();
        }
    }

    class OverDraftAccount : BankAccount3
    {
        double interestRate = 0.0025;
        double interestRateNeg = 0.06;

        public OverDraftAccount(string number, Customer holder, double balance) : base(number, holder, balance)
        {
        }
        public void Withdraw(double amount)
        {
            Balance = Balance - amount;
        }
        private new double CalculateInterest()
        {
            double interest;
            if (Balance > 0)
            {
                interest = Balance * interestRate;
            }
            else
            {
                interest = Balance * interestRateNeg;
            }
            return interest;
        }

        public new void CreditInterest()
        {            
                Balance = Balance + CalculateInterest();            
        }
    }
}