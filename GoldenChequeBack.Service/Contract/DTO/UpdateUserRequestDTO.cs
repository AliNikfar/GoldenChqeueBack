using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract.DTO
{
    public class UpdateUserRequestDTO
    {
        Guid Id { get; set; }
        public int UserId
        {
            get;
            set;
        }

        public int UserName
        {
            get;
            set;
        }
    }
}
