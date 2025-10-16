using System.ComponentModel.DataAnnotations.Schema;

namespace nextstep.domain.Entities
{
    [Table("users")] // maps to your PostgreSQL "users" table
    public class UserEntities
    {
        public int Id { get; set; }  // SERIAL PRIMARY KEY
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}