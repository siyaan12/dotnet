using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using System.Net;
using MyDodos.Domain.Wrapper;
using MySql.Data.MySqlClient;
using KoSoft.DocTemplate;

namespace MyDodos.Repository.TemplateManager
{
    public class DocRepository : IDocRepository
    {
        private readonly IConfiguration _configuration;
        public DocRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        // public Response<List<DocRepositoryBO>> GetDocRepository(int TenantID, int OwnerID, string OwnerType, int ProductID, string LocType)
        // {
        //     Response<List<DocRepositoryBO>> response;
        //     List<DocRepositoryBO> loginReturn = new List<DocRepositoryBO>();
        //     try
        //     {
        //         using (HttpClientHandler clientHandler = new HttpClientHandler())
        //         {
        //             clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        //             using (var httpClient = new HttpClient(clientHandler))
        //             {
        //                 using (var request = new HttpRequestMessage(new HttpMethod("GET"),  _configuration.GetSection("TemplateMgmtUrl").Value + "/api/template/GetDocRepository/"+ TenantID +"/"+ OwnerID +"/"+ OwnerType +"/"+ ProductID +"/"+ LocType))
        //                 {
        //                     var logresponse = httpClient.SendAsync(request).Result;
        //                     var json = logresponse.Content.ReadAsStringAsync().Result;
        //                     var jsondata = JObject.Parse(json);
        //                     var data = jsondata["data"];
        //                     loginReturn = JsonConvert.DeserializeObject<List<DocRepositoryBO>>(data.ToString());
        //                     if (loginReturn != null)
        //                     {
        //                         response = new Response<List<DocRepositoryBO>>(loginReturn, 200);
        //                     }
        //                     else
        //                     {
        //                         response = new Response<List<DocRepositoryBO>>(null, 500, "Data Not Found");
        //                     }
        //                 }
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         response = new Response<List<DocRepositoryBO>>(ex.Message, 500);
        //     }
        //     return response;
        // }
        // public Response<List<TemplateMgmt>> GetTemplatesByTenant(int TenantID, int ProductID, string RepoName, string RepoType)
        // {
        //     Response<List<TemplateMgmt>> response;
        //     List<TemplateMgmt> loginReturn = new List<TemplateMgmt>();
        //     try
        //     {
        //         using (HttpClientHandler clientHandler = new HttpClientHandler())
        //         {
        //             clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        //             using (var httpClient = new HttpClient(clientHandler))
        //             {
        //                 using (var request = new HttpRequestMessage(new HttpMethod("GET"),  _configuration.GetSection("TemplateMgmtUrl").Value + "/api/template/GetTemplatesByTenant/"+ 
        //         TenantID +"/"+ ProductID +"/"+ RepoName +"/"+ RepoType ))
        //                 {
        //                     var logresponse = httpClient.SendAsync(request).Result;
        //                     var json = logresponse.Content.ReadAsStringAsync().Result;
        //                     var jsondata = JObject.Parse(json);
        //                     var data = jsondata["data"];
        //                     loginReturn = JsonConvert.DeserializeObject<List<TemplateMgmt>>(data.ToString());
        //                     if (loginReturn != null)
        //                     {
        //                         response = new Response<List<TemplateMgmt>>(loginReturn, 200);
        //                     }
        //                     else
        //                     {
        //                         response = new Response<List<TemplateMgmt>>(null, 500, "Data Not Found");
        //                     }
        //                 }
        //             }

        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         response = new Response<List<TemplateMgmt>>(ex.Message, 500);
        //     }
        //     return response;
        // }
        // public Response<List<TagDataQueryBO>> GetMetatag(int ProductID,int TemplateID, int RequestID, string LeaveType)
        // {
        //     Response<List<TagDataQueryBO>> response;
        //     List<TagDataQueryBO> loginReturn = new List<TagDataQueryBO>();
        //     try
        //     {
        //         using (HttpClientHandler clientHandler = new HttpClientHandler())
        //         {
        //             clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        //             using (var httpClient = new HttpClient(clientHandler))
        //             {
        //                 using (var request = new HttpRequestMessage(new HttpMethod("GET"),  _configuration.GetSection("TemplateMgmtUrl").Value + "/api/template/GetMetatag/"+ ProductID +"/"+ TemplateID +"/"+ RequestID +"/"+ LeaveType))
        //                 {
        //                     var logresponse = httpClient.SendAsync(request).Result;
        //                     var json = logresponse.Content.ReadAsStringAsync().Result;
        //                     var jsondata = JObject.Parse(json);
        //                     var data = jsondata["data"];
        //                     loginReturn = JsonConvert.DeserializeObject<List<TagDataQueryBO>>(data.ToString());
        //                     if (loginReturn != null)
        //                     {
        //                         response = new Response<List<TagDataQueryBO>>(loginReturn, 200, "Data Retrived");
        //                     }
        //                     else
        //                     {
        //                         response = new Response<List<TagDataQueryBO>>(null, 500, "Data Not Found");
        //                     }
        //                 }
        //             }

        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         response = new Response<List<TagDataQueryBO>>(ex.Message, 500);
        //     }
        //     return response;
        // }
        // public Response<List<TemplateTagBO>> GetTemplateTagValues(List<TagDataQueryBO> oTagQuery)
        // {
        //     Response<List<TemplateTagBO>> response;
        //     List<TemplateTagBO> tg = new List<TemplateTagBO>();
        //     MySqlConnection sqlCon = null;
        //     try
        //     {
        //         string sTmpVal = string.Empty;

