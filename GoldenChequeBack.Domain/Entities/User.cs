
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Domain.Entities
{
    public  class User : BaseEntity
    {
        public int  UserId
        {
            get;
            set;
        }

        public int UserName
        {
            get;
            set;
        }
        //public Roles Role;
    }
}
