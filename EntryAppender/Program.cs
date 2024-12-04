
using Newtonsoft.Json;
using ConsoleApp1;

namespace EntryAppender
{
    class Program
    {
        static Random random = new Random(); // Define a single Random instance

        static void Main(string[] args)
        {

            string filePath = "F:\\Coding\\ProdMon\\test.json";
            string lastCheckedFilePath = "F:\\Coding\\ProdMon\\lastCheckedTimeStamp.txt";

            

            Console.WriteLine("Press 'q' to quit the sample, type 'add entry' to add a new entry.");
            while (true)
            {
                string input = Console.ReadLine();
                if (input.ToLower() == "q")
                {
                    break;
                }
                else if (input.ToLower() == "add entry")
                {
                    AddNewEntry(filePath);                    
                }
            }

            
        }

        private static void AddNewEntry(string filePath)
        {
            // Define the common values
            string articleDescriptions = "Reifen";
            string commonArticleNumber = "44322234";

            // Generate random data
            DateTime timestamp = DateTime.Now;            
            string dmc = GenerateRandomDmc(commonArticleNumber);
            string quality = random.Next(0, 2) == 0 ? "Gut" : "Schlecht";
            

            // Create a new MonitorEntry
            MonitorEntry newEntry = new MonitorEntry
            {
                Timestamp = timestamp,
                Dmc = dmc,
                Description = articleDescriptions,
                Quality = quality
            };

            // Read the existing entries
            List<MonitorEntry> entries;
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                entries = JsonConvert.DeserializeObject<List<MonitorEntry>>(jsonContent) ?? new List<MonitorEntry>();
            }
            else
            {
                entries = new List<MonitorEntry>();
            }

            // Add the new entry
            entries.Add(newEntry);

            // Write the updated entries back to the file
            string updatedJsonContent = JsonConvert.SerializeObject(entries, Formatting.Indented);
            File.WriteAllText(filePath, updatedJsonContent);

            Console.WriteLine($"Added new entry: {JsonConvert.SerializeObject(newEntry, Formatting.Indented)}");            
        }

        private static string GenerateRandomDmc(string commonArticleNumber)
        {
            // Generate the first 13 digits
            string firstPart = random.Next(100000, 1000000).ToString("D6") + random.Next(1000000, 10000000).ToString("D7");

            // Generate the last 2 digits
            string lastPart = random.Next(10, 100).ToString("D2");

            // Combine to form the complete DMC
            return firstPart + commonArticleNumber + lastPart;
        }
    }
}
