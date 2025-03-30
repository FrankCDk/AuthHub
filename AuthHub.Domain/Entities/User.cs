namespace AuthHub.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty; // Unique value to strengthen the password
        public string Status { get; set; } = string.Empty;
        public string FailedAttempts { get; set; } = string.Empty;
        public DateTime LockedUntil { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        AD = 1, //Administrador
        ST = 2, //Estuadiante
        TE = 3  //Profesor
    }

}
