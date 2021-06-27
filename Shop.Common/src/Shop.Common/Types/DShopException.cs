using System;

namespace Shop.Common.Types
{
    public class ShopException : Exception
    {
        public string Code { get; }

        public ShopException()
        {
        }

        public ShopException(string code)
        {
            Code = code;
        }

        public ShopException(string message, params object[] args) 
            : this(string.Empty, message, args)
        {
        }

        public ShopException(string code, string message, params object[] args) 
            : this(null, code, message, args)
        {
        }

        public ShopException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public ShopException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }        
    }
}