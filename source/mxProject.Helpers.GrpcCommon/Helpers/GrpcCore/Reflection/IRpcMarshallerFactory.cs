using System;
using System.Collections.Generic;
using System.Text;

using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Reflection
{

    /// <summary>
    /// Defines methods that gets or creates <see cref="Marshaller{T}"/>.
    /// </summary>
    public interface IRpcMarshallerFactory
    {

        /// <summary>
        /// Gets the marshaller for the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the data object.</typeparam>
        /// <returns>The marshaller.</returns>
        Marshaller<T> GetMarshaller<T>();

    }

}
