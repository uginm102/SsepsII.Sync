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
        private string _path = string.Empty, _driveName = string.Empty;

        public MainForm(ConfigurationManager configManager)
        {
            InitializeComponent();
            _configurationManager = configManager;
            SetLoggedInUserText(_configurationManager.LoggedInUser);
        }

        private void SetLoggedInUserText(SystemUser user)
        {
            _lblLoggedInUser.Text = string.Format("Welcome {0} {1} {2}",user.GivenName, user.OtherNames, user.Surname);
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
                this.Invoke(d, new object[] { });
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
                    if (SiteConfigServices.IsClient)
                    {
                        if (NetworkServices.IsNetworkAvailable())
                        {
                            if (_syncServices.ServerIsAvailable(_configurationManager.LoggedInUser)) SetMessage("Connected to server");
                            else SetMessage("Could not connect to server.");
                        }
                        else
                        {
                            SetMessage("No network found.");
                            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
                        }
                    }
                    else if (SiteConfigServices.IsServer)
                    {
                        if (_syncServices.ServerIsAvailable(_configurationManager.LoggedInUser)) SetMessage("Connected to server");
                        else SetMessage("Could not connect to server.");
                    }
                }
            }
        }

        private void _rbtnSendApproved_CheckedChanged(object sender, EventArgs e)
        {
            _btnSelectMDA.Enabled = _rbtnSendApproved.Checked;
        }

        private void _btnSelectFlashDisk_Click(object sender, EventArgs e)
        {
            FlashSelectionForm fsFrm = new FlashSelectionForm();
            using (WaitCursor wc = new WaitCursor())
            {
                fsFrm.ShowDialog();
                if (fsFrm.Selected)
                {
                    _path = fsFrm.DrivePath;
                    _driveName = fsFrm.DriveName;
                    _lblSelectedFlash.Text = _driveName != string.Empty ? string.Format(@"{0}{1}", _path, _driveName) : _path;
                }
            }
        }

        private void _btnSync_Click(object sender, EventArgs e)
        {
            using (WaitCursor wc = new WaitCursor())
            {
                if (_rbtnNetwork.Checked)
                {
                    NetworkSync();
                }
                else if (_rbtnFile.Checked)
                {
                    if (_path == string.Empty)
                    {
                        SetMessage("Please first select an option to sync");
                        return;
                    }
                    string fileToSave = string.Empty, fileName = string.Empty;

                    string folder = string.Format(@"{0}\{1}", _path, Constants.SSEPS_FOLDER);


                    if (_rbtnReceive.Checked)
                    {
                        MemoryStream ms = new MemoryStream();

                        if (SiteConfigServices.IsClient)
                        {
                            ClientImport(folder, ms);
                        }
                        else if (SiteConfigServices.IsServer)
                        {
                            ServerImport(folder, ms);
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                        if (_rbtnSendApproval.Checked)
                        {
                            if (!ExportDataForApproval(ref fileToSave, ref fileName)) return;
                        }
                        else if (_rbtnSendApproved.Checked)
                        {
                            if (!ExportApprovedData(ref fileToSave, ref fileName)) return;
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
                else
                {
                    SetMessage("Please first select an option to sync");
                }
            }
        }

        private bool ExportApprovedData(ref string fileToSave, ref string fileName)
        {
            //TODO this check is usually not called, because this option is only available while on server.
            if (!SiteConfigServices.IsServer)
            {
                SetMessage("Error writing file");
                SetMessage("You can only export approved data from a server");
                return false;
            }

            try
            {
                //TODO make sure that all the selected mda's are from the same site
                InstallationServices installationServices = new InstallationServices();
                if (installationServices.MdaIdsBelongToSameInstallation(_selectedMDAS.Select(x => x.id).ToList()))
                {
                    fileToSave = _syncServices.GetApprovedDataToSyncSecure(_selectedMDAS.Select(x => x.id).ToList(), _configurationManager.LoggedInUser);
                    fileName = Constants.SSEPS_FILE_APPROVED;
                    return true;
                }
                else
                {
                    SetMessage("Error writing file");
                    SetMessage("Selected mda's don't belong to same site");
                    return false;
                }
            }
            catch (ArgumentNullException ex)
            {
                SetMessage("Error writing file");
                switch (ex.ParamName)
                {
                    case "plainText":
                        SetMessage("Data to be exported not found 1");
                        break;
                    case "cipherText":
                        SetMessage("Data to be exported not found 2");
                        break;
                    case "sharedSecret":
                        SetMessage("Installation token not found on this machine");
                        break;
                    default:
                        SetMessage("Error type unknown on server");
                        break;
                }
                //TODO handle this exception return;
                return false;
            }
        }

        private bool ExportDataForApproval(ref string fileToSave, ref string fileName)
        {
            //TODO this check is usually not called, because this option is only available while on clients.
            if (!SiteConfigServices.IsClient)
            {
                SetMessage("Error writing file");
                SetMessage("You can only export data for from a client");
                return false;
            }

            try
            {
                fileToSave = string.Format("{0}|{1}", SiteConfigServices.SiteID, _syncServices.GetDataToSyncSecure(_configurationManager.LoggedInUser).ToString());
                fileName = Constants.SSEPS_FILE_SYNC;
                return true;
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
                //TODO handle this exception return;
                return false;
            }

        }

        private void ServerImport(string folder, MemoryStream ms)
        {
            //TODO this check is usually not called, because this option is only available while on server.
            if (!SiteConfigServices.IsServer)
            {
                SetMessage("Error writing file");
                SetMessage("You can only import data for approval on a server");
                return;
            }

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
                            break;
                        }
                        InstallationServices instalServices = new InstallationServices();
                        int siteId = int.Parse(splitFile[0]);
                        string token = instalServices.InstallationTokenBySiteId(siteId);
                        string realXml = Crypto.DecryptStringAES(splitFile[1], token);
                        XDocument xDoc = XDocument.Parse(realXml);
                        if (xDoc.Root.Name.ToString() == "Sync")
                        {
                            if (_syncServices.ImportDataForApproval(_configurationManager.LoggedInUser,
                                Crypto.EncryptStringAES(xDoc.ToString(), token), siteId))
                            {
                                SetMessage("Data imported successfully");
                            }
                            else
                            {
                                SetMessage("Failed to import data, server service down");
                            }
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

        private void NetworkSync()
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

        private void ClientImport(string folder, MemoryStream ms)
        {
            //TODO this check is usually not called, because this option is only available while on clients.
            if (!SiteConfigServices.IsClient)
            {
                SetMessage("Error writing file");
                SetMessage("You can only import approved data on a client");
                return;
            }

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
                            break;
                        }
                    }
                }
            }
            else
            {
                SetMessage("File not found on selected medium");
            }
        }

        private void _btnLogout_Click(object sender, EventArgs e)
        {
            _configurationManager.Logout();
            this.Close();
        }

        private void toolStripMenuItemDatabseSettings_Click(object sender, EventArgs e)
        {
            //Open database settings form
        }

        private void _btnSelectMDA_Click(object sender, EventArgs e)
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
    }
}
