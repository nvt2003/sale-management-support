using DataAccess.DAO;
using Objects.Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDAO _customerDAO;
        public CustomerRepository(CustomerDAO customerDAO)
        {
            _customerDAO = customerDAO;
        }
        public Task<Customer> AddCustomerAsync(Customer customer)
        {
            return _customerDAO.AddCustomerAsync(customer);
        }

        public Task<bool> DeleteCustomerAsync(int id)
        {
            return _customerDAO.DeleteCustomerAsync(id);
        }

        public Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return _customerDAO.GetAllCustomersAsync();
        }

        public Task<Customer> GetCustomerByIdAsync(int id)
        {
            return _customerDAO.GetCustomerByIdAsync(id);
        }

        public Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            return _customerDAO.UpdateCustomerAsync(customer);
        }
    }
}
