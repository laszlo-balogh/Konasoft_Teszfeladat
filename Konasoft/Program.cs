using Konasoft.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        Dictionary<string, List<Area>> areas = new Dictionary<string, List<Area>>();
        List<Country> countries = new List<Country>();

        List<Task> tasks = new List<Task>();
        
        Task areasTask = new Task(() =>
        {
            areas = ReadAreas();
        }, TaskCreationOptions.LongRunning);

        Task countriesTask = new Task(() =>
        {
            countries = ReadCountries();
        }, TaskCreationOptions.LongRunning);

        tasks.Add(areasTask);
        tasks.Add(countriesTask);
        tasks.ForEach(x => x.Start());

        await Task.WhenAll(tasks);

        Dictionary<string, OutputData> callResult = ProcessLogFile(areas, countries);


        using (StreamWriter writer = new StreamWriter("myoutput.txt"))
        {
            foreach (var result in callResult.Values.Select(x => x))
            {
                await writer.WriteLineAsync(result.ToString());
            }
        }

    }

    public static Dictionary<string, OutputData> ProcessLogFile(Dictionary<string, List<Area>> areas, List<Country> countries, string path = "input.txt")
    {
        Dictionary<string, OutputData> sumResult = new Dictionary<string, OutputData>();

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] fields = line.Split('\t');
                string called = fields[3];

                string possibleCountryCode = called.Substring(1, 2);
                string possibleAreaCode = called.Substring(2, 5);

                string countryCallingCode = countries.FirstOrDefault(x => possibleCountryCode.Contains(x.CallingCode)).CallingCode;
                string country = countries.FirstOrDefault(x => x.CallingCode == countryCallingCode).Name;

                Area tempAreaObj = areas.Values.SelectMany(x => x)
                    .Where(y => possibleAreaCode.Contains(y.Code))
                    .FirstOrDefault(z => z.CountryCallingCode == countryCallingCode);

                string areaCallingCode = tempAreaObj.Code;
                string area = tempAreaObj.Name;

                string key = countryCallingCode + areaCallingCode;

                if (!sumResult.Keys.Contains(key))
                {
                    sumResult[key] = new OutputData(country, area, countryCallingCode, areaCallingCode, int.Parse(fields[4]));
                }
                else
                {
                    sumResult[key].SumCallingDuration += int.Parse(fields[4]);
                }


            }
        }

        return sumResult;
    }

    public static Dictionary<string, List<Area>> ReadAreas(string path = "area.txt")
    {

        Dictionary<string, List<Area>> areasDict = new Dictionary<string, List<Area>>();
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] fields = line.Split('\t');
                string countryCode = fields[0];
                if (!areasDict.Keys.Contains(countryCode))
                {
                    areasDict.Add(countryCode, new List<Area>() { new Area(countryCode, fields[1], fields[2]) });
                }
                else
                {
                    areasDict[countryCode].Add(new Area(countryCode, fields[1], fields[2]));
                }
            }
        }

        return areasDict;
    }

    public static List<Country> ReadCountries(string path = "country.txt")
    {
        List<Country> countries = new List<Country>();
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] fields = line.Split('\t');
                countries.Add(new Country(fields[0], fields[1]));
            }
        }

        return countries;
    }
}

