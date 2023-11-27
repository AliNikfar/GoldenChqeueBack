using GoldenChequeBack.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IImageSelectorRepository
    {
        Task<ImageSelector> Upload(IFormFile file, ImageSelector image);
    }
}
