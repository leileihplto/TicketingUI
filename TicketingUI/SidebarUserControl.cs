using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicketingUI
{
    public class SidebarUserControl : UserControl
    {
        public event EventHandler? OnNewTicketClicked;
        // Event to send the view name (Inbox, Pinned, etc.)
        public event EventHandler<string>? OnMenuOptionClicked;

        public SidebarUserControl()
        {
            this.Width = 220;
            this.Dock = DockStyle.Left;
            this.BackColor = Color.WhiteSmoke;
            InitializeSidebar();
        }

        private void InitializeSidebar()
        {
            // New Ticket Button
            Button btnNew = new Button { Text = "+ New Ticket", BackColor = Color.FromArgb(230, 230, 230), FlatStyle = FlatStyle.Flat, Size = new Size(180, 45), Location = new Point(20, 20), Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnNew.FlatAppearance.BorderSize = 0;
            btnNew.Click += (s, e) => OnNewTicketClicked?.Invoke(this, EventArgs.Empty);
            this.Controls.Add(btnNew);

            string[] menus = { "Inbox", "Pinned", "Sent", "Archived" };
            string[] icons = { "::", "📌", "➤", "📥" };
            int startY = 90;

            for (int i = 0; i < menus.Length; i++)
            {
                string menuName = menus[i]; // Capture for closure
                Button menuBtn = new Button { Text = $"  {icons[i]}   {menuName}", TextAlign = ContentAlignment.MiddleLeft, FlatStyle = FlatStyle.Flat, BackColor = (i == 0) ? Color.LightGray : Color.Transparent, Size = new Size(220, 40), Location = new Point(0, startY + (i * 45)), Font = new Font("Segoe UI", 10) };
                menuBtn.FlatAppearance.BorderSize = 0;

                // Click Event
                menuBtn.Click += (s, e) =>
                {
                    // Reset all button colors
                    foreach (Control c in this.Controls) if (c is Button b && b != btnNew) b.BackColor = Color.Transparent;

                    // Highlight this one
                    menuBtn.BackColor = Color.LightGray;

                    // Notify Main Form
                    OnMenuOptionClicked?.Invoke(this, menuName);
                };

                this.Controls.Add(menuBtn);
            }

            // ... (Management section code remains the same) ...
            Label lblMgmt = new Label { Text = "Management", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.Gray, Location = new Point(20, 300) };
            this.Controls.Add(lblMgmt);

            string[] mgmt = { "Users", "Departments", "Concerns" };
            for (int i = 0; i < mgmt.Length; i++)
            {
                Button mgmtBtn = new Button { Text = $"  ⚙   {mgmt[i]}", TextAlign = ContentAlignment.MiddleLeft, FlatStyle = FlatStyle.Flat, Size = new Size(220, 40), Location = new Point(0, 330 + (i * 40)), Font = new Font("Segoe UI", 10) };
                mgmtBtn.FlatAppearance.BorderSize = 0;
                this.Controls.Add(mgmtBtn);
            }
        }
    }
}