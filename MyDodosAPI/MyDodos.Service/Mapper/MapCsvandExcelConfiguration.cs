using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper.Configuration;
using ExcelMapper;
using MyDodos.Domain.Document;
using MyDodos.Repository.Document;

namespace MyDodos.Service.Mapper
{
public static class CsvConfigurationdetails
{
    public static void Map<T>(this ClassMap<T> classMap, IDictionary<string, string> csvMappings)
    {
        foreach (var mapping in csvMappings)
        {
            var property = typeof(T).GetProperty(mapping.Key);

            if (property == null)
            {
                throw new ArgumentException($"Class {typeof(T).Name} does not have a property named {mapping.Key}");
            }

            classMap.Map(typeof(T), property).Name(mapping.Value);
        }
    }
}
public static class xlsxHelperExtensions
{
    public static void Mapper<T>(this ExcelClassMap<T> excelclassMap, IDictionary<string, string> xlsxMappings)
    {
        foreach (var mapping in xlsxMappings)
        {
            var property = typeof(T).GetProperty(mapping.Key);

            if (property == null)
            {
                throw new ArgumentException($"Class {typeof(T).Name} does not have a property named {mapping.Key}");
            }

        //     excelclassMap.Map().WithColumnName(mapping.Value);
        //    excelclassMap.Map(mapping.Key).WithColumnName("EducationalQualification");
        }
    }
}

}
