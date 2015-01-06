using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddRegistration
{
    public class Registration
    {
        public string CustomerID { set; get; }
        public string ProductCode { set; get; }
        public DateTime RegistrationDate { set; get; }

        public Registration() : base() { }
    }
}
