using System.Collections.Generic;
using HS.CustomerApp.Host.Models;

namespace HS.CustomerApp.Host.Logic
{
    public interface ICustomerService
    {
        void Create(CustomerModel customerModel);
        IEnumerable<CustomerModel> ReadAll();
        CustomerModel Read(long id);
        void Update(CustomerModel customerModel);
        void Delete(long id);
    }
}