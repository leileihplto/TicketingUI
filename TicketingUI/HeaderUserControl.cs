using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicketingUI
{
    public class HeaderUserControl : UserControl
    {
        public event EventHandler? OnMenuClicked;

        public HeaderUserControl()
        {
            this.Height = 60;
            this.Dock = DockStyle.Top;
            this.BackColor = Color.White;
            this.Padding = new Padding(10);
            InitializeHeader();
        }

        private void InitializeHeader()
        {
            Panel border = new Panel { Height = 1, Dock = DockStyle.Bottom, BackColor = Color.LightGray };
            this.Controls.Add(border);

            // 1. Hamburger Menu
            Label lblMenu = new Label
            {
                Text = "☰",
                Font = new Font("Segoe UI", 18),
                AutoSize = true,
                Location = new Point(15, 12),
                Cursor = Cursors.Hand
            };
            lblMenu.Click += (s, e) => OnMenuClicked?.Invoke(this, EventArgs.Empty);
            this.Controls.Add(lblMenu);

            // 2. LOGO
            PictureBox pbLogo = new PictureBox
            {
                Size = new Size(250, 40),
                Location = new Point(60, 10),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            try { pbLogo.Image = Properties.Resources.logo; } catch { }
            this.Controls.Add(pbLogo);

            // 3. Search Bar
            TextBox txtSearch = new TextBox { Size = new Size(400, 30), Location = new Point(400, 18), Font = new Font("Segoe UI", 11), Text = "🔍 Search..." };
            this.Controls.Add(txtSearch);

            // 4. Notification Bell 
            Label lblBell = new Label
            {
                Text = "🔔",
                Font = new Font("Segoe UI", 18),
                AutoSize = true,
                ForeColor = Color.Red,
                Anchor = AnchorStyles.Top | AnchorStyles.Right, // Sticks to right side
                Location = new Point(this.Width - 100, 12)
            };
            this.Controls.Add(lblBell);

            // 5. Profile Icon
            Label lblProfile = new Label
            {
                Text = "👤",
                Font = new Font("Segoe UI", 16),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right, // Sticks to right side
                Location = new Point(this.Width - 60, 12)
            };
            this.Controls.Add(lblProfile);
        }
    }
}