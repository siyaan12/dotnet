using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDodos.ViewModel.TicketingSystem
{
    public class RiseTicket
    {
        public string subject { get; set; }
        public string from { get; set; }
        public string message { get; set; }
        public string actAsType { get; set; }
        public string name { get; set; }
        public string actAsEmail { get; set; }
    }
    public class SaveTicket
    {
        public string message { get; set; }
        public int ticketId { get; set; }
    }
    public class Ticket
    {
        public int id { get; set; }
        public string subject { get; set; }
        public bool isCustomerView { get; set; }
        public Status status { get; set; }
        public object group { get; set; }
        public Type type { get; set; }
        public Priority priority { get; set; }
        public string formatedCreatedAt { get; set; }
        public string totalThreads { get; set; }
        public Agent agent { get; set; }
        public Customer customer { get; set; }
    }
    public class Status
    {
        public int id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string colorCode { get; set; }
        public int sortOrder { get; set; }
    }

    public class Type
    {
        public int id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public bool isActive { get; set; }
        public string name { get; set; }
    }

    public class Priority
    {
        public int id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string colorCode { get; set; }
    }

    public class Agent
    {
        public int id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public object profileImagePath { get; set; }
        public object smallThumbnail { get; set; }
        public bool isActive { get; set; }
        public bool isVerified { get; set; }
        public object designation { get; set; }
        public object contactNumber { get; set; }
        public object signature { get; set; }
        public object ticketAccessLevel { get; set; }
    }
    public class Customer
    {
        public int id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public object contactNumber { get; set; }
        public object profileImagePath { get; set; }
        public object smallThumbnail { get; set; }
    }
    public class TicketingInputBO
    {
        public string email { get; set; }
        public string name { get; set; }
    }
}
