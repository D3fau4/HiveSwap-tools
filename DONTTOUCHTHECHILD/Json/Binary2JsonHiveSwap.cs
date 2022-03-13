using System.Linq;
using Yarhl.FileFormat;
using Yarhl.IO;

namespace DONTTOUCHTHECHILD.Json
{
    public class Binary2JsonHiveSwap : IConverter<BinaryFormat, JsonHiveSwap>
    {
        private JsonHiveSwap _jsonHiveSwap;
        private TextReader reader;

        public JsonHiveSwap Convert(BinaryFormat source)
        {
            _jsonHiveSwap = new JsonHiveSwap();
            reader = new TextReader(source.Stream);
            var pos = 0;
            do
            {
                ParseLine(reader.ReadLine(), pos++);
            } while (!reader.Stream.EndOfStream);



            return _jsonHiveSwap;
        }

        private void ParseLine(string line, int position)
        {
            var keys = new string[]
            {
                "string _displayName",
                "string _name",
                "string _paramName",
                "string data",
                "string _statement",
                "string DisplayText",
                "string DisplayTextLine2",
                "string DisplayTextLine3",
                "string _cue"
            };
                
            foreach (var key in keys)
            {
                if (!line.Contains(key))
                    continue;


                var separator = line.IndexOf(':');
                var entry = line.IndexOf("\"", separator);
                var substring = line.Substring(entry+1);
                var text = substring.Contains("\"},") ? substring[..^3] : substring[..^2];

                if (!string.IsNullOrWhiteSpace(text))
                    _jsonHiveSwap.entries.Add(new JsonEntry()
                    {
                        Position = position,
                        Text = TextSanitizer(text),
                        Key = keys.FirstOrDefault(x=>x == key)
                    });
            }
            
        }

        private string TextSanitizer(string text)
        {
            return text.Replace("\\", "");
        }
    }
}
