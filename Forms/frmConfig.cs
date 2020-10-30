using BMS;
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
using System.Collections;
using BMS.Utils;

namespace BMS
{
	public partial class frmConfig : Form
	{
		public FontSize _FontSize;
		//public LoadAndonDetails _LoadAndonDetails;
		private BindingSource _bindingSource = new BindingSource();
		public frmConfig()
		{
			InitializeComponent();
		}
		
		private void frmConfig_Load(object sender, EventArgs e)
		{
			ArrayList arr = AndonConfigBO.Instance.FindAll();
			DataTable data = TextUtils.LoadDataFromSP("spGetConfigPLC", "A", new string[0] {}, new string[0] {});
			grvAreaPLC.AutoGenerateColumns = false;

			if(data.Rows.Count > 0)
			{
				grvAreaPLC.DataSource = _bindingSource;
				_bindingSource.DataSource = data;
							
			}

			if (arr.Count > 0)
			{
				AndonConfigModel andonConfig = (AndonConfigModel)arr[0];
				numFontValueCD.Value = andonConfig.FontSize1;
				numFontTitleCD.Value = andonConfig.FontSize2;
				numFontValuePlan.Value = andonConfig.FontSize3;
				numFontLabelPlan.Value = andonConfig.FontSize4;
				numFontTitleAndon.Value = andonConfig.FontSize5;
				numLabelTakt.Value = andonConfig.FontSize6;
				numValueTakt.Value = andonConfig.FontSize7;
				txtPort.Text = TextUtils.ToString(andonConfig.SocketPort);
				txtTakt.Text = TextUtils.ToString(andonConfig.Takt);
				txtTcpIp.Text = TextUtils.ToString(andonConfig.TcpIp);
			}
		}

