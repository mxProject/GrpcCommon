using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Reflection
{

    /// <summary>
    /// Factory class of <see cref="Marshaller{T}"/>.
    /// </summary>
    public class RpcMarshallerFactory : IRpcMarshallerFactory
    {

        #region ctor

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="serializerFactory">The factory of the serializer.</param>
        public RpcMarshallerFactory(IRpcSerializerFactory serializerFactory)
        {
            SerializerFactory = serializerFactory ?? Internals.NullSerializerFactory.Value;
        }

        #endregion

        #region static fields

        /// <summary>
        /// Default instance of this type.
        /// </summary>
        public static readonly RpcMarshallerFactory DefaultInstance = new RpcMarshallerFactory(null);

        #endregion

        #region serializer

        /// <summary>
        /// Gets the factory of the serializer.
        /// </summary>
        public IRpcSerializerFactory SerializerFactory { get; }

        #endregion

        #region marshaller

        private readonly Dictionary<Type, object> m_TypeMarshallers = new Dictionary<Type, object>();

        /// <summary>
        /// Gets the marshaler for the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the data object.</typeparam>
        /// <returns>The marshaller.</returns>
        public Marshaller<T> GetMarshaller<T>()
        {

            if (m_TypeMarshallers.TryGetValue(typeof(T), out object obj))
            {
                return (Marshaller<T>)obj;
            }

            lock (this)
            {

                if (m_TypeMarshallers.TryGetValue(typeof(T), out obj))
                {
                    return (Marshaller<T>)obj;
                }

                if (TryCreateCustomMarshaller(out Marshaller<T> marshaller))
                {
                    m_TypeMarshallers.Add(typeof(T), marshaller);
                    return marshaller;
                }

                if (TryCreateDefaultMarshaller(out marshaller))
                {
                    m_TypeMarshallers.Add(typeof(T), marshaller);
                    return marshaller;
                }

            }

            return null;

        }

        /// <summary>
        /// Create the custom marshaller for the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the data object.</typeparam>
        /// <param name="marshaller">The marshaller.</param>
        /// <returns></returns>
        private bool TryCreateCustomMarshaller<T>(out Marshaller<T> marshaller)
        {

            Func<T, byte[]> serializer = SerializerFactory.GetSerializer<T>();
            Func<byte[], T> deserializer = SerializerFactory.GetDeserializer<T>();

            if (serializer == null || deserializer == null)
            {
                marshaller = null;
                return false;
            }

            marshaller = new Marshaller<T>(serializer, deserializer);
            return true;

        }

        /// <summary>
        /// Create the default marshaller for the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the data object.</typeparam>
        /// <param name="marshaller">The marshaller.</param>
        /// <returns></returns>
        private bool TryCreateDefaultMarshaller<T>(out Marshaller<T> marshaller)
        {

            Type t = typeof(Google.Protobuf.IMessage<>).MakeGenericType(new Type[] { typeof(T) });

            if (!t.IsAssignableFrom(typeof(T)) || !HasDefaultConstructor(typeof(T)))
            {
                marshaller = null;
                return false;
            }

            object obj = typeof(RpcMarshallerFactory).GetMethod("CreateDefaultMarshaller", BindingFlags.Static | BindingFlags.NonPublic)
                .MakeGenericMethod(new Type[] { typeof(T) })
                .Invoke(null, new object[] { });

            marshaller = (Marshaller<T>)obj;
            return true;

        }

        /// <summary>
        /// Create the default marshaller for the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the data object.</typeparam>
        /// <returns>The marshaller.</returns>
        private static Marshaller<T> CreateDefaultMarshaller<T>() where T : Google.Protobuf.IMessage<T>, new()
        {

            byte[] serialize(T arg)
            {
                if (arg == null) { return null; }
                return Google.Protobuf.MessageExtensions.ToByteArray(arg);
            }

            T deserialize(byte[] data)
            {
                if (data == null) { return default(T); }
                return new Google.Protobuf.MessageParser<T>(delegate () { return new T(); }).ParseFrom(data);
            }

            return Marshallers.Create(serialize, deserialize);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static bool HasDefaultConstructor(Type t)
        {

            ConstructorInfo ctor = t.GetConstructor(new Type[] { });

            return (t != null);

        }

        #endregion

    }

}
