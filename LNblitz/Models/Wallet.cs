using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace LNblitz.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string AdminKey { get; set; }
        public string InvoiceKey { get; set; }
        public string ReadonlyKey { get; set; }

        public User User { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
