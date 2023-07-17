using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Wrapper;
using MyDodos.Domain.Attendance;
using MyDodos.Repository.Attendance;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyDodos.ViewModel.Common;
using Instrument.Utility;
using MyDodos.Repository.Auth;
using MyDodos.Repository.AzureStorage;
using MyDodos.Domain.AzureStorage;
using System.Globalization;
using MyDodos.ViewModel.Attendance;

namespace MyDodos.Service.Attendance
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IConfiguration _configuration;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IStorageConnect _storageConnect;
        public AttendanceService(IConfiguration configuration, IAttendanceRepository attendanceRepository, IAuthRepository authRepository, IStorageConnect storageConnect)
        {
            _configuration = configuration;
            _attendanceRepository = attendanceRepository;
            _authRepository = authRepository;
            _storageConnect = storageConnect;
        }
        public Response<int> SaveAttendanceUserLogData(ICollection<MachineInfo> attend)
        {
            Response<int> response;
            try
            {
                SaveOut result = new SaveOut();
                string output = string.Empty;
                foreach (var objatt in attend)
                {
                    objatt.MachineID = 0;
                    result = _attendanceRepository.SaveAttendanceUserLogData(objatt);
                    if (result.Id != 0)
                    {
                        var value = _attendanceRepository.SaveSwipeAttendanceData(objatt, result.Id);
                        var objresult = _attendanceRepository.GetEmpDetails(result.Id, value.AttendanceDate);
                        EmpStatusBO input = new EmpStatusBO
                        {
                            TenantID = objresult.TenantID,
                            LocationID = objresult.LocationID,
                            EmpID = value.EmpID,
                            AttendanceDate = value.AttendanceDate,
                            StartTime = objresult.StartTime,
                            EndTime = objresult.EndTime,
                            TimeIn = value.TimeIn,
                            TimeOut = value.TimeOut
                        };
                        output = _attendanceRepository.UpdateEmployeeStatus(input);
                    }
                }
                response = new Response<int>(result.Id, 200, result.Msg);
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveTerminal(DeviceMaster objdevices)
        {
            Response<int> response;
            try
            {
                InstrumentDA instrumentDA = new InstrumentDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);

                objdevices.DeviceIdentifier = (Guid.NewGuid()).ToString("N");

                //objdevices.ActivationKey = CreateRandomPassword(10);

                var result = instrumentDA.SaveDeviceMaster(objdevices);

                response = new Response<int>(result, 200, "Saved Successfully");
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<DeviceMaster>> GetTerminal(InputDeviceData objdevices)
        {
            Response<List<DeviceMaster>> response;
            try
            {
                InstrumentDA instrumentDA = new InstrumentDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);

                var result = instrumentDA.GetDeviceMaster(new DeviceMaster
                {
                    DeviceID = objdevices.DeviceID,
                    DevicePurpose = Convert.ToString(Devices.Terminal),
                    DeviceStatus = objdevices.DeviceStatus,
                    EntityID = objdevices.EntityID,
                    TenantID = objdevices.TenantID,
                });
                if (result.Count == 0)
                {
                    response = new Response<List<DeviceMaster>>(null, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<DeviceMaster>>(result, 200);
                }

            }
            catch (Exception ex)
            {
                response = new Response<List<DeviceMaster>>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> UpdateDeviceMaster(InputDeviceData objdevices)
        {
            Response<int> response;
            try
            {
                InstrumentDA instrumentDA = new InstrumentDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);

                var result = instrumentDA.UpdateDeviceMaster(new DeviceMaster
                {
                    DeviceID = objdevices.DeviceID,
                    DeviceStatus = objdevices.DeviceStatus,
                    SKUNumber = objdevices.MachineUniqueID,
                    TenantID = objdevices.TenantID,
                    EntityID = objdevices.EntityID,

                });
                if (result.Msg != "")
                {
                    response = new Response<int>(result.Id, 200, result.Msg);
                }
                else
                {
                    response = new Response<int>(result.Id, 200, "Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<DeviceMaster>> GetTerminalUniqueID(DeviceUnique deviceUnique)
        {
            Response<List<DeviceMaster>> response;
            try
            {
                List<DeviceMaster> deviceMasters = new List<DeviceMaster>();
                InstrumentDA instrumentDA = new InstrumentDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);

                var result = instrumentDA.CheckDeviceMaster(new DeviceMaster
                {
                    DevicePurpose = Convert.ToString(Devices.Terminal),
                    DeviceStatus = "",
                    DeviceID = 0,
                    DeviceIdentifier = deviceUnique.UniqueID,
                    SKUNumber = deviceUnique.MachineUniqueID,
                });

                if (result.Count == 0)
                {
                    response = new Response<List<DeviceMaster>>(null, 200, "Data Not Found");
                }
                else
                {
                    //var data = result.Where(x => x.DeviceIdentifier == deviceUnique.UniqueID
                    //&& x.SKUNumber == deviceUnique.MachineUniqueID).ToList();

                    //deviceMasters =data;

                    //var availabledata = result.Where(x => x.DeviceStatus == "Available").ToList();

                    //if(availabledata.Count!= 0)
                    //{
                    //    foreach(var device in availabledata)
                    //    {
                    //        deviceMasters.Add(device);
                    //    }
                    //}

                    response = new Response<List<DeviceMaster>>(result, 200);
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<DeviceMaster>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<DeviceMaster>> CheckTerminalUniqueID(DeviceUnique deviceUnique)
        {
            Response<List<DeviceMaster>> response;
            try
            {
                List<DeviceMaster> deviceMasters = new List<DeviceMaster>();
                InstrumentDA instrumentDA = new InstrumentDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);

                var result = instrumentDA.GetDeviceMaster(new DeviceMaster
                {
                    TenantID = deviceUnique.TenantID,
                    EntityID = deviceUnique.EntityID,
                    DevicePurpose = Convert.ToString(Devices.Terminal),
                    DeviceStatus = "",
                    DeviceID = 0

                });

                if (result.Count == 0)
                {
                    response = new Response<List<DeviceMaster>>(null, 200, "Data Not Found");
                }
                else
                {
                    var data = result.Where(x => x.DeviceIdentifier == deviceUnique.UniqueID
                    && x.SKUNumber == deviceUnique.MachineUniqueID && x.DeviceStatus == "Active").ToList();

                    if (data.Count == 0)
                    {
                        response = new Response<List<DeviceMaster>>(null, 200);
                    }
                    else
                    {
                        response = new Response<List<DeviceMaster>>(data, 200);
                    }


                }
            }
            catch (Exception ex)
            {
                response = new Response<List<DeviceMaster>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<DeviceMaster>> CheckTerminal(DeviceUnique deviceUnique)
        {
            Response<List<DeviceMaster>> response;
            try
            {
                List<DeviceMaster> deviceMasters = new List<DeviceMaster>();
                InstrumentDA instrumentDA = new InstrumentDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);

                var result = instrumentDA.CheckDeviceMaster(new DeviceMaster
                {
                    DevicePurpose = Convert.ToString(Devices.Terminal),
                    DeviceStatus = "Active",
                    DeviceID = 0,
                    DeviceIdentifier = deviceUnique.UniqueID,
                    SKUNumber = deviceUnique.MachineUniqueID,
                });

                if (result.Count == 0)
                {
                    var result1 = instrumentDA.CheckDeviceMaster(new DeviceMaster
                    {
                        DevicePurpose = Convert.ToString(Devices.Terminal),
                        DeviceStatus = "Locked",
                        DeviceID = 0,
                        DeviceIdentifier = deviceUnique.UniqueID,
                        SKUNumber = deviceUnique.MachineUniqueID,
                    });
                    if (result1.Count == 0)
                    {
                        var result2 = instrumentDA.CheckDeviceMaster(new DeviceMaster
                        {
                            DevicePurpose = Convert.ToString(Devices.Terminal),
                            DeviceStatus = "",
                            DeviceID = 0,
                            DeviceIdentifier = "",
                            SKUNumber = deviceUnique.MachineUniqueID,
                        });
                        if (result2.Count == 0)
                        {
                            var result3 = instrumentDA.CheckDeviceMaster(new DeviceMaster
                            {
                                DevicePurpose = Convert.ToString(Devices.Terminal),
                                DeviceStatus = "",
                                DeviceID = 0,
                                DeviceIdentifier = deviceUnique.UniqueID,
                                SKUNumber = "",

                            });
                            if (result3.Count != 0)
                            {
                                string skunum = string.Empty;
                                foreach (var device in result3)
                                {
                                    skunum = device.SKUNumber;
                                }

                                if (skunum == null || skunum == "")
                                {
                                    deviceMasters.Add(new DeviceMaster
                                    {
                                        DeviceLinked = false,
                                        IsActive = false,
                                        TenantID = result3[0].TenantID,
                                        EntityID = result3[0].EntityID
                                    });
                                    response = new Response<List<DeviceMaster>>(deviceMasters, 200, "Data Not Found");
                                }
                                else
                                {
                                    response = new Response<List<DeviceMaster>>(null, 200, "Device link");
                                }

                            }
                            else
                            {
                                response = new Response<List<DeviceMaster>>(null, 200, "Data Not Found");
                            }



                        }
                        else
                        {
                            response = new Response<List<DeviceMaster>>(null, 200, "Linked Another Device");
                        }


                    }
                    else
                    {
                        foreach (var device in result1)
                        {
                            device.DeviceLinked = true;
                            device.IsActive = false;
                            deviceMasters.Add(device);
                        }
                        response = new Response<List<DeviceMaster>>(deviceMasters, 200);
                    }


                }
                else
                {
                    foreach (var device in result)
                    {
                        device.DeviceLinked = true;
                        device.IsActive = true;
                        deviceMasters.Add(device);
                    }
                    response = new Response<List<DeviceMaster>>(result, 200);
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<DeviceMaster>>(ex.Message, 500);
            }
            return response;
        }
        public Response<GenActivationKey> GenActivationKey(GenActivationKey objKey)
        {
            Response<GenActivationKey> response;
            try
            {
                InstrumentDA instrumentDA = new InstrumentDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);

                objKey.ActivationKey = CreateRandomPassword(10);

                if (objKey.ActivationKey != null && objKey.ActivationKey != "")
                {
                    objKey.ExpiryDate = GetExpDate(Convert.ToInt32(objKey.ExpiryTime), objKey.ExpPeriod);
                }

                var result = instrumentDA.UpdateActivationKey(objKey);

                if (result != null)
                {
                    response = new Response<GenActivationKey>(result, 200);
                }
                else
                {
                    response = new Response<GenActivationKey>(result, 200);
                }
            }
            catch (Exception ex)
            {
                response = new Response<GenActivationKey>(ex.Message, 500);
            }
            return response;
        }
        private static string CreateRandomPassword(int length = 15)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        private DateTime? GetExpDate(int v, string expPeriod)
        {

            if (expPeriod == "mins")
            {
                return DateTime.Now.AddMinutes(v);

            }
            else if (expPeriod == "secs")
            {
                return DateTime.Now.AddSeconds(v);

            }
            else if (expPeriod == "hrs")
            {
                return DateTime.Now.AddHours(v);

            }
            else
            {
                return DateTime.Now.AddMinutes(v);
            }
        }
        public Response<int> DeleteDeviceLink(int DeviceID, int EntityID, int TenantID)
        {
            Response<int> response;
            try
            {
                InstrumentDA instrumentDA = new InstrumentDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);


                var result = instrumentDA.RemoveDeviceLink(DeviceID, EntityID, TenantID);

                response = new Response<int>(0, 200, "Updated Successfully");

            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> DeleteDevice(int DeviceID, int LocationID, int TenantID)
        {
            Response<int> response;
            try
            {
                InstrumentDA instrumentDA = new InstrumentDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);

                var result = instrumentDA.RemoveDevice(DeviceID, LocationID, TenantID);

                if (result.Id == 0)
                {
                    response = new Response<int>(result.Id, 200, result.Msg);
                }
                else
                {
                    response = new Response<int>(result.Id, 200, "Updated Successfully");
                }


            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveDeviceTypeMaster(List<DeviceTypeMaster> objdevice)
        {
            Response<int> response;
            try
            {
                int result = 0;
                InstrumentDA instrumentDA = new InstrumentDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);

                foreach (var item in objdevice)
                {
                    result = instrumentDA.SaveDeviceTypeMaster(item);
                }

                response = new Response<int>(result, 200, "Saved Successfully");
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<DeviceTypeMaster>> GetDeviceTypeMaster(DeviceTypeMaster objdevice)
        {
            Response<List<DeviceTypeMaster>> response;
            try
            {
                InstrumentDA instrumentDA = new InstrumentDA(_configuration.GetConnectionString("DODOSADBConnection").ToString(), DBType.MYSQL);

                var result = instrumentDA.GetDeviceTypeMaster(objdevice);
                if (result.Count == 0)
                {
                    response = new Response<List<DeviceTypeMaster>>(result, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<DeviceTypeMaster>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<DeviceTypeMaster>>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveEmpAttendanceManual(List<EmployeeRosterBO> objempros)
        {
            Response<int> response;
            EmployeeAttendanceBO objemployee = new EmployeeAttendanceBO();
            try
            {
                SaveOut result = new SaveOut();
                string output = string.Empty;
                foreach (var item in objempros)
                {
                    result = _attendanceRepository.SaveAttendanceRoster(item);

                    foreach (var objatt in item.objemp)
                    {
                        if (result.Id != 0)
                        {
                            var res = _attendanceRepository.SaveEmployeeAttendance(objatt, result.Id);
                            foreach (var obj in item.objemp)
                            {
                                var objresult = _attendanceRepository.GetEmpDetails(obj.EmpID, obj.AttendanceDate);
                                EmpStatusBO input = new EmpStatusBO
                                {
                                    TenantID = objresult.TenantID,
                                    LocationID = objresult.LocationID,
                                    EmpID = obj.EmpID,
                                    AttendanceDate = obj.AttendanceDate,
                                    StartTime = objresult.StartTime,
                                    EndTime = objresult.EndTime,
                                    TimeIn = obj.TimeIn,
                                    TimeOut = obj.TimeOut
                                };
                                output = _attendanceRepository.UpdateEmployeeStatus(input);
                            }
                        }
                        else
                        {
                            response = new Response<int>(result.Id, 200, result.Msg);
                        }
                    }
                }
                response = new Response<int>(result.Id, 200, result.Msg);
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EmployeeRosterBO>> GetEmpAttendanceManual(EmpAttendanceInputBO objplan)
        {
            Response<List<EmployeeRosterBO>> response;
            try
            {
                List<EmployeeRosterBO> objroster = new List<EmployeeRosterBO>();
                List<EmployeeAttendanceBO> objemployee = new List<EmployeeAttendanceBO>();
                var result = _attendanceRepository.GetEmpAttendanceRoster(objplan);
                objroster = result;
                if (result.Count() == 0)
                {
                    response = new Response<List<EmployeeRosterBO>>(objroster, 200, "Data Not Found");
                }
                else
                {
                    foreach (var item in objroster)
                    {
                        var res = _attendanceRepository.GetEmpAttendance(item.EmpID, item.RosterDate, item.TenantID, item.LocationID);
                        objemployee = res;
                        item.objemp = new List<EmployeeAttendanceBO>(objemployee);
                        objemployee = new List<EmployeeAttendanceBO>();
                    }
                    response = new Response<List<EmployeeRosterBO>>(objroster, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<EmployeeRosterBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EmployeeRosterBO>> GetEmpAttendance(AttendanceInputBO _att)
        {
            Response<List<EmployeeRosterBO>> response;
            try
            {
                List<EmployeeRosterBO> result = new List<EmployeeRosterBO>();
                result = _attendanceRepository.GetAttendanceRoster(_att);
                foreach (var att in result)
                {
                    _att.EmpID = att.EmpID;
                    _att.CurrentDate = att.RosterDate.ToString("yyyy-MM-dd");
                    att.objemp = _attendanceRepository.GetAttendanceEmp(_att);
                    //result.Add(att);       
                }

                response = new Response<List<EmployeeRosterBO>>(result, 200, "Data Reterived");
            }
            catch (Exception ex)
            {
                response = new Response<List<EmployeeRosterBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<AttendanceTerminalBO>> GetTerminalData(TerminalInputBO objinput)
        {
            Response<List<AttendanceTerminalBO>> response;
            try
            {
                // List<AttendnceTerminalBO> objres = new List<AttendnceTerminalBO>();
                var objres = _attendanceRepository.GetTerminalData(objinput);
                if (objres.Count() == 0)
                {
                    response = new Response<List<AttendanceTerminalBO>>(objres, 200, "Data Not Found");
                }
                else
                {
                    foreach (var att in objres)
                    {
                        att.base64Images = "";
                        att.IsCheckIn = _attendanceRepository.GetCheckInMsg(att.EmpID, att.LocationID, att.TenantID);
                        KoSoft.DocRepo.DocumentDA objdoc = new KoSoft.DocRepo.DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), KoSoft.DocRepo.DBType.MySQL);
                        //KoSoft.DocRepo.DocumentDA objdocservices = new KoSoft.DocRepo.DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), KoSoft.DocRepo.DBType.MySQL);
                        var result = objdoc.GetDocument(0, att.EmpID, "", "ProfileImage", att.TenantID);
                        if (result.Count > 0)
                        {
                            var objcont = objdoc.GetDocContainer(att.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                            //string TenantName = objcont[0].ContainerName.ToLower();
                            var doc = _storageConnect.DownloadDocument(new SaveDocCloudBO
                            {
                                CloudType = _configuration.GetSection("CloudType").Value,
                                Container = objcont[0].ContainerName.ToLower(),
                                fileName = result[0].GenDocName,
                                folderPath = result[0].DirectionPath,
                                ProductCode = Convert.ToString(_configuration.GetSection("ProductID").Value)

                            }).Result;
                            att.base64Images = doc.DocumentURL;
                        }
                    }
                    response = new Response<List<AttendanceTerminalBO>>(objres, 200, "Data Reterived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<AttendanceTerminalBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<AttendanceTrackingBO> GetAttendanceTracking(EmployeeRosterSearchBO objinp)
        {
            Response<AttendanceTrackingBO> response;
            AttendanceTrackingBO objout = new AttendanceTrackingBO();
            List<EmployeeAttendanceBO> objemployee = new List<EmployeeAttendanceBO>();
            List<EmployeeAttendanceBO> result1 = new List<EmployeeAttendanceBO>();
            try
            {
                string output1 = string.Empty;
                var result = _attendanceRepository.GetEmployeeRosterSearch(objinp);
                if (result.objoutlist.Count == 0)
                {
                    response = new Response<AttendanceTrackingBO>(null, "Data Not Found");
                }
                else
                {
                    foreach (var item in result.objoutlist)
                    {
                        result1 = _attendanceRepository.GetEmpAttendance(item.EmpID, item.RosterDate, item.TenantID, item.LocationID);
                        if (result1.Count == 0)
                        {
                            string attendanceDate = item.RosterDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                            var empdetails = _attendanceRepository.GetEmpDetails(item.EmpID, attendanceDate);
                            EmpStatusBO input = new EmpStatusBO
                            {
                                TenantID = empdetails.TenantID,
                                LocationID = empdetails.LocationID,
                                EmpID = item.EmpID,
                                AttendanceDate = attendanceDate,
                                StartTime = empdetails.StartTime,
                                EndTime = empdetails.EndTime,
                                TimeIn = null,
                                TimeOut = null
                            };
                            output1 = _attendanceRepository.UpdateEmployeeStatus(input);
                        }
                        else
                        {
                            foreach (var item1 in result1)
                            {
                                var empdetails = _attendanceRepository.GetEmpDetails(item1.EmpID, item1.AttendanceDate);
                                EmpStatusBO input = new EmpStatusBO
                                {
                                    TenantID = empdetails.TenantID,
                                    LocationID = empdetails.LocationID,
                                    EmpID = item1.EmpID,
                                    AttendanceDate = item1.AttendanceDate,
                                    StartTime = empdetails.StartTime,
                                    EndTime = empdetails.EndTime,
                                    TimeIn = item1.TimeIn,
                                    TimeOut = item1.TimeOut
                                };
                                output1 = _attendanceRepository.UpdateEmployeeStatus(input);
                            }
                        }
                    }

                    var outputobj = _attendanceRepository.GetEmployeeRosterSearch(objinp);
                    foreach (var item2 in outputobj.objoutlist)
                    {
                        objemployee = _attendanceRepository.GetEmpAttendance(item2.EmpID, item2.RosterDate, item2.TenantID, item2.LocationID);
                        item2.objemp = new List<EmployeeAttendanceBO>(objemployee);
                        objemployee = new List<EmployeeAttendanceBO>();
                    }
                    var resultcount = _attendanceRepository.GetCountData(objinp.objinput);
                    var output = _attendanceRepository.GetLeaveRequestData(objinp.objinput);
                    var value = _attendanceRepository.GetPermissionRequestData(objinp.objinput);
                    // var val = _attendanceRepository.GetTardiesData(objinp.objinput);
                    var final = _attendanceRepository.GetEmployee(objinp.objinput);
                    if (final == null)
                    {
                        response = new Response<AttendanceTrackingBO>(null, "Data Not Found");
                    }
                    else
                    {
                        final.base64images = "";
                        var res = _authRepository.GetDocument(0, objinp.objinput.EmpID, "", Convert.ToString(DocRepoName.ProfileImage), objinp.objinput.TenantID);
                        if (res.Count > 0)
                        {
                            var objcont = _authRepository.GetDocContainer(objinp.objinput.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                            string TenantName = objcont[0].ContainerName.ToLower();
                            var doc = _storageConnect.DownloadDocument(new SaveDocCloudBO
                            {
                                CloudType = _configuration.GetSection("CloudType").Value,
                                Container = TenantName,
                                fileName = res[0].GenDocName,
                                folderPath = res[0].DirectionPath,
                                ProductCode = Convert.ToString(_configuration.GetSection("ProductID").Value)

                            }).Result;
                            final.base64images = doc.DocumentURL;
                        }
                    }
                    objout.objcount = resultcount;
                    objout.objros = outputobj;
                    objout.objleave = output;
                    objout.objperm = value;
                    // objout.objtardies = val;
                    objout.emp = final;
                    if (objout.emp == null)
                    {
                        response = new Response<AttendanceTrackingBO>(objout, 200, "Data Not Found");
                    }
                    else
                    {
                        response = new Response<AttendanceTrackingBO>(objout, 200, "Data Retrieved");
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<AttendanceTrackingBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<string> AttendanceRosterGenerted(ShifRosterGenertedBO _shift)
        {
            Response<string> response;
            try
            {
                string result = string.Empty;
                if (_shift.EmpID > 0)
                {
                    result = _attendanceRepository.AttendanceRosterGenerted(_shift);
                }
                else
                {
                    var shift = _attendanceRepository.GetAttendanceEmployeeList(_shift.TenantID, _shift.LocationID);
                    if (shift.Count() > 0)
                    {
                        foreach (var empshift in shift)
                        {
                            _shift.EmpID = empshift.EmpID;
                            result = _attendanceRepository.AttendanceRosterGenerted(_shift);
                        }
                    }
                }
                response = new Response<string>(result, 200, "Saved Successfully");
            }
            catch (Exception ex)
            {
                response = new Response<string>(ex.Message, 500);
            }
            return response;
        }
        public Response<EmployeeAttendanceBO> AttendanceInandOut(AttendanceInandOutBO _shiftin)
        {
            Response<EmployeeAttendanceBO> response;
            try
            {
                string output = string.Empty;
                var result = _attendanceRepository.AttendanceInandOut(_shiftin);
                response = new Response<EmployeeAttendanceBO>(result, 200, result.Msg);
                var objresult = _attendanceRepository.GetEmpDetails(result.EmpID, result.AttendanceDate);
                EmpStatusBO input = new EmpStatusBO
                {
                    TenantID = objresult.TenantID,
                    LocationID = objresult.LocationID,
                    EmpID = result.EmpID,
                    AttendanceDate = result.AttendanceDate,
                    StartTime = objresult.StartTime,
                    EndTime = objresult.EndTime,
                    TimeIn = result.TimeIn,
                    TimeOut = result.TimeOut
                };
                output = _attendanceRepository.UpdateEmployeeStatus(input);
            }
            catch (Exception ex)
            {
                response = new Response<EmployeeAttendanceBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveAttendacePIN(EmpAttendanceConfigBO objconfig)
        {
            Response<int> response;
            try
            {

                switch (objconfig.ClockingMode)
                {
                    case "Terminal":

                        objconfig.AttendCardNo = CreateRandomPIN(4);

                        break;
                    case "Bio Metric":

                        objconfig.AttendCardNo = _attendanceRepository.GetEmpNumber(objconfig.EmpID);

                        break;
                }
                var result = _attendanceRepository.SaveAttendacePIN(objconfig, objconfig.AttendCardNo);

                if (result.Id == 0)
                {
                    response = new Response<int>(result.Id, 200, result.Msg);
                }
                else
                {
                    response = new Response<int>(result.Id, 200, result.Msg);
                }

            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        private static string CreateRandomPIN(int length = 4)
        {
            string validChars = "0123456789";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        public Response<EmployeeRosterSearchBO> GetEmployeeAttendanceManualSearch(EmployeeRosterSearchBO objplan)
        {
            Response<EmployeeRosterSearchBO> response;
            List<EmployeeAttendanceBO> objemployee = new List<EmployeeAttendanceBO>();
            try
            {
                var result = _attendanceRepository.GetEmployeeRosterSearch(objplan);
                if (result.objoutlist.Count() == 0)
                {
                    response = new Response<EmployeeRosterSearchBO>(result, 200, "Data Not Found");
                }
                else
                {
                    foreach (var item in result.objoutlist)
                    {
                        var output = _attendanceRepository.GetEmpAttendance(item.EmpID, item.RosterDate, item.TenantID, item.LocationID);
                        objemployee = output;
                        item.objemp = new List<EmployeeAttendanceBO>(objemployee);
                        objemployee = new List<EmployeeAttendanceBO>();
                    }
                    response = new Response<EmployeeRosterSearchBO>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {

                response = new Response<EmployeeRosterSearchBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EmployeeListBO>> GetEmployeeList(int TenantID,int LocationID)
        {
            Response<List<EmployeeListBO>> response;
            try
            {

                var result = _attendanceRepository.GetEmployeeList(TenantID,LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<EmployeeListBO>>(result, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<EmployeeListBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<EmployeeListBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> AddAttendanceProfile(List<WorkingHours> hours)
        {
            Response<SaveOut> response;
            try
            {
                    foreach (var str in hours)
                    {
                        var result = _attendanceRepository.AddAttendanceProfile(str);
                    }
                    response = new Response<SaveOut>(null,200,"Saved Successfully");
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message,500);
            }
            return response;
        }
        public Response<List<WorkingHours>> GetShiftList(int TenantID,int LocationID)
        {
            Response<List<WorkingHours>> response;
            try
            {

                var result = _attendanceRepository.GetShiftList(TenantID,LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<WorkingHours>>(result, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<WorkingHours>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<WorkingHours>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<ReturnShiftBO>> GetShiftName(int TenantID,int LocationID)
        {
            Response<List<ReturnShiftBO>> response;
            try
            {

                var result = _attendanceRepository.GetShiftName(TenantID,LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<ReturnShiftBO>>(result, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<ReturnShiftBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<ReturnShiftBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<ReturnShiftSchdBO>> GetAttendanceSchedule(int TenantID,int LocationID)
        {
            Response<List<ReturnShiftSchdBO>> response;
            try
            {

                var result = _attendanceRepository.GetAttendanceSchedule(TenantID,LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<ReturnShiftSchdBO>>(result, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<ReturnShiftSchdBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<ReturnShiftSchdBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> AttendanceRoster(List<HRRoasterBO> roster)
        {
            Response<SaveOut> response;
            try
            {
                    foreach (var str in roster)
                    {
                        var result = _attendanceRepository.AttendanceRoster(str);
                    }
                    response = new Response<SaveOut>(null,200,"Updated Successfully");
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message,500);
            }
            return response;
        }
        public Response<List<WorkingHours>> GetWorkingHours(int TenantID,int LocationID,string ShiftName)
        {
            Response<List<WorkingHours>> response;
            try
            {

                var result = _attendanceRepository.GetWorkingHours(TenantID,LocationID,ShiftName);
                if (result.Count == 0)
                {
                    response = new Response<List<WorkingHours>>(result, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<WorkingHours>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<WorkingHours>>(ex.Message, 500);
            }
            return response;
        }
        public Response<ReturnDetailBO> GetShiftRoasterEmp(int TenantID,int LocationID,int ShiftID)
        {
            Response<ReturnDetailBO> response;
            try
            {
                ReturnDetailBO emplist = new ReturnDetailBO();
                emplist.ShiftSchedule = _attendanceRepository.GetShiftRoasterEmp(TenantID,LocationID,ShiftID);
                if (emplist.ShiftSchedule != null)
                {
                    foreach(var item in emplist.ShiftSchedule)
                    {
                        item.AttendanceRoaster = _attendanceRepository.GetEmpShift(item.StartDate,item.EndDate,ShiftID,item.EmpID);
                    }
                }
                // else
                // {
                //     response = new Response<ReturnDetailBO>(result, 200, "Data Retrieved");
                // }
                response = new Response<ReturnDetailBO>(emplist, 200, "Data Retrieved");
            }
            catch (Exception ex)
            {
                response = new Response<ReturnDetailBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EmployeeListsBO>> GetEmployeeList(int TenantID, int LocationID,int EmpID)
        {
            Response<List<EmployeeListsBO>> response;
            try
            {
                var result = _attendanceRepository.GetEmployeeList(TenantID, LocationID, EmpID);       
                if (result == null)
                {      
                    response = new Response<List<EmployeeListsBO>>(result, 200, "Data Not Found");
                }
                else
                {   
                    response = new Response<List<EmployeeListsBO>>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<EmployeeListsBO>>(ex.Message, 500);
            }
            return response;
        }
    }
}