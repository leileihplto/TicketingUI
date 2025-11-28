#nullable disable // Disables strict null warnings
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicketingUI
{
    public class NewTicketForm : Form
    {
        public string TicketUser { get; private set; } = "";
        public string TicketConcern { get; private set; } = "";
        public string TicketDescription { get; private set; } = "";

        // Controls
        private TextBox txtSender;
        private ComboBox cbDepartment;
        private ComboBox cbConcern;
        private TextBox txtDescription;

        public NewTicketForm()
        {
            this.Text = "Ticket Sender";
            this.Size = new Size(400, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            InitializeForm();
        }

        private void InitializeForm()
        {
            int x = 20;
            int y = 20;
            int width = 340;

            // 1. Ticket Sender
            Label lblSender = new Label() { Text = "Ticket Sender", Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            this.Controls.Add(lblSender);
            y += 25;

            txtSender = new TextBox() { Location = new Point(x, y), Width = width, Font = new Font("Segoe UI", 10), Text = "SysAd-Intern-Lei" };
            this.Controls.Add(txtSender);
            y += 50;

            // HEADER
            Label lblHeader = new Label() { Text = "ISSUE DETAILS", Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            this.Controls.Add(lblHeader);
            y += 30;

            // 2. Department
            Label lblDept = new Label() { Text = "Department", Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 9) };
            this.Controls.Add(lblDept);
            y += 25;

            cbDepartment = new ComboBox() { Location = new Point(x, y), Width = width, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cbDepartment.Items.AddRange(new object[] { "IT INFRA", "System Admin", "Accounting", "HR" });
            cbDepartment.SelectedIndexChanged += CbDepartment_SelectedIndexChanged;
            this.Controls.Add(cbDepartment);
            y += 50;

            // 3. Area of Concern
            Label lblConcern = new Label() { Text = "Area of Concern", Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 9) };
            this.Controls.Add(lblConcern);
            y += 25;

            cbConcern = new ComboBox() { Location = new Point(x, y), Width = width, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            this.Controls.Add(cbConcern);
            y += 50;

            // 4. Description
            Label lblDesc = new Label() { Text = "Description", Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 9) };
            this.Controls.Add(lblDesc);
            y += 25;

            txtDescription = new TextBox() { Location = new Point(x, y), Width = width, Height = 100, Multiline = true, Font = new Font("Segoe UI", 10) };
            this.Controls.Add(txtDescription);
            y += 120;

            // 5. Buttons
            Button btnCreate = new Button()
            {
                Text = "Create",
                Location = new Point(240, y),
                Size = new Size(120, 35),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK
            };
            btnCreate.Click += BtnCreate_Click;

            Button btnCancel = new Button()
            {
                Text = "Cancel",
                Location = new Point(110, y),
                Size = new Size(120, 35),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel
            };

            this.Controls.Add(btnCreate);
            this.Controls.Add(btnCancel);
        }

        private void CbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbConcern.Items.Clear();
            cbConcern.Text = "";

            string selected = cbDepartment.SelectedItem?.ToString() ?? "";

            if (selected == "IT INFRA")
            {
                cbConcern.Items.AddRange(new object[] { "Biometrics Registration", "CCTV Review", "Internet/Network Issues" });
            }
            else if (selected == "System Admin")
            {
                cbConcern.Items.AddRange(new object[] { "Change Password", "Access Rights", "System Error" });
            }
            else
            {
                cbConcern.Items.AddRange(new object[] { "General Inquiry", "Others..." });
            }

            if (cbConcern.Items.Count > 0) cbConcern.SelectedIndex = 0;
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            this.TicketUser = txtSender.Text;
            string dept = cbDepartment.SelectedItem?.ToString() ?? "General";
            string concern = cbConcern.SelectedItem?.ToString() ?? "General";
            this.TicketConcern = $"{dept} - {concern}";
            this.TicketDescription = txtDescription.Text;
        }
    }
}