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
                    if (Directory.Exists(args[1]))
                    {
                        foreach (string str in Directory.GetFiles(args[1]))
                        {
                            cmd.print(str);
                            Hison hivejson = new Hison(File.ReadAllText(str));
                            json2Po po = new json2Po();
                            var meme = po.Convert(hivejson.text);
                            Po2Binary po2Binary = new Po2Binary();
                            var binary = po2Binary.Convert(meme);
                            var node1 = new Node(Path.GetFileName(args[1]), binary);
                            node1.Stream.WriteTo(Path.Combine("Output", Path.GetFileName(str).Replace(".json",".po")));
                        }
                    }
                    break;
            }
        }
    }
}
