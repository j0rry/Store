using System.Text.Json;

class Program
{
    class Data
    {
        // User Money
        public int Money { get; set; } = 100;

        // Items and prices
        public int Kebab { get; set; } = 20;
        public int Candy { get; set; } = 14;
        public int Vbucks { get; set; } = 90;
        
    }

    private static readonly string filePath = "data.json";

    static void Main(string[] args)
    {   


        Data data = LoadFile();

        // Reset Money with args
        if (args.Length > 0 && args[0].Equals("reset", StringComparison.OrdinalIgnoreCase))
        {
            data.Money = int.TryParse(args[1], out int result) ? result : 1000;
            System.Console.WriteLine($"Set to {data.Money}$");
            SaveData(data);
            return;
        }

        if(AskAge() < 18) {
            Console.WriteLine("Inte tillräckligt gammal!");
            Console.ReadLine();
            return;
        }

        while(data.Money > 0){
            Console.Clear();
            Console.WriteLine($"${data.Money}");
            AskItem(data);
            SaveData(data);
        }
        System.Console.WriteLine("Du har inga Pengar kvar!");

        Console.ReadLine();
    }

    static void AskItem(Data data)
    {
        int count;
        int choice;
        int totalCost;
        Console.WriteLine($"1. Kebab ({data.Kebab}$)\n2. Godis ({data.Candy}$)\n3. Vbucks ({data.Vbucks}$)\n");

        do
        {
            System.Console.Write("Vad ska du köpa: ");
        } while (!int.TryParse(Console.ReadLine(), out choice) || choice > 3 || choice < 1);

        switch (choice)
        {
            case 1:
                Console.WriteLine("Du valde kebab");
                totalCost = data.Kebab;
                break;
            case 2:
                System.Console.WriteLine("Du valde Godis");
                totalCost = data.Candy;
                break;
            case 3:
                System.Console.WriteLine("Du valde Vbucks");
                totalCost = data.Vbucks;
                break;
            default:
                totalCost = 0;
                break;
        }

        do
        {
            System.Console.Write("Hur många? : ");
        } while (!int.TryParse(Console.ReadLine(), out count));

        totalCost *= count;

        if (data.Money >= totalCost)
        {
            data.Money -= totalCost;
            Console.WriteLine($"Du köpte {count} för {totalCost}$, du har {data.Money}$ kvar.");
            SaveData(data);
        }
        else
        {
            Console.WriteLine("Du har inte tillräckligt med pengar.");
        }
        Thread.Sleep(500);
    }



    static int AskAge()
    {
        int age;
        do
        {
            System.Console.Write("Skriv din ålder: ");
        } while (!int.TryParse(Console.ReadLine(), out age));
        return age;
    }

    static Data LoadFile()
    {
        if (!File.Exists(filePath))
        {
            var defaultData = new Data();
            SaveData(defaultData);
            return defaultData;
        }

        string fileContent = File.ReadAllText(filePath);
        Data deserializedData = JsonSerializer.Deserialize<Data>(fileContent) ?? new Data();

        return deserializedData ?? new Data();
    }

    static void SaveData(Data data)
    {
        var json = JsonSerializer.Serialize(data);
        File.WriteAllText(filePath, json);
    }
}