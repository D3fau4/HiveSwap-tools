using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace DONTTOUCHTHECHILD
{
    internal class json2Po : IConverter<Texts, Po>
    {
        public Po Convert(Texts source)
        {
            //Read the language used by the user' OS, this way the editor can spellcheck the translation. - Thanks Liquid_S por the code
            var currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("HiveSwap: Act 1", "anyemail@D3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            for (int i = 0; i < source.Keys.Count; i++)
            {
                string original = source.Values[i];
                string translation = string.Empty;

                if (source.Values[i] == null)
                {
                    original = "<EMPTY>";
                    translation = "<EMPTY>";
                }

                po.Add(new PoEntry(original)
                {
                    Translated = translation,
                    Context = source.Keys[i],
                });
            }

            return po;
        }
    }
}
