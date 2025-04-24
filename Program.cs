using System.Reflection;

Arrow currentArrow = new();

currentArrow.SetArrowhead(WriteEnumOptions<Arrowheads>("Welcome to Vin Fletcher's Arrows. Let's start off with picking the arrowhead."));
currentArrow.SetFletching(WriteEnumOptions<Fletching>("Next let's pick a fletching for your arrow to be!"));

Console.WriteLine("Finally let's pick a length for that arrow! Enter an integer in range of 60 - 100");

while(currentArrow.GetShaftLength == 0)
{
    if(int.TryParse(Console.ReadLine(), out int choice))
    {
        if(choice > 100 || choice < 60)
        {
            Console.WriteLine($"{choice} is not in the range 60 - 100");
        }
        else
        {
            currentArrow.SetShaftLength(choice);
        }
    }
    else
    {
        Console.WriteLine("Please enter a valid integer!");
    }
} 

Console.WriteLine($"Your {currentArrow.GetShaftLength}cm arrow with a {currentArrow.GetArrowhead} arrowhead and {currentArrow.GetFletching} fletching will be ${currentArrow.GetCost():F2}");

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
    private Arrowheads arrowhead = default;
    private Fletching fletching = default;
    private int shaftLength = 0;

    public Arrowheads GetArrowhead => arrowhead;
    public Fletching GetFletching => fletching;
    public int GetShaftLength => shaftLength;

    public void SetArrowhead(Arrowheads value) => arrowhead = value;
    public void SetFletching(Fletching value) => fletching = value;
    public void SetShaftLength(int value) => shaftLength = value;

    public float GetCost()
    {
        float totalCost = 0;

        FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

        foreach (var field in fields)
        {
            var value = field.GetValue(this);

            if (value is Arrowheads arrowheadValue)
            {
                totalCost += (float)arrowhead;
            }
            else if (value is Fletching fletchingValue)
            {
                totalCost += (float)fletchingValue;
            }
            else if (value is int shaftLengthValue)
            {
                totalCost += shaftLengthValue * 0.05F;
            }
        }

        return totalCost;
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