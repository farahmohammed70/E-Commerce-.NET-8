namespace E_Commerce.BL.Dtos;

public class TokenDto
{
    public TokenDto(string jwtAsString, DateTime expiryDateTime)
    {
        JwtAsString = jwtAsString;
        ExpiryDateTime = expiryDateTime;
    }

    public string Token { get; set; } = string.Empty;
    public DateTime Expiry { get; set; }
    public string JwtAsString { get; }
    public DateTime ExpiryDateTime { get; }
}
