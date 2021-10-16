using System;

namespace API.Dominio.Exceptions
{
    [Serializable]
    public class CriptografiaException : Exception
    {
        public CriptografiaException() : base() { }
        public CriptografiaException(string message) : base(message) { }
        public CriptografiaException(string message, Exception inner) : base(message, inner) { }

        protected CriptografiaException(System.Runtime.Serialization.SerializationInfo info,
                                            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
