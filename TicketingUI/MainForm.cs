#nullable disable
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicketingUI
{
    public class MainForm : Form
    {
        private HeaderUserControl headerControl;
        private SidebarUserControl sidebarControl;
        private Panel mainContentPanel;

        private TicketToolbarUserControl ticketToolbar;
        private TicketTabControl ticketTabs;
        private TicketListUserControl ticketList;

        public MainForm()
        {
            this.Text = "Alternatives Food Corp - Ticketing";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 249);

            InitializeLayout();

            // --- CONNECTING THE LOGIC ---

            // 1. Sidebar -> New Ticket Popup
            if (sidebarControl != null)
                sidebarControl.OnNewTicketClicked += SidebarControl_OnNewTicketClicked;

            // 2. Sidebar -> Filter List (Inbox, Pinned, Archived)
            if (sidebarControl != null)
            {
                sidebarControl.OnMenuOptionClicked += (s, viewName) =>
                {
                    // This tells the grid: "Show me only Pinned tickets" etc.
                    ticketList.SetView(viewName);
                };
            }

            // 3. Tabs -> Status Filter (Open, Resolved)
            if (ticketTabs != null)
                ticketTabs.OnTabChanged += (s, status) => ticketList.FilterByStatus(status);

            // 4. Header -> Toggle Sidebar
            if (headerControl != null)
                headerControl.OnMenuClicked += (s, e) => sidebarControl.Visible = !sidebarControl.Visible;
        }

        private void InitializeLayout()
        {
            headerControl = new HeaderUserControl { Dock = DockStyle.Top };
            this.Controls.Add(headerControl);

            sidebarControl = new SidebarUserControl { Dock = DockStyle.Left };
            this.Controls.Add(sidebarControl);

            mainContentPanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(0) };
            this.Controls.Add(mainContentPanel);

            ticketList = new TicketListUserControl { Dock = DockStyle.Fill };
            mainContentPanel.Controls.Add(ticketList);

            ticketTabs = new TicketTabControl { Dock = DockStyle.Top };
            mainContentPanel.Controls.Add(ticketTabs);

            ticketToolbar = new TicketToolbarUserControl { Dock = DockStyle.Top };
            mainContentPanel.Controls.Add(ticketToolbar);

            ticketToolbar.BringToFront();
            ticketTabs.BringToFront();
            ticketList.BringToFront();
        }

        private void SidebarControl_OnNewTicketClicked(object sender, EventArgs e)
        {
            using (var form = new NewTicketForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ticketList.AddNewTicket(form.TicketUser, form.TicketConcern, "Open");
                }
            }
        }
    }
}