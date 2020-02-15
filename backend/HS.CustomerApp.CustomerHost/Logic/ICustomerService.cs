using System.Collections.Generic;
using System.Threading.Tasks;
using HS.CustomerApp.CustomerHost.Models;

namespace HS.CustomerApp.CustomerHost.Logic
{
    public interface ICustomerService
    {
        Task<long> Create(CustomerModel customerModel);
        IEnumerable<CustomerModel> ReadAll();
        CustomerModel Read(long id);
        void Update(CustomerModel customerModel);
        void Delete(long id);
    }
}