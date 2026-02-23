using System;

namespace TicketManagerIO
{
    public class Ticket
    {
        // Allowed values (kept as strings to match the assignment spec)
        public static readonly string[] AllowedPriorities = { "Low", "Medium", "High" };
        public static readonly string[] AllowedStatuses = { "Open", "In Progress", "Closed" };

        // Private variables initialized and named according to C# guidelines
        private string _id = "";
        private string _description = "";
        private string _priority = "Low";
        private string _status = "Open";

        // Id property
        public string Id
        {
            get => _id;

            // Create a set => method that rejects null or whitespace values
            // and throws an argument that the ID cannot be empty.
            set
            {
                var v = (value ?? "").Trim();
                if (string.IsNullOrWhiteSpace(v))
                    throw new ArgumentException("ID cannot be empty.");

                _id = v;
            }
        }

        // Description property
        public string Description
        {
            get => _description;

            // Create a set => method that rejects null or whitespace values
            // and throws an argument that the description cannot be empty.
            set
            {
                var v = (value ?? "").Trim();
                if (string.IsNullOrWhiteSpace(v))
                    throw new ArgumentException("Description cannot be empty.");

                _description = v;
            }
        }

        // Priority property
        public string Priority
        {
            get => _priority;

            // This one validates against the AllowedPriorities array
            set
            {
                var v = (value ?? "").Trim();

                // Reject anything not in the AllowedPriorities array
                if (Array.IndexOf(AllowedPriorities, v) < 0)
                    throw new ArgumentException(
                        $"Priority must be one of: {string.Join(", ", AllowedPriorities)}");

                _priority = v;
            }
        }

        // Status property
        public string Status
        {
            get => _status;

            // Make a set method in the same style as Priority,
            // rejecting anything not in the AllowedStatuses array
            set
            {
                var v = (value ?? "").Trim();

                if (Array.IndexOf(AllowedStatuses, v) < 0)
                    throw new ArgumentException(
                        $"Status must be one of: {string.Join(", ", AllowedStatuses)}");

                _status = v;
            }
        }

        // C#'s way to get the current DateTime from the system
        // DateCreated is automatically set when the object is created
        public DateTime DateCreated { get; private set; }

        // Default Constructor
        public Ticket()
        {
            // Automatically set DateCreated to current UTC time
            DateCreated = DateTime.UtcNow;
        }

        // Overloaded constructor that accepts
        // string id, string description, string priority, string status
        public Ticket(string id, string description, string priority, string status)
        {
            // Automatically set DateCreated
            DateCreated = DateTime.UtcNow;

            // Assign values to public class properties
            Id = id;
            Description = description;
            Priority = priority;
            Status = status;
        }

        // Public void called CloseTicket()
        // Sets Status to "Closed"
        public void CloseTicket()
        {
            Status = "Closed";
        }

        // Public void called ReopenTicket()
        // Sets Status to "Open"
        public void ReopenTicket()
        {
            Status = "Open";
        }

        // Public string that returns all public variables
        // in the following format:
        // [T1001] (High) - "Printer not working" | Status: Open | Created: 2025-10-29
        public string GetSummary()
        {
            return $"[{Id}] ({Priority}) - \"{Description}\" | Status: {Status} | Created: {DateCreated:yyyy-MM-dd}";
        }
    }
}