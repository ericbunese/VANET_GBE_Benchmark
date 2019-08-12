using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Algorithms
{
    public class SymmetricAlgorithm : IAlgorithm
    {
        public int AddVehicleToGroupView(Vehicle vehicle, Connection connection)
        {
            int total = 0;
            var nonSetupConnections = vehicle.Connections.Where(c => !c.Setup).ToList();
            foreach (var conn in nonSetupConnections)
            {
                total += 6;
                connection.Setup = true;
            }
            return nonSetupConnections.Count + total;
        }

        public int ProcessGroup(Vehicle vehicle)
        {
            return 1;
        }

        public int RemoveVehicleFromGroupView(Vehicle vehicle, Connection connection)
        {
            return vehicle.Connections.Count;
        }
    }
}
