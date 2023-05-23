using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Exceptions
{
    public class UserNameOrPasswordFailedException : Exception
    {
        public UserNameOrPasswordFailedException() : base("Kullanıcı Adı veya Şifre Hatalı!!")
        {
        }

        public UserNameOrPasswordFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserNameOrPasswordFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
