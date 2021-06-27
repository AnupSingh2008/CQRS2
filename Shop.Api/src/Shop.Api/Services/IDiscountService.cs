using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Api.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IDiscountService
    {
        [AllowAnyStatusCode]
        [Get("discounts/{id}")]
        Task<object> GetAsync([Path] Guid id);

        [AllowAnyStatusCode]
        [Get("discounts")]
        Task<object> FindAsync(Guid customerId);
    }

}
