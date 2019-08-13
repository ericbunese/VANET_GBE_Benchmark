﻿using System;
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
            if (!connection.Setup)
            {
                // Destination node must share certificate to source node in order to be added to the group.
                // It then gets the symmetric key.
                Report.SendTo(connection.Vehicle, vehicle, connection.Vehicle.Id, EMessageTypes.Join);
                vehicle.SetupConnection(connection.Vehicle);
                connection.Vehicle.SetupConnection(vehicle);
            }

            return vehicle.Connections.Count;
        }

        public int ProcessGroup(Vehicle vehicle)
        {
            // Just sends the message to everyone.
            return 1;
        }

        public int RemoveVehicleFromGroupView(Vehicle vehicle, Connection connection)
        {
            // Notifies everyone that it's disconnecting.
            return 1;
        }
    }
}
