using nextstep.domain.Entities;

namespace nextstep.application.Abstractions.Core
{
	public interface IAuthHandler
	{
        // Instance Method ; public access modifier, Token is the Return Type, Login is the mthod name and stringa nd password are the parameter
        // Any class that implements IAuthHandler must have a method called Login that takes an email and password and returns a Token.
        public Token Login(string email, string password);
	}
}