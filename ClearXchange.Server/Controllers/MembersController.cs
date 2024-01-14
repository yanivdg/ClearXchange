using ClearXchange.Server.Constants;
using ClearXchange.Server.Interfaces;
using ClearXchange.Server.Model;
using ClearXchange.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
//using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace ClearXchange.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly ILogger<MembersController> _logger;

        public MembersController(IMemberService memberService, ILogger<MembersController> logger)
        {
            //_repository = repository;
            _memberService = memberService;
            _logger = logger;
        }

        [HttpGet("GetAllMembers")]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            try
            {
                var members = await _memberService.GetAllMembers();
                if (members == null)
                {
                    return NotFound();
                }
                _logger.LogInformation($"All Members retrieved successfully.");
                return Ok(members);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving members");
                // Handle the exception and return an appropriate response...
                return StatusCode(500, ErrorMessages.InternalError);
            }
        }

        [HttpGet("GetMember/{id}")]
        public async Task<ActionResult<Member>> GetMember(string id)
        {
            try
            {
                var member = await _memberService.GetMemberById(id);
                if (member == null)
                {
                    return NotFound();
                }
                _logger.LogInformation($"Member with ID {id} retrieved successfully.");
                return Ok(member);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving member with ID {id}");
                // Handle the exception and return an appropriate response...
                return StatusCode(500, ErrorMessages.InternalError);
            }

        }

        [HttpGet("SearchByName/{searchString}")]
        public async Task<IActionResult> SearchByName(string searchString)
        {
            try
            {
                var members = await _memberService.Search(s => s.Name.Contains(searchString));
                return Ok(members);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving members records with substring {searchString}");
                // Handle the exception and return an appropriate response...
                return StatusCode(500, ErrorMessages.InternalError);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult> AddMember([FromBody] Member newMember)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            try
            {
                await _memberService.AddMember(newMember);
                return CreatedAtAction(nameof(GetMember), new { id = newMember.Id }, newMember);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.AddingToDbError);
                // Handle the exception and return an appropriate response...
                return StatusCode(500, ErrorMessages.InternalError);
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateMember(string id, [FromBody] Member updatedMember)
        {
            // Check if the provided ID matches the ID in the object
            if (id != updatedMember.Id)
            {
                return BadRequest(ErrorMessages.MisMatchIDError);
            }

            // Retrieve the existing member from the repository
            var existingMember = await _memberService.GetMemberById(id);

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
                await _memberService.UpdateMember(id,existingMember);
                _logger.LogInformation($"Member with ID {id} updated successfully.");
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error updating member with ID {id}");
                return StatusCode(500, ErrorMessages.InternalError);
            }  
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteMember(string id)
        {
            try
            {
                await _memberService.DeleteMember(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting member with ID {id}");
                return StatusCode(500, ErrorMessages.InternalError);
            }
        }

        [HttpGet("GetGenders")]
        public IActionResult GetGenders()
        {
            var enumValues = Enum.GetValues(typeof(Gender));
            var genderList = enumValues.Cast<Gender>()
                                      .Select(g => new { Key = g, Value = GetGenderDisplay(g) })
                                      .ToList();
            return Ok(genderList);
        }
        private string GetGenderDisplay(Gender gender)
        {
            return gender.ToString();
        }
    }
}
