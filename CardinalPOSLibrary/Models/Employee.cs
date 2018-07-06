using System;

namespace CardinalPOSLibrary.Models
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public bool Active { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PinCode { get; set; }
    }
}
