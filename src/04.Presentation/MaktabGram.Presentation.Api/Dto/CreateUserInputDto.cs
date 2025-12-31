namespace MaktabGram.Presentation.Api.Dto
{
    public class CreateUserInputDto
    {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Mobile { get; set; }
            public string? Username { get; set; }
            public string Password { get; set; }
            public int Otp { get; set; }
    }
}