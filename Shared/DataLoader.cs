using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Shared
{
    public static class DataLoader
    {

        public static List<PuzzleInput<T>> LoadInputData<T>(Func<string, T> contentModificationFunction)
        {
            return Directory.GetFiles(@"./input", "*.txt")
                .Select(filePath => new FileInfo(filePath))
                .Where(fileInfo => fileInfo.Name.Contains('_'))
                .Select(fileInfo => new PuzzleInput<T>(
                    Path.GetFileNameWithoutExtension(fileInfo.Name)
                        .Split('_')
                        .Last(),
                    File.ReadAllLines(fileInfo.FullName)
                        .Select(contentModificationFunction)
                        .ToList()))
                .ToList();
        }

        public static List<PuzzleInput<T>> LoadSingleLineInputData<T>(Func<string, T> contentModificationFunction, string splitChar)
        {
            return Directory.GetFiles(@"./input", "*.txt")
                .Select(filePath => new FileInfo(filePath))
                .Where(fileInfo => fileInfo.Name.Contains('_'))
                .Select(fileInfo => new PuzzleInput<T>(
                    Path.GetFileNameWithoutExtension(fileInfo.Name)
                        .Split('_')
                        .Last(),
                    File.ReadAllLines(fileInfo.FullName)
                        .SingleOrDefault(String.Empty)
                        .Split(splitChar)
                        .Where( elem => !String.IsNullOrEmpty(elem) )
                        .Select(contentModificationFunction)
                        ))
                .ToList();
        }

        public static List<PuzzleInput<string>> LoadInputData()
        {
            return LoadInputData<string>(line => line);
        }
    }

    public record class PuzzleInput<T>
    {
        public string Name { get; init; }
        public T[] Content { get; init; }

        public PuzzleInput(string name, IEnumerable<T> content)
        {
            Name = name;
            Content = content.ToArray();
        }

    }
}
