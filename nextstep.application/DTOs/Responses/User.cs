using System.ComponentModel.DataAnnotations.Schema;

namespace nextstep.application.DTOs.Responses
{
    [Table("users")] // maps to your PostgreSQL "users" table
    public class User
    {
        public int id { get; set; }  // SERIAL PRIMARY KEY
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        [Column("created_at")]
        public DateTime createdAt { get; set; }

        [Column("updated_at")]
        public DateTime updatedAt { get; set; }
    }
}