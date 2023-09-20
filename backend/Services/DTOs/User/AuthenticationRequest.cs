using System.ComponentModel.DataAnnotations;

namespace Services.DTOs;

public class AuthenticationRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get ; set; }
}