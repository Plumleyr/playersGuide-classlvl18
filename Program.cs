Arrow currentArrow = new();

currentArrow.Arrowhead = WriteEnumOptions<Arrowheads>("Welcome to Vin Fletcher's Arrows. Let's start off with picking the arrowhead.");
currentArrow.Fletching = WriteEnumOptions<Fletching>("Next let's pick a fletching for your arrow to be!");

Console.WriteLine("Finally let's pick a length for that arrow! Enter an integer in range of 60 - 100");

while(currentArrow.ShaftLength == 0)
{
    if(int.TryParse(Console.ReadLine(), out int choice))
    {
        if(choice > 100 || choice < 60)
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

class Arrow
{
    public Arrowheads Arrowhead { get; set; }
    public Fletching Fletching { get; set; }
    public int ShaftLength { get; set; }

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