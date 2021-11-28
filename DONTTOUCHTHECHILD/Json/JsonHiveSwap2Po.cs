using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace DONTTOUCHTHECHILD.Json
{
    public class JsonHiveSwap2Po : IConverter<JsonHiveSwap, Po>
    {
        public Po Convert(JsonHiveSwap source)
        {
            //Read the language used by the user' OS, this way the editor can spellcheck the translation. - Thanks Liquid_S por the code
            var currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Hiveswap", "anyemail@gmail.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            var i = 0;
            foreach (var text in source.entries)
            {
                po.Add(new PoEntry(text.Text)
                {
                    Context = $"{i++}|{text.Key}",
                    Reference = text.Position.ToString()
                });
            }

            return po;
        }
    }
}
