using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Thesis.Dto;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Thesis.Services
{
    public interface IPointService
    {
        Task<IEnumerable<PointDto>> GetAllPins();

    }
}
