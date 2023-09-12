namespace BinBetter.Api.Features.Goals
{
    public class GoalModel
    {
        public int GoalId { get; set; }

        public int BinId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Quantity { get; set; }

        public int Frequency { get; set; }

        public bool IsInBin { get; set; }
    }

    public record GoalModelEnvelope(GoalModel Goal);
}
