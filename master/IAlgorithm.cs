using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master
{
    public interface IAlgorithm
    {
        // Returns the number of messages sent to add a new group connection.
        int AddVehicleToGroupView(Vehicle vehicle, Connection connection);

        // Returns the number of messages sent to remove a group connection.
        int RemoveVehicleFromGroupView(Vehicle vehicle, Connection connection);

        // Sends messages: returns the number of messages sent to communicate in the group.
        int ProcessGroup(Vehicle vehicle);
    }
}
