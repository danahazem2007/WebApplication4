using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication4.Model;

namespace WebApplication4.Controllers
{
    public class Controller:ControllerBase
    {
        private string GenerateJwtToken(Subject subject)
        {
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

        }
    }
}
