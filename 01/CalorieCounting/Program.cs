using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace CalorieCounting;

class Program
{
    static void Main(string[] args)
    {

        var inputData = DataLoader.LoadInputData<SnackStash>();

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

    public readonly record struct SnackStash {
        public SnackStash(int[] snacks, int totalCalories)
        {
            Snacks = snacks;
            TotalCalories = Snacks.Sum();
        }

        public readonly int[] Snacks { get; init; }
        public int TotalCalories { get; init; }
    }
}
