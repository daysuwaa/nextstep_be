using FluentValidation;

namespace nextstep.Models.Requests
{
    public class RegisterReqModel
	{
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
	}

    public class RegisterValidator: AbstractValidator<RegisterReqModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required");

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required")
               .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
        }
    }
}


