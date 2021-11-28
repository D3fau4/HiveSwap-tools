using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace DONTTOUCHTHECHILD.Json
{
    public class JsonHiveSwap : IFormat
    {
        public List<JsonEntry> entries { get; set; }

        public JsonHiveSwap()
        {
            entries = new List<JsonEntry>();
        }
    }

    public class JsonEntry
    {
        public string Text { get; set; }
        public int Position { get; set; }
        public string Key { get; set; }
    }
}
