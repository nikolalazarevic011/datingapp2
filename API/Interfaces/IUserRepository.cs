using API.Entities;

namespace API;

public interface IUserRepository
{
    //methods that we want our user repository to implement
    void Update(AppUser user);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<AppUser>> GetUserAsync();
    Task<AppUser> GetUserByIdAsync(int id);
    Task<AppUser> GetUserByUsernameAsync(string username);
    Task<IEnumerable<MemberDto>> GetMembersAsync();

    Task<MemberDto> GetMemberAsync(string username);

}
