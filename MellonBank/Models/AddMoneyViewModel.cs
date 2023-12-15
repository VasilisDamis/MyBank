using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MellonBank.Areas.Identity.Data;

namespace MellonBank.Models
{
    public class AddMoneyViewModel
    {
        
        public string AccountNumber { get; set; }

        public decimal Amount { get; set; }

       
    }
}

