Module: Leave Management

ALTER TABLE tblHREmpLeaveAlloc
ADD COLUMN EmpAllocStatus varchar(30);

UPDATE tblHREmpLeaveAlloc SET EmpAllocStatus = 'Active' WHERE YearID= 5;