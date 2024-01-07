using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RembraceLogIn.Shared.Models
{
    public class AccountModel
    {
        public class ApplicationUser : IdentityUser
        {
            public List<Account> Accounts { get; set; }
        }

        public class Account
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [DataType(DataType.Currency)]
            public double Balance { get; set; } = 1000.00;
            public string AccountType { get; set; } = "Cheque";

            [JsonIgnore]
            public ApplicationUser User { get; set; }
        }
    }
}
