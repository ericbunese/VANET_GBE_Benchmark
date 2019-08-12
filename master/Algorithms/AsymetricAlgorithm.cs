using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Algorithms
{
    public class AsymmetricAlgorithm : IAlgorithm
    {
        public int AddVehicleToGroupView(Vehicle vehicle, Connection connection)
        {
            if (!connection.Setup)
            {
                // Must exchange keys between source and destination (done based upon diffie-hellman)
                // Source picks a secret number A and sends G^A mod P.
                Report.SendTo(vehicle, connection.Vehicle, vehicle.Id, EMessageTypes.Join);
                // Destination picks a secret number B and sends it back as G^B mod P.
                Report.SendTo(connection.Vehicle, vehicle, connection.Vehicle.Id, EMessageTypes.Join);

                vehicle.SetupConnection(connection.Vehicle);
                connection.Vehicle.SetupConnection(vehicle);
            }
            return vehicle.Connections.Count;
        }

        public int ProcessGroup(Vehicle vehicle)
        {
            // Sends a message to each neighbor.
            foreach (var conn in vehicle.Connections)
            {
                Report.SendTo(vehicle, conn.Vehicle, vehicle.Id, EMessageTypes.Default);
            }

            return vehicle.Connections.Count;
        }

        public int RemoveVehicleFromGroupView(Vehicle vehicle, Connection connection)
        {
            // Sends a message to each neighbor.
            foreach (var conn in vehicle.Connections)
            {
                Report.SendTo(vehicle, conn.Vehicle, vehicle.Id, EMessageTypes.Kick);
            }

            return vehicle.Connections.Count;
        }
    }
}
