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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void _btnSave_Click(object sender, EventArgs e)
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\SsepsII.Synchronisation\\1.0", true);
            if (myKey == null) myKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\SsepsII.Synchronisation\\1.0");
            myKey.SetValue("server", _txtServer.Text);
            myKey.SetValue("UserID", _txtUserId.Text);
            myKey.SetValue("Password", _txtPassword.Text);
            myKey.SetValue("database", _txtDatabase.Text);
        }

        private void _btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _btnCancel_Click(object sender, EventArgs e)
        {
            _txtServer.Text = string.Empty;
            _txtUserId.Text = string.Empty;
            _txtPassword.Text = string.Empty;
            _txtDatabase.Text = string.Empty;
        }
    }
}
