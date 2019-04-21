using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.Helpers.GrpcCore.Reflection.Internals
{

    /// <summary>
    /// Null implementation of <see cref="IRpcSerializerFactory"/>.
    /// </summary>
    internal sealed class NullSerializerFactory : IRpcSerializerFactory
    {

        internal static readonly NullSerializerFactory Value = new NullSerializerFactory();

        /// <summary>
        /// Gets the serializer for the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the data object.</typeparam>
        /// <returns>The serializer.</returns>
        Func<T, byte[]> IRpcSerializerFactory.GetSerializer<T>()
        {
            return null;
        }

        /// <summary>
        /// Gets the deserializer for the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the data object.</typeparam>
        /// <returns>The deserializer.</returns>
        Func<byte[], T> IRpcSerializerFactory.GetDeserializer<T>()
        {
            return null;
        }

    }

}
