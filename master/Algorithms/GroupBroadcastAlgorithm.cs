using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Algorithms
{
    public class GroupBroadcastAlgorithm : IAlgorithm
    {
        public int AddVehicleToGroupView(Vehicle vehicle, Connection connection)
        {
            return vehicle.Connections.Count;
        }

        public int ProcessGroup(Vehicle vehicle)
        {
            return 1;
        }

        public int RemoveVehicleFromGroupView(Vehicle vehicle, Connection connection)
        {
            return 1;
        }
    }
}
