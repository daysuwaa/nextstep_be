
namespace nextstep.domain.Entities
{
    public class Token
    {
        public required string token { get; set; }
        public DateTime tokenExpiry { get; set; }
        public Guid UserId { get; set; }
    }
}