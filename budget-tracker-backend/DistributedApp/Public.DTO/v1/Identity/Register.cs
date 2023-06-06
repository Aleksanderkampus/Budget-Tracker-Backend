using System.ComponentModel.DataAnnotations;

namespace Public.DTO.v1;

public class Register
{
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Email incorrect length")]
    public string Email { get; set; } = default!;
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Password incorrect length")]
    public string Password { get; set; } = default!;
}