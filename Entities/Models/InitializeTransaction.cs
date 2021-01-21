using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public partial class InitializeTransaction
    {
        public string Amount { get; set; }
        public string Email { get; set; }
    }

    public partial class VerifyTransaction
    { 
     public string Reference { get; set; }
    
    }
}
