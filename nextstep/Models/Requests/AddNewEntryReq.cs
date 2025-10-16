// This represents what your frontend sends to your backend endpoint (a POST request).
using Microsoft.AspNetCore.Http;

namespace nextstep.Models.Requests
{
    public class AddNewEntry
    {
        public required string Title { get; set; }
        public int UserId { get; set; }
        public required string Mood { get; set; }
        public required string Content { get; set; }
        public required string Excerpt { get; set; }
        public required string ImageUrl { get; set; }
    }
}