        //         foreach (TagDataQueryBO oQry in oTagQuery)
        //         {
        //             sqlCon = new MySqlConnection(_configuration.GetConnectionString("KOHAMSADBConnection"));
        //             using (MySqlCommand cmd = new MySqlCommand(oQry.DBQuery, sqlCon))
        //             {
        //                 sqlCon.Open();
        //                 cmd.Parameters.Clear();
        //                 foreach (string q1 in oQry.ConditionFields)
        //                 {
        //                     cmd.Parameters.Add(new MySqlParameter("@" + q1, oQry.oQueryParamValue[q1])); //  oQueryParamValue[q1]));
        //                 }
        //                 if (oQry.dbType == "SP")
        //                     cmd.CommandType = CommandType.StoredProcedure;
        //                 else
        //                     cmd.CommandType = CommandType.Text;
        //                 MySqlDataReader reader = cmd.ExecuteReader();
        //                 if (reader.HasRows)
        //                 {
        //                     reader.Read();
        //                     for (int idx = 0; idx < reader.FieldCount; idx++)
        //                     {
        //                         tg.Add(new TemplateTagBO { TagName = reader.GetName(idx), TagValue = Convert.ToString(reader.GetValue(idx)) });
        //                     }
        //                 }
        //                 else
        //                 {
        //                     foreach (string ff in oQry.FieldList)
        //                     {
        //                         tg.Add(new TemplateTagBO { TagName = ff, TagValue = " " });
        //                     }
        //                 }
        //                 reader.Close();
        //             }
        //         }
        //         response = new Response<List<TemplateTagBO>>(tg, 200, "Data Retrived");
        //     }
        //     catch (Exception ex)
        //     {
        //         response = new Response<List<TemplateTagBO>>(ex.Message, 500);
        //     }
        //     return response;
        // }
        // public int SaveDocument(GenDocumentBO d1)
        // {
        //     int rtnval = 0;
        //     DynamicParameters dyParam = new DynamicParameters();
        //     dyParam.Add("@Doc_ID", d1.DocID, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@repository_Id", d1.RepositoryID, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@document_Name", d1.DocumentName, DbType.String, ParameterDirection.Input);
        //     dyParam.Add("@doc_Type", d1.DocType, DbType.String, ParameterDirection.Input);
        //     dyParam.Add("@orgDoc_Name", d1.OrgDocName, DbType.String, ParameterDirection.Input);
        //     dyParam.Add("@genDoc_Name", d1.GenDocName, DbType.String, ParameterDirection.Input);
        //     dyParam.Add("@en_tity", d1.Entity, DbType.String, ParameterDirection.Input);
        //     dyParam.Add("@doc_Key", d1.DocKey, DbType.String, ParameterDirection.Input);
        //     dyParam.Add("@doc_Size", d1.DocSize, DbType.Decimal, ParameterDirection.Input);
        //     dyParam.Add("@entity_Id", d1.EntityID, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@tenant_Id", d1.TenantID, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@DocType_ID", d1.DocTypeID, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@direction_Path", d1.DirectionPath, DbType.String, ParameterDirection.Input);
        //     dyParam.Add("@Created_By", d1.CreatedBy, DbType.Int32, ParameterDirection.Input);
            
