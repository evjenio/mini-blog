namespace MiniBlog.Core.Domain
{
    public interface IEntity<TIdentity> where TIdentity : struct
    {
        TIdentity Id { get; set; }
    }
}
