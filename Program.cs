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

    static void Main()
    {   
        Data data = LoadFile();
        Console.WriteLine(data.Money);

        if(AskAge() !>= 18) {
            Console.WriteLine("Inte tillräckligt gammal!");
            return;
        }

        while(data.Money > 0){

        }

        Console.ReadLine();
    }


    static void AskItem(Data data){
        int count;
        int choice;
        Console.WriteLine($"1. Kebab ({data.Kebab}$)\n2. Godis ({data.Candy})\n3. Vbucks ({data.Vbucks})\n");
        
        do{
            System.Console.Write("Vad ska du köpa: ");
        }while(!int.TryParse(Console.ReadLine(), out choice));
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
        Data deserializedData = JsonSerializer.Deserialize<Data>(fileContent);

        return deserializedData ?? new Data();
    }

    static void SaveData(Data data)
    {
        var json = JsonSerializer.Serialize(data);
        File.WriteAllText(filePath, json);
    }
}