using BLL.Contracts.App;

namespace Tests.Services;

public class GetUserIdService : IGetUserIdService
{
    public Guid GetUserId()
    {
        return Guid.Parse("65774695-bd99-4096-a0fb-7494da17b322");
    }
}