using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicketingUI
{
    public class TicketTabControl : UserControl
    {
        public event EventHandler<string>? OnTabChanged;
        private Button? currentActiveBtn;

        public TicketTabControl()
        {
            this.Dock = DockStyle.Top;
            this.Height = 40;
            this.BackColor = Color.WhiteSmoke;
            this.Padding = new Padding(10, 5, 0, 0);
            InitializeTabs();
        }

        private void InitializeTabs()
        {
            Button btnAll = CreateTab("All", true);
            currentActiveBtn = btnAll; // Default selection
            CreateTab("Open", false);
            CreateTab("In Progress", false);
            CreateTab("Resolved", false);
        }

        private Button CreateTab(string text, bool isActive)
        {
            Button tab = new Button();
            tab.Text = text;
            tab.Dock = DockStyle.Left;
            tab.Width = 120;
            tab.FlatStyle = FlatStyle.Flat;
            tab.FlatAppearance.BorderSize = 0;
            tab.Font = new Font("Segoe UI", 9, isActive ? FontStyle.Bold : FontStyle.Regular);
            tab.BackColor = isActive ? Color.Silver : Color.Transparent;

            tab.Click += (s, e) =>
            {
                // Reset old button
                if (currentActiveBtn != null)
                {
                    currentActiveBtn.BackColor = Color.Transparent;
                    currentActiveBtn.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                }

                // Set new button active
                tab.BackColor = Color.Silver;
                tab.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                currentActiveBtn = tab;

                // Notify Main Form
                OnTabChanged?.Invoke(this, text);
            };

            this.Controls.Add(tab);
            tab.BringToFront();
            return tab;
        }
    }
}