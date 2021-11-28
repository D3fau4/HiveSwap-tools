using System;
using System.IO;
using System.Linq;
using DONTTOUCHTHECHILD.Json;
using Yarhl.FileSystem;
using Yarhl.Media.Text;

namespace DONTTOUCHTHECHILD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            cmd.print("Welcome to DONTTOUCHTHECHILD v" + ProgramInfo.version, (ConsoleColor)cmd.LogType.Information);

            switch (args[0].ToUpper())
            {
                case "--JSON2PO":
                    if (Directory.Exists(args[1]))
                    {
                        var container = NodeFactory.FromDirectory(args[1], "*.json");

                        foreach (var child in container.Children)
                        {
                            GeneratePo(child);
                        }
                    }
                    else
                    {
                        cmd.print("The directory doesn't exist.", (ConsoleColor)cmd.LogType.Error);
                        Environment.Exit(0);
                    }
                    break;
                case "--PO2JSON":
                    if (Directory.Exists(args[1]))
                    {
                        var containerPo = NodeFactory.FromDirectory(args[1], "*.po").Children.ToList();
                        var containerJson = NodeFactory.FromDirectory(args[1], "*.json").Children.ToList();

                        foreach (var child in containerPo)
                        {
                            var jsonName = child.Name.Replace(".po", "");
                            if (!containerJson.Exists(x => x.Name == jsonName))
                            {
                                cmd.print($"The original json file \"{jsonName}\" doesn't exist.", (ConsoleColor)cmd.LogType.Warning);
                                continue;
                            }
                            GenerateJson(child, containerJson.Find(x=>x.Name == jsonName));
                        }
                    }
                    else
                    {
                        cmd.print("The directory doesn't exist.", (ConsoleColor)cmd.LogType.Error);
                        Environment.Exit(0);
                    }
                    break;
                default:
                    cmd.print("USAGE:");
                    cmd.print("Export json to po");
                    cmd.print("USAGE: DONTTOUCHTHECHILD --JSON2PO \"hiveswap_files\"");
                    cmd.print("Export po to json");
                    cmd.print("USAGE: DONTTOUCHTHECHILD --PO2JSON \"json_with_po_files\"");
                    break;
            }
        }

        private static void GeneratePo(Node child)
        {
            if (!Directory.Exists("out"))
                Directory.CreateDirectory("out");

            cmd.print($"Exporting {child.Name}...");
            var nodeResult = child.TransformWith(new Binary2JsonHiveSwap()).TransformWith(new JsonHiveSwap2Po());

            if (nodeResult != null)
            {
                var po = nodeResult.GetFormatAs<Po>();
                if (po.Entries.Count != 0)
                {
                    child.TransformWith(new Po2Binary()).Stream.WriteTo($"out{Path.DirectorySeparatorChar}{child.Name}.po");
                }

            }
        }

        private static void GenerateJson(Node child, Node oriChild)
        {
            cmd.print($"Importing {child.Name}...");
            if (!Directory.Exists("imported"))
                Directory.CreateDirectory("imported");

            child.TransformWith(new Binary2Po()).TransformWith(new Po2JsonHiveSwap()).TransformWith(new JsonHiveSwap2Binary()
            {
                OriginalJson = oriChild.Stream
            }).Stream.WriteTo($"imported{Path.DirectorySeparatorChar}{oriChild.Name}");
        }
    }
}
