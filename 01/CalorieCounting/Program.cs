using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace CalorieCounting;

class Program
{
    static void Main(string[] args)
    {
        var inputData = DataLoader.LoadAllRawInputData<SnackStash>(content => content
            .Split("\r\n\r\n")
            .Select(group => group
                .Split("\r\n")
                .Select(int.Parse)
                .ToArray()
            )
            .Select(group => new SnackStash(group))
            );

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

    private static int Part1(PuzzleInput<SnackStash> input)
    {
        return input.Content.Max(x => x.TotalCalories);
    }

    private static int Part2(PuzzleInput<SnackStash> input)
    {
        return input.Content.OrderByDescending(stash => stash.TotalCalories).Take(3).Sum(stash => stash.TotalCalories);
    }

    public readonly record struct SnackStash
    {
        public SnackStash(int[] snacks)
        {
            Snacks = snacks;
            TotalCalories = Snacks.Sum();
        }

        public readonly int[] Snacks { get; init; }
        public int TotalCalories { get; init; }
    }
}
