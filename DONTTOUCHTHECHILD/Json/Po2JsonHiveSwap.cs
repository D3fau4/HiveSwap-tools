using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace DONTTOUCHTHECHILD.Json
{
    public class Po2JsonHiveSwap : IConverter<Po, JsonHiveSwap>
    {
        public JsonHiveSwap Convert(Po source)
        {
            var json = new JsonHiveSwap();
            foreach (var entry in source.Entries)
            {
                json.entries.Add(new JsonEntry()
                {
                    Key = entry.Context.Split("|")[1],
                    Position = System.Convert.ToInt32(entry.Reference),
                    Text = entry.Text
                });
            }

            return json;
        }
    }
}
