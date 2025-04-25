Console.WriteLine("Welcome to Vin Fletcher's Arrows are we making a custom arrow or picking from a predefined one?");
Console.WriteLine("0: Predefined");
Console.WriteLine("1: Custom");

int.TryParse(Console.ReadLine(), out int arrowChoice);

Arrow currentArrow = new();

if (arrowChoice == 0)
{
    Console.WriteLine("Thanks for choosing a predifined arrow! Let's pick which arrow you would like today.");

    var arrowFactoryMethods = new List<Func<Arrow>>
    {
        Arrow.CreateBeginnerArrow,
        Arrow.CreateMarksmanArrow,
        Arrow.CreateEliteArrow
    };

    // Returns prompt for user to pick predefined arrow
    for(int i = 0; i < arrowFactoryMethods.Count; i++)
    {
        Console.WriteLine($"{i}: {ReturnFactoryPrompt(arrowFactoryMethods[i])}");
    }

    bool actualArrow = false;

    while (!actualArrow)
    {
        if(int.TryParse(Console.ReadLine(), out int predefinedArrowChoice))
        {
            if(predefinedArrowChoice > arrowFactoryMethods.Count || predefinedArrowChoice < 0)
            {
                Console.WriteLine($"{predefinedArrowChoice} is not in range 0 - {arrowFactoryMethods.Count}");
            }
            else
            {
                currentArrow = arrowFactoryMethods[predefinedArrowChoice]();
                actualArrow = true;
            }
        }
        else
        {
            Console.WriteLine($"Please enter an integer in range of 0 - {arrowFactoryMethods.Count}");
        }
    }
}
else
{
    currentArrow.Arrowhead = WriteEnumOptions<Arrowheads>("Thanks for choosing a custom arrow! Let's start off with picking the arrowhead.");
    currentArrow.Fletching = WriteEnumOptions<Fletching>("Next let's pick a fletching for your arrow to be!");

    Console.WriteLine("Finally let's pick a length for that arrow! Enter an integer in range of 60 - 100");

    while (currentArrow.ShaftLength == 0)
    {
        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            if (choice > 100 || choice < 60)
            {
                Console.WriteLine($"{choice} is not in the range 60 - 100");
            }
            else
            {
                currentArrow.ShaftLength = choice;
            }
        }
        else
        {
            Console.WriteLine("Please enter a valid integer!");
        }
    }
}

Console.WriteLine($"Your {currentArrow.ShaftLength}cm arrow with a {currentArrow.Arrowhead} arrowhead and {currentArrow.Fletching} fletching will be ${currentArrow.GetCost()}");

T WriteEnumOptions<T>(string prompt) where T : Enum
{
    Console.WriteLine(prompt);

    T[] enumOptions = (T[])Enum.GetValues(typeof(T));

    for (int i = 0; i < enumOptions.Length; i++)
    {
        Console.WriteLine($"{i}: {enumOptions[i]}");
    }

    int choice;
    bool validResponse = false;

    do
    {
        if (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine($"Enter an integer 0 to {enumOptions.Length - 1}.");
        }
        else if (choice >= enumOptions.Length || choice < 0)
        {
            Console.WriteLine($"{choice} is not in the range of 0 to {enumOptions.Length - 1}");
        }
        else
        {
            validResponse = true;
        }

    } while (!validResponse);

    return enumOptions[choice];
}

// Takes in an Arrow Factory method and returns a usable prompt to showcase name and description of each method.
string ReturnFactoryPrompt(Func<Arrow> factory)
{
    Arrow arrow = factory();

    string methodName = factory.Method.Name;
    string arrowName = methodName.Replace("Create", "");

    var readableName = System.Text.RegularExpressions.Regex
        .Replace(arrowName, "(\\B[A-Z])", " $1");

    string fullDescription = $"{readableName}: {arrow.Arrowhead}, {arrow.Fletching}, {arrow.ShaftLength}cm";

    return fullDescription;
}

class Arrow
{
    public Arrowheads Arrowhead { get; set; }
    public Fletching Fletching { get; set; }
    public int ShaftLength { get; set; }

    static public Arrow CreateBeginnerArrow()
    {
        return new Arrow
        {
            Arrowhead = Arrowheads.Wood,
            Fletching = Fletching.Goose,
            ShaftLength = 75
        };
    }

    static public Arrow CreateMarksmanArrow()
    {
        return new Arrow
        {
            Arrowhead = Arrowheads.Steel,
            Fletching = Fletching.Goose,
            ShaftLength = 65
        };
    }

    static public Arrow CreateEliteArrow()
    {
        return new Arrow
        {
            Arrowhead = Arrowheads.Steel,
            Fletching = Fletching.Plastic,
            ShaftLength = 95
        };
    }

    public float GetCost()
    {
        float arrowheadValue = (float)Arrowhead;
        float fletchingValue = (float)Fletching;
        float shaftLengthValue = ShaftLength * 0.05F;

        return arrowheadValue + fletchingValue + shaftLengthValue;
    }
}

enum Arrowheads
{
    Steel = 10,
    Obsidian = 5,
    Wood = 3
}

enum Fletching
{
    Plastic = 10,
    Turkey = 5,
    Goose = 3
}