        //     var conn = this.GetConnection();
        //     if (conn.State == ConnectionState.Closed)
        //     {
        //         conn.Open();
        //     }
        //     if (conn.State == ConnectionState.Open)
        //     {
        //         var query = "sp_SaveHRDoc";
        //         rtnval = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //     }
        //     conn.Close();
        //     conn.Dispose();            
        //     return rtnval;            

        // }
        // public List<GenDocumentBO> GetDocument(int docId,int entityId,string docKey,string entity,int tenantId)
        // {
        //     List<GenDocumentBO> rtnval = new List<GenDocumentBO>();
        //     DynamicParameters dyParam = new DynamicParameters();
        //     dyParam.Add("@docId", docId, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@entityId", entityId, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@docKey", docKey, DbType.String, ParameterDirection.Input);
        //     dyParam.Add("@entity", entity, DbType.String, ParameterDirection.Input);
        //     dyParam.Add("@tenantId", tenantId, DbType.Int32, ParameterDirection.Input);
        //     var conn = this.GetConnection();
        //     if (conn.State == ConnectionState.Closed)
        //     {
        //         conn.Open();
        //     }
        //     if (conn.State == ConnectionState.Open)
        //     {
        //         var query = "sp_Getdocinfo";
        //         rtnval = SqlMapper.Query<GenDocumentBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
        //     }
        //     conn.Close();
        //     conn.Dispose();
        //     return rtnval;
        // }
        // public int DeleteDocument(int docId)
        // {
        //     int rtnval = 0;
        //     DynamicParameters dyParam = new DynamicParameters();
        //     dyParam.Add("@docId", docId, DbType.Int32, ParameterDirection.Input);
           

        //     var conn = this.GetConnection();
        //     if (conn.State == ConnectionState.Closed)
        //     {
        //         conn.Open();
        //     }
        //     if (conn.State == ConnectionState.Open)
        //     {
        //         var query = "sp_Deletedocinfo";
        //         SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //         rtnval = docId;
        //     }
        //     conn.Close();
        //     conn.Dispose();
        //     return rtnval;
        // }
        // public string GetTemplateContentDetail(string tplPath, List<MetaBO> oMetaData = null)
        // {
        //     string rtnVal = string.Empty;
        //     var owners = (new WebClient()).DownloadData(tplPath);
        //     string data = Convert.ToBase64String(owners);
        //     var base64EncodedBytes = System.Convert.FromBase64String(data);
        //     rtnVal = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        //     if (oMetaData != null)
        //     {
        //         foreach (MetaBO oMeta in oMetaData)
        //         {
        //             rtnVal = rtnVal.Replace(oMeta.MetaName, oMeta.MetaValue);
        //         }
        //     }
        //     return rtnVal;
        // }
        public Response<List<TemplateTagBO>> GetTemplateTagValues(List<TagDataQueryBO> oTagQuery)
        {
            Response<List<TemplateTagBO>> response;
            List<TemplateTagBO> tg = new List<TemplateTagBO>();
            MySqlConnection sqlCon = null;
            try
            {
                string sTmpVal = string.Empty;

                foreach (TagDataQueryBO oQry in oTagQuery)
                {
                    sqlCon = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
                    using (MySqlCommand cmd = new MySqlCommand(oQry.DBQuery, sqlCon))
                    {
                        sqlCon.Open();
                        cmd.Parameters.Clear();
                        foreach (string q1 in oQry.ConditionFields)
                        {
                            cmd.Parameters.Add(new MySqlParameter("@" + q1, oQry.oQueryParamValue[q1])); //  oQueryParamValue[q1]));
                        }
                        if (oQry.dbType == "SP")
                            cmd.CommandType = CommandType.StoredProcedure;
                        else
                            cmd.CommandType = CommandType.Text;
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            for (int idx = 0; idx < reader.FieldCount; idx++)
                            {
                                tg.Add(new TemplateTagBO { TagName = reader.GetName(idx), TagValue = Convert.ToString(reader.GetValue(idx)) });
                            }
                        }
                        else
                        {
                            foreach (string ff in oQry.FieldList)
                            {
                                tg.Add(new TemplateTagBO { TagName = ff, TagValue = " " });
                            }
                        }
                        reader.Close();
                    }
                }
                response = new Response<List<TemplateTagBO>>(tg, 200, "Data Retrived");
            }
            catch (Exception ex)
            {
                response = new Response<List<TemplateTagBO>>(ex.Message, 500);
            }
            return response;
        }
        // public string GetTemplateWithContent(string TplFileFullPath, List<TemplateTagBO> oTagValues)
        // {
        //     string rtnText = string.Empty;

