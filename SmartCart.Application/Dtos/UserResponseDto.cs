namespace SmartCart.Application.Dtos
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
