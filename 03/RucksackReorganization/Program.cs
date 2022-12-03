using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using Shared;

namespace RucksackReorganization;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine('a' + 'a');

        var inputData = DataLoader.LoadInputData(line => new Rucksack(line));

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

    private static int Part1(PuzzleInput<Rucksack> input)
    {
        var translator = new PriorityTranslator();
        return input.Content
            .Select(x => x.FindDuplicateItem())
            .Select(translator.GetPriority)
            .Sum();
    }

    private static int Part2(PuzzleInput<Rucksack> input)
    {
        var translator = new PriorityTranslator();
        return input.Content
            .Chunk(3)
            .Select(FindBadge)
            .Select(translator.GetPriority)
            .Sum();
    }

    private static char FindBadge(Rucksack[] rucksacks)
    {
        if (rucksacks.Length != 3)
            throw new ArgumentOutOfRangeException(nameof(rucksacks));

        var orderedRucksacks = rucksacks.OrderBy(x => x.Items.Length);
        var smallestRucksack = orderedRucksacks.First();
        var remainingRucksacks = orderedRucksacks.Skip(1).ToArray();

        foreach (var item in smallestRucksack.Items)
        {
            if (remainingRucksacks.All(rucksack => rucksack.UniqueItems.Contains(item)))
                return item;
        }

        throw new InvalidOperationException("Rucksacks dont contain a badge");
    }
}

readonly record struct Rucksack
{
    public string Items { get; init; }
    public ReadOnlyMemory<char> ItemList { get; init; }
    public ReadOnlyMemory<char> FirstCompartment { get; init; }
    public ReadOnlyMemory<char> SecondCompartment { get; init; }
    public HashSet<char> UniqueItems { get; init; }

    public Rucksack(string itemList)
    {
        Items = itemList;
        ItemList = Items.AsMemory();
        var midPoint = ItemList.Length / 2;
        FirstCompartment = ItemList[0..midPoint];
        SecondCompartment = ItemList[midPoint..];

        UniqueItems = Items.ToHashSet();
    }

    public char FindDuplicateItem()
    {
        var firstSegment = FirstCompartment.Span;
        var secondSegment = SecondCompartment.Span;

        foreach (var firstItem in firstSegment)
        {
            foreach (var secondItem in secondSegment)
            {
                if (firstItem == secondItem)
                {
                    return firstItem;
                }
            }
        }
        return char.MinValue;
    }
}

class PriorityTranslator
{
    private readonly Dictionary<char, int> _priorities;
    private const string charRange = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public PriorityTranslator()
    {
        _priorities = Enumerable
            .Range(0, charRange.Length)
            .ToDictionary(index => charRange[index], index => index + 1);
    }

    public int GetPriority(char item)
    {
        return _priorities[item];
    }
}
