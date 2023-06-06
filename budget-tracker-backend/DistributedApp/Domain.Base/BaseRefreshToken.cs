using System.ComponentModel.DataAnnotations;

namespace Domain.Base;

public class BaseRefreshToken : BaseRefreshToken<Guid>
{
}


public class BaseRefreshToken<TKey> : DomainIdentityId<TKey> 
    where TKey : struct, IEquatable<TKey>
{
    [MaxLength(64)]
    public string RefreshToken { get; set; } = Guid.NewGuid().ToString();

    public DateTime ExpirationTime { get; set; } = DateTime.UtcNow.AddDays(7);
    
    [MaxLength(64)]
    public string? PreviousRefreshToken { get; set; } 
    // UTC
    public DateTime? PreviousExpirationTime { get; set; } = DateTime.UtcNow.AddDays(7);

}