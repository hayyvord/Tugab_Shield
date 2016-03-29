using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tugab
{
    public class MessageString : EventArgs
    {
        public string Message { get; private set; }

        public MessageString(string message)
        {
            this.Message = message;
        }
    }
}
