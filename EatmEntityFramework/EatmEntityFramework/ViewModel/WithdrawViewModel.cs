
using EatmEntityFramework.Models;
using System.Collections.Generic;

namespace EatmEntityFramework.ViewModel
{
    public class WithdrawViewModel
    {
        public int Id { get; set; }
        public double WithdrawAmount { get; set; }
        public double CurrentBalance { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}