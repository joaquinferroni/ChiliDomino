using System.Text;
using System.Text.RegularExpressions;

namespace Domino
{
    public class Domino
    {
        public Dictionary<char, Dictionary<char, int>> Stones { get; init; }
        public bool IsCircular { get; init; }
        private int _TotalStones { get; init; }
        private Domino(Dictionary<char, Dictionary<char, int>> chain, bool isCircular, int totalStones)
        {
            Stones = chain;
            IsCircular = isCircular;
            _TotalStones = totalStones;
        }

        public static Domino Create(string chain)
        {
            bool isValid = Regex.IsMatch(chain, @"^[\[\]\s0-9|]+$");
            if (!isValid || string.IsNullOrWhiteSpace(chain))
            {
                throw new ArgumentException("Input has not valid characters", nameof(chain));
            }
            var stones = Regex.Replace(chain.Trim(), @"[\[\]]", "").Split(' ');
            var stonesCombinations = new Dictionary<char, Dictionary<char, int>>();
            var pairCounter = new Dictionary<char, int>();

            foreach (var stone in stones)
            {
                var numbers = stone.Replace("|", "").ToCharArray();
                AddStone(stonesCombinations, numbers);
                //The stone should be added in both directions if not the same number
                if (numbers[0] != numbers[1])
                    AddStone(stonesCombinations, [numbers[1], numbers[0]]);

                if (numbers[0] == numbers[1])
                {
                    continue;
                }
                else
                {
                    if (pairCounter.ContainsKey(numbers[0]))
                    {
                        pairCounter[numbers[0]]++;
                    }
                    else
                    {
                        pairCounter.Add(numbers[0], 1);
                    }
                    if (pairCounter.ContainsKey(numbers[1]))
                    {
                        pairCounter[numbers[1]]++;
                    }
                    else
                    {
                        pairCounter.Add(numbers[1], 1);
                    }
                }
            }
            var isCircular = pairCounter.All(p => p.Value % 2 == 0);
            return new Domino(stonesCombinations, isCircular, stones.Length);
        }

        public string GetSolution()
        {
            if (!IsCircular)
            {
                return "This game is not circular, please try with a different game";
            }
            //work with a copy, to allow this method to be called more than once
            var copyStonesToWorkWith = Stones.ToDictionary(
                entry => entry.Key,
                entry => entry.Value.ToDictionary(
                    innerEntry => innerEntry.Key,
                    innerEntry => innerEntry.Value
                )
            );
            var sb = new StringBuilder("");
            var count = 0;
            //pick a random stone to start with
            var initialIndex = (new Random()).Next(copyStonesToWorkWith.Count);
            var initialValue = copyStonesToWorkWith.ElementAt(initialIndex);
            var currentKey = initialValue.Key;
            while (count < _TotalStones)
            {
                //use the stone with repeated values asap
                if (copyStonesToWorkWith[currentKey].TryGetValue(currentKey, out int value) && value > 0)
                {
                    sb.Append($" [{currentKey}|{currentKey}]");
                    copyStonesToWorkWith[currentKey][currentKey] = --value;
                }
                else
                {
                    var nextElement = copyStonesToWorkWith[currentKey].First(v => v.Value > 0).Key;
                    sb.Append($" [{currentKey}|{nextElement}]");
                    copyStonesToWorkWith[currentKey][nextElement]--;
                    copyStonesToWorkWith[nextElement][currentKey]--;
                    currentKey = nextElement;
                }

                count++;
            }
            return sb.ToString().Trim();
        }

        private static void AddStone(Dictionary<char, Dictionary<char, int>> d, char[] stone)
        {
            if (!d.ContainsKey(stone[0]))
            {
                d.Add(stone[0], new() { { stone[1], 1 } });
            }
            else
            {
                if (d[stone[0]].ContainsKey(stone[1]))
                {
                    d[stone[0]][stone[1]]++;
                }
                else
                {
                    d[stone[0]].Add(stone[1], 1);
                }
            }
        }
    }
}
