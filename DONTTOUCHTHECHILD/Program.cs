using System;
using System.IO;

namespace DONTTOUCHTHECHILD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            cmd.print("Welcome to DONTTOUCHTHECHILD v" + ProgramInfo.version, (ConsoleColor)cmd.LogType.Information);

            switch(args[0])
            {
                case "--JSON2PO":
                    Hison hivejson = new Hison(File.ReadAllText(args[1]));
                    break;
            }
        }
    }
}
