using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using ModelLayer.DTO;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;

namespace BusinessLayer.Service
{
    public class AddressBookBL : IAddressBookBL
    {
        IAddressBookRL _repository;

        public AddressBookBL(IAddressBookRL repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AddressBookEntityEntry>> GetAllContacts() => await _repository.GetAllContacts();
        public async Task<AddressBookEntityEntry?> GetContactById(int id) => await _repository.GetContactById(id);
        public async Task<AddressBookEntityEntry> AddContact(AddressBookEntityEntry contact) => await _repository.AddContact(contact);
        public async Task<AddressBookEntityEntry?> UpdateContact(int id, AddressBookEntityEntry contact) => await _repository.UpdateContact(id, contact);
        public async Task<bool> DeleteContact(int id) => await _repository.DeleteContact(id);
    }
}
