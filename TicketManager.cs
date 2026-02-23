using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
namespace TicketManagerIO
{
    public class TicketManager
    {
        private readonly List<Ticket> _tickets = new();
        public void AddTicket(Ticket t)
        {
            //First check if the ticket is null and throw
            ArgumentNullException(nameof(t)) if so
            //Now use your FindTicket(t.Id) if it is not null, throw an
InvalidOperationException
//In your exception, include that "A ticket with {t.Id} already
exists"
        //Finally call _tickets.Add(t);
}
        public bool RemoveTicket(string id)
        {
            //call FindTicket(id) and store it in a var
            //If the var is null, return false
            //If the var is true, call _tickets.Remove(t) and then return
            true
        }
        public Ticket? FindTicket(string id)
        {
            //Call a foreach (var t in tickets)
            //Do an if statement and check if t.Id Equals id. Use
            string.Equals()
        //If yes, return t;
        //After the foreach finishes, return null. There was no matching
ticket found
        }
        public void DisplayAllTickets()
        {
            if (_tickets.Count == 0)
            {
                Console.WriteLine("No tickets found.");
                return;
            }
            Console.WriteLine("\n--- Ticket List ---");
            foreach (var t in _tickets)
                Console.WriteLine(t.GetSummary());
        }
        public int GetOpenCount()
        {
            int count = 0;
            foreach (var t in _tickets)
                if (!string.Equals(t.Status, "Closed",
                StringComparison.OrdinalIgnoreCase))
                    count++;
            return count;
        }
        // ---------- CSV Persistence ----------
        // Header: Id,Description,Priority,Status,DateCreated
        public void SaveTickets(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(path))!);
            using var sw = new StreamWriter(path, false, new
            UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            sw.WriteLine("Id,Description,Priority,Status,DateCreated");
            foreach (var t in _tickets)
            {
                string line = string.Join(",",
                //Fill in the rest of this join statement by using
                CsvEscape on each variable.
                //Use commas in between statements
                CsvEscape(t.DateCreated.ToString("o",
                CultureInfo.InvariantCulture)) // ISO 8601
                );
                sw.WriteLine(line);
            }
        }
        public void LoadTickets(string path)
        {
            using var sr = new StreamReader(path, Encoding.UTF8);
            // Clear current list before loading new file
            _tickets.Clear();
            string? header = sr.ReadLine();
            if (header is null)
                throw new InvalidDataException("File is empty.");
            int lineNo = 1;
            int loaded = 0, skipped = 0;
            while (!sr.EndOfStream)
            {
                string? line = sr.ReadLine();
                lineNo++;
                if (string.IsNullOrWhiteSpace(line)) { continue; }
                try
                {
                    var cols = CsvParse(line);
                    if (cols.Count != 5)
                        throw new InvalidDataException($"Expected 5 columns, found
                        { cols.Count }.");
                //Create variables to hold the information from each
column.Call the array of cols[0] to cols[4]
                //Assign them to the appropriate variables to use
wiht the constructor later
                //The purpose of this is to save the date from the
loaded CSVs
                if (!DateTime.TryParse(created, CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind, out var when))
                        throw new InvalidDataException("Invalid DateCreated.");
                    //Call your overloaded constructor here and enter the
                    values from the columns
                // Override auto-created date with persisted value, this adds
the timestamp from above if it's valid
                typeof(Ticket).GetProperty(nameof(Ticket.DateCreated))!
                .SetValue(t, when);
                    //Call your AddTicket() method here and pass in the Ticket from
                    the constructor
                    loaded++;
                }
                catch (Exception ex)
                {
                    skipped++;
                    Console.WriteLine($"Skipped line {lineNo}: {ex.Message}");
                }
            }
            Console.WriteLine($"Load complete. Loaded: {loaded}, Skipped:
        { skipped}.");
        }
        //!!!!! CHANGE NOTHING BELOW THIS LINE, IT HANDLES CSV I/O
        // ---------- Minimal CSV helpers (handles quotes & commas) ----------
        private static string CsvEscape(string input)
        {
            bool needsQuotes = input.Contains(',') || input.Contains('"') ||
            input.Contains('\n') || input.Contains('\r');
            if (!needsQuotes) return input;
            return "\"" + input.Replace("\"", "\"\"") + "\"";
        }
        private static List<string> CsvParse(string line)
        {
            var result = new List<string>();
            var sb = new StringBuilder();
            bool inQuotes = false;
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (inQuotes)
                {
                    if (c == '"')
                    {
                        // Escaped quote?
                        if (i + 1 < line.Length && line[i + 1] == '"')
                        {
                            sb.Append('"');
                            i++;
                        }
                        else
                        {
                            inQuotes = false;
                        }
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
                else
                {
                    if (c == ',')
                    {
                        result.Add(sb.ToString());
                        sb.Clear();
                    }
                    else if (c == '"')
                    {
                        inQuotes = true;
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
            }
            result.Add(sb.ToString());
            return result;
        }
    }
}