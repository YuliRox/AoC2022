using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace RockPaperScissors;

class Program
{
    static void Main(string[] args)
    {

        var inputData = DataLoader.LoadInputData<Round>(r =>
        {
            var input = r.Split(" ").Select(SymbolFromString);
            return new Round(input.First(), input.Last());
        });

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

    private static int Part1(PuzzleInput<Round> input)
    {
        return 0;
    }

    private static int Part2(PuzzleInput<Round> input)
    {
        return 0;
    }

    public readonly record struct Round
    {
        public Round(Symbol ownChoice, Symbol opponentChoice)
        {
            OwnChoice = ownChoice;
            OpponentChoice = opponentChoice;
        }

        public readonly Symbol OwnChoice { get; init; }
        public readonly Symbol OpponentChoice { get; init; }

    }
    public static Symbol SymbolFromString(string rawInput) => rawInput switch
    {
        "A" or "X" => Symbol.Rock,
        "B" or "Y" => Symbol.Paper,
        "C" or "Z" => Symbol.Scissors,
        _ => throw new ArgumentException(nameof(rawInput))
    };

    public enum Symbol
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

}
