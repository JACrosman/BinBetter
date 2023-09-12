namespace BinBetter.Api.Security
{
    public interface ICurrentUserAccessor
    {
        string? GetCurrentUsername();
        int GetCurrentUserId();
    }
}
