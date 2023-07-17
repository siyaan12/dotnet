using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Master;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyDodosBackBone // Note: actual namespace depends on the project name.
{
    public class Program
    {
        private static IConfiguration _configuration;
        private static string AuditLog;
        public Program(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        static int msgCount = 0;
        static bool isProcess = false;

        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                                          .AddEnvironmentVariables()
                                          .Build();
            GetAppSettingsFile();

            writeToConsole("///*****--------------------------------------------------------------------------------------*****///");
            writeToConsole("");
            writeToConsole("           Console start...", true, true);
            writeToConsole("           +---------------------------------------------------------------------------+");
            writeToConsole("           |                         DODOS CONSOLE APPLICATION                         |");
            writeToConsole("           +---------------------------------------------------------------------------+");

            System.Threading.Timer t = new System.Threading.Timer(TimerCallback, null, 100, 60000);   //86400000 -- oneday
            // Wait for the user to hit <Enter>
            //  System.Threading.Thread.Sleep(100000);
            Console.ReadLine();
        }

        private static void TimerCallback(Object o)
        {
            if (!isProcess)
            {
                isProcess = true;
                writeToConsole("");
                writeToConsole("            New batch start: " + DateTime.Now);
                writeToConsole("           -----------------------------------------------------------------------------");
                writeToConsole("           <----                   Starting to process Year Deatils                ---->");
                writeToConsole("           -----------------------------------------------------------------------------");


                writeToConsole("                                    Current Year Details                                ");
                writeToConsole("           -----------------------------------------------------------------------------");
                writeToConsole("");
                SETTINGSAVEYEAR();
                AttendanceMode();
                LeaveReport();
                GeneratePayroll();
                TimesheetReport();
                HRSalaryPeriod();
                writeToConsole("");
                writeToConsole("///*****--------------------------------------------------------------------------------------*****///");
                writeToConsole("");
                isProcess = false;
            }
        }
        private static void SETTINGSAVEYEAR()
        {
            MasterDA objmaster = new MasterDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, 1, 1);
            MasterInputBO master = new MasterInputBO();
            MasYear objyeardata = new MasYear();
            master.EntityName = "Tenant";
            master.ProductID = Convert.ToInt32(_configuration.GetSection("ProductID").Value);
            var objtenent = objmaster.GetTenantDetails(master);
            if (objtenent.Count > 0)
            {
                foreach (var tenant in objtenent)
                {
                    master.EntityName = "Location";
                    master.ProductID = Convert.ToInt32(_configuration.GetSection("ProductID").Value);
                    master.TenantID = tenant.TenantID;
                    var objLocation = objmaster.GetLocationDetails(master);
                    
                    foreach (var location in objLocation)
                    {
                        master.EntityName = "Year";
                        master.ProductID = Convert.ToInt32(_configuration.GetSection("ProductID").Value);
                        master.TenantID = tenant.TenantID;
                        master.LocationID = location.LocationID;
                        // var objYear = objmaster.GetYearDetails(master);
                        // foreach (var year in objYear)
                        // {
                            objyeardata.YearID = 0;
                            objyeardata.TenantID = tenant.TenantID;
                            objyeardata.LocationID = location.LocationID;
                            objyeardata.DueDate = firstDayOfMonth.Date;

                            var objrtn = objmaster.SaveMasYear(objyeardata);
                            
                            if (objrtn > 0)
                            {
                                writeToConsole("            Year Created Date:" + DateTime.Now.Date.ToString() + "YearID Is:" + objrtn);
                            }
                            else
                            {
                                writeToConsole("            Nxt Year Waiting..." + DateTime.Now.Date.AddYears(1).ToString());
                            }
                        // }
                    }
                }
            }
            else
            {
                writeToConsole("            Error: Please added Tenant details " + DateTime.Now.ToString());
            }
        }
        private static void AttendanceMode()
        {
            MasterDA objmaster = new MasterDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);
            MasterDA objservice = new MasterDA(_configuration.GetConnectionString("KOPRODADBConnection").ToString(), DBType.MYSQL);
            List<EntSubscribeProdBO> objproduct = objmaster.GetProduct();
            foreach (var item in objproduct)
            {
                var res = objservice.GetProductDetails(Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                foreach (var items in res)
                {
                    var value = objmaster.GetLocation(items.TenantID);

                    foreach (var val in value)
                    {
                        var final = objmaster.GetDeviceID(val.TenantID,val.LocationID);

                        foreach (var output in final)
                        {
                            if(output.ExpiryDate <= DateTime.UtcNow.AddMinutes(5))
                            {
                                var result = objmaster.UpdateDeviceExpiryDate(output.DeviceID);
                            }
                        }
                    }
                }
            }
        }
        private static void LeaveReport()
        {
            MasterDA objmaster = new MasterDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);
            MasterDA objservice = new MasterDA(_configuration.GetConnectionString("KOPRODADBConnection").ToString(), DBType.MYSQL);
            List<EntSubscribeProdBO> objproduct = objmaster.GetProduct();
            foreach (var item in objproduct)
            {
                var res = objservice.GetProductDetails(Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                foreach (var items in res)
                {
                    var value = objmaster.GetLocation(items.TenantID);
                    foreach (var val in value)
                    {
                        objmaster.SaveLeaveReport(val.TenantID,val.LocationID);
                    }
                }
            }
        }
        private static void HRSalaryPeriod()
        {
            MasterDA objmaster = new MasterDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);
            MasterDA objservice = new MasterDA(_configuration.GetConnectionString("KOPRODADBConnection").ToString(), DBType.MYSQL);
            List<EntSubscribeProdBO> objproduct = objmaster.GetProduct();
            foreach (var item in objproduct)
            {
                var res = objservice.GetProductDetails(Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                foreach (var items in res)
                {
                    var value = objmaster.GetLocation(items.TenantID);
                    foreach (var val in value)
                    {
                        objmaster.SaveHRSalaryPeriod(val.TenantID,val.LocationID);
                    }
                }
            }
        }
        public static void GeneratePayroll()
        {
            MasterDA objmaster = new MasterDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);
            MasterDA objservice = new MasterDA(_configuration.GetConnectionString("KOPRODADBConnection").ToString(), DBType.MYSQL);
            List<EntSubscribeProdBO> objproduct = objmaster.GetProduct();
            SaveResult objsave = new SaveResult();
            foreach (var item in objproduct)
            {
                var res = objservice.GetProductDetails(Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                foreach (var items in res)
                {
                    var value = objmaster.GetLocation(items.TenantID);
                    foreach (var val in value)
                    {
                        var output = objmaster.GetYear(val.TenantID,val.LocationID);
                        foreach (var final in output)
                        {
                            var datas = objmaster.GetSalaryPeriod(final.TenantID,final.LocationID,final.YearID);
                            foreach (var result in datas)
                            {
                            using (HttpClientHandler clientHandler = new HttpClientHandler())
                            {
                                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                                using (var httpClient = new HttpClient(clientHandler))
                                {
                                   using (var request = new HttpRequestMessage(new HttpMethod("GET"), _configuration.GetSection("BaseURL").Value + "/api/Payroll/PayrollGenerateManually/"+result.SalaryPeriodID +"/" +result.TenantID +"/" +result.LocationID ))
                                    {
                                        var logresponse = httpClient.SendAsync(request).Result;
                                        var json = logresponse.Content.ReadAsStringAsync().Result;
                                        var jsondata = JObject.Parse(json);
                                        var data = jsondata["data"];
                                        objsave = JsonConvert.DeserializeObject<SaveResult>(data.ToString());
                                    }
                                }
                            }
                            }
                        }
                    }
                }
            }
        }
        private static void TimesheetReport()
        {
            MasterDA objmaster = new MasterDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);
            MasterDA objservice = new MasterDA(_configuration.GetConnectionString("KOPRODADBConnection").ToString(), DBType.MYSQL);
            List<TimesheetBO> objtime = new List<TimesheetBO>();
            List<EntSubscribeProdBO> objproduct = objmaster.GetProduct();
            foreach (var item in objproduct)
            {
                var res = objservice.GetProductDetails(Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                foreach (var items in res)
                {
                    var value = objmaster.GetLocation(items.TenantID);
                    foreach (var val in value)
                    {
                        var output = objmaster.GetYear(val.TenantID,val.LocationID);
                        foreach (var final in output)
                        {
                            objtime = objmaster.GetTimesheet(final.TenantID,final.YearID);
                            foreach (var item1 in objtime)
                            {
                                objmaster.SaveTimesheetReport(item1);
                            }
                        }
                    }
                }
            }
        }
         private static void GetTimesheetException()
        {
            MasterDA objmaster = new MasterDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);
            MasterDA objservice = new MasterDA(_configuration.GetConnectionString("KOPRODADBConnection").ToString(), DBType.MYSQL);
            List<EntSubscribeProdBO> objproduct = objmaster.GetProduct();
            List<TimeSheetException> loginReturn = new List<TimeSheetException>();
            foreach (var item in objproduct)
            {
                var res = objservice.GetProductDetails(Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                foreach (var items in res)
                {
                    var value = objmaster.GetLocation(items.TenantID);
                    foreach (var val in value)
                    {
                        using (HttpClientHandler clientHandler = new HttpClientHandler())
                        {
                            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                            using (var httpClient = new HttpClient(clientHandler))
                            {
                                using (var request = new HttpRequestMessage(new HttpMethod("GET"), _configuration.GetSection("BaseURL").Value + "/api/Timesheet/GetConsoleTimesheet/"+val.TenantID +"/" +val.LocationID ))
                                {
                                    var logresponse = httpClient.SendAsync(request).Result;
                                    var json = logresponse.Content.ReadAsStringAsync().Result;
                                    var jsondata = JObject.Parse(json);
                                    var data = jsondata["data"];
                                    loginReturn = JsonConvert.DeserializeObject<List<TimeSheetException>>(data.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }
        private static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
        }
        static public void writeToConsole(string msg, bool isDate = false, bool toFile = false)
         {
            if(isDate)
            {
            Console.Write(" " + DateTime.Now.ToString() + " -> " + msg);
            if (toFile)
            File.AppendAllText(AuditLog + "DodosLog" + DateTime.Now.ToString("yyyyMMdd") + ".txt", " " + DateTime.Now.ToString() + " -> " + msg + Environment.NewLine);
            }
            else
            {
            Console.Write(" " + msg);
            if (toFile)
           File.AppendAllText(AuditLog + "DodosLog" + DateTime.Now.ToString("yyyyMMdd") + ".txt", msg + Environment.NewLine);

            }
            Console.WriteLine();
        }
    }
}