using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System;
using NLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using Newtonsoft.Json;
using MyDodos.Repository.Auth;
using MyDodos.Service.Auth;
using MyDodos.Repository.LeaveManagement;
using MyDodos.Service.LeaveManagement;
using MyDodos.Repository.Holiday;
using MyDodos.Repository.Document;
using MyDodos.Repository.AzureStorage;
using MyDodos.Repository.TimeOff;
using MyDodos.Service.TimeOff;
using MyDodos.Repository.Employee;
using MyDodos.Service.Employee;
using MyDodos.Repository.Mail;
using MyDodos.Repository.Administrative;
using MyDodos.Service.Administrative;
using MyDodos.Repository.Attendance;
using MyDodos.Service.Attendance;
using MyDodos.Service.HolidayManagement;
using MyDodos.Service.ProjectManagement;
using MyDodos.Repository.ProjectManagement;
using MyDodos.Service.BenefitManagement;
using MyDodos.Repository.BenefitManagement;
using MyDodos.Service.Master;
using MyDodos.Repository.Master;
using MyDodos.Service.HR;
using MyDodos.Repository.HR;
using MyDodos.Service.Entitlement;
using MyDodos.Repository.Entitlement;
using Microsoft.AspNetCore.Http;
using MyDodos.Service.ConnectionString;
using MyDodos.Repository.ConnectionString;
using MyDodos.Service.TemplateManager;
using MyDodos.Service.Document;
using MyDodos.Repository.TemplateManager;
using MyDodos.Service.Report;
using MyDodos.Repository.Report;
using MyDodos.Service.Dashboard;
using MyDodos.Repository.Dashboard;
using MyDodos.Service.Payroll;
using MyDodos.Repository.Payroll;
using MyDodos.Service.TicketingSystem;
using MyDodos.Repository.HRMS;
using MyDodos.Service.HRMS;

namespace MyDodos.API
{
    class Program
    {
        private static string _applicationPath = string.Empty;
        private static string _contentRootPath = string.Empty;
        public IConfiguration Configuration { get; set; }
        public Program(IWebHostEnvironment env)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            _applicationPath = env.WebRootPath;
            _contentRootPath = env.ContentRootPath;
            var builder = new ConfigurationBuilder()
                .SetBasePath(_contentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: false);
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString =builder.Configuration.GetConnectionString("KOHAMSADBConnection");

            var securityurl = builder.Configuration.GetSection("SecurityUrl").Value;
            var audience = builder.Configuration.GetSection("Audience").Value;
            var cognitoIssuer = builder.Configuration.GetSection("CognitoIssuer").Value;
            var cognitoAudience = builder.Configuration.GetSection("CognitoAudience").Value;

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            //Azure
            builder.Services.AddTransient<IMailRepository, MailRepository>();
            //Mail
            builder.Services.AddTransient<IStorageConnect, StorageConnect>();
            //Staging
            //builder.Services.AddTransient<IStagingRepository, StagingRepository>();
            //Auth
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            //Ticketing system
            builder.Services.AddScoped<ITicketingSystemService, TicketingSystemService>();
            //Leave Management
            builder.Services.AddScoped<ILeaveService, LeaveService>();
            builder.Services.AddScoped<ILeaveRepository, LeaveRepository>();
            //Holiday Management
            builder.Services.AddScoped<IHolidayRepository, HolidayRepository>();
            builder.Services.AddScoped<IHolidayService, HolidayService>();
            //Time Off
            builder.Services.AddScoped<ITimeOffService, TimeOffService>();
            builder.Services.AddScoped<ITimeOffRepository, TimeOffRepository>();
            //Employee
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //Administrative
            builder.Services.AddScoped<IAdministrativeService, AdministrativeService>();
            builder.Services.AddScoped<IAdministrativeRepository, AdministrativeRepository>();

            builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
            builder.Services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            //Attendance
            builder.Services.AddScoped<IAttendanceService, AttendanceService>();
            builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            //Report
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddScoped<IReportRepository, ReportRepository>();
            //BenefitManagement Grade
            builder.Services.AddScoped<IGradeService, GradeService>();
            builder.Services.AddScoped<IGradeRepository, GradeRepository>();
            //BenefitManagement MedicalInsurance
            builder.Services.AddScoped<IMedicalInsuranceService, MedicalInsuranceService>();
            builder.Services.AddScoped<IMedicalInsuranceRepository, MedicalInsuranceRepository>();
            //BenefitManagement LeaveBenefit
            builder.Services.AddScoped<ILeaveBenefitService, LeaveBenefitService>();
            builder.Services.AddScoped<ILeaveBenefitRepository, LeaveBenefitRepository>();
            //BenefitManagement BenefitGroup
            builder.Services.AddScoped<IBenefitGroupService, BenefitGroupService>();
            builder.Services.AddScoped<IBenefitGroupRepository, BenefitGroupRepository>();
             //BenefitManagement DisabilityBenefit
            builder.Services.AddScoped<IDisabilityBenefitService, DisabilityBenefitService>();
            builder.Services.AddScoped<IDisabilityBenefitRepository, DisabilityBenefitRepository>();
            //Dashboard
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
            //Permission
            builder.Services.AddScoped<IPermissionService, PermissionService>();
            builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
            //SpecialPermission
            builder.Services.AddScoped<ISpecialPermissionService, SpecialPermissionService>();
            builder.Services.AddScoped<ISpecialPermissionRepository, SpecialPermissionRepository>();
            //Project Management
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<ITimeSheetService, TimeSheetService>();
            builder.Services.AddScoped<ITimeSheetRepository, TimeSheetRepository>();
            //Master
            builder.Services.AddScoped<IMasterService, MasterService>();
            builder.Services.AddScoped<IMasterRepository, MasterRepository>();
            //HR
            builder.Services.AddScoped<IOnBoardService, OnBoardService>();
            builder.Services.AddScoped<IOnBoardRepository, OnBoardRepository>();
            builder.Services.AddScoped<IOffBoardService, OffBoardService>();
            builder.Services.AddScoped<IOffBoardRepository, OffBoardRepository>();
            //Common
            builder.Services.AddScoped<ICommonService, CommonService>();
            builder.Services.AddScoped<ICommonRepository, CommonRepository>();
            //Entitlement
            builder.Services.AddScoped<ISecurityService, SecurityService>();
            //builder.Services.AddScoped<IEntitlementService, EntitlementService>();
            builder.Services.AddScoped<IEntitlementRepository, EntitlementRepository>();
            //Entitlement
            builder.Services.AddScoped<ITemplateMgmtService, TemplateMgmtService>();
            builder.Services.AddScoped<IDocRepository, DocRepository>();
            //Document
            builder.Services.AddScoped<IDocumentFileService, DocumentFileService>();
            builder.Services.AddScoped<IDocumentFileRepository, DocumentFileRepository>();
            //HRMSINSTANCE
            builder.Services.AddScoped<IHrmsInstanceService, HrmsInstanceService>();
            builder.Services.AddScoped<IHrmsInstanceRepository, HrmsInstanceRepository>();
            //Document Duplicate
            // builder.Services.AddScoped<IDocumentServicecheck, DocumentServicecheck>();
            // builder.Services.AddScoped<IDocumentRepositorycheck, DocumentRepositorycheck>();
            //ConnectionString
            builder.Services.AddScoped<IConnectionStringService, ConnectionStringService>();
            builder.Services.AddScoped<IConnectionStringRepository, ConnectionStringRepository>();
            //Payroll
            builder.Services.AddScoped<IPayrollService, PayrollService>();
            builder.Services.AddScoped<IPayrollRepository, PayrollRepository>();

            builder.Services.AddScoped<IPayrollSlipService, PayrollSlipService>();
            builder.Services.AddScoped<IPayrollSlipRepository, PayrollSlipRepository>();

            builder.Services.AddScoped<IPayrollRevisonService, PayrollRevisonService>();
            builder.Services.AddScoped<IPayrollRevisonRepository, PayrollRevisonRepository>();

            builder.Services.AddScoped<IPayrollMasterService, PayrollMasterService>();
            builder.Services.AddScoped<IPayrollMasterRepository, PayrollMasterRepository>();
            //Dashboard
            builder.Services.AddScoped<IDashBoardRepository, DashBoardRepository>();
            builder.Services.AddScoped<IDashBoardService, DashBoardService>();
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Service Injected

            #endregion

            builder.Services.AddCors(options =>
            {

                var corsUrlSection = builder.Configuration.GetSection("AllowedOrigins");
                var corsUrls = corsUrlSection.Get<string[]>();
                options.AddPolicy("CorsPolicy",
                    Configuration => Configuration
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    );
            });

            #region Authenticate 

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("Custom", o =>
            {

                o.Authority = securityurl;
                o.Audience = audience;
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    //other settings
                    ClockSkew = TimeSpan.Zero
                };

            })
             .AddJwtBearer("Cognito", options =>
             {
                 options.TokenValidationParameters = GetCognitoTokenValidationParams(cognitoIssuer, cognitoAudience);
                 options.MetadataAddress = $"{cognitoIssuer}/.well-known/openid-configuration";
             });
            builder.Services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes("Custom", "Cognito")
                    .Build();

                var approvedPolicyBuilder = new AuthorizationPolicyBuilder()
                       .RequireAuthenticatedUser()
                       .AddAuthenticationSchemes("Custom", "Cognito")
                       ;
                options.AddPolicy("approved", approvedPolicyBuilder.Build());
            });

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }

        #region Token Validate

        private static TokenValidationParameters GetCognitoTokenValidationParams(string cognitoIssuer, string cognitoAudience)
        {
            //var cognitoIssuer =
            //    $"https://cognito-idp.us-east-1.amazonaws.com/us-east-1_YV1kc1h6f";
            var jwtKeySetUrl = $"{cognitoIssuer}/.well-known/jwks.json";
            //var cognitoAudience = "3kfctt9n7900h7t2luk4cs0v9b";

            return new TokenValidationParameters
            {
                IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
                {
                    // get JsonWebKeySet from AWS
                    //var json = new HttpClient().DownloadString(jwtKeySetUrl);

                    string json = string.Empty;
                    using (var handler = new HttpClientHandler())
                    {
                        using (var client = new HttpClient(handler))
                        {
                            using (
                                HttpResponseMessage response = client
                                    .GetAsync(jwtKeySetUrl)
                                    .Result
                            )
                            {
                                using (HttpContent content = response.Content)
                                {
                                    json = content.ReadAsStringAsync().Result;
                                }
                            }
                        }
                    }

                    // serialize the result
                    var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(json).Keys;

                    // cast the result to be the type expected by IssuerSigningKeyResolver
                    return (IEnumerable<SecurityKey>)keys;
                },
                ValidIssuer = cognitoIssuer,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateLifetime = false,
                ValidAudience = cognitoAudience,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        }
        #endregion

    }
}
