using Microsoft.AspNetCore.Identity;

namespace Dima.Api.Models;

public class User:IdentityUser<long>
{
    public List<IdentityUserRole<long>>? Roles { get; set; }
}
