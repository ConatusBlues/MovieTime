using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTime.Infrastructure
{
    public class IApiClient
    {
        Task<T> GetAsync<T>(string endpoint);
    }
}
