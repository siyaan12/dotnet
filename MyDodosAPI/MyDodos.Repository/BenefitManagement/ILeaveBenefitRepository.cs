using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Common;

namespace MyDodos.Repository.BenefitManagement
{
    public interface ILeaveBenefitRepository
    {
        SaveOut SaveMasLeaveGroup(MasLeaveGroupBO objgroup);
        List<MasLeaveGroupBO> GetMasLeaveGroup(int LeaveGroupID, int TenantID, int LocationID);
        List<MasLeaveGroupBO> GetMasLeaveGroupByGroupType(int GroupTypeID, int TenantID, int LocationID);
        SaveOut DeleteMasLeaveGroup(int LeaveGroupID, int TenantID, int LocationID);
        SaveOut SaveLeaveAllocReference(LeaveAllocReferenceBO objref, int LeaveGroupID);
        List<LeaveAllocReferenceBO> GetLeaveAllocReference(int LeaveGroupID, int TenantID, int LocationID);
        SaveOut DeleteLeaveAllocReference(int LeaveGroupID, int TenantID, int LocationID);
    }
}