namespace ASI.Basecode.Services.Interfaces
{
    /// <summary>
    /// Stand-in identity provider until web login/auth is reintroduced.
    /// Returns the current operator (admin or custodian) acting on the data.
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// User id of the operator, or null when nobody is selected yet.
        /// </summary>
        long? GetActorUserId();

        /// <summary>
        /// Human readable name of the operator for audit details.
        /// </summary>
        string GetActorName();
    }
}
