using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSEPS_II.Synchronisation
{
    public partial class FlashSelectionForm : Form
    {
        internal bool Canceled { get; set; }
        internal bool Selected { get; set; }

        public bool HasSsepsFolder = false;
        public string DrivePath = string.Empty;

        public FlashSelectionForm()
        {
            InitializeComponent();
            PrepareMedium();
        }

        private void PrepareMedium()
        {

            int topSseps = 5, topPrev = 5, topOthers = 5;
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady == true)
                {
                    if (d.VolumeLabel.ToLower().Contains(Constants.SSEPS_FLASH))
                    {
                        RadioButton ssepsRbtn = ControlsManager.NewRadioButton(d.Name, string.Format("{0}{1} u", d.Name, d.VolumeLabel), topSseps);
                        ssepsRbtn.MouseClick += Medium_MouseClick;
                        _panelSsepsMedium.Controls.Add(ssepsRbtn);
                        topSseps += 20;
                    }
                    else if (d.RootDirectory.EnumerateDirectories(Constants.SSEPS_FOLDER).Count() == 1)
                    {
                        RadioButton previousRbtn = ControlsManager.NewRadioButton(d.Name, string.Format("{0}{1}", d.Name, d.VolumeLabel), topPrev);
                        previousRbtn.MouseClick += Medium_MouseClick;
                        _panelPreviousMedium.Controls.Add(previousRbtn);
                        topPrev += 20;
                    }
                    else
                    {
                        RadioButton othersRbtn = ControlsManager.NewRadioButton(d.Name, string.Format("{0}{1}", d.Name, d.VolumeLabel), topOthers);
                        othersRbtn.MouseClick += Medium_MouseClick;
                        _panelOtherMedium.Controls.Add(othersRbtn);
                        topOthers += 20;
                    }
                }
            }
        }

        void Medium_MouseClick(object sender, MouseEventArgs e)
        {
            DrivePath = (sender as RadioButton).Text;
            //TODO we can add it to the tag if this is slow
            if ((sender as RadioButton).Parent.Name == _panelSsepsMedium.Name || (sender as RadioButton).Parent.Name == _panelPreviousMedium.Name)
            {
                HasSsepsFolder = true;
            }
            else
            {
                HasSsepsFolder = false;
            }
            foreach (Control cntrl in _panelSsepsMedium.Controls)
            {
                if (cntrl is RadioButton)
                {
                    RadioButton tempRbtn = cntrl as RadioButton;
                    tempRbtn.Checked = tempRbtn.Name == (sender as RadioButton).Name ? true : false;
                }
            }

            foreach (Control cntrl in _panelPreviousMedium.Controls)
            {
                if (cntrl is RadioButton)
                {
                    RadioButton tempRbtn = cntrl as RadioButton;
                    tempRbtn.Checked = tempRbtn.Name == (sender as RadioButton).Name ? true : false;
                }
            }

            foreach (Control cntrl in _panelOtherMedium.Controls)
            {
                if (cntrl is RadioButton)
                {
                    RadioButton tempRbtn = cntrl as RadioButton;
                    tempRbtn.Checked = tempRbtn.Name == (sender as RadioButton).Name ? true : false;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Canceled = true;
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Selected = true;
            this.Close();
        }
    }
}
