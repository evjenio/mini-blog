using System;
using Mapster;

namespace MiniBlog.Core.Mappers
{
    /// <summary>
    /// Dto mapper.
    /// </summary>
    public class ObjectMapper : IObjectMapper
    {
        /// <summary>
        /// Map from TSource to TDestination.
        /// </summary>
        /// <typeparam name="TDestination">Destination type</typeparam>
        /// <param name="source">Source object</param>
        /// <returns>Destination object</returns>
        public TDestination Map<TDestination>(object source)
        {
            return source.Adapt<TDestination>();
        }
    }
}
