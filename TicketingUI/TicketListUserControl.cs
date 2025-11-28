#nullable disable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TicketingUI
{
    // A simple class to hold our Ticket Data
    public class Ticket
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool IsPinned { get; set; } = false;
        public bool IsArchived { get; set; } = false;
        public string Sender { get; set; }
        public string Concern { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }

    public class TicketListUserControl : UserControl
    {
        private DataGridView dgv;
        private ContextMenuStrip contextMenu; // The Right-Click Menu

        private List<Ticket> allTickets = new List<Ticket>();
        private string currentView = "Inbox"; // Can be: Inbox, Pinned, Sent, Archived

        public TicketListUserControl()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            // 1. Setup the Grid
            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                EnableHeadersVisualStyles = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false
            };

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 35;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9);

            // 2. Add Columns
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "📌", Width = 30, Name = "colPin" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Display Name", Width = 150 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Concern - Description", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Status", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Date", Width = 100 });

            // 3. Events for Clicking
            dgv.CellClick += Dgv_CellClick;

            // 4. Setup Right-Click Menu (Context Menu)
            contextMenu = new ContextMenuStrip();
            ToolStripMenuItem itemArchive = new ToolStripMenuItem("📥 Archive Ticket");
            itemArchive.Click += (s, e) => ArchiveSelectedTicket();
            contextMenu.Items.Add(itemArchive);
            dgv.ContextMenuStrip = contextMenu;

            // 5. Sample Data
            AddNewTicket("John Doe", "Software Bug - App crashes", "Open");
            AddNewTicket("Jane Smith", "Feature Request - Dark Mode", "Resolved");

            this.Controls.Add(dgv);
        }

        public void AddNewTicket(string user, string concern, string status)
        {
            var ticket = new Ticket
            {
                Sender = user,
                Concern = concern,
                Status = status,
                Date = DateTime.Now
            };
            allTickets.Add(ticket);
            RefreshGrid();
        }

        // --- FILTERING LOGIC ---
        public void SetView(string viewName)
        {
            this.currentView = viewName;
            RefreshGrid(); // Re-draw grid based on new view
        }

        public void FilterByStatus(string status)
        {
            // Optional: Implement tab filtering if needed
            // For now, we focus on Sidebar filtering
        }

        private void RefreshGrid()
        {
            dgv.Rows.Clear();

            // Filter the master list based on what "View" we are in
            IEnumerable<Ticket> filtered = allTickets;

            if (currentView == "Inbox")
            {
                // Show everything NOT archived
                filtered = filtered.Where(t => !t.IsArchived);
            }
            else if (currentView == "Pinned")
            {
                // Show only Pinned (and not archived)
                filtered = filtered.Where(t => t.IsPinned && !t.IsArchived);
            }
            else if (currentView == "Archived")
            {
                // Show only Archived
                filtered = filtered.Where(t => t.IsArchived);
            }
            // "Sent" acts like Inbox for now

            // Add filtered rows to grid
            foreach (var t in filtered)
            {
                string pinIcon = t.IsPinned ? "📌" : "☆"; // Show Pin or Star
                dgv.Rows.Add(pinIcon, t.Sender, t.Concern, t.Status, t.Date.ToString("MMM-dd"));

                // Save the Ticket object in the row so we can find it later
                dgv.Rows[dgv.Rows.Count - 1].Tag = t;
            }
        }

        // --- PINNING LOGIC (Click the first column) ---
        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0) // Column 0 is the Pin column
            {
                var row = dgv.Rows[e.RowIndex];
                var ticket = row.Tag as Ticket; // Get the data

                if (ticket != null)
                {
                    ticket.IsPinned = !ticket.IsPinned; // Toggle status
                    RefreshGrid(); // Update UI
                }
            }
        }

        // --- ARCHIVING LOGIC (Called by Right-Click) ---
        private void ArchiveSelectedTicket()
        {
            if (dgv.SelectedRows.Count > 0)
            {
                var row = dgv.SelectedRows[0];
                var ticket = row.Tag as Ticket;

                if (ticket != null)
                {
                    ticket.IsArchived = true; // Mark as archived
                    RefreshGrid(); // It will disappear from Inbox!
                }
            }
        }
    }
}