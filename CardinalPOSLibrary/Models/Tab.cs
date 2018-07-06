using System;

namespace CardinalPOSLibrary.Models
{
    public class Tab
    {
        public Guid TabId { get; set; }
        public TabState CurrentState { get; set; }
        public DateTime TimestampOpened { get; set; }
        public DateTime TimestampClosed { get; set; }
        public DateTime TimestampFinalized { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardToken { get; set; }
    }
}
