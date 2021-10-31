using System;
using System.IO;
using Yarhl.FileSystem;
using Yarhl.Media.Text;

namespace DONTTOUCHTHECHILD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            cmd.print("Welcome to DONTTOUCHTHECHILD v" + ProgramInfo.version, (ConsoleColor)cmd.LogType.Information);

            switch (args[0])
            {
                case "--JSON2PO":
                    Hison hivejson = new Hison(File.ReadAllText(args[1]));
                    json2Po po = new json2Po();
                    var meme = po.Convert(hivejson.text);
                    Po2Binary po2Binary = new Po2Binary();
                    var binary = po2Binary.Convert(meme);
                    var node1 = new Node(Path.GetFileName(args[1]), binary);
                    node1.Stream.WriteTo("meme1.po");
                    break;
            }
        }
    }
}
