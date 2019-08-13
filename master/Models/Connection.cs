using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master
{
    public class Connection
    {
        // Determines if it has been touched in the current timestamp.
        public bool Touch = false;
        // Determines if it's a new connection (added in current timestamp)
        public bool IsNew = false;
        // Determines if this connection has been setup (used in Symmetric algorithm)
        public bool Setup = false;
        // The neighbor
        public Vehicle Vehicle;
        // Trust indicator
        public double Trust;

        public Connection()
        {
            Random random = new Random();
            Trust = random.NextDouble();
        }
    }
}
