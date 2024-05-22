using E_Commerce.BL.Dtos;
using E_Commerce.DAL;
using E_Commerce.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.APIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public UsersController(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }


    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        var user = new User
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber,
            UserRole = registerDto.UserRole
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        // Add the user to the specified role
        if (!string.IsNullOrEmpty(registerDto.UserRole))
        {
            var roleResult = await _userManager.AddToRoleAsync(user, registerDto.UserRole);
            if (!roleResult.Succeeded)
            {
                return BadRequest(roleResult.Errors);
            }
        }

        var claims = new List<Claim>
    {
        new (ClaimTypes.NameIdentifier, user.Id.ToString()),
        new (ClaimTypes.Name, user.UserName),
        new (ClaimTypes.Email, user.Email),
        new (ClaimTypes.Role, user.UserRole) // Add role claim
    };
        await _userManager.AddClaimsAsync(user, claims);
        return Ok("Registered Successfully, Please login");
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<TokenDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return Unauthorized();
        }
        bool isAuthenticated = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!isAuthenticated)
        {
            return Unauthorized();
        }

        // Retrieve user claims and roles
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        // Create role claims from roles
        var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));

        // Combine user claims and role claims
        var allClaims = userClaims.Concat(roleClaims);

        // Retrieve secret key
        var keyFromConfig = _configuration.GetValue<string>("SecretKey")!;
        var keyInBytes = Encoding.ASCII.GetBytes(keyFromConfig);
        var key = new SymmetricSecurityKey(keyInBytes);

        // Create signing credentials
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        // Set token expiration
        var expiryDateTime = DateTime.Now.AddDays(2);

        // Generate JWT token
        var jwt = new JwtSecurityToken(
            claims: allClaims,
            signingCredentials: signingCredentials,
            expires: expiryDateTime
        );

        // Serialize JWT token to string
        var jwtAsString = new JwtSecurityTokenHandler().WriteToken(jwt);

        // Return token DTO
        return new TokenDto(jwtAsString, expiryDateTime);
    }


}
