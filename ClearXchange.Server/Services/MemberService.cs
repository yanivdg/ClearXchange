using ClearXchange.Server.Data;
using ClearXchange.Server.Interfaces;
using ClearXchange.Server.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClearXchange.Server.Services
{
    public class MemberService : IMemberService
    {
        private readonly IRepository<Member> _memberRepository;
        private readonly IValidationService _validationService;
        //private readonly ILogger<MemberService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MemberService(IRepository<Member> userRepository, 
                             IValidationService validationService, 
                             IHttpContextAccessor httpContextAccessor)//,ILogger<MemberService> logger)
        {
            _memberRepository = userRepository;
            _validationService = validationService;
            //_logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IEnumerable<Member>> GetAllMembers()
        {
            var authorizedRoles = new[] { "Administrator", "Manager" }; // Define allowed roles
            var userRoles = true;//_httpContextAccessor.HttpContext?.User.IsInRole(authorizedRoles); // Check for role membership

            if (!userRoles)
            {
                throw new UnauthorizedAccessException("Insufficient permissions to access all members");
            }
            return await _memberRepository.GetAll();
        }

        public async Task<Member> GetMemberById(string id)
        {
            _validationService.ValidateID(id);
            try
            {
                return await _memberRepository.GetById(id);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error retrieving member by ID: {Id}", id);
                throw;
            }

        }

        public async Task<IEnumerable<Member>> Search(Expression<Func<Member, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return await _memberRepository.Search(predicate);
        }
        public async Task AddMember(Member member)
        {
            _validationService.ValidateMember(member);
            try
            {
                await _memberRepository.Add(member);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error adding member: {Name}", member.Name);
                throw;
            }
        }

        public async Task UpdateMember(string id,Member member)
        {
            var val = GetMemberById(id);
            if (val == null)
            _validationService.ValidateID(id);
            try
            {
                if (val != null)
                {
                    await _memberRepository.Update(member);
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error updating member: {id}", member.Id);
                throw;
            }

        }

        public async Task DeleteMember(string id)
        {
            try
            {
                // Validate the id (if needed)
                var val = await _memberRepository.GetById(id);

                if (val != null)
                {
                    // Perform the deletion
                    await _memberRepository.Delete(id);
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error deleting member: {id}", id);
                throw;
            }
        }

    }
}
