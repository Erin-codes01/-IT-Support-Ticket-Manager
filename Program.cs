using System;
using System.IO;

namespace TicketManagerIO
{
    internal static class Program
    {
        private static void Main()
        {
            // Create a var of your new TicketManager() class. Initialize as new.
            // Suggested name is 'manager'
            var manager = new TicketManager();

            // Greet the user warmly
            Console.WriteLine("=== Welcome To IT Support Ticket Manager ===");

            // Encapsulate the UI in a while loop
            bool running = true;
            while (running)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add Ticket");
                Console.WriteLine("2. Remove Ticket");
                Console.WriteLine("3. Display All Tickets");
                Console.WriteLine("4. Close A Ticket");
                Console.WriteLine("5. Reopen A Ticket");
                Console.WriteLine("6. Save Tickets to File");
                Console.WriteLine("7. Load Tickets from File");
                Console.WriteLine("8. Exit");
                Console.Write("Choose: ");

                string? choice = Console.ReadLine()?.Trim();

                try
                {
                    switch (choice)
                    {
                        // Call each case and use the appropriate TicketManager method.
                        // Don't forget that each switch statement requires a break;

                        case "1":
                            AddTicketMenu(manager);
                            break;

                        case "2":
                            RemoveTicketMenu(manager);
                            break;

                        case "3":
                            manager.DisplayAllTickets();
                            break;

                        case "4":
                            UpdateTicketStatus(manager, true);
                            break;

                        case "5":
                            UpdateTicketStatus(manager, false);
                            break;

                        case "6":
                            SaveMenu(manager);
                            break;

                        case "7":
                            LoadMenu(manager);
                            break;

                        case "8":
                            running = false;
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            // Give the user a goodbye message
            Console.WriteLine("Goodbye! Thank you for using the Ticket Manager.");
        }

        private static void AddTicketMenu(TicketManager manager)
        {
            Console.Write("Enter Ticket ID (e.g., T1001): ");
            string id = Console.ReadLine() ?? "";

            Console.Write("Enter Description: ");
            string desc = Console.ReadLine() ?? "";

            Console.Write("Enter Priority (Low/Medium/High): ");
            string priority = NormalizeCase(Console.ReadLine());

            Console.Write("Enter Status (Open/In Progress/Closed): ");
            string status = NormalizeCase(Console.ReadLine());

            // Create a new Ticket using the overloaded constructor
            var ticket = new Ticket(id, desc, priority, status);

            // Then use AddTicket from your TicketManager to pass it to the list
            manager.AddTicket(ticket);

            Console.WriteLine("Ticket added!");
        }

        private static void RemoveTicketMenu(TicketManager manager)
        {
            Console.Write("Enter Ticket ID to remove: ");
            string id = Console.ReadLine() ?? "";

            // Check if the Ticket ID exists, then call RemoveTicket()
            bool removed = manager.RemoveTicket(id);

            // Update the user if it was removed or not. Ternary operator is your friend.
            Console.WriteLine(removed
                ? "Ticket removed successfully!"
                : "Sorry the ticket was not found.");
        }

        // Create another Private Static Void to Close and Reopen tickets.
        // There are many ways you could do this. This void accepts either option from the UI.
        private static void UpdateTicketStatus(TicketManager manager, bool close)
        {
            Console.Write("Enter Ticket ID: ");
            string id = Console.ReadLine() ?? "";

            var ticket = manager.FindTicket(id);

            if (ticket == null)
            {
                Console.WriteLine("Sorry the ticket was not found.");
                return;
            }

            if (close)
            {
                ticket.CloseTicket();
                Console.WriteLine("Ticket was closed.");
            }
            else
            {
                ticket.ReopenTicket();
                Console.WriteLine("Ticket was reopened.");
            }
        }

        // This method should require no change. Learn from this for your LoadMenu() module
        private static void SaveMenu(TicketManager manager)
        {
            Console.Write("Enter path to save CSV (e.g., tickets.csv): ");
            string path = Console.ReadLine() ?? "";

            try
            {
                manager.SaveTickets(path);
                Console.WriteLine($"Saved to {Path.GetFullPath(path)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Save failed: {ex.Message}");
            }
        }

        private static void LoadMenu(TicketManager manager)
        {
            Console.Write("Enter path to load CSV (e.g., tickets.csv): ");
            string path = Console.ReadLine() ?? "";

            // Create a Try/Catch/Catch block here
            // You're going to try to open the file
            try
            {
                manager.LoadTickets(path);
                Console.WriteLine("Tickets loaded successfully!");
            }
            // One catch will be if the file does not exist
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }
            // At least one other catch will be if the file is not formatted correctly
            catch (InvalidDataException ex)
            {
                Console.WriteLine($"Invalid file format: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Load failed: {ex.Message}");
            }
        }

        // Used to prevent rejection based on "low" instead of "Low" etc.
        private static string NormalizeCase(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "";

            var s = input.Trim().ToLowerInvariant();

            if (s == "low") return "Low";
            if (s == "medium" || s == "med") return "Medium";
            if (s == "high") return "High";
            if (s == "open") return "Open";
            if (s == "in progress" || s == "in-progress" || s == "progress") return "In Progress";
            if (s == "closed" || s == "close") return "Closed";

            return input.Trim(); // Leave as-is; Ticket validation will enforce allowed set
        }
    }
}