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
            var input = r.Split(" ");
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
            Console.WriteLine("Total Score: {0}", value);

            Console.WriteLine($"< Part-1 for {inputSet.Name}");


            Console.WriteLine($"> Part-2 for {inputSet.Name}");

            var value2 = Part2(inputSet);
            Console.WriteLine("Value: {0}", value2);

            Console.WriteLine($"< Part-2 for {inputSet.Name}");
        }
    }

    private static int Part1(PuzzleInput<Round> input)
    {
        return input.Content.Select(r => GameSolver.ResolveRoundByChoices(r)).Sum();
    }

    private static int Part2(PuzzleInput<Round> input)
    {
        return input.Content.Select(r => GameSolver.ResolveRoundByOutcome(r)).Sum();
    }

}

public readonly record struct Round
{
    public Round(string opponentChoice, string strategyChoice)
    {
        StrategyChoice = strategyChoice;
        OpponentChoice = opponentChoice;
    }

    public readonly string StrategyChoice { get; init; }
    public readonly string OpponentChoice { get; init; }

}

public enum Symbol
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

public enum GameResult
{
    Lost = 0,
    Draw = 3,
    Won = 6
}

public static class GameSolver
{

    public static int ResolveRoundByChoices(Round round)
    {
        var ownChoice = SymbolFromString(round.StrategyChoice);
        var opponentChoice = SymbolFromString(round.OpponentChoice);

        if (ownChoice == opponentChoice)
        {
            return (int)GameResult.Draw + (int)ownChoice;
        }
        else if (
            (ownChoice == Symbol.Rock && opponentChoice == Symbol.Scissors) ||
            (ownChoice == Symbol.Scissors && opponentChoice == Symbol.Paper) ||
            (ownChoice == Symbol.Paper && opponentChoice == Symbol.Rock)
        )
        {
            return (int)GameResult.Won + (int)ownChoice;
        }
        else
        {
            return (int)GameResult.Lost + (int)ownChoice;
        }
    }

    public static int ResolveRoundByOutcome(Round round)
    {
        var opponentChoice = SymbolFromString(round.OpponentChoice);
        var gameResult = ResultFromString(round.StrategyChoice);
        var gameInstruction = (opponentChoice, gameResult);

        return gameInstruction switch
        {
            (_, GameResult.Draw) => (int)GameResult.Draw + (int)gameInstruction.opponentChoice,
            (Symbol.Rock, GameResult.Won) => (int)GameResult.Won + (int)Symbol.Paper,
            (Symbol.Paper, GameResult.Won) => (int)GameResult.Won + (int)Symbol.Scissors,
            (Symbol.Scissors, GameResult.Won) => (int)GameResult.Won + (int)Symbol.Rock,
            (Symbol.Rock, GameResult.Lost) => (int)GameResult.Lost + (int)Symbol.Scissors,
            (Symbol.Paper, GameResult.Lost) => (int)GameResult.Lost + (int)Symbol.Rock,
            (Symbol.Scissors, GameResult.Lost) => (int)GameResult.Lost + (int)Symbol.Paper,
            _ => throw new ArgumentException("Invalid combination of symbols", nameof(round))
        };
    }

    private static GameResult ResultFromString(string strategyChoice) => strategyChoice switch
    {
        "X" => GameResult.Lost,
        "Y" => GameResult.Draw,
        "Z" => GameResult.Won,
        _ => throw new ArgumentException("Invalid input symbol", nameof(strategyChoice))
    };


    public static Symbol SymbolFromString(string rawInput) => rawInput switch
    {
        "A" or "X" => Symbol.Rock,
        "B" or "Y" => Symbol.Paper,
        "C" or "Z" => Symbol.Scissors,
        _ => throw new ArgumentException("Invalid input symbol", nameof(rawInput))
    };

}