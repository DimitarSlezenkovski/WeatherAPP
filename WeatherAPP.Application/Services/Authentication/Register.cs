using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPP.Data.Entities.Users;
using WeatherAPP.Infrastructure.Services;
using WeatherAPP.Infrastructure.Validation;
using WeatherAPP.Repository.Users;

namespace WeatherAPP.Application.Services.Authentication
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class RegisterResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
    }

    public class Register : ServiceBase<RegisterRequest, RegisterResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<User> passwordHasher;

        public Register(ServiceContext context, 
            IUserRepository userRepository, 
            IPasswordHasher<User> passwordHasher) : base(context)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }

        public class InputValidator : InputValidator<RegisterRequest>
        {
            private readonly IUserRepository userRepository;
            public InputValidator(IUserRepository userRepository)
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Email).Must(email => CheckIfUserExists(email));
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
                this.userRepository = userRepository;
            }
            private async Task<bool> CheckIfUserExistsAsync(string email)
            {
                var isExisting = await userRepository.CheckIfUserExists(email);
                
                if (!isExisting) return true;
                
                return false;
            }

            private bool CheckIfUserExists(string email)
            {
                return Task.Run(() => CheckIfUserExistsAsync(email)).Result;
            }
        }

        public override async Task<RegisterResponse> Handle(RegisterRequest input)
        {
            var user = new User();

            var response = new RegisterResponse
            {
                Success = true
            };

            user.Email = input.Email;
            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.Password = passwordHasher.HashPassword(user, input.Password);

            await userRepository.Insert(user);
            await userRepository.SaveChangesAsync();

            return response;
        }
    }
}
