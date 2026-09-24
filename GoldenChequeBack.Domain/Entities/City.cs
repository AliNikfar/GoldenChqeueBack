using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Domain.Entities
{
    public class City : BaseEntity
    {
        [Required]
        public string Name
        {
            get;
            set;
        }
        [Required]
        public string CityCode
        {
            get;
            set;
        }
        // Foreign key (mapped to the existing FK column)
        public Guid OstanId { get; set; }

        [Required]
        public State Ostan
        {
            get;
            set;
        }
    }
}
