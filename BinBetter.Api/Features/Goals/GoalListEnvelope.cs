using BinBetter.Api.Data.Domain;

namespace BinBetter.Api.Features.Goals
{
    public class GoalListEnvelope
    {
        public List<Goal> Goals { get; set; } = new();

        public int GoalsCount { get; set; }
    }
}
