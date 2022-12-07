using AdventOfCode2022.Common.Abstraction;

namespace AdventOfCode2022.Day._07
{
    public class Solver : ISolver
    {

        public readonly string cd_backToRoot = "$ cd /";
        public readonly string cd_goToChild = "$ cd";
        public readonly string cd_goToParent = "$ cd ..";
        public readonly string ls = "$ ls";
        public readonly string dir = "dir";

        public string Title => "2022 - Day 7: No Space Left On Device";

        public async Task<string> PartOne()
        {
            var maximumSizeOfDirectory = 100000;

            var root = new Directory("/", null);
            Directory currentDirectory = null;

            foreach (var line in await System.IO.File.ReadAllLinesAsync("./202207.txt"))
            {
                if (line == cd_backToRoot)
                {
                    currentDirectory = root;
                }
                else if (line.Equals(cd_goToParent))
                {
                    currentDirectory = currentDirectory.Parrent;
                }
                else if (line.StartsWith(cd_goToChild))
                {
                    var directoryName = line.Split(" ").Last();

                    currentDirectory = currentDirectory.Children.FirstOrDefault(x => x.Name == directoryName);
                }
                else if (line.Equals(ls))
                {
                    continue;
                }
                else if (line.StartsWith(dir))
                {
                    var directoryName = line.Split(" ").Last();
                    currentDirectory.AddChild(directoryName);
                }
                else
                {
                    var fileData = line.Split(" ");
                    currentDirectory.AddFile(int.Parse(fileData.First()), fileData.Last());

                }
            }

            var findResult = FindDirectories(root, maximumSizeOfDirectory);

            return findResult.Sum(x => x.GetSize()).ToString();

        }

        public async Task<string> PartTwo()
        {
            var fileSystemSize = 70000000;
            var requiredSize = 30000000;

            var root = new Directory("/", null);
            Directory currentDirectory = null;

            foreach (var line in await System.IO.File.ReadAllLinesAsync("./202207.txt"))
            {
                if (line == cd_backToRoot)
                {
                    currentDirectory = root;
                }
                else if (line.Equals(cd_goToParent))
                {
                    currentDirectory = currentDirectory.Parrent;
                }
                else if (line.StartsWith(cd_goToChild))
                {
                    var directoryName = line.Split(" ").Last();

                    currentDirectory = currentDirectory.Children.FirstOrDefault(x => x.Name == directoryName);
                }
                else if (line.Equals(ls))
                {
                    continue;
                }
                else if (line.StartsWith(dir))
                {
                    var directoryName = line.Split(" ").Last();
                    currentDirectory.AddChild(directoryName);
                }
                else
                {
                    var fileData = line.Split(" ");
                    currentDirectory.AddFile(int.Parse(fileData.First()), fileData.Last());

                }
            }

            var currentSize = fileSystemSize - root.GetSize();

            var result = GetDirectoryInfo(root).OrderBy(x => x.GetSize()).First(x => (currentSize + x.GetSize()) > requiredSize);

            return result.GetSize().ToString();
        }

        public HashSet<Directory> FindDirectories(Directory root, int maximumSizeOfDirectories)
        {
            var result = new HashSet<Directory>();
            var rootSize = root.GetSize();

            if (rootSize < maximumSizeOfDirectories)
            {
                result.Add(root);
            }

            foreach (var child in root.Children)
            {
                var childResults = FindDirectories(child, maximumSizeOfDirectories);
                foreach (var childResult in childResults)
                {
                    result.Add(childResult);
                }
            }


            return result;
        }

        public IEnumerable<Directory>GetDirectoryInfo(Directory root)
        {
            yield return root;
            foreach(var child in root.Children.SelectMany(x => GetDirectoryInfo(x)))
            {
                yield return child;
            }
        }
    }

    public class Directory
    {
        public Directory(string name, Directory parrent)
        {
            Name = name;
            Parrent = parrent;
        }

        public string Name { get; set; }
        public Directory Parrent { get; set; }
        public List<Directory> Children { get; set; } = new List<Directory>();
        public List<File> Files { get; set; } = new List<File>();

        public int GetSize()
        {
            return Children.Sum(x => x.GetSize()) + Files.Sum(x => x.Size);
        }

        public void AddChild(string directoryName)
        {
            if (Children.All(x => x.Name != directoryName))
            {
                Children.Add(new Directory(directoryName, this));
            }
        }

        public void AddFile(int size, string fileName)
        {
            if (Files.All(x => x.Name != fileName))
            {
                Files.Add(new File(size, fileName));
            }
        }
    }

    public record File(int Size, string Name);
}