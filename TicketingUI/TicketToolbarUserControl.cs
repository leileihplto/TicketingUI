using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicketingUI
{
    public class TicketToolbarUserControl : UserControl
    {
        public TicketToolbarUserControl()
        {
            this.Dock = DockStyle.Top;
            this.Height = 50;
            this.BackColor = Color.White;
            this.Paint += (s, e) => e.Graphics.DrawLine(Pens.LightGray, 0, this.Height - 1, this.Width, this.Height - 1);
            InitializeToolbar();
        }

        private void InitializeToolbar()
        {
            Button btnRefresh = new Button() { Text = "↻ Refresh", FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10), Location = new Point(10, 10), Size = new Size(100, 30) };
            btnRefresh.FlatAppearance.BorderSize = 0;
            this.Controls.Add(btnRefresh);

            Button btnFilter = new Button() { Text = "⚲ Filter", FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10), Location = new Point(120, 10), Size = new Size(100, 30) };
            btnFilter.FlatAppearance.BorderSize = 0;
            this.Controls.Add(btnFilter);

            Label lblPagination = new Label() { Text = "< 1-20 of 20 >", Font = new Font("Segoe UI", 10), AutoSize = true, Anchor = AnchorStyles.Top | AnchorStyles.Right, Location = new Point(this.Width - 150, 15) };
            this.Controls.Add(lblPagination);
        }
    }
}