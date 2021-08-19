﻿using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;

namespace CoderByteCleanJson
{
    class Program
    {
        static void Main()
        {

            WebRequest request = WebRequest.Create("https://coderbyte.com/api/challenges/json/json-cleaning");
            WebResponse response = request.GetResponse();

            using (Stream data = response.GetResponseStream())
            {
                StreamReader read = new StreamReader(data);
                string text = read.ReadToEnd();
                dynamic json = JObject.Parse(text);

                RemoveJson(json["name"]);
                RemoveJson(json["age"]);
                RemoveJson(json["DOB"]);
                RemoveJson(json["hobbies"]);
                RemoveJson(json["education"]);
                Console.WriteLine(json);
            }

            response.Close();
        }

        static void RemoveJson(dynamic node)
        {
            bool removed = true;
            while (removed)
            {
                try
                {
                    foreach (var item in node)
                    {
                        if (item.Value.ToString() == "" || item.Value.ToString() == "N/A")
                        {
                            item.Remove();
                            removed = true;
                            break;
                        }
                        else
                            removed = false;
                    }
                }
                catch (Exception)
                {
                    removed = false;
                }
            }
        }

    }
}
