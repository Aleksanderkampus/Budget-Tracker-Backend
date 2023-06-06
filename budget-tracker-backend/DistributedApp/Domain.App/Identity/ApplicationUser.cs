using Domain.Contracts.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity;

public class ApplicationUser : IdentityUser<Guid>, IDomainIdentityId
{
    public ICollection<Account>? Accounts { get; set; }

    public ICollection<ApplicationRefreshToken>? RefreshTokens { get; set; }
}