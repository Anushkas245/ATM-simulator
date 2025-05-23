using System;
using System.Windows.Forms;
using System.Drawing;

namespace ATM
{
    public partial class Form1 : Form
    {
        private decimal balance = 1000m;

        private GroupBox grpLogin;
        private Label lblUser, lblPass, lblStatus, lblBalance;
        private TextBox txtUsername, txtPassword, txtAmount;
        private Button btnLogin, btnDeposit, btnWithdraw;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Form settings
            this.Text = "ATM";
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10);

            // GroupBox for Login
            grpLogin = new GroupBox()
            {
                Text = "User Login",
                Size = new Size(350, 130),
                Location = new Point(20, 20),
            };

            lblUser = new Label() { Text = "Username:", Location = new Point(20, 30), AutoSize = true };
            txtUsername = new TextBox() { Location = new Point(120, 27), Width = 180 };

            lblPass = new Label() { Text = "Password:", Location = new Point(20, 70), AutoSize = true };
            txtPassword = new TextBox() { Location = new Point(120, 67), Width = 180, UseSystemPasswordChar = true };

            btnLogin = new Button()
            {
                Text = "Login",
                Location = new Point(230, 100),
                Width = 90,
                Height = 35,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.MediumSlateBlue,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.MouseEnter += (s, e) => btnLogin.BackColor = Color.SlateBlue;
            btnLogin.MouseLeave += (s, e) => btnLogin.BackColor = Color.MediumSlateBlue;
            btnLogin.Click += BtnLogin_Click;

            grpLogin.Controls.Add(lblUser);
            grpLogin.Controls.Add(txtUsername);
            grpLogin.Controls.Add(lblPass);
            grpLogin.Controls.Add(txtPassword);
            grpLogin.Controls.Add(btnLogin);

            // Status Label
            lblStatus = new Label()
            {
                Location = new Point(20, 160),
                Width = 350,
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Balance Label
            lblBalance = new Label()
            {
                Location = new Point(20, 190),
                Width = 350,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.DarkGreen,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };

            // Amount TextBox
            txtAmount = new TextBox()
            {
                Location = new Point(120, 230),
                Width = 150,
                Visible = false,
                PlaceholderText = "Enter amount"
            };

            // Deposit Button
            btnDeposit = new Button()
            {
                Text = "Deposit",
                Location = new Point(20, 270),
                Width = 150,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Visible = false
            };
            btnDeposit.FlatAppearance.BorderSize = 0;
            btnDeposit.MouseEnter += (s, e) => btnDeposit.BackColor = Color.SeaGreen;
            btnDeposit.MouseLeave += (s, e) => btnDeposit.BackColor = Color.MediumSeaGreen;
            btnDeposit.Click += BtnDeposit_Click;

            // Withdraw Button
            btnWithdraw = new Button()
            {
                Text = "Withdraw",
                Location = new Point(220, 270),
                Width = 150,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Visible = false
            };
            btnWithdraw.FlatAppearance.BorderSize = 0;
            btnWithdraw.MouseEnter += (s, e) => btnWithdraw.BackColor = Color.Firebrick;
            btnWithdraw.MouseLeave += (s, e) => btnWithdraw.BackColor = Color.IndianRed;
            btnWithdraw.Click += BtnWithdraw_Click;

            // Add controls to Form
            this.Controls.Add(grpLogin);
            this.Controls.Add(lblStatus);
            this.Controls.Add(lblBalance);
            this.Controls.Add(txtAmount);
            this.Controls.Add(btnDeposit);
            this.Controls.Add(btnWithdraw);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "anushka" && txtPassword.Text == "1234")
            {
                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = "Login successful!";
                ShowATMControls(true);
                UpdateBalanceDisplay();
            }
            else
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "Invalid username or password.";
                ShowATMControls(false);
            }
        }

        private void BtnDeposit_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtAmount.Text, out decimal amount) && amount > 0)
            {
                balance += amount;
                UpdateBalanceDisplay();
                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = $"Deposited {amount:C}";
            }
            else
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "Enter a valid amount to deposit.";
            }
            txtAmount.Clear();
        }

        private void BtnWithdraw_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtAmount.Text, out decimal amount) && amount > 0)
            {
                if (amount <= balance)
                {
                    balance -= amount;
                    UpdateBalanceDisplay();
                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Text = $"Withdrew {amount:C}";
                }
                else
                {
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Text = "Insufficient balance.";
                }
            }
            else
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "Enter a valid amount to withdraw.";
            }
            txtAmount.Clear();
        }

        private void UpdateBalanceDisplay()
        {
            lblBalance.Text = $"Current Balance: {balance:C}";
        }

        private void ShowATMControls(bool show)
        {
            lblBalance.Visible = show;
            txtAmount.Visible = show;
            btnDeposit.Visible = show;
            btnWithdraw.Visible = show;

            grpLogin.Enabled = !show;
            lblStatus.Text = "";
        }
    }
}
