using AutoMapper;
using CalculatorAPI.Contracts;
using CalculatorAPI.Data;
using CalculatorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorAPI.Domain 
{
    public class ServiceDomain : IServiceDomain
    {
        //private readonly List<CustomerModel> _customers;
        private readonly IDataContext _data;
        private readonly IMapper _mapper;

        public ServiceDomain(IMapper mapper, IDataContext data)
        {
            _mapper = mapper;
            _data = data;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _mapper.Map<List<Customer>>(_data.Customers.ToList());
            //return _data.Customers.ToList();
        }

        public Customer GetCustomerById(int id)
        {
            //var findCostumer = _customers.Where(e => e.Id == id).FirstOrDefault();
            var findCostumer = _data.Customers.Where(e => e.Id == id).FirstOrDefault();

            if (findCostumer == null) return null;

            return _mapper.Map<Customer>(findCostumer);
        }

        public Customer AddCustomer(Customer newCustomer)
        {
            //CustomerModel newCustomerModel = new CustomerModel {
            //    Name = newCustomer.Name,
            //    DOB = newCustomer.DOB
            //};

            try
            {
                //newCustomerModel.Id = _customers.Max(e => e.Id) + 1;
                //newCustomer.Id = newCustomerModel.Id;
                //_customers.Add(newCustomerModel);
                //newCustomer.Id = _customers.Max(e => e.Id) + 1;
                newCustomer.Id = _data.Customers.Max(e => e.Id) + 1;

                //_customers.Add(_mapper.Map<CustomerModel>(newCustomer));
                _data.Customers.Add(_mapper.Map<CustomerModel>(newCustomer));
                return newCustomer;
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateCustomer(int id, Customer editedCustomer)
        {
            //var existingCustomer = _customers.Where(e => e.Id == id).FirstOrDefault();
            var existingCustomer = _data.Customers.Where(e => e.Id == id).FirstOrDefault();

            if (existingCustomer == null) return false;

            existingCustomer.Name = editedCustomer.Name;
            existingCustomer.DOB = editedCustomer.DOB;

            return true;
        }

        public bool DeleteCustomerById(int id)
        {
            //CustomerModel findCostumer = _customers.Where(e => e.Id == id).FirstOrDefault();
            //var findCostumer = _customers.Where(e => e.Id == id).FirstOrDefault();
            var findCostumer = _data.Customers.Where(e => e.Id == id).FirstOrDefault();

            if (findCostumer == null)
                return false;

            //_customers.Remove(findCostumer);
            _data.Customers.Remove(findCostumer);
            return true;
        }

    }
}
