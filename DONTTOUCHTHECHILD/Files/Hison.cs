using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DONTTOUCHTHECHILD
{
    internal class Hison
    {
        JObject json;
        Texts text;

        public Hison(string file)
        {
            text = new Texts();
            json = JObject.Parse(file);

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
                    foreach(var item3 in item2["0 InteractableTarget data"]["0 Outcome Outcome"]["0 vector _message"]["0 Array Array"])
                    {
                        string keyname = (string)item["0 Verb data"]["1 string _name"] + "0Outcome_" + count++;
                        text.Keys.Add(keyname);
                        text.Values.Add((string)item2["1 string data"]);
                    }
                }
            }
        }
    }
}
