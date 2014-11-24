using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrazyMelsRestService.Models
{
    static public class Parsing
    {
        static public SortedList<string, string> parseInputValuePairs(String input)
        {
            Char parameterDelimiter = '/';
            Char valueDelimiter = '=';

            String[] parameters = input.Split(new Char[] { parameterDelimiter }, StringSplitOptions.RemoveEmptyEntries);

            SortedList<String, String> paramValues = new SortedList<string, string>();

            foreach (String a in parameters)
            {
                String[] temp = a.Split(new Char[] { valueDelimiter },StringSplitOptions.RemoveEmptyEntries);
                if (temp.Length == 2)
                {
                    if (paramValues.ContainsKey(temp[0]))
                    {
                        throw new Exception("Duplicate Parameter input exists");
                    }
                    else
                    {
                        paramValues.Add(temp[0], temp[1].Trim());
                    }
                }
                else if (temp.Length == 1)
                {
                    if (paramValues.ContainsKey(temp[0]))
                    {
                        throw new Exception("Duplicate Parameter input exists");
                    }
                    else
                    {
                        paramValues.Add(temp[0], String.Empty);
                    }
                }
                else
                {
                    throw new Exception("Invalid Field-Value Pair");
                }

            }

            if(paramValues.Count == 0)
            {
                throw new Exception("No Search Data Indicated");
            }


            return paramValues;




        }
    }
}