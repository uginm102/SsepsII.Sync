using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSEPS_II.Synchronisation
{
    public partial class LoginForm : Form
    {
        private ConfigurationManager _configManager;
        public LoginForm(ConfigurationManager configManager)
        {
            InitializeComponent();
            lblVersionNumber.Text = Application.ProductVersion;
            _configManager = configManager;
        }

        private void _btnLogin_Click(object sender, EventArgs e)
        {
            using (WaitCursor wc = new WaitCursor())
            {
                if (_txtUsername.Text.Trim() == String.Empty)
                {
                    errorProviderLogin.SetError(_txtUsername, "Username is Required");
                }
                else
                {
                    errorProviderLogin.SetError(_txtUsername, "");
                }

                if (_txtPassword.Text.Trim() == String.Empty)
                {
                    errorProviderLogin.SetError(_txtPassword, "Password is Required");
                }
                else
                {
                    errorProviderLogin.SetError(_txtPassword, "");
                }

                _configManager.Login(_txtUsername.Text, _txtPassword.Text);
                if (_configManager.IsLoggedIn) this.Close();
                else
                {
                    _lblFeedback.Text = "Invalid username or password.";
                }
            }
        }

        private void _btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
