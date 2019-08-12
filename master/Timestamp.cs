using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master
{
    public class Timestamp
    {
        public int Count;
        public int NumberOfVehicles;
        public int NumberOfNewConnections = 0;
        public int NumberOfDisconnections = 0;
        public int NumberOfConnectionMessages = 0;
        public int NumberOfDisconnectionMessages = 0;
        public int NumberOfGroupMessages = 0;

        public double AverageNumberOfNewConnections;
        public double AverageNumberOfDisconnections;
        public double AverageNumberOfConnectionMessages;
        public double AverageNumberOfDisconnectionMessages;
        public double AverageNumberOfGroupMessages;

        public double MessagesSent = 0;
        public double JoinMessagesSent = 0;
        public double KickMessagesSent = 0;

        public Timestamp(int count)
        {
            Count = count;
        }
    }
}
