using ClearXchange.Server.Constants;
using ClearXchange.Server.Data;
using ClearXchange.Server.Interfaces;
using ClearXchange.Server.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace ClearXchange.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IRepository<Member> _repository;
        //private readonly Logger<MembersController> _logger;

        public MembersController(IRepository<Member> repository)//, Logger<MembersController> logger)
        {
            _repository = repository;
            //_logger = logger;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            var members = await _repository.GetAll();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(string id)
        {
            try
            {
                var member = await _repository.GetById(id);
                if (member == null)
                {
                    return NotFound();
                }
                //_logger.LogInformation($"Member with ID {id} retrieved successfully.");
                return Ok(member);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error retrieving member with ID {id}");
                // Handle the exception and return an appropriate response...
                return StatusCode(500, ErrorMessages.InternalError);
            }

        }

        [HttpPost("")]
        public async Task<ActionResult> AddMember([FromBody] Member newMember)
        {
            try
            {
                await _repository.Add(newMember);
                return CreatedAtAction(nameof(GetMember), new { id = newMember.Id }, newMember);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, ErrorMessages.AddingToDbError);
                // Handle the exception and return an appropriate response...
                return StatusCode(500, ErrorMessages.InternalError);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMember(string id, [FromBody] Member updatedMember)
        {
            // Check if the provided ID matches the ID in the object
            if (id != updatedMember.Id)
            {
                return BadRequest(ErrorMessages.MisMatchIDError);
            }

            // Retrieve the existing member from the repository
            var existingMember = await _repository.GetById(id);

            // Check if the member exists
            if (existingMember == null)
            {
                return NotFound(ErrorMessages.NotFound);
            }

            // Update the properties of the existing member
            existingMember.Name = updatedMember.Name;
            existingMember.DateOfBirth = updatedMember.DateOfBirth;
            existingMember.Email = updatedMember.Email;
            existingMember.Gender = updatedMember.Gender;
            existingMember.Phone = updatedMember.Phone;

            try
            {
                // Save the changes to the repository
                await _repository.Update(existingMember);
                //_logger.LogInformation($"Member with ID {id} updated successfully.");
                return Ok();
            }
            catch(Exception ex)
            {
                //_logger.LogError(ex, $"Error updating member with ID {id}");
                return StatusCode(500, ErrorMessages.InternalError);
            }  
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMember(string id)
        {
            try
            {
                await _repository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error deleting member with ID {id}");
                return StatusCode(500, ErrorMessages.InternalError);
            }
        }
    }
}
