// they represent what the frontend sends or receives 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using nextstep.application.DTOs.Responses;

namespace nextstep.application.DTOs.Responses
{
    [Table("entries")] // maps to your PostgreSQL "entries" table
    public class Entry
    {
        public int Id { get; set; }

        public required string Title { get; set; }
        public required string Mood { get; set; }
        public required string Content { get; set; }
        public required string Excerpt { get; set; }
        public required string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}

