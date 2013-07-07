using SsepsII.Synchronisation.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SsepsII.Synchronisation.DAL;
using System.IO;
using Ionic.Zip;
using System.Xml.Linq;

namespace SSEPS_II.Synchronisation
{
    public partial class MainForm : Form
    {
        delegate void SetTextCallback(string text);
        private ConfigurationManager _configurationManager;
        private SyncServices _syncServices = new SyncServices();
        private List<MDAListObj> _selectedMDAS = new List<MDAListObj>();
        private string _path = string.Empty;

        public MainForm(ConfigurationManager configManager)
        {
            InitializeComponent();
            _configurationManager = configManager;
            _lblLoggedInUser.Text = _configurationManager.LoggedInUser.Username;
        }

        void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            using (WaitCursor wc = new WaitCursor())
            {
                if (e.IsAvailable)
                {
                    if (NetworkServices.IsNetworkAvailable())
                    {
                        SetMessage("network found.");

                        if (_syncServices.LoginRemoteService(_configurationManager.LoggedInUser)) SetMessage("Connected to server");
                        else SetMessage("Could not connect to server.");
                    }
                }
            }
        }

        private void _rbtnFile_MouseClick(object sender, MouseEventArgs e)
        {
            //FlashSelectionForm fsFrm = new FlashSelectionForm();
            //fsFrm.ShowDialog();
            //if (fsFrm.Selected)
            //{
            //    _path = fsFrm.DrivePath;
            //    _lblSelectedFlash.Text = _path;
            //}
        }

