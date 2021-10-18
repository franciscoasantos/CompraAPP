using System;

namespace KafkaConsumer.Dominio.Exceptions
{
    [Serializable]
    public class PagamentoException : Exception
    {
        public PagamentoException() : base() { }
        public PagamentoException(string message) : base(message) { }
        public PagamentoException(string message, Exception inner) : base(message, inner) { }

        protected PagamentoException(System.Runtime.Serialization.SerializationInfo info,
                                            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
