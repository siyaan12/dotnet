Module : HR Core

CREATE TABLE `tblHREmpExperience` (
  `EmpExpID` int NOT NULL AUTO_INCREMENT,
  `EmpID` int DEFAULT NULL,
  `CompanyName` varchar(155) DEFAULT NULL,
  `Designation` varchar(150) DEFAULT NULL,
  `DurationFrom` date DEFAULT NULL,
  `DurationTo` date DEFAULT NULL,
  `YearOfExp` int DEFAULT NULL,
  `CTC` decimal(10,2) DEFAULT NULL,
  `WorkLocation` varchar(50) DEFAULT NULL,
  `TenantID` int DEFAULT NULL,
  `LocationID` int DEFAULT NULL,
  `CreatedBy` int DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `ModifiedBy` int DEFAULT NULL,
  `ModifiedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`EmpExpID`)
)  


DELIMITER $$
CREATE DEFINER=`mydodos_dbadmin`@`%` PROCEDURE `sp_SaveHREmpExperience`(
IN empExpId INT,
IN empId int,
IN companyName varchar(155),
IN designation varchar(155),
IN durationFrom date,
IN durationTo date,
IN yearOfExp int,
IN cTc decimal(10,2),
IN workLocation varchar(50),
IN tenantId int,
-- IN locationId int,
IN createdBy int
)
BEGIN
	if(empExpId=0)then
		begin
			IF(SELECT count(a.EmpExpID) FROM tblHREmpExperience a WHERE a.EmpID = empId and a.CompanyName = companyName) = 0 THEN
				BEGIN
					INSERT INTO tblHREmpExperience (
					`EmpID`,
					`CompanyName`,
                    `Designation`,
                    `DurationFrom`,
					`DurationTo`,
                    `YearOfExp`,
                    `CTC`,
                    `WorkLocation`,
					`TenantID`,
					-- `LocationID`,
					`CreatedBy`,
					`CreatedOn`
					) VALUES (
					empId,
					companyName,
                    designation,
                    durationFrom,
					durationTo,
                    yearOfExp,
					cTc,
                    workLocation,
					tenantId,
                    -- locationId,
					createdBy,
					now()
					);
					SELECT LAST_INSERT_ID() as ReqID,'Saved Successfully' as Msg;
				END;
ELSE
	BEGIN
		UPDATE tblHREmpExperience SET 
		`EmpID` = empId,
		`CompanyName` = companyName,
        `Designation` = designation,
		`DurationFrom` = durationFrom,
		`DurationTo` = durationTo,
		`YearOfExp` = yearOfExp,
		`CTC` = cTc,
        `WorkLocation` = workLocation,
		`TenantID` = tenantId,
		-- `LocationID` = locationId,
		`ModifiedBy` = createdBy,
		`ModifiedOn` = now()
		 WHERE tblHREmpExperience.EmpExpID = empExpId;
		SELECT ReqID,'Saved Successfully' as Msg;
	END;
END IF;
end;
else
BEGIN
		UPDATE tblHREmpExperience SET 
		`EmpID` = empId,
		`CompanyName` = companyName,
        `Designation` = designation,
		`DurationFrom` = durationFrom,
		`DurationTo` = durationTo,
		`YearOfExp` = yearOfExp,
		`CTC` = cTc,
        `WorkLocation` = workLocation,
		`TenantID` = tenantId,
		-- `LocationID` = locationId,
		`ModifiedBy` = createdBy,
		`ModifiedOn` = now()
		 WHERE tblHREmpExperience.EmpExpID = empExpId;
		SELECT ReqID,'Saved Successfully' as Msg;
	END;
END IF;
END$$
DELIMITER ;



DELIMITER $$
CREATE DEFINER=`mydodos_dbadmin`@`%` PROCEDURE `sp_GetHREmpExperience`(IN empId int)
BEGIN
SELECT a.* FROM tblHREmpExperience as a WHERE a.EmpID = empId;
END$$
DELIMITER ;


DELIMITER $$
CREATE DEFINER=`mydodos_dbadmin`@`%` PROCEDURE `sp_GetHRMasLeaveCategory`(
IN categoryId int,
IN tenantId int,
IN locationId int)
BEGIN
	If (categoryId<>0) then
		Begin
			select a.* from tblHRMasLeaveCategory as a where a.CategoryID = categoryId and a.TenantID = Tenant_ID and a.LocationID = locationId;
		End;
	else
		Begin
			select a.* from tblHRMasLeaveCategory as a where a.TenantID = Tenant_ID and a.LocationID = locationId;
		End;
end if;
END$$
DELIMITER ;


DELIMITER $$
CREATE DEFINER=`mydodos_dbadmin`@`%` PROCEDURE `sp_DeleteHRLeaveAllocReference`(
IN leaveAllocationId int)
BEGIN
	delete from tblHRLeaveAllocReference a where a.LeaveAllocationID = leaveAllocationId;
END$$
DELIMITER ;


DELIMITER $$
CREATE DEFINER=`mydodos_dbadmin`@`%` PROCEDURE `sp_GetHRLeaveAllocReference`(
IN leaveAllocationId int,
IN tenantId int,
IN locationId int)
BEGIN
	If (leaveAllocationId<>0) then
		Begin
			select a.* from tblHRLeaveAllocReference as a where a.LeaveAllocationID = leaveAllocationId and a.TenantID = Tenant_ID and a.LocationID = locationId;
		End;
	else
		Begin
			select a.* from tblHRLeaveAllocReference as a where a.TenantID = Tenant_ID and a.LocationID = locationId;
		End;
end if;
END$$
DELIMITER ;


DELIMITER $$
CREATE DEFINER=`mydodos_dbadmin`@`%` PROCEDURE `sp_DeleteHRMasLeaveCategory`(
IN categoryId int)
BEGIN
	delete from tblHRMasLeaveCategory a where a.CategoryID = categoryId;
END$$
DELIMITER ;




DELIMITER $$
CREATE DEFINER=`mydodos_dbadmin`@`%` PROCEDURE `sp_DeleteHREmpExperience`(IN empExpId int)
BEGIN
DELETE FROM tblHREmpExperience WHERE EmpExpID = empExpId;
select empExpId as ReqID, "Deleted Successfully" as Msg;
END$$
DELIMITER ; 




CREATE TABLE `tblStgDataReference` (
  `StgDataReferID` int NOT NULL AUTO_INCREMENT,
  `EntityName` varchar(250) DEFAULT NULL,
  `TenantID` int DEFAULT NULL,
  `LocationID` int DEFAULT NULL,
  `UniqueBatchNO` varchar(36) DEFAULT NULL,
  `Filename` varchar(50) DEFAULT NULL,
  `UploadedOn` datetime DEFAULT NULL,
  `BatchStatus` varchar(30) DEFAULT NULL,
  `IdentityName` varchar(250) DEFAULT NULL,
  `ActionType` varchar(250) DEFAULT NULL,
  `TemplateID` int DEFAULT NULL,
  PRIMARY KEY (`StgDataReferID`)
)


CREATE TABLE `tblStgDataEmployee` (
  `StgDataID` int NOT NULL AUTO_INCREMENT,
  `EmpNumber` varchar(100) DEFAULT NULL,
  `Prefix` varchar(20) DEFAULT NULL,
  `FirstName` varchar(150) DEFAULT NULL,
  `MiddleName` varchar(150) DEFAULT NULL,
  `LastName` varchar(150) DEFAULT NULL,
  `EmpName` varchar(255) DEFAULT NULL,
  `Gender` varchar(50) DEFAULT NULL,
  `DOB` varchar(50) DEFAULT NULL,
  `MobileNo` varchar(50) DEFAULT NULL,
  `FathersName` varchar(50) DEFAULT NULL,
  `MothersName` varchar(50) DEFAULT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `BloodGroup` varchar(50) DEFAULT NULL,
  `EducationalQualification` varchar(50) DEFAULT NULL,
  `DepartmentID` int DEFAULT NULL,
  `Department` varchar(50) DEFAULT NULL,
  `RoleID` int DEFAULT NULL,
  `RoleName` varchar(50) DEFAULT NULL,
  `ManagerID` int DEFAULT NULL,
  `ManagerName` varchar(75) DEFAULT NULL,
  `LocationID` int DEFAULT NULL,
  `TenantID` int DEFAULT NULL,
  `DateOfJoining` date DEFAULT NULL,
  `Address` varchar(255) DEFAULT NULL,
  `Address2` varchar(255) DEFAULT NULL,
  `City` varchar(50) DEFAULT NULL,
  `State` varchar(50) DEFAULT NULL,
  `Country` varchar(50) DEFAULT NULL,
  `Specializations` varchar(500) DEFAULT NULL,
  `LastEmploymentDetails` varchar(500) DEFAULT NULL,
  `StageFieldName` varchar(4000) DEFAULT NULL,
  `UniqueBatchNO` varchar(36) NOT NULL,
  `InsertDate` datetime DEFAULT NULL,
  `ProcessStatus` varchar(30) DEFAULT NULL,
  `IsExcepations` bit(1) DEFAULT NULL,
  `ExcepationFieldName` varchar(4000) DEFAULT NULL,
  PRIMARY KEY (`StgDataID`)
)


CREATE TABLE `tblStgHoliday` (
  `StgDataID` int NOT NULL AUTO_INCREMENT,
  `HolidayName` varchar(100) DEFAULT NULL,
  `HolidayDate` date DEFAULT NULL,
  `Description` varchar(150) DEFAULT NULL,
  `HolidayType` varchar(50) DEFAULT NULL,
  `TenantID` int DEFAULT NULL,
  `LocationID` int DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `ModifiedOn` datetime DEFAULT NULL,
  `IsLocked` bit(1) DEFAULT NULL,
  `StageFieldName` varchar(4000) DEFAULT NULL,
  `UniqueBatchNO` varchar(36) DEFAULT NULL,
  `InsertDate` datetime DEFAULT NULL,
  `ProcessStatus` varchar(30) DEFAULT NULL,
  `IsExcepations` bit(1) DEFAULT NULL,
  PRIMARY KEY (`StgDataID`)
)



DELIMITER $$
CREATE DEFINER=`mydodos_dbadmin`@`%` PROCEDURE `sp_HRSaveStageDocument`(IN ID int, IN entityname varchar(250),IN  tenantid int,IN UniqueBatchNO varchar(36),IN locationId int,IN filename VARCHAR(50),
IN Identity_Name varchar(250),IN Template_ID int, IN Action_Type varchar(250), Batch_Status varchar(75))
BEGIN
Declare rtn varchar(36) DEFAULT '';
IF(COALESCE(ID,0) = 0)
THEN
BEGIN
  INSERT INTO tblStgDataReference(EntityName, TenantID,LocationID,UniqueBatchNO,Filename, BatchStatus,UploadedOn,TemplateID,IdentityName,ActionType)
  VALUES(entityname, tenantid, locationId, UniqueBatchNO, filename, Batch_Status,CURDATE(),Template_ID,Identity_Name,Action_Type);
  set  rtn = UniqueBatchNO;
  END;
END IF;
  select rtn As id;
END$$
DELIMITER ;




DELIMITER $$
CREATE DEFINER=`mydodos_dbadmin`@`%` PROCEDURE `sp_GetStageCheckDetails`(IN Tenant_ID int, IN Institution_ID int, IN Common_ID int, IN Common_Name varchar(255),IN Entity_Name varchar(255))
BEGIN
IF(Entity_Name = 'CourseName')
THEN
	SELECT CourseID FROM tblACAPrograms WHERE TenantID = Tenant_ID AND InstitutionID = Institution_ID AND LOWER(replace(trim(CourseName) , ' ','')) = Common_Name AND CourseStatus = 'Active';
ELSE IF (Entity_Name = 'BatchName')
THEN
  SELECT BatchID FROM tblACAProgramInstance WHERE TenantID = Tenant_ID AND InstitutionID = Institution_ID AND CourseID = Common_ID;
	-- SELECT BatchID FROM tblACABatchEnrollment WHERE TenantID = Tenant_ID AND InstitutionID = Institution_ID AND LOWER(replace(trim(BatchName) , ' ','')) = Common_Name AND BatchStatus = 'Active';
ELSE IF (Entity_Name = 'DepartmentName')
THEN
   -- SELECT DeptID FROM tblACAProgramInstance WHERE TenantID = Tenant_ID AND InstitutionID = Institution_ID AND CourseID = Common_ID;
	if(Common_Name <> '')
	THEN	
		SELECT DeptID FROM tblMasDepartment WHERE TenantID = Tenant_ID AND InstitutionID = Institution_ID AND LOWER(replace(trim(DeptShortName) , ' ','')) = Common_Name AND DeptStatus = 'Active';
	else
		SELECT DeptID FROM tblACAProgramInstance WHERE TenantID = Tenant_ID AND InstitutionID = Institution_ID AND CourseID = Common_ID;
	END IF;
ELSE IF (Entity_Name = 'RoleName')
THEN
	SELECT RoleID FROM tblEntRole WHERE  LOWER(replace(trim(RoleName) , ' ','')) = Common_Name AND RoleID IN (SELECT RoleID FROM tblAssociatedRole WHERE DeptID = Common_ID);
ELSE IF (Entity_Name = 'ManagerName')
THEN
	SELECT EmpID FROM tblEmployee  WHERE InstitutionID = Institution_ID AND DepartmentID = Common_ID AND LOWER(replace(trim(REPLACE(CONCAT(FirstName, ' ',COALESCE(MiddleName,''),' ', COALESCE(LastName,'')), '  ',' ')), ' ','')) = Common_Name;
END IF;
END IF;
END IF;
END IF;
END IF;
END$$
DELIMITER ;




DELIMITER $$
CREATE DEFINER=`mydodos_dbadmin`@`%` PROCEDURE `sp_StageSaveEmployeeDoc`(IN StgData_ID int,IN empNumber VARCHAR(100),IN prefix VARCHAR(20),IN First_Name VARCHAR(100),IN Middle_Name VARCHAR(100),IN Last_Name VARCHAR(100),
IN empName VARCHAR(255), IN Gen_der varchar(30),IN DO_B date,IN Blood_Group VARCHAR(100), IN Mobile_No VARCHAR(100),IN fathersName varchar(50),IN mothersName varchar(50),IN E_mail VARCHAR(150),
IN Add_ress VARCHAR(255),IN Address_2 VARCHAR(255),IN C_ity VARCHAR(100),IN S_tate VARCHAR(100),IN C_ountry VARCHAR(100),IN Nation_ality VARCHAR(100),
IN Depart_ment VARCHAR(100),IN Department_ID int,IN Role_ID int,IN D_esignation VARCHAR(100),IN Manager_ID int,IN Manager_Name VARCHAR(100),IN locationId int,
IN Tenant_ID int,IN Educational_Qualification VARCHAR(500), IN Speciali_zations VARCHAR(500), IN LastEmployment_Details VARCHAR(500),
IN Date_Of_Joining date,IN Stage_FieldName VARCHAR(4000),IN UniqueBatch_NO VARCHAR(36), Exce_pation BIT, Excepation_FieldName VARCHAR(4000))
BEGIN
Declare rtn varchar(36) DEFAULT '';
IF(COALESCE(StgData_ID,0) = 0)
THEN
BEGIN
INSERT INTO tblStgDataEmployee(EmpNumber, Prefix, FirstName, MiddleName, LastName,EmpName, Gender, DOB, 
MobileNo,FathersName,MothersName, Email, Address,Address2, City, State, Country, Department, DepartmentID, RoleID, RoleName, 
ManagerID, ManagerName, LocationID, TenantID, EducationalQualification, Specializations, LastEmploymentDetails,
DateOfJoining,StageFieldName,UniqueBatchNO, InsertDate,ProcessStatus, IsExcepations, ExcepationFieldName)
  VALUES(empNumber, prefix,TRIM(First_Name), Middle_Name, TRIM(Last_Name), empName, Gen_der, DO_B, 
  TRIM(Mobile_No), TRIM(E_mail),fathersName,mothersName, Add_ress,Address_2, C_ity, S_tate, C_ountry,  Depart_ment, Department_ID, Role_ID, D_esignation,
  Manager_ID, Manager_Name, locationId, Tenant_ID, Educational_Qualification, Speciali_zations, LastEmployment_Details,
  Date_Of_Joining, Stage_FieldName,UniqueBatch_NO,CURDATE(),'Active',Exce_pation, Excepation_FieldName);
  set  rtn = UniqueBatch_NO;
  END;
  ELSE
  UPDATE tblStgDataEmployee SET IsExcepations = Exce_pation, ExcepationFieldName = Excepation_FieldName WHERE StgDataID = StgData_ID;
END IF;
select rtn As id;
END$$
DELIMITER ;





DELIMITER $$
CREATE DEFINER=`mydodos_dbadmin`@`%` PROCEDURE `sp_StageSaveHolidayDoc`(IN StgData_ID Int,IN CalEvent_Name varchar(50),IN  Event_Date date,IN Descri_ption varchar(250),IN Event_Type varchar(50),IN Tenant_ID int,
IN Institution_ID int, IN UniqueBatch_NO varchar(36),IN Stage_FieldName varchar(4000), IN Excep_tions bit)
BEGIN
Declare rtn varchar(36) DEFAULT '';
IF(COALESCE(StgData_ID,0) = 0)
THEN
BEGIN
  INSERT INTO tblStgHoliday(HolidayName, HolidayDate,Description,HolidayType,TenantID, InstitutionID,IsLocked,UniqueBatchNO,InsertDate,ProcessStatus,IsExcepations,StageFieldName)
  VALUES(CalEvent_Name, Event_Date, Descri_ption, Event_Type, Tenant_ID, Institution_ID, 0, UniqueBatch_NO, now(), 'Active', Excep_tions,Stage_FieldName);
  set  rtn = UniqueBatch_NO;
  END;
END IF;
  select rtn As id;
END$$
DELIMITER ;





DELIMITER $$
CREATE DEFINER=`mydodos_dbadmin`@`%` PROCEDURE `sp_GetDataReference`(IN tenantId INT, IN locationId INT,IN entirtyName varchar(250),  IN uniqueNO varchar(36))
BEGIN
IF(COALESCE(uniqueNO,'') <> '')
THEN
     SELECT * FROM tblStgDataReference WHERE LocationID = locationId AND UniqueBatchNO = uniqueNO AND BatchStatus = 'Active' order by StgDataReferID Desc;
ELSE
     SELECT * FROM tblStgDataReference WHERE TenantID = tenantId AND LocationID = locationId AND BatchStatus = 'Active' order by StgDataReferID Desc; -- AND EntityName = Entity_Name;
END IF;
END$$
DELIMITER ;




DELIMITER $$
CREATE DEFINER=`mydodos_dbadmin`@`%` PROCEDURE `sp_GetStageAllData`(IN tenantId int,
IN locationId int,
IN entityName varchar(250),
IN uniqueNO varchar(36),

IN search_data varchar(75),   
IN page_No int, 
IN page_Size int, 
IN orderBy_Column varchar(70), 
IN order_By varchar(15))
BEGIN
DECLARE  dsearchdata, dorderByColumn varchar(75) default '';
DECLARE dpageNbr,  dpageSize,  dfirstRec,  dlastRec,  dtotalRows INT DEFAULT 0;  

SET dsearchdata = LOWER(search_data);
SET dpageNbr := page_No;  
SET dpageSize := page_Size;  
SET dorderByColumn := LTRIM(RTRIM(orderBy_Column))  ;  
SET dfirstRec := ( dpageNbr - 1 ) * dpageSize ; 
SET dlastRec := ( dpageNbr * dpageSize + 1 );  
SET dtotalRows := dfirstRec - dlastRec + 1 ;

if(COALESCE(entityName,'') = 'Employee')
	THEN

	SET @TotalCount  := (SELECT COUNT(*) FROM tblStgDataEmployee WHERE UniqueBatchNO = uniqueNO AND ProcessStatus = 'Active' );
	SET @Excepations  := (SELECT COUNT(CASE WHEN COALESCE(IsExcepations,1)  = 1 THEN 1 END) FROM tblStgDataEmployee WHERE UniqueBatchNO = uniqueNO AND ProcessStatus = 'Active');
	SET @Validdata  := (SELECT COUNT(CASE WHEN COALESCE(IsExcepations,0)  = 0 THEN 1 END) FROM tblStgDataEmployee WHERE UniqueBatchNO = uniqueNO AND ProcessStatus = 'Active');

	DROP table IF EXISTS vwStageSearchData;
	CREATE TEMPORARY TABLE vwStageSearchData 
	SELECT *, ROW_NUMBER() OVER (ORDER BY  StgDataID,

			CASE WHEN dorderByColumn = 'FirstName' AND order_By='ASC'  
				THEN FirstName  
			END ASC,  
			CASE WHEN dorderByColumn = 'FirstName' AND order_By='DESC'  
				THEN FirstName  
			END DESC,
			CASE WHEN dorderByColumn = 'Gender' AND order_By='ASC'  
				THEN Gender  
			END ASC,  
			CASE WHEN dorderByColumn = 'Gender' AND order_By='DESC'  
				THEN Gender  
			END DESC,
			CASE WHEN dorderByColumn = 'EmpNumber' AND order_By='ASC'  
				THEN EmpNumber  
			END ASC,  
			CASE WHEN dorderByColumn = 'EmpNumber' AND order_By='DESC'  
				THEN EmpNumber  
			END DESC
	) AS ROWNUM, Count(*) over () AS SearchTotalCount, @TotalCount AS TotalCount,@Excepations AS Excepations, @Validdata AS Validdata,
    REPLACE(CONCAT(FirstName, ' ', COALESCE(MiddleName,''),' ', COALESCE(LastName,'')), '  ',' ') as FullName, COALESCE(DATE_FORMAT(convert(DateOfJoining,datetime) , '%m/%d/%Y'),'') AS JoiningDate,
    COALESCE(DATE_FORMAT(convert(DOB,datetime) , '%m/%d/%Y'),'') AS DateOfBirth
    FROM tblStgDataEmployee WHERE UniqueBatchNo = uniqueNO AND ProcessStatus = 'Active';  
		-- SELECT *,@TotalCount AS TotalCount,@Excepations AS Excepations, @Validdata AS Validdata FROM tblStgDataEmployee WHERE UniqueBatchNO = Unique_BatchNo;
	ELSE IF(COALESCE(entityName,'') = 'Holiday')
	THEN
	SET @TotalCount  := (SELECT COUNT(*) FROM tblStgHoliday WHERE UniqueBatchNO = uniqueNO AND ProcessStatus = 'Active');
	SET @Excepations  := (SELECT COUNT(CASE WHEN COALESCE(IsExcepations,1)  = 1 THEN 1 END) FROM tblStgHoliday WHERE UniqueBatchNO = uniqueNO AND ProcessStatus = 'Active');
	SET @Validdata  := (SELECT COUNT(CASE WHEN COALESCE(IsExcepations,0)  = 0 THEN 1 END) FROM tblStgHoliday WHERE UniqueBatchNO = uniqueNO AND ProcessStatus = 'Active');
		
	DROP table IF EXISTS vwStageSearchData;
	CREATE TEMPORARY TABLE vwStageSearchData 
	SELECT *, ROW_NUMBER() OVER (ORDER BY  StgDataID,

			CASE WHEN dorderByColumn = 'HolidayName' AND order_By='ASC'  
				THEN HolidayName  
			END ASC,  
			CASE WHEN dorderByColumn = 'HolidayName' AND order_By='DESC'  
				THEN HolidayName  
			END DESC,
			CASE WHEN dorderByColumn = 'HolidayDate' AND order_By='ASC'  
				THEN HolidayDate  
			END ASC,  
			CASE WHEN dorderByColumn = 'HolidayDate' AND order_By='DESC'  
				THEN HolidayDate  
			END DESC
	) AS ROWNUM, Count(*) over () AS SearchTotalCount, @TotalCount AS TotalCount,@Excepations AS Excepations, @Validdata AS Validdata  FROM tblStgHoliday WHERE UniqueBatchNo = uniqueNO AND ProcessStatus = 'Active';
		-- SELECT *,@TotalCount AS TotalCount,@Excepations AS Excepations, @Validdata AS Validdata FROM tblStgDataEventTracker WHERE UniqueBatchNO = UniqueNO;
	END IF;
	END IF;

if(COALESCE(dsearchdata,'')  <> '') THEN
begin

	IF(COALESCE(entityName,'') = 'Employee')
	THEN
		select * from vwStageSearchData AS a  where ((a.FullName LIKE CONCAT('%',  dsearchdata , '%') )
		OR (a.EmpNumber LIKE  CONCAT('%', dsearchdata , '%') ) OR (a.Gender LIKE CONCAT(dsearchdata , '%') ) OR (convert(a.DOB,char(50)) LIKE CONCAT('%',  dsearchdata , '%') )
	    OR (a.MobileNo LIKE CONCAT(dsearchdata , '%') )
        ) AND
        a.ROWNUM > dfirstRec   AND a.ROWNUM < dlastRec;
	ELSE IF(COALESCE(entityName,'') = 'Holiday')
	THEN
		select * from vwStageSearchData  where 
        ((a.CalEventName LIKE CONCAT('%',  dsearchdata , '%') )
		OR (convert(a.EventDate,char(50)) LIKE  CONCAT('%', dsearchdata , '%') ) 
        ) AND 
         ROWNUM > dfirstRec   AND ROWNUM < dlastRec;
	END IF;
	END IF;
END;
ELSE
BEGIN
	IF(COALESCE(entityName,'') = 'Employee')
	THEN
		select * from vwStageSearchData  where ROWNUM > dfirstRec   AND ROWNUM < dlastRec;
	ELSE IF(COALESCE(entityName,'') = 'Holiday')
	THEN
		select * from vwStageSearchData  where ROWNUM > dfirstRec   AND ROWNUM < dlastRec;
	END IF;
	END IF;
END;
END IF;
END$$
DELIMITER ;






DELIMITER $$
CREATE DEFINER=`mydodos_dbadmin`@`%` PROCEDURE `sp_StageCompleted`(IN Entity_Name Varchar(50),IN UniqueNO varchar(36), IN StgData_ID int)
BEGIN
if(COALESCE(Entity_Name,'') = 'Employee')
THEN
	if(StgData_ID >0)
	THEN
	   update tblStgDataEmployee SET  ProcessStatus = 'Completed'  WHERE StgDataID = StgData_ID;
	   IF((SELECT COUNT(*) FROM tblStgDataEmployee WHERE UniqueBatchNO = UniqueNO AND ProcessStatus = 'Active') = 0)
	   THEN
		SET SQL_SAFE_UPDATES = 0;
	   update tblStgDataReference SET  BatchStatus= 'Completed' WHERE UniqueBatchNO = UniqueNO AND  0 IN (SELECT COUNT(*) FROM tblStgDataEmployee WHERE UniqueBatchNO = UniqueNO AND ProcessStatus = 'Active');
	   SET SQL_SAFE_UPDATES = 1;
	   END IF;
	 End If;
ELSE IF(COALESCE(Entity_Name,'') = 'Holiday')
THEN
	if(StgData_ID > 0)
	THEN
    update tblStgDataEventTracker SET  ProcessStatus = 'Completed'  WHERE StgDataID = StgData_ID;
		IF((SELECT COUNT(*) FROM tblStgDataEventTracker WHERE UniqueBatchNO = UniqueNO AND ProcessStatus = 'Active') = 0)
		THEN
			SET SQL_SAFE_UPDATES = 0;
			update tblStgDataReference SET  BatchStatus= 'Completed' WHERE UniqueBatchNO = UniqueNO AND  0 IN (SELECT COUNT(*) FROM tblStgDataEventTracker WHERE UniqueBatchNO = UniqueNO AND ProcessStatus = 'Active');
            SET SQL_SAFE_UPDATES = 1;
		END IF;
    END IF;	 
END IF;
END IF;
END$$
DELIMITER ;






DELIMITER $$
CREATE DEFINER=`mydodos_dbadmin`@`%` PROCEDURE `sp_GetDataEmployee`(IN UniqueNO varchar(36), IN Entity_Name varchar(150),IN StgData_ID int)
BEGIN

if(COALESCE(Entity_Name,'') = 'Employee')
THEN
if(StgData_ID =0)
THEN
SET @TotalCount  := (SELECT COUNT(*) FROM tblStgDataEmployee WHERE UniqueBatchNO = UniqueNO);
SET @Excepations  := (SELECT COUNT(CASE WHEN COALESCE(IsExcepations,1)  = 1 THEN 1 END) FROM tblStgDataEmployee WHERE UniqueBatchNO = UniqueNO);
SET @Validdata  := (SELECT COUNT(CASE WHEN COALESCE(IsExcepations,0)  = 0 THEN 1 END) FROM tblStgDataEmployee WHERE UniqueBatchNO = UniqueNO);
    
    SELECT *,@TotalCount AS TotalCount,@Excepations AS Excepations, @Validdata AS Validdata FROM tblStgDataEmployee WHERE UniqueBatchNO = UniqueNO;
 else
 SELECT * FROM tblStgDataEmployee WHERE StgDataID = StgData_ID;
 End If;
ELSE IF(COALESCE(Entity_Name,'') = 'Holiday')
THEN
if(StgData_ID =0)
THEN
SET @TotalCount  := (SELECT COUNT(*) FROM tblStgDataEventTracker WHERE UniqueBatchNO = UniqueNO);
SET @Excepations  := (SELECT COUNT(CASE WHEN COALESCE(IsExcepations,1)  = 1 THEN 1 END) FROM tblStgDataEventTracker WHERE UniqueBatchNO = UniqueNO);
SET @Validdata  := (SELECT COUNT(CASE WHEN COALESCE(IsExcepations,0)  = 0 THEN 1 END) FROM tblStgDataEventTracker WHERE UniqueBatchNO = UniqueNO);
    
    SELECT *,@TotalCount AS TotalCount,@Excepations AS Excepations, @Validdata AS Validdata FROM tblStgDataEventTracker WHERE UniqueBatchNO = UniqueNO;
    else
 SELECT * FROM tblStgDataEventTracker WHERE StgDataID = StgData_ID;
 End If;
END IF;
END IF;
END$$
DELIMITER ;






DELIMITER $$
CREATE DEFINER=`mydodos_dbadmin`@`%` PROCEDURE `sp_GetStageReference`(

IN tenantId int,
IN locationId int,
IN entirtyName varchar(250),

IN search_data varchar(75),   
IN page_No int, 
IN page_Size int, 
IN orderBy_Column varchar(70), 
IN order_By varchar(15)
)
BEGIN
DECLARE  dsearchdata, dorderByColumn varchar(75) default '';
DECLARE dpageNbr,  dpageSize,  dfirstRec,  dlastRec,  dtotalRows INT DEFAULT 0;  

SET dsearchdata = LOWER(search_data);
SET dpageNbr := page_No;  
SET dpageSize := page_Size;  
SET dorderByColumn := LTRIM(RTRIM(orderBy_Column))  ;  
SET dfirstRec := ( dpageNbr - 1 ) * dpageSize ; 
SET dlastRec := ( dpageNbr * dpageSize + 1 );  
SET dtotalRows := dfirstRec - dlastRec + 1 ;

IF(COALESCE(dsearchdata,'')  <> '')
THEN
BEGIN
		WITH CTE_Results  
		AS (  
		SELECT ROW_NUMBER() OVER (ORDER BY  EntityName, 
  
			CASE WHEN dorderByColumn = 'EntityName' AND order_By='ASC'  
					THEN EntityName  
			END ASC,  
			CASE WHEN dorderByColumn = 'IdentityName' AND order_By='DESC'  
					THEN IdentityName  
			END DESC  
  
	  ) AS ROWNUM,  
	  Count(*) over () AS SearchTotalCount,  
	 TenantID,BatchStatus,EntityName,IdentityName,Filename,ActionType,LocationID,UploadedOn,UniqueBatchNO
	 FROM tblStgDataReference  WHERE TenantID = tenantId AND LocationID = locationId AND EntityName = entirtyName AND
     ((IdentityName LIKE  CONCAT('%',  dsearchdata , '%')) OR  ( Filename LIKE  CONCAT( dsearchdata , '%') )
        OR ( ActionType LIKE  CONCAT( dsearchdata , '%') ))
     )
     SELECT  
		SearchTotalCount,		  
		TenantID,LocationID,BatchStatus,EntityName,IdentityName,Filename,ActionType,UploadedOn,UniqueBatchNO
	FROM CTE_Results AS cte WHERE  ROWNUM > dfirstRec   AND ROWNUM < dlastRec    
    order by cte.StgDataReferID Desc;
    END;
ELSE
BEGIN
WITH CTE_Results  
		AS (  
		SELECT ROW_NUMBER() OVER (ORDER BY  EntityName, 
  
			CASE WHEN dorderByColumn = 'EntityName' AND order_By='ASC'  
					THEN EntityName  
			END ASC,  
			CASE WHEN dorderByColumn = 'IdentityName' AND order_By='DESC'  
					THEN IdentityName  
			END DESC  
  
	  ) AS ROWNUM,  
	  Count(*) over () AS SearchTotalCount,  
	 TenantID,BatchStatus,EntityName,IdentityName,Filename,ActionType,LocationID,UploadedOn,UniqueBatchNO
	 FROM tblStgDataReference  WHERE TenantID = tenantId AND LocationID = locationId AND EntityName = entirtyName
     )
     SELECT  
		SearchTotalCount,		  
		TenantID,LocationID,BatchStatus,EntityName,IdentityName,Filename,ActionType,UploadedOn,UniqueBatchNO
	FROM CTE_Results AS cte WHERE  ROWNUM > dfirstRec   AND ROWNUM < dlastRec    
    order by cte.StgDataReferID Desc;
END;
    END IF;
END$$
DELIMITER ;




insert into tblHRBPTransaction(BProcessID, BPTransName, BeginWhen, EndsWhen, PreCondition, PostCondition, BusinessUnit, TransStatus, TransOrder, IsMandatory, BusinessURL, PrevURL, NxtURL, LocationID)
select 2, 'Request Initiation', BeginWhen, EndsWhen, PreCondition, PostCondition, BusinessUnit, 'Active', 1, 1, 'human-resource/emp-offboard/emp-request', null, 'human-resource/emp-offboard/emp-checklist', 5 from tblHRBPTransaction where BPTransID = 1

insert into tblHRBPTransaction(BProcessID, BPTransName, BeginWhen, EndsWhen, PreCondition, PostCondition, BusinessUnit, TransStatus, TransOrder, IsMandatory, BusinessURL, PrevURL, NxtURL, LocationID)
select 2, 'Check List', BeginWhen, EndsWhen, PreCondition, PostCondition, BusinessUnit, 'Active', 2, 1, 'human-resource/emp-offboard/emp-checklist', 'human-resource/emp-offboard/emp-request', 'human-resource/emp-offboard/emp-readytoexit', 5 from tblHRBPTransaction where BPTransID = 1


insert into tblHRBPTransaction(BProcessID, BPTransName, BeginWhen, EndsWhen, PreCondition, PostCondition, BusinessUnit, TransStatus, TransOrder, IsMandatory, BusinessURL, PrevURL, NxtURL, LocationID)
select 2, 'Ready to exit', BeginWhen, EndsWhen, PreCondition, PostCondition, BusinessUnit, 'Active', 3, 1, 'human-resource/emp-offboard/emp-readytoexit', 'human-resource/emp-offboard/emp-checklist', null, 5 from tblHRBPTransaction where BPTransID = 1



insert into tblHRBPTransaction(BProcessID, BPTransName, BeginWhen, EndsWhen, PreCondition, PostCondition, BusinessUnit, TransStatus, TransOrder, IsMandatory, BusinessURL, PrevURL, NxtURL, LocationID)
select 8, 'Request Initiation', BeginWhen, EndsWhen, PreCondition, PostCondition, BusinessUnit, 'Active', 1, 1, 'human-resource/emp-offboard/emp-request', null, 'human-resource/emp-offboard/emp-checklist', 6 from tblHRBPTransaction where BPTransID = 1

insert into tblHRBPTransaction(BProcessID, BPTransName, BeginWhen, EndsWhen, PreCondition, PostCondition, BusinessUnit, TransStatus, TransOrder, IsMandatory, BusinessURL, PrevURL, NxtURL, LocationID)
select 8, 'Check List', BeginWhen, EndsWhen, PreCondition, PostCondition, BusinessUnit, 'Active', 2, 1, 'human-resource/emp-offboard/emp-checklist', 'human-resource/emp-offboard/emp-request', 'human-resource/emp-offboard/emp-readytoexit', 6 from tblHRBPTransaction where BPTransID = 1


insert into tblHRBPTransaction(BProcessID, BPTransName, BeginWhen, EndsWhen, PreCondition, PostCondition, BusinessUnit, TransStatus, TransOrder, IsMandatory, BusinessURL, PrevURL, NxtURL, LocationID)
select 8, 'Ready to exit', BeginWhen, EndsWhen, PreCondition, PostCondition, BusinessUnit, 'Active', 3, 1, 'human-resource/emp-offboard/emp-readytoexit', 'human-resource/emp-offboard/emp-checklist', null, 6 from tblHRBPTransaction where BPTransID = 1




public string UpdateCenEntAppUser(UserProfileData objuser)
{
string data = "";
using (HttpClientHandler clientHandler = new HttpClientHandler())
{clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
using (var httpClient = new HttpClient(clientHandler))
{
using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/UpdateAppuserInfo"))
{string SendResult = JsonConvert.SerializeObject(objuser);
 request.Content = new StringContent(SendResult);
request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
var logresponse = httpClient.SendAsync(request).Result;
var json = logresponse.Content.ReadAsStringAsync().Result;
 var jsondata = JObject.Parse(json);
data = jsondata["data"].ToString();
}
}
}
return data;
}




alter table tblMileStone add column Percentage decimal(10,2) after EndDate
alter table tblPPProject add column CompletionMode varchar(50) after CompPercent
sp_SaveNewProject - Changes
sp_SaveMileStone  - Changes
sp_SavePPSponsor
sp_GetPPSponsor
sp_DeletePPSponsor	
sp_SavePPInitiativeType
sp_GetPPInitiativeType
sp_DeletePPInitiativeType
sp_DeleteProject
sp_DeletePPProjectRole
sp_SavePPProjectDocumentType
sp_GetPPProjectDocumentType
sp_DeletePPProjectDocumentType
sp_SaveMasterTSNonBillable
sp_SaveApplyTimeSheet
sp_SaveTimesheetOverAll
sp_SaveTimeSheetflagged


CREATE TABLE `tblPPProjectDocumentType` (
  `DocumentTypeID` int NOT NULL AUTO_INCREMENT,
  `DocumentType` varchar(250) DEFAULT NULL,
  `Description` varchar(250) DEFAULT NULL,
  `TenantID` int DEFAULT NULL,
  `LocationID` int DEFAULT NULL,
  `DocumentTypeStatus` varchar(50) DEFAULT NULL,
  `CreatedBy` int DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `ModifiedBy` int DEFAULT NULL,
  `ModifiedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`DocumentTypeID`)
)