using Domain.Base;

namespace Domain.Identity;

public class ApplicationRefreshToken: BaseRefreshToken
{
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
}