        private void _rbtnFile_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxFile.Enabled = _rbtnFile.Checked;
        }

        private void ClearMessages()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this._txtFeedback.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetMessage);
                this.Invoke(d, new object[] {  });
            }
            else
            {
                _txtFeedback.Text = string.Empty;
            }
        }
        private void SetMessage(string message)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this._txtFeedback.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetMessage);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                _txtFeedback.AppendText(string.Format("{0}{1}", message, Environment.NewLine));
            }
        }

        private void _rbtnNetwork_CheckedChanged(object sender, EventArgs e)
        {
            if (_rbtnNetwork.Checked)
            {
                using (WaitCursor wc = new WaitCursor())
                {
                    if (NetworkServices.IsNetworkAvailable())
                    {
                        if (_syncServices.LoginRemoteService(_configurationManager.LoggedInUser)) SetMessage("Connected to server");
                        else SetMessage("Could not connect to server.");
                    }
                    else
                    {
                        SetMessage("No network found.");
                        NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
                    }
                }
            }
            else
            {
                NetworkChange.NetworkAvailabilityChanged -= NetworkChange_NetworkAvailabilityChanged;
            }
        }

        private void _rbtnSendApproved_CheckedChanged(object sender, EventArgs e)
        {
            if (_rbtnSendApproved.Checked)
            {
                MDASelectionForm frm = new MDASelectionForm();
                frm.ShowDialog();
                using (WaitCursor wc = new WaitCursor())
                {
                    _selectedMDAS = frm.SelectedMda();
                    foreach (MDAListObj item in _selectedMDAS)
                    {
                        SetMessage(string.Format("Added mda | {0}", item.Name));
                    }
                }
            }
        }

        private void _btnSelectFlashDisk_Click(object sender, EventArgs e)
        {
            FlashSelectionForm fsFrm = new FlashSelectionForm();
            fsFrm.ShowDialog();
            using (WaitCursor wc = new WaitCursor())
            {
                if (fsFrm.Selected)
                {
                    _path = fsFrm.DrivePath;
                    _lblSelectedFlash.Text = _path;
                }
            }
        }

        private void _btnSync_Click(object sender, EventArgs e)
        {
            using (WaitCursor wc = new WaitCursor())
            {
                if (_rbtnNetwork.Checked)
                {
                    SetMessage("Receiving data from server");
                    if (_syncServices.ReceiveSyncDataTroughLAN(_configurationManager.LoggedInUser))
                    {
                        SetMessage("Data received from server");
                    }
                    else
                    {
                        SetMessage("Data could not be received from server");
                    }

                    SetMessage("Sending data to server");
                    if (_syncServices.SendSyncDataThroughLAN(_configurationManager.LoggedInUser))
                    {
                        SetMessage("Data sent to server");
                    }
                    else
                    {
                        SetMessage("Data could not be sent to server");
                    }
                }
                else if (_rbtnFile.Checked)
                {
                    string fileToSave = string.Empty, fileName = string.Empty;

                    string folder = string.Format(@"{0}\{1}", _path, Constants.SSEPS_FOLDER);
                    

                    if (_rbtnReceive.Checked)
                    {
                        MemoryStream ms = new MemoryStream();
                        StreamReader sr = new StreamReader(ms);

                        if (SiteConfigServices.IsClient)
                        {
                            string approveFileName = string.Format(@"{0}\{1}", folder, Constants.SSEPS_FILE_APPROVED);
                            if (File.Exists(approveFileName))
                            {
                                SetMessage("File found on selected medium");
                                SetMessage(string.Format("File name {0} ", approveFileName));
                                SetMessage("Importing data");
                                using (ZipFile zip = ZipFile.Read(approveFileName))
                                {
                                    foreach (ZipEntry entry in zip)
                                    {
                                        entry.Extract(ms);
                                        ms.Position = 0;
                                        StreamReader reader = new StreamReader(ms);

                                        //string data = reader.ReadToEnd();
                                        try
                                        {
                                            XDocument xDoc = XDocument.Parse(Crypto.DecryptStringAES(reader.ReadToEnd(), SiteConfigServices.InstallationToken));
                                            if (xDoc.Root.Name.ToString() == "Approved")
                                            {
                                                _syncServices.ImportApprovedData(xDoc);
                                                SetMessage("Data imported successfully");
                                            }
                                            else
                                            {
                                                SetMessage("Failed to import data, file of unknown type");
                                            }
                                        }
                                        catch (ArgumentNullException ex)
                                        {
                                            SetMessage("Error writing file");
                                            switch (ex.ParamName)
                                            {
                                                case "cipherText":
                                                    SetMessage("Data to be imported not found");
                                                    break;
                                                case "sharedSecret":
                                                    SetMessage("Installation token not found on this machine");
                                                    break;
                                                default:
                                                    SetMessage("Error type unknown");
                                                    break;
                                            }
                                            return;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                SetMessage("File not found on selected medium");
                            }
                        }
                        else if (SiteConfigServices.IsServer)
                        {
                            string syncFileName = string.Format(@"{0}\{1}", folder, Constants.SSEPS_FILE_SYNC);
                            if (File.Exists(syncFileName))
                            {
                                SetMessage("File found on selected medium");
                                SetMessage(string.Format("File name {0} ", syncFileName));
                                SetMessage("Importing data");
                                using (ZipFile zip = ZipFile.Read(syncFileName))
                                {
                                    foreach (ZipEntry entry in zip)
                                    {
                                        
                                        entry.Extract(ms);
                                        ms.Position = 0;
                                        StreamReader reader = new StreamReader(ms);
                                        String file = reader.ReadToEnd();
                                        string[] splitFile = file.Split('|');
                                        if (splitFile.Count() != 2)
                                        {
                                            SetMessage("File in wrong format");
                                            SetMessage("This file needs to be re-exported");
                                            return;
                                        }
                                        InstallationServices instalServices = new InstallationServices();
                                        string token = instalServices.InstallationTokenBySiteId(int.Parse(splitFile[0]));
                                        string realXml = Crypto.DecryptStringAES(splitFile[1], token);
                                        XDocument xDoc = XDocument.Parse(realXml);
                                        if (xDoc.Root.Name.ToString() == "Sync")
                                        {
                                            _syncServices.ImportDataForApproval(xDoc);
                                            SetMessage("Data imported successfully");
                                        }
                                        else
                                        {
                                            SetMessage("Failed to import data, file of unknown type");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                SetMessage("File not found on selected medium");
                            }
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                        if (_rbtnSendApproval.Checked)
                        {
                            try
                            {
                                fileToSave = _syncServices.GetDataToSyncSecure(_configurationManager.LoggedInUser).ToString();
                            }
                            catch (ArgumentNullException ex)
                            {
                                SetMessage("Error writing file");
                                switch (ex.ParamName)
                                {
                                    case "plainText":
                                        SetMessage("Data to be exported not found");
                                        break;
                                    case "sharedSecret":
                                        SetMessage("Installation token not found on this machine");
                                        break;
                                    default:
                                        SetMessage("Error type unknown");
                                        break;
                                }
                                return;
                            }
                            fileName = Constants.SSEPS_FILE_SYNC;
                        }
                        else if (_rbtnSendApproved.Checked)
                        {
                            try
                            {
                                fileToSave = _syncServices.GetApprovedDataToSyncSecure(_selectedMDAS.Select(x => x.id).ToList());
                            }
                            catch (ArgumentNullException ex)
                            {
                                SetMessage("Error writing file");
                                switch (ex.ParamName)
                                {
                                    case "plainText":
                                        SetMessage("Data to be exported not found");
                                        break;
                                    case "sharedSecret":
                                        SetMessage("Installation token not found on this machine");
                                        break;
                                    default:
                                        SetMessage("Error type unknown");
                                        break;
                                }
                                return;
                            }
                            fileName = Constants.SSEPS_FILE_APPROVED;
                        }

                        using (ZipFile zip = new ZipFile())
                        {
                            SetMessage("Saving file on selected medium");
                            zip.AddEntry(fileName, fileToSave);
                            zip.Save(string.Format(@"{0}\{1}\{2}", _path, Constants.SSEPS_FOLDER, fileName));
                            SetMessage("File successfully saved");
                        }
                    }
                }
            }
        }

        private void _btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsFrm = new SettingsForm();
            settingsFrm.ShowDialog();
        }

        private void _btnLogout_Click(object sender, EventArgs e)
        {
            _configurationManager.Logout();
            this.Close();
        }
    }
}
