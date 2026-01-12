namespace todo_api.Models.Dtos
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public UserDTO user { get; set; }
    }
}
