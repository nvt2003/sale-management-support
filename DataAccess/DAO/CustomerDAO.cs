using Microsoft.EntityFrameworkCore;
using Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CustomerDAO
    {
        private readonly MyDBShopContext _context;

        public CustomerDAO(MyDBShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customer.ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.Customer.FindAsync(id);
        }

        // Add a new customer
        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        // Update an existing customer
        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            _context.Customer.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        // Delete a customer
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return false;
            }

            _context.Customer.Remove(customer);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
