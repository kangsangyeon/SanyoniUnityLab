using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using UnityEngine;

public class CsvHelperExample : MonoBehaviour
{
    private void Start()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            NewLine = Environment.NewLine,
        };

        using (var stream = File.Open($"{Application.streamingAssetsPath}/DataTable/SkillDataTable.csv", FileMode.Open))
        using (var reader = new StreamReader(stream))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<SkillData>().ToList();
            records.ForEach(x => Debug.Log(x.ToString()));
        }
    }
}