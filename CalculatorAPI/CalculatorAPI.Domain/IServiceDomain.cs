using CalculatorAPI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorAPI.Domain
{
    internal interface IServiceDomain
    {
        IEnumerable<Customer> GetAll();
        Customer GetCustomerById(int id);
        Customer AddCustomer(Customer customer); 
        Customer UpdateCustomer(int id, Customer customer);
        void DeleteCustomerById(int id);

    }
}
