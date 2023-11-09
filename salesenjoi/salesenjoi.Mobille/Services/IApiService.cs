using salesenjoi.Mobille.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salesenjoi.Mobille.Services
{
    public interface IApiService
    {
        Task<Response> PostAsync<T>(string urlBase, string servicePrefix, string controller, T model);
    }
}
