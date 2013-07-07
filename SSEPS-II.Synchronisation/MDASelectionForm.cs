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
                    var testOne = cmbGovernmentLevel.SelectedValue;
                    var testTwo = cmbGovernmentLevel.SelectedItem;
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
            LoadGovernment();
        }

        private void cmbGovernment_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMdas();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Canceled = true;
            this.Close();
        }

        private void MDASelectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void _dgvMDAs_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            using (WaitCursor wc = new WaitCursor())
            {
                DataGridViewRow row = _dgvMDAs.Rows[e.RowIndex];

                MDAListObj obj = row.DataBoundItem as MDAListObj;
                if (obj.Selected)
                {
                    row.DefaultCellStyle.BackColor = ColorScheme.NeutralButtonColor;
                    obj.Selected = false;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = ColorScheme.Alternating;
                    obj.Selected = true;
                }
            }
            ////if(row.Cells[e.ColumnIndex] is DataGridViewCheckBoxCell)
            //{
            //    DataGridViewCheckBoxCell cell = row.Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;
            //    if (cell.Value == null) cell.Value = false;

            //    cell.Value = cell.Value.ToString() == "True" ? false : true;
            //    row.Selected = (bool)cell.Value;
            //}
            //if ((bool)(row.Cells["CheckBox"].Value) == true)
            //{
            //    row.Selected = true;
            //}
            //else
            //{
            //    this.dgvSelectAll.Rows[Row.Index].Selected = false;
            //}
        }
    }
}
