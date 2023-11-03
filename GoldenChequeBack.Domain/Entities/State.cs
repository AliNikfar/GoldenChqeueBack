using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Domain.Entities
{
    public class State:BaseEntity
    {
        [Required]
        public string Name
        {
            get;
            set;
        }
    }
}
