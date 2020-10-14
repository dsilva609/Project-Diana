namespace Project.Diana.WebApi.Features.User.Login
{
    public class LoginResponse
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public int UserNum { get; set; }
    }
}