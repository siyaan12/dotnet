using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Document;
using MyDodos.Domain.Mail;
using MyDodos.ViewModel.Document;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MyDodos.Repository.Mail
{
    public class MailRepository : IMailRepository
    {
        private readonly IConfiguration configuration;
        public MailRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public string GetTemplateContentDetail(string tplPath, List<MetaBO> oMetaData = null)
        {
            string rtnVal = string.Empty;
            var owners = (new WebClient()).DownloadData(tplPath);
            string data = Convert.ToBase64String(owners);
            var base64EncodedBytes = System.Convert.FromBase64String(data);
            rtnVal = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            if (oMetaData != null)
            {
                foreach (MetaBO oMeta in oMetaData)
                {
                    rtnVal = rtnVal.Replace(oMeta.MetaName, oMeta.MetaValue);
                }
            }
            return rtnVal;
        }
        public List<MailNotifyBO> GetMailReportName(int EmpID)
        {
            List<MailNotifyBO> result = new List<MailNotifyBO>();
            DynamicParameters dyParam = new DynamicParameters();
            var conn = this.GetConnection();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMailReportName";

                result = SqlMapper.Query<MailNotifyBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();

            }
            conn.Close();
            conn.Dispose();
            return result;

        }
        public async Task<int> SendMail(MailModel notification)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("EmailHookUrl").Value))
                {
                    string SendResult = JsonConvert.SerializeObject(notification);
                    request.Content = new StringContent(SendResult);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = await httpClient.SendAsync(request);
                    var json = await response.Content.ReadAsStringAsync();
                    var jsons = JsonConvert.DeserializeObject<int>(json);
                    return jsons;
                }
            }
        }
    }
}   
    
