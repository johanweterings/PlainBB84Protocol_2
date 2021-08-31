using System;
using System.Threading.Tasks;
using Microsoft.Quantum.Simulation.Simulators;
using PlainBB84Protocol;

namespace Host
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var aliceBits = "";
            var aliceBases = "";
            var bobBits = "";
            var bobBases = "";
            var sentPhoton = "";
            var comparedBases = "";
            var sharedKey = "";
            
            int numberOfBits = 50;
            if (args.Length > 1)
            {
                int.TryParse(args[0], out numberOfBits);
            };

            var sim = new QuantumSimulator(throwOnReleasingQubitsNotInZeroState: true);
            var result = await RunBB84Protocol.Run(sim, numberOfBits);
            bool match;

            foreach (var set in result)
            {
                aliceBases += set.Item1 ? "X " : "Z ";
                aliceBits += set.Item2 ? "1 " : "0 ";
                bobBases += set.Item3 ? "X " : "Z ";
                bobBits += set.Item4 ? "1 " : "0 ";
                sentPhoton += set.Item1 ? (set.Item2 ? "- " : "+ ") : (set.Item2 ? "1 " : "0 ");

                match = set.Item1 == set.Item3;
                comparedBases += match ? (set.Item1 ? "X " : "Z ") : "  ";
                sharedKey += match ? (set.Item2 ? "1 " : "0 ") : string.Empty;
            }

            Console.WriteLine("Quantum Transmission\n");
            Console.WriteLine(aliceBases);
            Console.WriteLine(aliceBits);
            Console.WriteLine(sentPhoton);
            Console.WriteLine(bobBases);
            Console.WriteLine(bobBits);

            Console.WriteLine("\nPublic discussion\n");
            Console.WriteLine(comparedBases);
            Console.WriteLine($"Resulting shared key: {sharedKey}");


            Console.ReadLine();
        }
    }
}
