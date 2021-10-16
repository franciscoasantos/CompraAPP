using System;

namespace API.Dominio.Exceptions
{
    [Serializable]
    public class CadastroException : Exception
    {
        public CadastroException() : base() { }
        public CadastroException(string message) : base(message) { }
        public CadastroException(string message, Exception inner) : base(message, inner) { }

        protected CadastroException(System.Runtime.Serialization.SerializationInfo info,
                                            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
