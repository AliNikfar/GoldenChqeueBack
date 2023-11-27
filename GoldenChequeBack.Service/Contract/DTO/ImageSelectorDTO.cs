using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract.DTO
{
    public  class ImageSelectorDTO
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileExtention { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
