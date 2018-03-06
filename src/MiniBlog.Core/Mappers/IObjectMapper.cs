namespace MiniBlog.Core.Mappers
{
    /// <summary>
    /// DTO mapper
    /// </summary>
    public interface IObjectMapper
    {
        /// <summary>
        /// Map from TSource to TDestination
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TDestination">Destination type</typeparam>
        /// <param name="source">Source object</param>
        /// <returns>Destination object</returns>
        TDestination Map<TSource, TDestination>(TSource source);
    }
}