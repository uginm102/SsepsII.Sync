using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SsepsII.Synchronisation.Services;
using SsepsII.Synchronisation.DAL;

namespace SSEPS_II.Synchronisation
{
    public partial class MDASelectionForm : Form
    {
        internal bool Canceled { get; set; }
        internal bool Selected { get; set; }
        private MdaServices _mdaServices = new MdaServices();
        private List<MDAListObj> _mapings = new List<MDAListObj>();
        public MDASelectionForm()
        {
            InitializeComponent();
            LoadGovernmentLevel();
        }

        private void LoadGovernmentLevel()
        {
            using (WaitCursor wc = new WaitCursor())
            {
                cmbGovernmentLevel.DataSource = new ListServices().GovernmentLevel;
                cmbGovernmentLevel.DisplayMember = "listText";
                cmbGovernmentLevel.ValueMember = "listValue";
                cmbGovernmentLevel.SelectedIndex = -1;
            }
        }

        private void LoadGovernment()
        {
            using (WaitCursor wc = new WaitCursor())
            {
                if (cmbGovernmentLevel.SelectedValue != null && !(cmbGovernmentLevel.SelectedValue is ListItem))
                {
                    cmbGovernment.DataSource = _mdaServices.GetGovernmentsByLevel((int)cmbGovernmentLevel.SelectedValue);
                    cmbGovernment.DisplayMember = "governmentName";
                    cmbGovernment.ValueMember = "governmentID";
                    cmbGovernment.SelectedIndex = -1;
                }
            }
        }

        private void LoadMdas()
        {
            using (WaitCursor wc = new WaitCursor())
            {
                if (cmbGovernment.SelectedValue != null && !(cmbGovernment.SelectedValue is Government))
                {
                    foreach (MdaGovernmentMapping map in _mdaServices.GetGovtMDAs((int)cmbGovernment.SelectedValue))
                    {
                        _mapings.Add(new MDAListObj() { id = map.mdaID, Name = map.mdaName });
                    }
                    if (cmbGovernment.SelectedValue != null)
                    {
                        _dgvMDAs.DataSource = _mapings;

                        StyleGrid();
                    }
                }
            }
        }

        private void StyleGrid()
        {
            _dgvMDAs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvMDAs.MultiSelect = false;
            _dgvMDAs.Columns["Selected"].Width = 50;
            _dgvMDAs.Columns["Selected"].HeaderText = "Select";
            _dgvMDAs.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public List<MDAListObj> SelectedMda()
        {
            if (_dgvMDAs.DataSource == null) return new List<MDAListObj>();
            return (_dgvMDAs.DataSource as List<MDAListObj>).Where(x => x.Selected).ToList();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Selected = true;
            this.Close();
        }

        private void cmbGovernmentLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbGovernment.DataSource = null;
            LoadGovernment();
        }

        private void cmbGovernment_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dgvMDAs.DataSource = null;
            LoadMdas();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Canceled = true;
            this.Close();
        }

        private void _dgvMDAs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            using (WaitCursor wc = new WaitCursor())
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 1)
                {
                    DataGridViewRow row = _dgvMDAs.Rows[e.RowIndex];

                    MDAListObj obj = row.DataBoundItem as MDAListObj;
                    if (obj.Selected)
                    {
                        row.DefaultCellStyle.BackColor = ColorScheme.Alternating;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = ColorScheme.NeutralButtonColor;
                    }
                    
                }
            }
        }

        private void _dgvMDAs_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            _dgvMDAs.EndEdit();
        }
    }
}
