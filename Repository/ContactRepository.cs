using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.DBContexts;
using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactContext _dbContext;

        public ContactRepository(ContactContext dbContext) => _dbContext = dbContext;
        public void DeleteContact(int ContactId)
        {
            var contact = _dbContext.Contacts.Find(ContactId);
            _dbContext.Contacts.Remove(contact);
            Save();
        }

        public Contact GetContactById(int ContactId) => _dbContext.Contacts.Find(ContactId);

        public IEnumerable<Contact> GetContacts() => _dbContext.Contacts.ToList();

        public void InsertContact(Contact Contact)
        {
            _dbContext.Add(Contact);
            Save();
        }

        public void Save() => _dbContext.SaveChanges();

        public void UpdateContact(Contact Contact)
        {
            _dbContext.Entry(Contact).State = EntityState.Modified;
            Save();
        }
    }
}
