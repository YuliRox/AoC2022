using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Shared
{
    public static class DataLoader
    {
        private static IEnumerable<FileInfo> ForInputFiles()
        {
            return Directory.GetFiles(@"./input", "*.txt")
                .Select(filePath => new FileInfo(filePath))
                .Where(fileInfo => fileInfo.Name.Contains('_'))
                .Where(fileInfo => fileInfo.Length > 0);
        }

        private static string GetInputName(FileInfo fileInfo)
        {
            return Path.GetFileNameWithoutExtension(fileInfo.Name)
                        .Split('_')
                        .Last();
        }


        public static List<PuzzleInput<T>> LoadInputData<T>(Func<string, T> contentModificationFunction)
        {
            return ForInputFiles()
                .Select(fileInfo => new PuzzleInput<T>(
                    GetInputName(fileInfo),
                    File.ReadAllLines(fileInfo.FullName)
                        .Select(contentModificationFunction)
                        .ToList()))
                .ToList();
        }

        public static List<PuzzleInput<T>> LoadSingleLineInputData<T>(Func<string, T> contentModificationFunction, string splitChar)
        {
            return ForInputFiles()
                .Select(fileInfo => new PuzzleInput<T>(
                    GetInputName(fileInfo),
                    File.ReadAllLines(fileInfo.FullName)
                        .SingleOrDefault(String.Empty)
                        .Split(splitChar)
                        .Where(elem => !String.IsNullOrEmpty(elem))
                        .Select(contentModificationFunction)
                        ))
                .ToList();
        }

        public static List<PuzzleInput<T>> LoadGroupedInputData<T>(Func<string, T> contentModificationFunction, string seperator)
        {
            return ForInputFiles()
                .Select(fileInfo => new PuzzleInput<T>(
                    GetInputName(fileInfo),
                    File.ReadAllText(fileInfo.FullName)
                        .Split(seperator)
                        .Select(contentModificationFunction)
                        .ToList()))
                .ToList();
        }

        public static List<PuzzleInput<T>> LoadAllRawInputData<T>(Func<string, IEnumerable<T>> contentModificationFunction)
        {
            return ForInputFiles()
                .Select(fileInfo => new PuzzleInput<T>(
                    GetInputName(fileInfo),
                    contentModificationFunction(File.ReadAllText(fileInfo.FullName))
                    ))
                .ToList();
        }

        public static List<PuzzleInput<T>> LoadRawInputData<T>(Func<IEnumerable<string>, IEnumerable<T>> contentModificationFunction, string seperator)
        {
            return ForInputFiles()
                .Select(fileInfo => new PuzzleInput<T>(
                    GetInputName(fileInfo),
                    contentModificationFunction(File.ReadAllLines(fileInfo.FullName))
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
