using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Repository
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetContacts();
        Contact GetContactById(int ContactId);
        void InsertContact(Contact Contact);
        void UpdateContact(Contact Contact);
        void DeleteContact(int ContactId);
        void Save();
    }
}
