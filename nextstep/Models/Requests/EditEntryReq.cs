namespace nextstep.Models.Requests
{
    public class EditEntryReq
	{
        public  string? Title { get; set; }
        public int? UserId { get; set; }
        public  string? Mood { get; set; }
        public  string? Content { get; set; }
        public  string? Excerpt { get; set; }
        public  string? ImageUrl { get; set; }
    }
}

