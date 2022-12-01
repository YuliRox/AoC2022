using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {

            var inputData = DataLoader.LoadInputData();

            foreach (var inputSet in inputData)
            {
                if (!inputSet.Content.Any())
                {
                    continue;
                }
                Console.WriteLine($"> Part-1 for {inputSet.Name}");

                var value = Part1(inputSet);
                Console.WriteLine("Value: {0}", value);

                Console.WriteLine($"< Part-1 for {inputSet.Name}");


                Console.WriteLine($"> Part-2 for {inputSet.Name}");

                var value2 = Part2(inputSet);
                Console.WriteLine("Value: {0}", value2);

                Console.WriteLine($"< Part-2 for {inputSet.Name}");
            }
        }

        private static int Part1(PuzzleInput<string> input)
        {
            return 0;
        }

        private static int Part2(PuzzleInput<string> input)
        {
            return 0;
        }
    }
}
