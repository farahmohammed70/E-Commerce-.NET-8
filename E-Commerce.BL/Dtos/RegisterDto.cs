using E_Commerce.DAL.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.BL.Dtos;

public class RegisterDto
{
    [Required]
    public string UserName { get; set; } = string.Empty;


    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;


    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;


    [Required]
    public string Password { get; set; } = string.Empty;

    public Gender Gender { get; set; }

    public string UserRole { get; set; }
}