		private bool checkInput(string takt, string tcp, string port)
		{	
			if (tcp == "")
			{
				MessageBox.Show("You have not entered value to TCP-IP", "Notice", MessageBoxButtons.OK);
				return false;
			}			
			if (port == "")
			{
				MessageBox.Show("You have not entered value to Port", "Notice", MessageBoxButtons.OK);
				return false;
			}
			if (takt == "")
			{
				MessageBox.Show("You have not entered value to Takt", "Notice", MessageBoxButtons.OK);
				return false;
			}
			return true;
		}
		private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
			{
				e.Handled = true;
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		private void btnSaveAreaPLC_Click(object sender, EventArgs e)
		{
			foreach(DataRowView r in _bindingSource)
			{
				ConfigPLCModel configPLC = ConfigPLCBO.Instance.FindByPK(TextUtils.ToInt(r.Row["ID"])) as ConfigPLCModel;
				configPLC.AreaDelay = TextUtils.ToString(r.Row["AreaDelay"]);
				configPLC.AreaRisk = TextUtils.ToString(r.Row["AreaRisk"]);
				ConfigPLCBO.Instance.Update(configPLC);
			}
			MessageBox.Show("Config address area successfully!", "Notice", MessageBoxButtons.OK);
			grvAreaPLC.DataSource = _bindingSource;
		}

		private void btnSaveFontSize_Click(object sender, EventArgs e)
		{
			// check đã nhập chưa
			bool checkPass = checkInput(txtTakt.Text, txtTcpIp.Text, txtPort.Text);
			if (!checkPass)
			{
				return;
			}

			ArrayList arr = AndonConfigBO.Instance.FindAll();
			if (arr.Count > 0)
			{
				AndonConfigModel andonConfig = (AndonConfigModel)arr[0];
				andonConfig.FontSize1 = numFontValueCD.Value;
				andonConfig.FontSize2 = numFontTitleCD.Value;
				andonConfig.FontSize3 = numFontValuePlan.Value;
				andonConfig.FontSize4 = numFontLabelPlan.Value;
				andonConfig.FontSize5 = numFontTitleAndon.Value;
				andonConfig.FontSize6 = numLabelTakt.Value;
				andonConfig.FontSize7 = numValueTakt.Value;
				andonConfig.TcpIp = TextUtils.ToString(txtTcpIp.Text);
				andonConfig.SocketPort = TextUtils.ToInt(txtPort.Text);
				andonConfig.Takt = TextUtils.ToInt(txtTakt.Text);
				AndonConfigBO.Instance.Update(andonConfig);
				MessageBox.Show("Config font size successfully! ");
				TextUtils.ExcuteSQL("exec spUpdateTakt @Takt = " + andonConfig.Takt);
			}
			else
			{
				AndonConfigModel andonConfig = new AndonConfigModel();
				andonConfig.FontSize1 = numFontValueCD.Value;
				andonConfig.FontSize2 = numFontTitleCD.Value;
				andonConfig.FontSize3 = numFontValuePlan.Value;
				andonConfig.FontSize4 = numFontLabelPlan.Value;
				andonConfig.FontSize5 = numFontTitleAndon.Value;
				andonConfig.FontSize6 = numLabelTakt.Value;
				andonConfig.FontSize7 = numValueTakt.Value;
				andonConfig.TcpIp = TextUtils.ToString(txtTcpIp.Text);
				andonConfig.SocketPort = TextUtils.ToInt(txtPort.Text);
				andonConfig.Takt = TextUtils.ToInt(txtTakt.Text);
				AndonConfigBO.Instance.Insert(andonConfig);
				MessageBox.Show("Config font size successfully! ");
				TextUtils.ExcuteSQL("exec spUpdateTakt @Takt = " + andonConfig.Takt);
			}
		}

		private void numFontValueCD_ValueChanged(object sender, EventArgs e)
		{
			_FontSize(numFontValueCD.Value, numFontTitleCD.Value, numFontValuePlan.Value, numFontLabelPlan.Value, numFontTitleAndon.Value, numLabelTakt.Value, numValueTakt.Value);
		}

		private void numFontTitleCD_ValueChanged(object sender, EventArgs e)
		{
			_FontSize(numFontValueCD.Value, numFontTitleCD.Value, numFontValuePlan.Value, numFontLabelPlan.Value, numFontTitleAndon.Value, numLabelTakt.Value, numValueTakt.Value);
		}

		private void numFontTitleAndon_ValueChanged(object sender, EventArgs e)
		{
			_FontSize(numFontValueCD.Value, numFontTitleCD.Value, numFontValuePlan.Value, numFontLabelPlan.Value, numFontTitleAndon.Value, numLabelTakt.Value, numValueTakt.Value);
		}

		private void numFontValuePlan_ValueChanged(object sender, EventArgs e)
		{
			_FontSize(numFontValueCD.Value, numFontTitleCD.Value, numFontValuePlan.Value, numFontLabelPlan.Value, numFontTitleAndon.Value, numLabelTakt.Value, numValueTakt.Value);

		}

		private void numFontLabelPlan_ValueChanged(object sender, EventArgs e)
		{
			_FontSize(numFontValueCD.Value, numFontTitleCD.Value, numFontValuePlan.Value, numFontLabelPlan.Value, numFontTitleAndon.Value, numLabelTakt.Value, numValueTakt.Value);
		}

		private void numLabelTakt_ValueChanged(object sender, EventArgs e)
		{
			_FontSize(numFontValueCD.Value, numFontTitleCD.Value, numFontValuePlan.Value, numFontLabelPlan.Value, numFontTitleAndon.Value, numLabelTakt.Value, numValueTakt.Value);

		}

		private void numValueTakt_ValueChanged(object sender, EventArgs e)
		{
			_FontSize(numFontValueCD.Value, numFontTitleCD.Value, numFontValuePlan.Value, numFontLabelPlan.Value, numFontTitleAndon.Value, numLabelTakt.Value, numValueTakt.Value);

		}
	}	
}
