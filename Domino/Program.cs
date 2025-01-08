// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello Gamers, Lets run some samples allowed in this game");

var s = "[1|2] [2|3] [3|1]";
var dom = Domino.Domino.Create(s);

Console.WriteLine($"\nFor input {s}");
Console.WriteLine($"- Is circular? - {dom.IsCircular}");
Console.WriteLine($"- Lets see a valid solution - {dom.GetSolution()}");

s = "[1|2] [2|3] [4|1]";
dom = Domino.Domino.Create(s);

Console.WriteLine($"\nFor input {s}");
Console.WriteLine($"- Is circular? - {dom.IsCircular}");
Console.WriteLine($"- Lets see a valid solution - {dom.GetSolution()}");


s = "[1|2] [2|3] [3|1] [2|2] [2|2]";
 dom = Domino.Domino.Create(s);

Console.WriteLine($"\nFor input {s}");
Console.WriteLine($"- Is circular? - {dom.IsCircular}");
Console.WriteLine($"- Lets see a valid solution - {dom.GetSolution()}");



s = "[2|2] [2|2]";
dom = Domino.Domino.Create(s);
Console.WriteLine($"\nFor input {s}");
Console.WriteLine($"- Is circular? - {dom.IsCircular}");
Console.WriteLine($"- Lets see a valid solution - {dom.GetSolution()}");

s = "[2|2]";
dom = Domino.Domino.Create(s);
Console.WriteLine($"\nFor input {s}");
Console.WriteLine($"- Is circular? - {dom.IsCircular}");
Console.WriteLine($"- Lets see a valid solution - {dom.GetSolution()}");



while (true)
{

    Console.WriteLine("\nDo you want to try your own example? 1- YES other key- NO");
    var input = Console.ReadLine();
    if(int.TryParse(input, out var value) && value ==1)
    {
        Console.WriteLine("Awesome, insert your custom chain...");
        var chain = Console.ReadLine();
        if (string.IsNullOrEmpty(chain))
        {
            continue;
        }
        dom = Domino.Domino.Create(chain);
        Console.WriteLine($"- Is circular? - {dom.IsCircular}");
        Console.WriteLine($"- Lets see a valid solution - {dom.GetSolution()}");
    }
    else
    {
        break;
    }
}