        //     //if (File.Exists(TplFileFullPath))
        //     //{
                
        //     var owners = (new WebClient()).DownloadData(TplFileFullPath);
        //     string data = Convert.ToBase64String(owners);

        //     var base64EncodedBytes = System.Convert.FromBase64String(data);
        //    string data1=  System.Text.Encoding.UTF8.GetString(base64EncodedBytes);


        //     // rtnText = Url.ReadAllText(TplFileFullPath);

        //     List<TemplateTagBO> rpTags = oTagValues.FindAll(x => x.RepeatTagName != "");
        //     if (rpTags.Count > 0)
        //     {
        //         string cpFileText = data1;
        //         string spSubText = string.Empty;
        //         int rpStart; int rpEnd;

        //         var RepeatVal = rpTags.Select(x => x.RepeatTagName).Distinct();
        //         if (RepeatVal!=null)
        //         {
        //             foreach (string rpVal in RepeatVal)
        //             {
        //                 if (!string.IsNullOrEmpty(rpVal))
        //                 {
        //                     rpStart = cpFileText.IndexOf(rpVal);
        //                     rpEnd = cpFileText.IndexOf(rpVal, rpStart + rpVal.Length);
        //                     spSubText = cpFileText.Substring(rpStart + rpVal.Length, rpEnd - (rpStart + rpVal.Length));
        //                     cpFileText = cpFileText.Substring(0, rpStart) + "_" + rpVal + "_" + cpFileText.Substring(rpEnd + rpVal.Length);
        //                     string s1 = SetRepeatItemValues(rpTags.FindAll(y => y.RepeatTagName == rpVal), spSubText);
        //                     rtnText = cpFileText.Replace("_" + rpVal + "_", s1);
        //                 }
        //             }
                    
        //         }
        //         if (string.IsNullOrEmpty( rtnText))
        //         {
        //             rtnText = cpFileText;
        //         }               
               
        //         foreach (TemplateTagBO Tag in oTagValues)
        //         {
        //             rtnText = rtnText.Replace(Tag.TagName, Tag.TagValue);
        //         }
        //     }
        //     else
        //     {
        //         rtnText = "Template file missing or invalid path";
        //     }
        //     return rtnText;
        // }
        // private string SetRepeatItemValues(List<TemplateTagBO> oRpValues, string rpString)
        // {
        //     string rtnVal = string.Empty;

        //     var RepeatVal = oRpValues.Select(x => x.RepeatTagRowNumber).Distinct();
        //     foreach (int rpVal in RepeatVal)
        //     {
        //         if (rpVal > 0)
        //         {
        //             List<TemplateTagBO> rpTags = oRpValues.FindAll(x => x.RepeatTagRowNumber == rpVal);

        //             string rpString1 = rpString;

        //             foreach (TemplateTagBO Tag in rpTags)
        //             {
        //                 rpString1 = rpString1.Replace(Tag.TagName, Tag.TagValue);
        //             }
        //             rtnVal = rtnVal + rpString1;
        //         }
        //     }
        //     return rtnVal;
        // }
        // public List<DocContainerBO> GetDocContainer(int tenantId,int productId)
        // {
        //     List<DocContainerBO> rtnval = new List<DocContainerBO>();
        //     DynamicParameters dyParam = new DynamicParameters();
        //     dyParam.Add("@tenantId", tenantId, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@productId", productId, DbType.Int32, ParameterDirection.Input);
            
        //     var conn = this.GetConnection();
        //     if (conn.State == ConnectionState.Closed)
        //     {
        //         conn.Open();
        //     }
        //     if (conn.State == ConnectionState.Open)
        //     {
        //         var query = "sp_GetDocContanier";
        //         rtnval = SqlMapper.Query<DocContainerBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
        //     }
        //     conn.Close();
        //     conn.Dispose();
        //     return rtnval;
        // }
    }
}
