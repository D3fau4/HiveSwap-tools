using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace DONTTOUCHTHECHILD
{
    internal class Hison
    {
        JObject json;
        public Texts text;

        public Hison(string file)
        {
            text = new Texts();
            json = JObject.Parse(file);

            if (json["0 MonoBehaviour Base"].Contains("0 Verb _verbs"))
            {
                foreach (var item in json["0 MonoBehaviour Base"]["0 Verb _verbs"]["0 Array Array"])
                {
                    int count = 0;
                    foreach (var item2 in item["0 Verb data"]["0 Outcome _outcome"]["0 vector _message"]["0 Array Array"])
                    {
                        string keyname = (string)item["0 Verb data"]["1 string _name"] + "_outcome_" + count++;
                        text.Keys.Add(keyname);
                        text.Values.Add((string)item2["1 string data"]);
                    }
                    foreach (var item2 in item["0 Verb data"]["0 InteractableTarget _interactableTargets"]["0 Array Array"])
                    {
                        foreach (var item3 in item2["0 InteractableTarget data"]["0 Outcome Outcome"]["0 vector _message"]["0 Array Array"])
                        {
                            string keyname = (string)item["0 Verb data"]["1 string _name"] + "_0Outcome_" + count++;
                            text.Keys.Add(keyname);
                            text.Values.Add((string)item2["1 string data"]);
                        }
                    }
                }
            }
            else if (json["0 MonoBehaviour Base"]["0 ConversationLine _lines"].Any())
            {
                foreach (var item in json["0 MonoBehaviour Base"]["0 ConversationLine _lines"]["0 Array Array"])
                {
                    foreach (var item2 in item["0 ConversationLine data"]["0 ConversationStatement _statementOptions"]["0 Array Array"])
                    {
                        cmd.print((string)item2["0 ConversationStatement data"]["1 string _statement"]);
                    }
                }
            }
            else
            {
                cmd.print("Error", (ConsoleColor)cmd.LogType.Error);
            }
        }
    }
}
