public class CreateReviewRequest
{
    public int Rating { get; set; }
    public string? Description { get; set; }

    public string EntityType { get; set; } // store, restaurant, product
    public List<AspectScoreRequest> Aspects { get; set; }
}

public class AspectScoreRequest
{
    public string AspectId { get; set; }
    public int Score { get; set; } // 1-5
}
