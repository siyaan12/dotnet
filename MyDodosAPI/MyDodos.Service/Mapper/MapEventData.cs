using CsvHelper.Configuration;
using MyDodos.Domain.Document;
using System;
using ExcelMapper;
using MyDodos.ViewModel.Document;

namespace MyDodos.Service.Mapper
{
    public sealed class MapEventData : ClassMap<StageHolidayBO>
    {
        
        public MapEventData()
        {
            
            Map(x => x.HolidayName).Name("HolidayName");
            Map(x => x.HolidayDate).Name("HolidayDate");
            Map(x => x.Description).Name("Description");
            Map(x => x.HolidayType).Name("HolidayType");

        }
    }
     public class xlsxMapEventData : ExcelClassMap<StageHolidayBO>
    {       
        public xlsxMapEventData()
        {
            Map(x => x.HolidayName).WithColumnName("HolidayName");
            Map(x => x.HolidayDate).WithColumnName("HolidayDate");
            Map(x => x.Description).WithColumnName("Description");
            Map(x => x.HolidayType).WithColumnName("HolidayType");
        }
    }
}