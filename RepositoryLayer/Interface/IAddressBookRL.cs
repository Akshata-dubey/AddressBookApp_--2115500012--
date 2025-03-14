using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.DTO;

namespace RepositoryLayer.Interface
{
    public interface IAddressBookRL
    {
        Task<IEnumerable<AddressBookEntityEntry>> GetAllContacts();
        Task<AddressBookEntityEntry?> GetContactById(int id);
        Task<AddressBookEntityEntry> AddContact(AddressBookEntityEntry contact);
        Task<AddressBookEntityEntry?> UpdateContact(int id, AddressBookEntityEntry contact);
        Task<bool> DeleteContact(int id);
    }
}
