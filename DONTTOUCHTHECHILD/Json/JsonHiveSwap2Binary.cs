using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yarhl.FileFormat;
using Yarhl.IO;

namespace DONTTOUCHTHECHILD.Json
{
    public class JsonHiveSwap2Binary : IConverter<JsonHiveSwap, BinaryFormat>
    {
        public DataStream OriginalJson { get; set; }
        public bool BlackList { get; set; }
        private TextDataWriter writer;
        private TextDataReader reader;
        private JsonHiveSwap json;

        public BinaryFormat Convert(JsonHiveSwap source)
        {
            if (OriginalJson == null)
                throw new Exception("You need to load the original json file.");

            writer = new TextDataWriter(new DataStream(), Encoding.UTF8)
            {
                NewLine = "\r\n"
            };
            reader = new TextDataReader(OriginalJson);
            json = source;

            GenerateNewJson();

            return new BinaryFormat(writer.Stream);
        }


        private void GenerateNewJson()
        {
            reader.Stream.Position = 0;
            var i = 0;
            do
            {
                if (json.entries.Exists(x => x.Position == i))
                {
                    var text = reader.ReadLine();

                    if (text.Contains("string _displayName") || text.Contains("string _paramName"))
                    {
                        writer.WriteLine(text);
                    }
                    else if (BlackList && text.Contains("string _name"))
                    {
                        writer.WriteLine(text);
                    }
                    else
                    {

                        var delimitator = text.IndexOf(':');
                        var end = text.Substring(text.Contains("\"},") ? text.Length - 3 : text.Length - 2);

                        writer.WriteLine($"{text.Substring(0, delimitator)}: " +
                                         $"\"{SanitizeString(json.entries.FirstOrDefault(x => x.Position == i)?.Text)}{end}");
                    }

                }
                else
                {
                    writer.WriteLine(reader.ReadLine());
                }
                i++;
            } while (!reader.Stream.EndOfStream);
        }

        private string SanitizeString(string text)
        {
            return text.Replace("\"", "\\\"");
        }


        public static List<string> fileBlackList = new List<string>()
        {
            "Attic Key-sharedassets0.assets-2574-MonoBehaviour.json",
            "Autotune Mic no batt-sharedassets0.assets-2575-MonoBehaviour.json",
            "Autotune Mic-sharedassets0.assets-2576-MonoBehaviour.json",
            "Ballet Dance-sharedassets0.assets-2401-MonoBehaviour.json",
            "Bent Silver Spoon-sharedassets0.assets-2577-MonoBehaviour.json",
            "Big Book of Beasts-sharedassets0.assets-2578-MonoBehaviour.json",
            "Bowl of Milk-sharedassets0.assets-2579-MonoBehaviour.json",
            "Carrier Pigeon-sharedassets0.assets-2581-MonoBehaviour.json",
            "Cuebat-sharedassets0.assets-2402-MonoBehaviour.json",
            "C_Cell Batteries-sharedassets0.assets-2580-MonoBehaviour.json",
            "Dammeks Hoodie-sharedassets0.assets-2583-MonoBehaviour.json",
            "Diary Key-sharedassets0.assets-2584-MonoBehaviour.json",
            "D_Cell Batteries-sharedassets0.assets-2582-MonoBehaviour.json",
            "Empty Bowl-sharedassets0.assets-2586-MonoBehaviour.json",
            "Example Backpack-sharedassets0.assets-3361-MonoBehaviour.json",
            "Example Book in Backpack-sharedassets0.assets-3362-MonoBehaviour.json",
            "Example Book-sharedassets0.assets-3363-MonoBehaviour.json",
            "Example Bowl-sharedassets0.assets-3364-MonoBehaviour.json",
            "Example Throw Pillow-sharedassets0.assets-3365-MonoBehaviour.json",
            "E_Cell Batteries-sharedassets0.assets-2585-MonoBehaviour.json",
            "Flare Gun (empty)-sharedassets0.assets-2587-MonoBehaviour.json",
            "Flares-sharedassets0.assets-2588-MonoBehaviour.json",
            "Flarp Manual-sharedassets0.assets-2589-MonoBehaviour.json",
            "Flashlight Container-sharedassets0.assets-2590-MonoBehaviour.json",
            "Flashlight No Batt-sharedassets0.assets-2403-MonoBehaviour.json",
            "FlashlightAbility-sharedassets0.assets-2404-MonoBehaviour.json",
            "Green Key-sharedassets0.assets-2591-MonoBehaviour.json",
            "Grub Juice-sharedassets0.assets-2592-MonoBehaviour.json",
            "Hover Pad-sharedassets0.assets-2594-MonoBehaviour.json",
            "Joey and Jude 1 (Deprecated)-sharedassets0.assets-2413-MonoBehaviour.json",
            "Joey and Jude 2-sharedassets0.assets-2414-MonoBehaviour.json",
            "Joey Static-sharedassets0.assets-2409-MonoBehaviour.json",
            "Joey Tablet-sharedassets0.assets-2562-MonoBehaviour.json",
            "Joey-sharedassets0.assets-2567-MonoBehaviour.json",
            "JoeyScene1Hints-sharedassets0.assets-2570-MonoBehaviour.json",
            "JoeyScene2Hints-sharedassets0.assets-2571-MonoBehaviour.json",
            "Jude Static-sharedassets0.assets-2435-MonoBehaviour.json",
            "Jude-sharedassets0.assets-2568-MonoBehaviour.json",
            "JudeHints-sharedassets0.assets-2572-MonoBehaviour.json",
            "Loaded Flare Gun-sharedassets0.assets-2405-MonoBehaviour.json",
            "Lusus Milk-sharedassets0.assets-2595-MonoBehaviour.json",
            "Magic Spice Mix-sharedassets0.assets-2596-MonoBehaviour.json",
            "New Item-sharedassets0.assets-2597-MonoBehaviour.json",
            "Pet Treats-sharedassets0.assets-2598-MonoBehaviour.json",
            "Pogs-sharedassets0.assets-2599-MonoBehaviour.json",
            "Pouch with Marbles-sharedassets0.assets-2601-MonoBehaviour.json",
            "Power Cell-sharedassets0.assets-2602-MonoBehaviour.json",
            "Pusher Playbook-sharedassets0.assets-2603-MonoBehaviour.json",
            "Ref Joey Tablet-sharedassets0.assets-2563-MonoBehaviour.json",
            "Ref Xef Tablet-sharedassets0.assets-2564-MonoBehaviour.json",
            "Sketchy Diagram-sharedassets0.assets-2604-MonoBehaviour.json",
            "Sloth Treats-sharedassets0.assets-2605-MonoBehaviour.json",
            "Software Box-sharedassets0.assets-2606-MonoBehaviour.json",
            "Stale Cracker-sharedassets0.assets-2607-MonoBehaviour.json",
            "Tap Dance-sharedassets0.assets-2406-MonoBehaviour.json",
            "Telekinesis-sharedassets0.assets-2407-MonoBehaviour.json",
            "Test Ability-sharedassets0.assets-3353-MonoBehaviour.json",
            "Vet Medkit-sharedassets0.assets-2608-MonoBehaviour.json"
        };
    }
}
