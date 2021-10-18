using System;

namespace KafkaConsumer.Dominio.Exceptions
{
    [Serializable]
    public class PedidoException : Exception
    {
        public PedidoException() : base() { }
        public PedidoException(string message) : base(message) { }
        public PedidoException(string message, Exception inner) : base(message, inner) { }

        protected PedidoException(System.Runtime.Serialization.SerializationInfo info,
                                            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
