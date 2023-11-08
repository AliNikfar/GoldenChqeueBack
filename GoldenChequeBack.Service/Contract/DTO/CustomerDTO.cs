using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract.DTO
{
    public  class CustomerDTO
    {

        public int Code
        {
            get;
            set;
        }
        
        public string Name
        {
            get;
            set;
        }
        
        public string LastName
        {
            get;
            set;
        }
        
        public string FatherName
        {
            get;
            set;
        }

        public string PhoneNum
        {
            get;
            set;
        }

        public string Mob1
        {
            get;
            set;
        }

        public string Mob2
        {
            get;
            set;
        }

        public string Mob3
        {
            get;
            set;
        }

        public Guid City
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string PostalCode
        {
            get;
            set;
        }


        public string Details
        {
            get;
            set;
        }

        public int? MaxBuyPrice
        {
            get;
            set;
        }

        public DateTime? BirthDate
        {
            get;
            set;
        }

        public Guid CustomerRate
        {
            get;
            set;
        }
    }
}
