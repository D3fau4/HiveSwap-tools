using System.Collections.Generic;
using Yarhl.FileFormat;

namespace DONTTOUCHTHECHILD
{
    internal class Texts : IFormat
    {
        public List<string> Keys { get; set; }
        public List<string> Values { get; set; }
        public Dictionary<string, string> TranslatedValues { get; set; }
        public Texts()
        {
            Keys = new List<string>();
            Values = new List<string>();
            TranslatedValues = new Dictionary<string, string>();
        }
    }
}
