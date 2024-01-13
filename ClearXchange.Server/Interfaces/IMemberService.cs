using ClearXchange.Server.Model;
using System.Linq.Expressions;

namespace ClearXchange.Server.Interfaces
{
    public interface IMemberService
    {
        Task<Member> GetMemberById(string id);
        Task<IEnumerable<Member>> Search(Expression<Func<Member, bool>> predicate);
        Task<IEnumerable<Member>> GetAllMembers();
        Task AddMember(Member member);
        Task UpdateMember(string id,Member member);
        Task DeleteMember(string id);
    }
}
