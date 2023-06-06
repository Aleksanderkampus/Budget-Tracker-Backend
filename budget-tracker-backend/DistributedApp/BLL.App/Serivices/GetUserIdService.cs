using System.Security.Claims;
using BLL.Contracts.App;

namespace BLL.App.Serivices;

public class GetUserIdService : IGetUserIdService
{
    public Guid GetUserId()
    {
        return Guid.Parse(ClaimsPrincipal.Current!.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}