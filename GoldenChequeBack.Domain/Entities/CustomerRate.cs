using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Domain.Entities
{
    public class CustomerRate : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Title
        {
            get;
            set;
        }
    }
}
