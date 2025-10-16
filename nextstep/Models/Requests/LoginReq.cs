using FluentValidation;

namespace nextstep.Models.Requests
{
    public class LoginReqModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }


    public class LoginReqValidator : AbstractValidator<LoginReqModel>
    {
        public LoginReqValidator()
        {
            RuleFor(x => x.Email)
              .NotEmpty().WithMessage("Email is required")
              .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
        }
    }
}