using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.ProjectManagement;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.ProjectManagement;
using MySql.Data.MySqlClient;

namespace MyDodos.Repository.ProjectManagement
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IConfiguration _configuration;
        public ProjectRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public LeaveRequestModelMsg SavePPSponsor(PPSponsorBO sponsor)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@sponsorId",sponsor.SponsorID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@sponsorName",sponsor.SponsorName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@shortName",sponsor.ShortName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@description",sponsor.Description,DbType.String,ParameterDirection.Input);
            dyParam.Add("@clientStatus",sponsor.ClientStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@tenantId",sponsor.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",sponsor.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",sponsor.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SavePPSponsor";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<PPSponsorBO> GetPPSponsor(int TenantID,int LocationID,int SponsorID)
        {
            List<PPSponsorBO> objacademic = new List<PPSponsorBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@sponsorId",SponsorID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPPSponsor";
                objacademic = SqlMapper.Query<PPSponsorBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objacademic;
        }
        public LeaveRequestModelMsg DeletePPSponsor(int SponsorID)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@sponsorId",SponsorID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeletePPSponsor";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();         
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public LeaveRequestModelMsg SavePPInitiativeType(PPInitiativeTypeBO type)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@initiativeTypeId",type.InitiativeTypeID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@initiativeType",type.InitiativeType,DbType.String,ParameterDirection.Input);
            dyParam.Add("@description",type.Description,DbType.String,ParameterDirection.Input);
            dyParam.Add("@initiativeTypeStatus",type.InitiativeTypeStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@isBillable",type.IsBillable,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@tenantId",type.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",type.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",type.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SavePPInitiativeType";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<PPInitiativeTypeBO> GetPPInitiativeType(int TenantID,int LocationID,int InitiativeTypeID)
        {
            List<PPInitiativeTypeBO> objacademic = new List<PPInitiativeTypeBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@initiativeTypeId",InitiativeTypeID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPPInitiativeType";
                objacademic = SqlMapper.Query<PPInitiativeTypeBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objacademic;
        }
        public LeaveRequestModelMsg DeletePPInitiativeType(int InitiativeTypeID)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@initiativeTypeId",InitiativeTypeID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeletePPInitiativeType";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();         
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public LeaveRequestModelMsg SaveNewProject(PPProjectBO project)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@projectId",project.ProjectID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectNo",project.ProjectNo,DbType.String,ParameterDirection.Input);
            dyParam.Add("@projectName",project.ProjectName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@projShortName",project.ProjShortName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@initiativeTypeId",project.InitiativeTypeID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@isBillable",project.IsBillable,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@billableName",project.BillableName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@estStartDate",project.EstStartDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@estEndDate",project.EstEndDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@yearId",project.YearID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@priority",project.Priority,DbType.String,ParameterDirection.Input);
            dyParam.Add("@description",project.Description,DbType.String,ParameterDirection.Input);
            dyParam.Add("@projStatus",project.ProjStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@completionMode",project.CompletionMode,DbType.String,ParameterDirection.Input);
            //dyParam.Add("@projectFrom",project.ProjectFrom,DbType.String,ParameterDirection.Input);
            dyParam.Add("@tenantId",project.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",project.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",project.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveNewProject";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public LeaveRequestModelMsg DeleteProject(int ProjectID)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteProject";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();         
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public LeaveRequestModelMsg SaveProject(PPProjectBO project)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@projectId",project.ProjectID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@isBillable",project.IsBillable,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@isClientProject",project.IsClientProject,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@billableName",project.BillableName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@managerId",project.ManagerID,DbType.String,ParameterDirection.Input);
            dyParam.Add("@sponsorId",project.SponsorID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@sponsor",project.Sponsor,DbType.String,ParameterDirection.Input);
            dyParam.Add("@executiveSponsor",project.ExecutiveSponsor,DbType.String,ParameterDirection.Input);
            dyParam.Add("@estStartDate",project.EstStartDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@estEndDate",project.EstEndDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@actStartDate",project.ActStartDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@actEndDate",project.ActEndDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@compPercent",project.CompPercent,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@projStatus",project.ProjStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@tenantId",project.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",project.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",project.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveProjectDetail";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<vwProjectDetails> GetProjectDetails(int TenantID,int LocationID,int ProjectID)
        {
            List<vwProjectDetails> objproject = new List<vwProjectDetails>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectDetails";
                objproject = SqlMapper.Query<vwProjectDetails>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objproject;
        }
        public List<vwProjectDetailList> GetProjectDetailList(int TenantID,int LocationID,int ProjectID)
        {
            List<vwProjectDetailList> objproject = new List<vwProjectDetailList>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectDetails";
                objproject = SqlMapper.Query<vwProjectDetailList>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objproject;
        }
        public GetProjectList GetProjectData(GetProjectList inputParam)
        {
            GetProjectList objproject = new GetProjectList();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenentId", inputParam.objProjectListInput.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", inputParam.objProjectListInput.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@projStatus", inputParam.objProjectListInput.ProjStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@search_data", inputParam.ServerSearchables.search_data, DbType.String, ParameterDirection.Input);
            dyParam.Add("@page_No", inputParam.ServerSearchables.page_No, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@page_Size", inputParam.ServerSearchables.page_Size, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@orderBy_Column", inputParam.ServerSearchables.orderBy_Column, DbType.String, ParameterDirection.Input);
            dyParam.Add("@order_By", inputParam.ServerSearchables.order_By, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectDataSearch";
                var data= SqlMapper.Query<ProjectListBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                objproject.objProjectList = data;
                objproject.objProjectListInput = inputParam.objProjectListInput;
                objproject.ServerSearchables = inputParam.ServerSearchables;
                if(objproject.objProjectList.Count>0)
                {
                    objproject.ServerSearchables.RecordsTotal = objproject.objProjectList[0].TotalCount;
                    objproject.ServerSearchables.RecordsFiltered = objproject.objProjectList.Count();
                }
            }
            if(objproject.objProjectList.Count > 0)
            {
                foreach(var objteam in objproject.objProjectList)
                {
                    //if (objteam.ManagerID!=null && objteam.ManagerID!="")
                    //{
                        objteam.ManagerList = GetProjectManagerList(objteam.TenantID,objteam.ProjectID);
                    //}
                    objteam.TeamList = GetProjectMemberDetails(objteam.TenantID,objteam.LocationID,objteam.ProjectID,objteam.GetDateTime);
                }
            }
            conn.Close();
            conn.Dispose();
            return objproject;
        }
        public Percentage GetProjectPercentage(int ProjectID)
        {
            Percentage objproject = new Percentage();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectPercentage";
                objproject = SqlMapper.Query<Percentage>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objproject;
        }
        public LeaveRequestModelMsg SaveProjectTeamMember(PPProjectTeam project)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@projTeamMemId",project.ProjTeamMemID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectId",project.ProjectID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",project.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@deptId",project.DeptID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@roleId",project.RoleID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@isProjectManager",project.IsProjectManager,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@roleRate",project.RoleRate,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@assignedStatus",project.AssignedStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@assignStart",project.AssignStart,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@assignEnd",project.AssignEnd,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@tenantId",project.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",project.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",project.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SavePPProjectTeam";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<GetDepartmentdropdowm> GetProjectMemberDropDown(int TenantID,int LocationID,int DeptID,int EmpID)
        {
            List<GetDepartmentdropdowm> objproject = new List<GetDepartmentdropdowm>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@deptId",DeptID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",EmpID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectMemberDropDown";
                objproject = SqlMapper.Query<GetDepartmentdropdowm>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objproject;
        }
        public List<GetResourceList> GetProjectResourceDropDown(int TenantID,int LocationID,int DeptID,int EmpID)
        {
            List<GetResourceList> objproject = new List<GetResourceList>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@deptId",DeptID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",EmpID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectMemberDropDown";
                objproject = SqlMapper.Query<GetResourceList>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objproject;
        }
        // public List<GetRoleList> GetRoleDropDown(int TenantID,int LocationID,int DeptID,int EmpID)
        // {
        //     List<GetRoleList> objproject = new List<GetRoleList>();
        //     DynamicParameters dyParam = new DynamicParameters();
        //     dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
        //     dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
        //     dyParam.Add("@deptId",DeptID,DbType.Int32,ParameterDirection.Input);
        //     dyParam.Add("@empId",EmpID,DbType.Int32,ParameterDirection.Input);
        //     var conn = this.GetConnection();
        //     if (conn.State == ConnectionState.Closed)
        //     {
        //         conn.Open();
        //     }
        //     if (conn.State == ConnectionState.Open)
        //     {
        //         var query = "sp_GetProjectMemberDropDown";
        //         objproject = SqlMapper.Query<GetRoleList>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
        //     }
        //     conn.Close();
        //     conn.Dispose();
        //     return objproject;
        // }
        public List<ProjectMemberDetailBO> GetProjectMemberDetails(int TenantID,int LocationID,int ProjectID,DateTime GetDateTime)
        {
            List<ProjectMemberDetailBO> objproject = new List<ProjectMemberDetailBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectMemberDetails";
                objproject = SqlMapper.Query<ProjectMemberDetailBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objproject;
        }
        public List<ManagerList> GetProjectManagerList(int TenantID,int ProjectID)
        {
            List<ManagerList> objproject = new List<ManagerList>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectManagerList";
                objproject = SqlMapper.Query<ManagerList>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objproject;
        }
        public List<ProjectManagerDropDownBO> GetProjectManager(int TenantID,int LocationID,int ProjectID)
        {
            List<ProjectManagerDropDownBO> objproject = new List<ProjectManagerDropDownBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectManagerDropDown";
                objproject = SqlMapper.Query<ProjectManagerDropDownBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objproject;
        }
        public LeaveRequestModelMsg SaveProjectTask(PPProjectTaskBO task)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@pTaskId",task.PTaskID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@taskName",task.TaskName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@taskDescription",task.TaskDescription,DbType.String,ParameterDirection.Input);
            dyParam.Add("@isTimeTrack",task.IsTimeTrack,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@isBillable",task.IsBillable,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@projectId",task.ProjectID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@mileStoneId",task.MileStoneID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@estStartDate",task.EstStartDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@estEndDate",task.EstEndDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@actStartDate",task.ActStartDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@actEndDate",task.ActEndDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@estEffort",task.EstEffort,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@actEffort",task.ActEffort,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@taskStatus",task.TaskStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@taskPriority",task.TaskPriority,DbType.String,ParameterDirection.Input);
            dyParam.Add("@isProjEstDateExten",task.IsProjEstDateExten,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@projEstEndDate",task.ProjEstEndDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@referenceNo",task.ReferenceNo,DbType.String,ParameterDirection.Input);
            dyParam.Add("@tenantId",task.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",task.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",task.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveProjectTask";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<PPProjectTaskBO> GetProjectTask(int TenantID,int LocationID,int ProjectID,int MileStoneID)
        {
            List<PPProjectTaskBO> objtask = new List<PPProjectTaskBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@mileStoneId",MileStoneID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectTask";
                objtask = SqlMapper.Query<PPProjectTaskBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
        public LeaveRequestModelMsg SaveMileStone(MileStoneBO milestone)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@mileStoneId",milestone.MileStoneID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@mileStoneName",milestone.MileStoneName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@description",milestone.Description,DbType.String,ParameterDirection.Input);
            dyParam.Add("@startDate",milestone.StartDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@endDate",milestone.EndDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@percentage",milestone.Percentage,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@mileStoneComments",milestone.MileStoneComments,DbType.String,ParameterDirection.Input);
            dyParam.Add("@mileStoneStatus",milestone.MileStoneStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@projectId",milestone.ProjectID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tenantId",milestone.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",milestone.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",milestone.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveMileStone";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<MileStoneBO> GetMileStone(int TenantID,int LocationID,int ProjectID)
        {
            List<MileStoneBO> objtask = new List<MileStoneBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMileStone";
                objtask = SqlMapper.Query<MileStoneBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
        public List<ProjectAssignedMember> GetPTaskAssignedMembers(int TenantID,int LocationID,int PTaskID)
        {
            List<ProjectAssignedMember> objtask = new List<ProjectAssignedMember>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@pTaskId",PTaskID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPTaskAssignedMembers";
                objtask = SqlMapper.Query<ProjectAssignedMember>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
        public LeaveRequestModelMsg SaveProjectTaskHistory(PPProjectTaskHistoryBO taskHistory)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@pTaskHisId",taskHistory.PTaskHisID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@pTaskId",taskHistory.PTaskID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@actEffort",taskHistory.ActEffort,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@tenantId",taskHistory.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",taskHistory.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",taskHistory.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveProjectTaskHistory";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public LeaveRequestModelMsg SaveProjectRole(PPProjectRoleBO role)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@projectRoleId",role.ProjectRoleID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectRole",role.ProjectRole,DbType.String,ParameterDirection.Input);
            dyParam.Add("@description",role.Description,DbType.String,ParameterDirection.Input);
            dyParam.Add("@roleStatus",role.RoleStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@tenantId",role.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",role.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",role.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveProjectRole";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<PPProjectRoleBO> GetProjectRole(int TenantID,int LocationID,int ProjectRoleID)
        {
            List<PPProjectRoleBO> objtask = new List<PPProjectRoleBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectRoleId",ProjectRoleID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectRole";
                objtask = SqlMapper.Query<PPProjectRoleBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
        public LeaveRequestModelMsg DeleteProjectRole(int ProjectRoleID)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@projectRoleId",ProjectRoleID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeletePPProjectRole";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();         
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public LeaveRequestModelMsg SaveProjectTaskResource(PPTaskResourceBO task)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@projTaskResId",task.ProjTaskResID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@pTaskId",task.PTaskID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",task.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectId",task.ProjectID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@isProjTask",task.IsProjTask,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@taskStatus",task.TaskStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@tenantId",task.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",task.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",task.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveProjectTaskResource";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<TaskExtenDate> GetTaskExtendate(int TenantID,int ProjectID)
        {
            List<TaskExtenDate> objproject = new List<TaskExtenDate>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTaskExtendate";
                objproject = SqlMapper.Query<TaskExtenDate>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objproject;
        }
        public List<FinancialByProjectRole> GetFinancialByProjectRole(int ProjectID, int TenantID)
        {
            List<FinancialByProjectRole> objtask = new List<FinancialByProjectRole>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetFinancialByProjectRole";
                objtask = SqlMapper.Query<FinancialByProjectRole>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
        public List<FinancialByProjectRole> GetFinancialByProjectMemberRole(int ProjectID, int TenantID)
        {
            List<FinancialByProjectRole> objtask = new List<FinancialByProjectRole>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetFinancialByProjectMemberRole";
                objtask = SqlMapper.Query<FinancialByProjectRole>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
        public LeaveRequestModelMsg UpdateProjectRoleAmount(PPProjectTeam project)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@projectId",project.ProjectID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",project.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@roleId",project.RoleID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@paidAmount",project.PaidAmount,DbType.Decimal,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_UpdateProjectRoleAmount";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<PPProjectListBO> GetProjectLists(int TenantID, int LocationID,int ProjectID)
        {
            List<PPProjectListBO> objtask = new List<PPProjectListBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectLists";
                objtask = SqlMapper.Query<PPProjectListBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
        public LeaveRequestModelMsg SaveProjectDocumentType(PPProjectDocumentTypeBO type)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@documentTypeId",type.DocumentTypeID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@documentType",type.DocumentType,DbType.String,ParameterDirection.Input);
            dyParam.Add("@description",type.Description,DbType.String,ParameterDirection.Input);
            dyParam.Add("@projectId",type.ProjectID,DbType.String,ParameterDirection.Input);
            dyParam.Add("@documentTypeStatus",type.DocumentTypeStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@tenantId",type.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",type.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",type.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SavePPProjectDocumentType";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<PPProjectDocumentTypeBO> GetProjectDocumentType(int TenantID,int LocationID,int DocumentTypeID,int ProjectID)
        {
            List<PPProjectDocumentTypeBO> rtnval = new List<PPProjectDocumentTypeBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@documentTypeId",DocumentTypeID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPPProjectDocumentType";
                rtnval = SqlMapper.Query<PPProjectDocumentTypeBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public PPProjectDocumentTypeBO GetProjectDocument(int TenantID,int LocationID,int DocumentTypeID,int ProjectID,int ProductID)
        {
            PPProjectDocumentTypeBO rtnval = new PPProjectDocumentTypeBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@documentTypeId",DocumentTypeID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPPProjectDocumentType";
                rtnval = SqlMapper.Query<PPProjectDocumentTypeBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public LeaveRequestModelMsg DeletePPProjectDocumentType(int DocumentTypeID)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@documentTypeId",DocumentTypeID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeletePPProjectDocumentType";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();         
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public MileStonePercentage GetMileSoneTaskPercentage(int ProjectID,int MileStoneID)
        {
            MileStonePercentage objproject = new MileStonePercentage();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@mileStoneId",MileStoneID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMileStonePercentage";
                objproject = SqlMapper.Query<MileStonePercentage>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objproject;
        }
        public LeaveRequestModelMsg UpdateProjectEstEndDate(vwProjectDetailList project)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@estEndDate",project.EstEndDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@projectId",project.ProjectID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tenantId",project.TenantID,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_UpdateProjectExtenDate";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public LeaveRequestModelMsg UpdateRemoveTaskMembers(PPTaskResourceBO task)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@projectId",task.ProjectID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@taskId",task.PTaskID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empIds",task.EmpIDs,DbType.String,ParameterDirection.Input);
            dyParam.Add("@taskStatus",task.TaskStatus,DbType.String,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_UpdateRemoveTaskMembers";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public LeaveRequestModelMsg DeleteProjectMileStone(int MileStone)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@mileStoneId",MileStone,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteProjectMileStone";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();         
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public LeaveRequestModelMsg DeleteProjectTask(int PTaskID)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@pTaskId",PTaskID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteProjectTask";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();         
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<ProjectReportBO> GetProjectReport(int TenantID, int LocationID,int ProjectID)
        {
            List<ProjectReportBO> objtask = new List<ProjectReportBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectReport";
                objtask = SqlMapper.Query<ProjectReportBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
        public List<ProjectTaskReportBO> GetProjectTaskReport(int TenantID, int LocationID,int ProjectID)
        {
            List<ProjectTaskReportBO> objtask = new List<ProjectTaskReportBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectTaskReport";
                objtask = SqlMapper.Query<ProjectTaskReportBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
        public List<PPProjectPaymentHistoryBO> GetPPProjectPaymentHistory(int TenantID, int LocationID,int EmpID)
        {
            List<PPProjectPaymentHistoryBO> objtask = new List<PPProjectPaymentHistoryBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",EmpID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPPProjectPaymentHistory";
                objtask = SqlMapper.Query<PPProjectPaymentHistoryBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
        public LeaveRequestModelMsg SaveProjectPaymentHistory(PPProjectPaymentHistoryBO project)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@paymentId",project.PaymentID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@timeSheetId",project.TimeSheetID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@weekNo",project.WeekNo,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",project.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@paymentAmount",project.PaymentAmount,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@paymentStatus",project.PaymentStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@yearId",project.YearID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tenantId",project.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",project.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",project.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SavePPProjectPaymentHistory";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public LeaveRequestModelMsg SaveNewWorkGroupProject(PPProjectBO project)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@projectId",project.ProjectID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectNo",project.ProjectNo,DbType.String,ParameterDirection.Input);
            dyParam.Add("@projectName",project.ProjectName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@projShortName",project.ProjShortName,DbType.String,ParameterDirection.Input);
            //dyParam.Add("@initiativeTypeId",project.InitiativeTypeID,DbType.Int32,ParameterDirection.Input);
            //dyParam.Add("@isBillable",project.IsBillable,DbType.Boolean,ParameterDirection.Input);
            //dyParam.Add("@billableName",project.BillableName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@estStartDate",project.EstStartDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@estEndDate",project.EstEndDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@managerId",project.ManagerID,DbType.String,ParameterDirection.Input);
            dyParam.Add("@yearId",project.YearID,DbType.Int32,ParameterDirection.Input);
            //dyParam.Add("@priority",project.Priority,DbType.String,ParameterDirection.Input);
            dyParam.Add("@description",project.Description,DbType.String,ParameterDirection.Input);
            dyParam.Add("@projStatus",project.ProjStatus,DbType.String,ParameterDirection.Input);
            //dyParam.Add("@projectFrom",project.ProjectFrom,DbType.String,ParameterDirection.Input);
            //dyParam.Add("@completionMode",project.CompletionMode,DbType.String,ParameterDirection.Input);
            dyParam.Add("@tenantId",project.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",project.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",project.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveNewWorkGroupProject";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<ProjectWorkGroupSummaryBO> GetProjectWorkGroupSummary(int TenantID,int LocationID)
        {
            List<ProjectWorkGroupSummaryBO> rtnval = new List<ProjectWorkGroupSummaryBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            //dyParam.Add("@projectFrom",ProjectFrom,DbType.String,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectWorkGroupSummary";
                rtnval = SqlMapper.Query<ProjectWorkGroupSummaryBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<PPProjectListBO> GetProjectWorkGroupLists(PPProjectListBO project)
        {
            List<PPProjectListBO> objtask = new List<PPProjectListBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",project.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",project.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectId",project.ProjectID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projStatus",project.ProjStatus,DbType.String,ParameterDirection.Input);
            //dyParam.Add("@projectFrom",project.ProjectFrom,DbType.String,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectWorkGroupLists";
                objtask = SqlMapper.Query<PPProjectListBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
    }
}