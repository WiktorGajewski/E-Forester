using System.Text.Json.Serialization;

namespace E_Forester.Application.DataTransferObjects.Account
{
    public class TokenDto
    {
        public string AccessToken { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
