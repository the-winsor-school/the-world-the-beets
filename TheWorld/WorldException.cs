using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    /// <summary>
    /// A generic game Exception within TheWorld.
    /// </summary>
    public class WorldException : Exception
    {
        /// <summary>
        /// What object had this exception
        /// </summary>
        public object Sender { get; protected set; }


        public WorldException(string message, object sender = null, Exception innerException = null)
            : base(message, innerException)
        {
            Sender = sender;
        }
    }
}
