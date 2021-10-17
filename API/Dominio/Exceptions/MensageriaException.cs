using System;

namespace API.Dominio.Exceptions
{
    [Serializable]
    public class MensageriaException : Exception
    {
        public MensageriaException() : base() { }
        public MensageriaException(string message) : base(message) { }
        public MensageriaException(string message, Exception inner) : base(message, inner) { }

        protected MensageriaException(System.Runtime.Serialization.SerializationInfo info,
                                            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
