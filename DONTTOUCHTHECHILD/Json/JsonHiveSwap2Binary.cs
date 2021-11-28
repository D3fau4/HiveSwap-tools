using System;
using System.Linq;
using System.Text;
using Yarhl.FileFormat;
using Yarhl.IO;

namespace DONTTOUCHTHECHILD.Json
{
    public class JsonHiveSwap2Binary : IConverter<JsonHiveSwap, BinaryFormat>
    {
        public DataStream OriginalJson { get; set; }
        private TextWriter writer;
        private TextReader reader;
        private JsonHiveSwap json;

        public BinaryFormat Convert(JsonHiveSwap source)
        {
            if (OriginalJson == null)
                throw new Exception("You need to load the original json file.");

            writer = new TextWriter(new DataStream(), Encoding.UTF8)
            {
                NewLine = "\r\n"
            };
            reader = new TextReader(OriginalJson);
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
                    var delimitator = text.IndexOf(':');
                    var end = text.Substring(text.Contains("\"},") ? text.Length-3 : text.Length - 2);

                    writer.WriteLine($"{text.Substring(0, delimitator)}: " +
                                     $"\"{SanitizeString(json.entries.FirstOrDefault(x => x.Position == i)?.Text)}{end}");
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
    }
}
