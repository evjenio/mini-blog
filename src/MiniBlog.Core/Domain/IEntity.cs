namespace MiniBlog.Core.Domain
{
    /// <summary>
    /// Entity interface
    /// </summary>
    /// <typeparam name="TIdentity">Identity type</typeparam>
    public interface IEntity<TIdentity> where TIdentity : struct
    {
        /// <summary>
        /// Id
        /// </summary>
        TIdentity Id { get; set; }
    }
}
