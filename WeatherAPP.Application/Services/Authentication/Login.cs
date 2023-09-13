using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WeatherAPP.Data.Entities.Users;
using WeatherAPP.Infrastructure.Services;
using WeatherAPP.Infrastructure.Validation;
using WeatherAPP.Repository.Users;

namespace WeatherAPP.Application.Services.Authentication
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }

    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public UserViewModel? User { get; set; } = null;
        public class UserViewModel
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string Email { get; set; } = null!;

            public static UserViewModel GetUser(User user)
            {
                return new UserViewModel
                {
                    Id = user.Id,
                    FirstName= user.FirstName,
                    LastName= user.LastName,
                    Email= user.Email
                };
            }
        }
    }

    public class Login : ServiceBase<LoginRequest, LoginResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<User> passwordHasher;

        public Login(ServiceContext context, 
            IUserRepository userRepository, 
            IPasswordHasher<User> passwordHasher) : base(context)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }

        public class InputValidator : InputValidator<LoginRequest>
        {
            private readonly IUserRepository userRepository;
            public InputValidator(IUserRepository userRepository)
            {
                this.userRepository = userRepository;
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Email).Must(email =>
                {
                    var isExisting = Task.Run(() => CheckIfUserExists(email)).Result;
                    return isExisting;
                });
                RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            }
            private async Task<bool> CheckIfUserExists(string email)
            {
                return await userRepository.CheckIfUserExists(email);
            }
        }
        
        public override async Task<LoginResponse> Handle(LoginRequest input)
        {
            var user = await userRepository.GetUser(input.Email);
            var passwordsMatch = passwordHasher.VerifyHashedPassword(user, user.Password, input.Password);
            var userViewModel = LoginResponse.UserViewModel.GetUser(user);

            if (passwordsMatch == PasswordVerificationResult.Success)
            {
                return new LoginResponse {
                    Success = true,
                    Message = "",
                    User = userViewModel
                };
            }

            return new LoginResponse
            {
                Success = false,
                Message = "Invalid credentials."
            };
        }
    }
}
