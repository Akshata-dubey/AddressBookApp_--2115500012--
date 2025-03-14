using BusinessLayer.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using ModelLayer.Validators;

namespace AddressBookApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressBookController : ControllerBase
    {
        private readonly IAddressBookBL _service;
        private readonly IValidator<AddressBookEntityEntry> _validator;

        public AddressBookController(IAddressBookBL service, IValidator<AddressBookEntityEntry> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressBookEntityEntry>>> GetAllContacts()
        {
            var contacts = await _service.GetAllContacts();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressBookEntityEntry>> GetContactById(int id)
        {
            var contact = await _service.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public async Task<ActionResult<AddressBookEntityEntry>> AddContact([FromBody] AddressBookEntityEntry contact)
        {
            var validationResult = await _validator.ValidateAsync(contact);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var addedContact = await _service.AddContact(contact);
            return CreatedAtAction(nameof(GetContactById), new { id = addedContact.Id }, addedContact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] AddressBookEntityEntry contact)
        {
            var updatedContact = await _service.UpdateContact(id, contact);
            if (updatedContact == null) return NotFound("Contact not found.");
            return Ok(updatedContact);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var deleted = await _service.DeleteContact(id);
            if (!deleted) return NotFound("Contact not found.");
            return NoContent();
        }
    }
}
