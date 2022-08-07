using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using ContactManager.Models;
using ContactManager.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository ContactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            ContactRepository = contactRepository;
        }

        // GET: api/Contact
        [HttpGet]
        public IActionResult Get()
        {
            var contacts = ContactRepository.GetContacts();
            return new OkObjectResult(contacts);
        }

        // GET: api/Contact/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var contact = ContactRepository.GetContactById(id);
            return new OkObjectResult(contact);
        }

        // POST: api/Contact
        [HttpPost]
        public IActionResult Post([FromBody] Contact contact)
        {
            using (var scope = new TransactionScope())
            {
                ContactRepository.InsertContact(contact);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = contact.Id }, contact);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Contact contact)
        {
            if (contact != null)
            {
                using (var scope = new TransactionScope())
                {
                    ContactRepository.UpdateContact(contact);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ContactRepository.DeleteContact(id);
            return new OkResult();
        }
    }
}
