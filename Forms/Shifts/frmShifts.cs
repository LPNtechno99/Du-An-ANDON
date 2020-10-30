using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
	public partial class frmShifts : Form
	{
		private bool _isAdd;
		public frmShifts()
		{
			InitializeComponent();
		}

		

		private void frmShifts_Load(object sender, EventArgs e)
		{
			loadShifts();
			ClearInterface();
		}

		
		#region Methods
		private void loadShifts()
		{			
			DataTable data = TextUtils.Select("Select * from Shift");
			grdData.DataSource = data;
			grvData.BestFitColumns();

		}
		private void SetInterface(bool isEdit)
		{
			
			grdData.Enabled = !isEdit;

			btnSave.Visible = isEdit;
			btnCancel.Visible = isEdit;

			btnNew.Visible = !isEdit;
			btnEdit.Visible = !isEdit;
			btnDelete.Visible = !isEdit;
		}

		private void ClearInterface()
		{
			txtName.Text = "";
			DateTime date = DateTime.Now.Date;
			pickerStart.Value = date.AddHours(0);
			pickerEnd.Value = date.AddHours(0);
			pickerStartBreak1.Value = date.AddHours(0);
			pickerEndBreak1.Value = date.AddHours(0);
			pickerStartBreak2.Value = date.AddHours(0);
			pickerEndBreak2.Value = date.AddHours(0);
			pickerStartBreak3.Value = date.AddHours(0);
			pickerEndBreak3.Value = date.AddHours(0);
			pickerStartBreak4.Value = date.AddHours(0);
			pickerEndBreak4.Value = date.AddHours(0);
		}

		private bool checkValid(DateTime startTime, DateTime endTime, DateTime startBreak, DateTime endBreak)
		{
			if(startBreak <= startTime)
			{
				MessageBox.Show("Start Time Break value invalid!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return false;
			}
			
			if(startTime <= endTime)
			{
				if (endBreak >= endTime)
				{
					MessageBox.Show("End Time Break value invalid!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return false;
				}
			}
			/*if (startBreak >= endBreak)
			{
				MessageBox.Show("Value invalid!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return false;
			}*/
			/*if (startTime >= endTime)
			{
				MessageBox.Show("Value invalid!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return false;
			}*/
			if (txtName.Text == "")
			{
				MessageBox.Show("You have not entered the shift name!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return false;
			}
			return true;
		}
		#endregion

		#region events
		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (!grvData.IsDataRow(grvData.FocusedRowHandle))
				return;
			SetInterface(true);
			_isAdd = false;
			txtName.Text = TextUtils.ToString(grvData.GetRowCellValue(grvData.FocusedRowHandle, "Name"));
			pickerStart.Value = TextUtils.ToDate3(grvData.GetRowCellValue(grvData.FocusedRowHandle, "StartTime"));
			pickerEnd.Value = TextUtils.ToDate3(grvData.GetRowCellValue(grvData.FocusedRowHandle, "EndTime"));
			pickerStartBreak1.Value = TextUtils.ToDate3(grvData.GetRowCellValue(grvData.FocusedRowHandle, "StartTimeBreak1"));
			pickerEndBreak1.Value = TextUtils.ToDate3(grvData.GetRowCellValue(grvData.FocusedRowHandle, "EndTimeBreak1"));
			pickerStartBreak2.Value = TextUtils.ToDate3(grvData.GetRowCellValue(grvData.FocusedRowHandle, "StartTimeBreak2"));
			pickerEndBreak2.Value = TextUtils.ToDate3(grvData.GetRowCellValue(grvData.FocusedRowHandle, "EndTimeBreak2"));
			pickerStartBreak3.Value = TextUtils.ToDate3(grvData.GetRowCellValue(grvData.FocusedRowHandle, "StartTimeBreak3"));
			pickerEndBreak3.Value = TextUtils.ToDate3(grvData.GetRowCellValue(grvData.FocusedRowHandle, "EndTimeBreak3"));
			pickerStartBreak4.Value = TextUtils.ToDate3(grvData.GetRowCellValue(grvData.FocusedRowHandle, "StartTimeBreak4"));
			pickerEndBreak4.Value = TextUtils.ToDate3(grvData.GetRowCellValue(grvData.FocusedRowHandle, "EndTimeBreak4"));
		}
		private void btnNew_Click(object sender, EventArgs e)
		{
			SetInterface(true);
			_isAdd = true;
		}
		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (checkValid(pickerStart.Value, pickerEnd.Value, pickerStartBreak1.Value, pickerStartBreak2.Value))
				{
					ShiftModel shift;
					if (_isAdd)
					{
						shift = new ShiftModel();						
					}
					else
					{
						int ID = Convert.ToInt32(grvData.GetRowCellValue(grvData.FocusedRowHandle, "ID").ToString());
						shift = ShiftBO.Instance.FindByPK(ID) as ShiftModel;						
					}
					DateTime sTime = pickerStart.Value;
					DateTime eTime = pickerEnd.Value;
					TimeSpan timeSpan = eTime - sTime;
					int totalSeconds = TextUtils.ToInt(timeSpan.TotalSeconds);
					
					shift.Name = txtName.Text;
					shift.StartTime = sTime;
					shift.EndTime = eTime;
					shift.StartTimeBreak1 = pickerStartBreak1.Value;
					shift.EndTimeBreak1 = pickerEndBreak1.Value;
					shift.StartTimeBreak2 = pickerStartBreak2.Value;
					shift.EndTimeBreak2 = pickerEndBreak2.Value;
					shift.StartTimeBreak3 = pickerStartBreak3.Value;
					shift.EndTimeBreak3 = pickerEndBreak3.Value;
					shift.StartTimeBreak4 = pickerStartBreak4.Value;
					shift.EndTimeBreak4 = pickerEndBreak4.Value;
					shift.TotalTime = sTime.Date.AddHours(timeSpan.Hours).AddMinutes(timeSpan.Minutes);
					
					if (_isAdd)
					{
						ShiftBO.Instance.Insert(shift);
					}
					else
					{
						ShiftBO.Instance.Update(shift);
					}
					SetInterface(false);
					ClearInterface();
					loadShifts();
				}
			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);				
			}
			

		}

		#endregion

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (!grvData.IsDataRow(grvData.FocusedRowHandle))
				return;
			int ID = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, "ID").ToString());
			string strName = grvData.GetRowCellValue(grvData.FocusedRowHandle, "Name").ToString();

			DialogResult result = MessageBox.Show(String.Format("Are you want to delete [{0}] ?", strName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.No) return;
			try
			{
				ShiftBO.Instance.Delete(ID);
				loadShifts();
			}
			catch (Exception)
			{
				MessageBox.Show("An error occurred during processing, please try again later!");
			}
			
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			SetInterface(false);
			ClearInterface();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{

		}
	}
}
