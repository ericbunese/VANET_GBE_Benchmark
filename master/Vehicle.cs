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

        public Vehicle()
        {
            Connections = new List<Connection>();
        }

        public void AddNeighbor(Vehicle vehicle)
        {
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
            var index = this.Connections.FindIndex(c => c.Vehicle == vehicle);
            if (index >= 0)
            {
                this.Connections.RemoveAt(index);
            }
        }

        public void BeginTimestamp(Timestamp ts)
        {
            for (int i = 0; i<Connections.Count;++i)
            {
                var conn = Connections[i];

                if (!conn.Touch)
                {
                    ts.NumberOfDisconnections += 1;
                    ts.NumberOfDisconnectionMessages += Algorithm.RemoveVehicleFromGroupView(this, conn);
                    Connections.RemoveAt(i--);
                }
                else if (conn.IsNew)
                {
                    ts.NumberOfNewConnections += 1;
                    ts.NumberOfConnectionMessages += Algorithm.AddVehicleToGroupView(this, conn);
                    conn.IsNew = false;
                }
            }
        }

        public void StepTimestamp(Timestamp ts)
        {
            ts.NumberOfVehicles++;
            ts.NumberOfGroupMessages += Algorithm.ProcessGroup(this);
        }

        public void EndTimestamp(Timestamp ts)
        {
            foreach (var conn in Connections)
            {
                conn.Touch = false;
            }
        }
    }
}