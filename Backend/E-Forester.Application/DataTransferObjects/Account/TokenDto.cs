using E_Forester.Model.Enums;
using System.Text.Json.Serialization;

namespace E_Forester.Application.DataTransferObjects.Account
{
    public class TokenDto
    {
        public string AccessToken { get; set; }

        public UserRole UserRole { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
