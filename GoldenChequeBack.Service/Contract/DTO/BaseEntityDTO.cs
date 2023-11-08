using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract.DTO
{
    public class BaseEntityDTO
    {
        public Guid Id { get; set; }

        public DateTime RegisterDate { get; set; }
        public int RegisterUser { get; set; }
        public DateTime LastChangeDate { get; set; }
        public int LastChangeUser { get; set; }
        public bool Visable { get; set; }
        public bool Author { get; set; }
    }
}
