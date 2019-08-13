using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master
{
    public class Report
    {
        public static List<Vehicle> Vehicles;
        public static Statistics Statistics;
        public static Timestamp Timestamp;
        public static double EvilChance = 0.05;

        public static string[] GetFiles(string directory)
        {
            return Directory.GetFiles(directory, "*.txt");
        }

        public static Timestamp ProcessFile(string filename, IAlgorithm algorithm)
        {
            Vehicles = new List<Vehicle>();
            Statistics = new Statistics();

            using (StreamReader file = new StreamReader(filename))
            {
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    if (ln.Contains("["))
                    {
                        int value = GetTimestamp(ln);
                        Timestamp = Statistics.AddTimestamp(value);

                        foreach (var v in Vehicles)
                        {
                            v.BeginTimestamp(Timestamp);
                        }
                        foreach (var v in Vehicles)
                        {
                            v.StepTimestamp(Timestamp);
                        }
                        foreach (var v in Vehicles)
                        {
                            v.EndTimestamp(Timestamp);
                        }
                    }
                    else
                    {
                        var parse = ln.Split(' ').ToList();
                        string id1 = parse[0], id2 = parse[1];
                        Vehicle v1 = null, v2 = null;

                        foreach (var v in Vehicles)
                        {
                            if (v.Id == id1)
                            {
                                v1 = v;
                                continue;
                            }
                            if (v.Id == id2)
                            {
                                v2 = v;
                                continue;
                            }
                        }

                        if (v1 == null)
                        {
                            v1 = new Vehicle
                            {
                                Id = id1,
                                Algorithm = algorithm
                            };
                            Vehicles.Add(v1);
                        }

                        if (v2 == null)
                        {
                            v2 = new Vehicle
                            {
                                Id = id2,
                                Algorithm = algorithm
                            };
                            Vehicles.Add(v2);
                        }

                        v1.AddNeighbor(v2);
                        v2.AddNeighbor(v1);
                    }
                }

                file.Close();
                return Statistics.Dump(algorithm.GetType().ToString(), filename);
            }
        }

        private static int GetTimestamp(string line)
        {
            return int.Parse(line.Replace("[", "").Replace("]", ""));
        }

        public static void SendTo(Vehicle origin, Vehicle destination, string message, EMessageTypes type)
        {
            if (type == EMessageTypes.Default)
                Timestamp.MessagesSent++;
            else if (type == EMessageTypes.Join)
                Timestamp.JoinMessagesSent++;
            else if (type == EMessageTypes.Kick)
                Timestamp.KickMessagesSent++;

            var str = origin.ReceiveFrom(destination, destination.ReceiveFrom(origin, message));
        }
    }
}
