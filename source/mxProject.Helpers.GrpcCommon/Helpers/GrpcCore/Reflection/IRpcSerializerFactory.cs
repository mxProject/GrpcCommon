using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.Helpers.GrpcCore.Reflection
{

    /// <summary>
    /// Defines methods that gets or creates the serializer.
    /// </summary>
    public interface IRpcSerializerFactory
    {

        /// <summary>
        /// Gets the serializer for the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the data object.</typeparam>
        /// <returns>The serializer.</returns>
        Func<T, byte[]> GetSerializer<T>();

        /// <summary>
        /// Gets the deserializer for the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the data object.</typeparam>
        /// <returns>The deserializer.</returns>
        Func<byte[], T> GetDeserializer<T>();

    }

}
