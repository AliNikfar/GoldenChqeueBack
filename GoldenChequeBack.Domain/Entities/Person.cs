using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name{ get; set; }
        public string Family{ get; set; }
        public string FatherName{ get; set; }
        public string PhoneNumber{ get; set; }
        public string Mob1{ get; set; }
        public string Mob2{ get; set; }
        public string Mob3{ get; set; }
        public int ActivedMob { get; set; }
        public string Address{ get; set; }
        public  int PersonCode{ get; set; }
        public string Detail{ get; set; }

    }
}
