using System;
using System.Threading.Tasks;
using RestEase;
using Shop.Api.Models.Operations;

namespace Shop.Api.Services
{
    public interface IOperationsService
    {
        [AllowAnyStatusCode]
        [Get("operations/{id}")]
        Task<Operation> GetAsync([Path] Guid id);          
    }
}