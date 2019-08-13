using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master
{
    public class Vehicle
    {
        public string Id;
        public List<Connection> Connections;
        public IAlgorithm Algorithm;
        public bool isEvil = false;

        public Vehicle()
        {
            Connections = new List<Connection>();
            Random random = new Random();
            isEvil = (random.NextDouble() <= Report.EvilChance);
        }

        public void AddNeighbor(Vehicle vehicle)
        {
            // Create a new neighbor reference.
            var index = this.Connections.FindIndex(c => c.Vehicle == vehicle);
            if (index < 0)
            {
                this.Connections.Add(new Connection
                {
                    Vehicle = vehicle,
                    Touch = true,
                    IsNew = true
                });
            }
            else
                this.Connections[index].Touch = true;
        }

        public void RemoveNeighbor(Vehicle vehicle)
        {
            // Remove a neighbor reference.
            var index = this.Connections.FindIndex(c => c.Vehicle == vehicle);
            if (index >= 0)
            {
                this.Connections.RemoveAt(index);
            }
        }

        public void BeginTimestamp(Timestamp ts)
        {
            // For each neighboring node (connected)
            for (int i = 0; i<Connections.Count;++i)
            {
                var conn = Connections[i];

                if (!conn.Touch)
                {
                    // Disconnect if it's no longer in range.
                    ts.NumberOfDisconnections += 1;
                    ts.NumberOfDisconnectionMessages += Algorithm.RemoveVehicleFromGroupView(this, conn);
                    Connections.RemoveAt(i--);
                }
                else if (conn.IsNew)
                {
                    // It's a new connection. Add it to the group.
                    ts.NumberOfNewConnections += 1;
                    ts.NumberOfConnectionMessages += Algorithm.AddVehicleToGroupView(this, conn);
                    conn.IsNew = false;
                }
            }
        }

        public void StepTimestamp(Timestamp ts)
        {
            // Process the group as all connected nodes.
            ts.NumberOfVehicles++;
            ts.NumberOfGroupMessages += Algorithm.ProcessGroup(this);
        }

        public void EndTimestamp(Timestamp ts)
        {
            // Mark all connections as untouched for next timestamp.
            foreach (var conn in Connections)
            {
                conn.Touch = false;
            }
        }

        public string ReceiveFrom(Vehicle vehicle, string message)
        {
            Connection conn = Connections.Where(c => c.Vehicle.Id == vehicle.Id).FirstOrDefault();
            if (conn != null && conn.Trust > 0.5)
            {
                return $"{Id} <= {message}";
            }
            else
            {
                return null;
            }
        }

        public void SetupConnection(Vehicle vehicle)
        {
            Connection conn = Connections.Where(c => c.Vehicle.Id == vehicle.Id).FirstOrDefault();
            if (conn != null)
            {
                conn.Setup = true;
            }
        }
    }
}