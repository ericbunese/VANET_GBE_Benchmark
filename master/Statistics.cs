﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master
{
    public class Statistics
    {
        public List<Timestamp> Timestamps;

        public Statistics()
        {
            Timestamps = new List<Timestamp>();
        }

        public Timestamp AddTimestamp(int count)
        {
            Timestamp ts = new Timestamp(count);
            Timestamps.Add(ts);
            return ts;
        }

        public Timestamp Dump()
        {
            Timestamp final = new Timestamp(0);
            int total = Timestamps.Count;

            foreach (var ts in Timestamps)
            {
                final.NumberOfConnectionMessages += ts.NumberOfConnectionMessages;
                final.NumberOfDisconnectionMessages += ts.NumberOfDisconnectionMessages;
                final.NumberOfDisconnections += ts.NumberOfDisconnections;
                final.NumberOfGroupMessages += ts.NumberOfGroupMessages;
                final.NumberOfNewConnections += ts.NumberOfNewConnections;
                final.NumberOfVehicles += ts.NumberOfVehicles;
            }

            Timestamps.Clear();

            final.NumberOfVehicles = (final.NumberOfVehicles / total);
            final.AverageNumberOfConnectionMessages = (final.NumberOfConnectionMessages / total);
            final.AverageNumberOfDisconnectionMessages = (final.NumberOfDisconnectionMessages / total);
            final.AverageNumberOfGroupMessages = (final.NumberOfGroupMessages / total);
            final.AverageNumberOfNewConnections = (final.NumberOfNewConnections / total);
            final.AverageNumberOfDisconnections = (final.NumberOfDisconnections / total);

            return final;
        }
    }
}
