using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using mxProject.Helpers.GrpcCore.Reflection;

namespace Test.mxProject.Helpers.Common
{
    public class MessagePackMarshaller : IRpcMarshallerFactory
    {

        public static readonly MessagePackMarshaller Current = new MessagePackMarshaller();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Marshaller<T> GetMarshaller<T>()
        {
            if (Marshallers.TryGetValue(typeof(T), out object obj))
            {
                return (Marshaller<T>)obj;
            }

            lock (Marshallers)
            {
                if (Marshallers.TryGetValue(typeof(T), out obj))
                {
                    return (Marshaller<T>)obj;
                }

                Marshaller<T> marshaller = CreateMarshaller<T>();
                Marshallers.Add(typeof(T), marshaller);
                return marshaller;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private Marshaller<T> CreateMarshaller<T>()
        {
            return new Marshaller<T>(
                obj => MessagePack.MessagePackSerializer.Serialize<T>(obj)
                , bytes => MessagePack.MessagePackSerializer.Deserialize<T>(bytes)
                );
        }

        private readonly Dictionary<Type, object> Marshallers = new Dictionary<Type, object>();

    }

}
