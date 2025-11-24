public class AddReviewRequest
{
    public int Rating { get; set; } // 1-5
    public string? Description { get; set; }
    public string? Comments { get; set; }
    public List<string>? Aspects { get; set; }
}
