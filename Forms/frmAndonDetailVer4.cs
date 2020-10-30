using BMS;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace BMS
{
	public delegate void FontSize(decimal fontSize1, decimal fontSize2, decimal fontSize3, decimal fontSize4, decimal fontSize5, decimal fontSize6, decimal fontSize7);
	public delegate void SendNotUseCD(int status, string CD);
	public delegate void SendStatusActual(int isFinish);
	public delegate void SendData(int type, int value, string CD);
	public delegate void SendRisk(int type, int value);
	public delegate void SendIsStart(int isStart);
	public partial class frmAndonDetailVer4 : Form
	{
		public AndonModel _OAndonModel;
		public StatusColorCDModel _OStatusColorCDModel;
		private int _isFinishActual = 0;
		private int _isStartAndon = 0;
		public ManualResetEvent _EventUpdateAndon;

		//AutoResetEvent newStatusColorEvent;

		private Thread _threadCD1;
		private Thread _threadCD2;
		private Thread _threadCD3;
		private Thread _threadCD4;
		private Thread _threadCD5;
		private Thread _threadCD6;
		private Thread _threadCD7;
		private Thread _threadCD8;
		private Thread _threadCD81;
		private Thread _threadCD9;
		private Thread _threadResetTakt;
		private Thread _threadUpdateStatusColorCD;
		private Thread _threadUpdateAndon;
		private Thread _threadChangeColor;
		private Thread _threadUpdateCurrent;
		private Thread _threadLoadAndon;
		private Thread _threadLoadStatusColorCD;
		private bool _isFisrtStart = false;

		//public ManualResetEvent _EventUpdate;
		//private StatusColor _statusColor;

		frmServer _frmServer;

		public frmAndonDetailVer4()
		{
			InitializeComponent();
		}

		private void frmAndon_Load(object sender, EventArgs e)
		{
			_frmServer = new frmServer();
			_frmServer.ShowInTaskbar = false;
			_frmServer._SendData = new SendData(sendData);
			_frmServer.Show();
			_OAndonModel = new AndonModel();
			_OStatusColorCDModel = new StatusColorCDModel();

			//Event update Andon

			_EventUpdateAndon = new ManualResetEvent(true);
			//_EventUpdate = new ManualResetEvent(true);
			// Load font size trong bảng FontConfig
			ArrayList arrAndonConfig = AndonConfigBO.Instance.FindAll();
			if (arrAndonConfig.Count > 0)
			{
				AndonConfigModel andonConfig = (AndonConfigModel)arrAndonConfig[0];
				fontSizefn(andonConfig.FontSize1, andonConfig.FontSize2, andonConfig.FontSize3, andonConfig.FontSize4, andonConfig.FontSize5, andonConfig.FontSize6, andonConfig.FontSize7);
			}

			//_OStatusColorModel = new StatusColorCDModel();
			progressBar.Properties.Minimum = 0;
			//timer1.Enabled = true;
			bgwCD1.WorkerSupportsCancellation = true;
			bgwCD2.WorkerSupportsCancellation = true;

			// Chạy thread load Andon theo thời gian hiện tại
			_threadLoadAndon = new Thread(new ThreadStart(LoadAndon));
			_threadLoadAndon.IsBackground = true;
			_threadLoadAndon.Start();

			// Chạy thread load StatusColor theo thời gian hiện tại
			_threadLoadStatusColorCD = new Thread(new ThreadStart(LoadStatusColorCD));
			_threadLoadStatusColorCD.IsBackground = true;
			_threadLoadStatusColorCD.Start();
			Thread.Sleep(500);

			// Thread reset Takt time khi các CD hoàn thành.
			_threadResetTakt = new Thread(new ThreadStart(threadResetTaktTime));
			_threadResetTakt.IsBackground = true;
			_threadResetTakt.Start();

			#region KHởi tạo thread tính delay
			//Thread tính delay CD1
			_threadCD1 = new Thread(new ThreadStart(checkDelayCD1));
			_threadCD1.IsBackground = true;
			_threadCD1.Start();
			//Thread tính delay CD2
			_threadCD2 = new Thread(new ThreadStart(checkDelayCD2));
			_threadCD2.IsBackground = true;
			_threadCD2.Start();
			//Thread tính delay CD3
			_threadCD3 = new Thread(new ThreadStart(checkDelayCD3));
			_threadCD3.IsBackground = true;
			_threadCD3.Start();
			//Thread tính delay CD4
			_threadCD4 = new Thread(new ThreadStart(checkDelayCD4));
			_threadCD4.IsBackground = true;
			_threadCD4.Start();
			//Thread tính delay CD5
			_threadCD5 = new Thread(new ThreadStart(checkDelayCD5));
			_threadCD5.IsBackground = true;
			_threadCD5.Start();
			//Thread tính delay CD6
			_threadCD6 = new Thread(new ThreadStart(checkDelayCD6));
			_threadCD6.IsBackground = true;
			_threadCD6.Start();
			//Thread tính delay CD7
			_threadCD7 = new Thread(new ThreadStart(checkDelayCD7));
			_threadCD7.IsBackground = true;
			_threadCD7.Start();
			//Thread tính delay CD8
			_threadCD8 = new Thread(new ThreadStart(checkDelayCD8));
			_threadCD8.IsBackground = true;
			_threadCD8.Start();
			//Thread tính delay CD81
			_threadCD81 = new Thread(new ThreadStart(checkDelayCD81));
			_threadCD81.IsBackground = true;
			_threadCD81.Start();
			//Thread tính delay CD9
			_threadCD9 = new Thread(new ThreadStart(checkDelayCD9));
			_threadCD9.IsBackground = true;
			_threadCD9.Start();
			#endregion

			_threadUpdateStatusColorCD = new Thread(new ThreadStart(threadUpdateStatusColorCD));
			_threadUpdateStatusColorCD.IsBackground = true;
			_threadUpdateStatusColorCD.Start();

			_threadUpdateAndon = new Thread(new ThreadStart(threadUpdateAndon));
			_threadUpdateAndon.IsBackground = true;
			_threadUpdateAndon.Start();

			//Thread thay đổi màu khi CD đổi trạng thái
			_threadChangeColor = new Thread(new ThreadStart(threadChangeBackgroundColor));
			_threadChangeColor.IsBackground = true;
			_threadChangeColor.Start();

			// Thread update lại plan current
			_threadUpdateCurrent = new Thread(new ThreadStart(threadUpdatePlanCurrent));
			_threadUpdateCurrent.IsBackground = true;
			_threadUpdateCurrent.Start();

			// BackgroundWorker để hiển thị CD và số lượng SP
			bgwCD1.RunWorkerAsync();
			bgwCD2.RunWorkerAsync();
		}

		#region methods
		private void sendData(int type, int value, string CD)
		{
			if (type == 3)// cập nhật số lượng thực tế
			{
				_isFinishActual = value;
			}
			else if (type == 4)// khởi động takt time
			{
				_isStartAndon = value;
			}
			else if (type == 10)
			{
				switch (CD)
				{
					case "CD1":
						_OStatusColorCDModel.CD1 = value == 10 ? 4 : (progressBar.Position != 0 ? 1 : 2);
						break;
					case "CD2":
						_OStatusColorCDModel.CD2 = value == 10 ? 4 : (progressBar.Position != 0 ? 1 : 2);
						break;
					case "CD3":
						_OStatusColorCDModel.CD3 = value == 10 ? 4 : (progressBar.Position != 0 ? 1 : 2);
						break;
					case "CD4":
						_OStatusColorCDModel.CD4 = value == 10 ? 4 : (progressBar.Position != 0 ? 1 : 2);
						break;
					case "CD5":
						_OStatusColorCDModel.CD5 = value == 10 ? 4 : (progressBar.Position != 0 ? 1 : 2);
						break;
					case "CD6":
						_OStatusColorCDModel.CD6 = value == 10 ? 4 : (progressBar.Position != 0 ? 1 : 2);
						break;
					case "CD7":
						_OStatusColorCDModel.CD7 = value == 10 ? 4 : (progressBar.Position != 0 ? 1 : 2);
						break;
					case "CD8":
						_OStatusColorCDModel.CD8 = value == 10 ? 4 : (progressBar.Position != 0 ? 1 : 2);
						break;
					case "CD8-1":
						_OStatusColorCDModel.CD81 = value == 10 ? 4 : (progressBar.Position != 0 ? 1 : 2);
						break;
					case "CD9":
						_OStatusColorCDModel.CD9 = value == 10 ? 4 : (progressBar.Position != 0 ? 1 : 2);
						break;
				}
				StatusColorCDBO.Instance.Update(_OStatusColorCDModel);
			}
			
		}
		private void editAndon(bool isEdit)
		{
			if(!isEdit)
			{
				_EventUpdateAndon.Reset();
			}
			else
			{
				_EventUpdateAndon.Set();
			}
		}
		private void checkDelayCD1()
		{
			DateTime fDelayTime = new DateTime();
			bool isBeginDelay = false;
			bool isBeginOK = true;
			while (true)
			{
				Thread.Sleep(300);
				try
				{
					if (_OAndonModel.ID == 0) continue;
					if (!_OAndonModel.IsStart) continue;
					if (_isBreakTime)
					{
						//tính lại delay
						isBeginDelay = false;
						continue;
					}

					if (_OStatusColorCDModel.CD1 == 2)
					{
						if (!isBeginDelay)
						{
							fDelayTime = DateTime.Now;
							isBeginDelay = true;
							isBeginOK = true;
						}
					}

					if (_OStatusColorCDModel.CD1 == 4)
					{
						if (isBeginDelay && isBeginOK)
						{
							//Cất vào bảng AndonDetail
							var timeSpan = DateTime.Now - fDelayTime;
							double periodTime = timeSpan.TotalSeconds;
							AndonDetailModel andonDetail = new AndonDetailModel();
							andonDetail.ProductStepCode = "CD1";
							andonDetail.PeriodTime = TextUtils.ToInt(Math.Round(periodTime, 0));
							andonDetail.StartTime = fDelayTime;
							andonDetail.EndTime = DateTime.Now;
							andonDetail.MakeTime = andonDetail.PeriodTime + _OAndonModel.Takt;
							andonDetail.Type = 1;
							AndonDetailBO.Instance.Insert(andonDetail);
							isBeginOK = false;
							isBeginDelay = false;
						}
					}
				}
				catch (Exception ex)
				{
					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":checkDelayCD1(): " + ex.ToString() + Environment.NewLine);
				}

			}
		}
		private void checkDelayCD2()
		{
			DateTime fDelayTime = new DateTime();
			bool isBeginDelay = false;
			bool isBeginOK = true;
			while (true)
			{
				Thread.Sleep(300);
				try
				{
					if (_OAndonModel.ID == 0) continue;
					if (!_OAndonModel.IsStart) continue;
					if (_isBreakTime)
					{
						isBeginDelay = false;
						continue;
					}

					if (_OStatusColorCDModel.CD2 == 2)
					{
						if (!isBeginDelay)
						{
							fDelayTime = DateTime.Now;
							isBeginDelay = true;
							isBeginOK = true;
						}
					}

					if (_OStatusColorCDModel.CD2 == 4)
					{
						if (isBeginDelay && isBeginOK)
						{
							//Cất vào bảng AndonDetail
							var timeSpan = DateTime.Now - fDelayTime;
							double periodTime = timeSpan.TotalSeconds;
							AndonDetailModel andonDetail = new AndonDetailModel();
							andonDetail.ProductStepCode = "CD2";
							andonDetail.PeriodTime = TextUtils.ToInt(Math.Round(periodTime, 0));
							andonDetail.StartTime = fDelayTime;
							andonDetail.EndTime = DateTime.Now;
							andonDetail.MakeTime = andonDetail.PeriodTime + _OAndonModel.Takt;
							andonDetail.Type = 1;
							AndonDetailBO.Instance.Insert(andonDetail);
							isBeginOK = false;
							isBeginDelay = false;
						}
					}
				}
				catch (Exception ex)
				{
					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":checkDelayCD2(): " + ex.ToString() + Environment.NewLine);
				}


			}
		}
		private void checkDelayCD3()
		{
			DateTime fDelayTime = new DateTime();
			bool isBeginDelay = false;
			bool isBeginOK = true;
			while (true)
			{
				Thread.Sleep(300);
				try
				{
					if (_OAndonModel.ID == 0) continue;
					if (!_OAndonModel.IsStart) continue;
					if (_isBreakTime)
					{
						isBeginDelay = false;
						continue;
					}

					if (_OStatusColorCDModel.CD3 == 2)
					{
						if (!isBeginDelay)
						{
							fDelayTime = DateTime.Now;
							isBeginDelay = true;
							isBeginOK = true;
						}
					}

					if (_OStatusColorCDModel.CD3 == 4)
					{
						if (isBeginDelay && isBeginOK)
						{
							//Cất vào bảng AndonDetail
							var timeSpan = DateTime.Now - fDelayTime;
							double periodTime = timeSpan.TotalSeconds;
							AndonDetailModel andonDetail = new AndonDetailModel();
							andonDetail.ProductStepCode = "CD3";
							andonDetail.PeriodTime = TextUtils.ToInt(Math.Round(periodTime, 0));
							andonDetail.StartTime = fDelayTime;
							andonDetail.EndTime = DateTime.Now;
							andonDetail.MakeTime = andonDetail.PeriodTime + _OAndonModel.Takt;
							andonDetail.Type = 1;
							AndonDetailBO.Instance.Insert(andonDetail);
							isBeginOK = false;
							isBeginDelay = false;
						}
					}
				}
				catch (Exception ex)
				{
					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":checkDelayCD3(): " + ex.ToString() + Environment.NewLine);
				}
			}
		}
		private void checkDelayCD4()
		{
			DateTime fDelayTime = new DateTime();
			bool isBeginDelay = false;
			bool isBeginOK = true;
			while (true)
			{
				Thread.Sleep(300);
				try
				{
					if (_OAndonModel.ID == 0) continue;
					if (!_OAndonModel.IsStart) continue;
					if (_isBreakTime)
					{
						isBeginDelay = false;
						continue;
					}
					if (_OStatusColorCDModel.CD4 == 2)
					{
						if (!isBeginDelay)
						{
							fDelayTime = DateTime.Now;
							isBeginDelay = true;
							isBeginOK = true;
						}
					}

					if (_OStatusColorCDModel.CD4 == 4)
					{
						if (isBeginDelay && isBeginOK)
						{
							//Cất vào bảng AndonDetail
							var timeSpan = DateTime.Now - fDelayTime;
							double periodTime = timeSpan.TotalSeconds;
							AndonDetailModel andonDetail = new AndonDetailModel();
							andonDetail.ProductStepCode = "CD4";
							andonDetail.PeriodTime = TextUtils.ToInt(Math.Round(periodTime, 0));
							andonDetail.StartTime = fDelayTime;
							andonDetail.EndTime = DateTime.Now;
							andonDetail.MakeTime = andonDetail.PeriodTime + _OAndonModel.Takt;
							andonDetail.Type = 1;
							AndonDetailBO.Instance.Insert(andonDetail);
							isBeginOK = false;
							isBeginDelay = false;
						}
					}
				}
				catch (Exception ex)
				{
					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":checkDelayCD4(): " + ex.ToString() + Environment.NewLine);
				}
			}
		}
		private void checkDelayCD5()
		{
			DateTime fDelayTime = new DateTime();
			bool isBeginDelay = false;
			bool isBeginOK = true;
			while (true)
			{
				Thread.Sleep(300);
				try
				{
					if (_OAndonModel.ID == 0) continue;
					if (!_OAndonModel.IsStart) continue;
					if (_isBreakTime)
					{
						isBeginDelay = false;
						continue;
					}
					if (_OStatusColorCDModel.CD5 == 2)
					{
						if (!isBeginDelay)
						{
							fDelayTime = DateTime.Now;
							isBeginDelay = true;
							isBeginOK = true;
						}
					}

					if (_OStatusColorCDModel.CD5 == 4)
					{
						if (isBeginDelay && isBeginOK)
						{
							//Cất vào bảng AndonDetail
							var timeSpan = DateTime.Now - fDelayTime;
							double periodTime = timeSpan.TotalSeconds;
							AndonDetailModel andonDetail = new AndonDetailModel();
							andonDetail.ProductStepCode = "CD5";
							andonDetail.PeriodTime = TextUtils.ToInt(Math.Round(periodTime, 0));
							andonDetail.StartTime = fDelayTime;
							andonDetail.EndTime = DateTime.Now;
							andonDetail.MakeTime = andonDetail.PeriodTime + _OAndonModel.Takt;
							andonDetail.Type = 1;
							AndonDetailBO.Instance.Insert(andonDetail);
							isBeginOK = false;
							isBeginDelay = false;
						}
					}
				}
				catch (Exception ex)
				{
					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":checkDelayCD5(): " + ex.ToString() + Environment.NewLine);
				}
			}
		}
		private void checkDelayCD6()
		{
			DateTime fDelayTime = new DateTime();
			bool isBeginDelay = false;
			bool isBeginOK = true;
			while (true)
			{
				Thread.Sleep(300);
				try
				{
					if (_OAndonModel.ID == 0) continue;
					if (!_OAndonModel.IsStart) continue;
					if (_isBreakTime)
					{
						isBeginDelay = false;
						continue;
					}
					if (_OStatusColorCDModel.CD6 == 2)
					{
						if (!isBeginDelay)
						{
							fDelayTime = DateTime.Now;
							isBeginDelay = true;
							isBeginOK = true;
						}
					}

					if (_OStatusColorCDModel.CD6 == 4)
					{
						if (isBeginDelay && isBeginOK)
						{
							//Cất vào bảng AndonDetail
							var timeSpan = DateTime.Now - fDelayTime;
							double periodTime = timeSpan.TotalSeconds;
							AndonDetailModel andonDetail = new AndonDetailModel();
							andonDetail.ProductStepCode = "CD6";
							andonDetail.PeriodTime = TextUtils.ToInt(Math.Round(periodTime, 0));
							andonDetail.StartTime = fDelayTime;
							andonDetail.EndTime = DateTime.Now;
							andonDetail.MakeTime = andonDetail.PeriodTime + _OAndonModel.Takt;
							andonDetail.Type = 1;
							AndonDetailBO.Instance.Insert(andonDetail);
							isBeginOK = false;
							isBeginDelay = false;
						}
					}
				}
				catch (Exception ex)
				{
					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":checkDelayCD6(): " + ex.ToString() + Environment.NewLine);
				}
			}
		}
		private void checkDelayCD7()
		{
			DateTime fDelayTime = new DateTime();
			bool isBeginDelay = false;
			bool isBeginOK = true;
			while (true)
			{
				Thread.Sleep(300);
				try
				{
					if (_OAndonModel.ID == 0) continue;
					if (!_OAndonModel.IsStart) continue;
					if (_isBreakTime)
					{
						isBeginDelay = false;
						continue;
					}
					if (_OStatusColorCDModel.CD7 == 2)
					{
						if (!isBeginDelay)
						{
							fDelayTime = DateTime.Now;
							isBeginDelay = true;
							isBeginOK = true;
						}
					}

					if (_OStatusColorCDModel.CD7 == 4)
					{
						if (isBeginDelay && isBeginOK)
						{
							//Cất vào bảng AndonDetail
							var timeSpan = DateTime.Now - fDelayTime;
							double periodTime = timeSpan.TotalSeconds;
							AndonDetailModel andonDetail = new AndonDetailModel();
							andonDetail.ProductStepCode = "CD7";
							andonDetail.PeriodTime = TextUtils.ToInt(Math.Round(periodTime, 0));
							andonDetail.StartTime = fDelayTime;
							andonDetail.EndTime = DateTime.Now;
							andonDetail.MakeTime = andonDetail.PeriodTime + _OAndonModel.Takt;
							andonDetail.Type = 1;
							AndonDetailBO.Instance.Insert(andonDetail);
							isBeginOK = false;
							isBeginDelay = false;
						}
					}
				}
				catch (Exception ex)
				{
					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":checkDelayCD7(): " + ex.ToString() + Environment.NewLine);
				}

			}
		}
		private void checkDelayCD8()
		{
			DateTime fDelayTime = new DateTime();
			bool isBeginDelay = false;
			bool isBeginOK = true;
			while (true)
			{
				Thread.Sleep(300);
				try
				{
					if (_OAndonModel.ID == 0) continue;
					if (!_OAndonModel.IsStart) continue;
					if (_isBreakTime)
					{
						isBeginDelay = false;
						continue;
					}
					if (_OStatusColorCDModel.CD8 == 2)
					{
						if (!isBeginDelay)
						{
							fDelayTime = DateTime.Now;
							isBeginDelay = true;
							isBeginOK = true;
						}
					}

					if (_OStatusColorCDModel.CD8 == 4)
					{
						if (isBeginDelay && isBeginOK)
						{
							//Cất vào bảng AndonDetail
							var timeSpan = DateTime.Now - fDelayTime;
							double periodTime = timeSpan.TotalSeconds;
							AndonDetailModel andonDetail = new AndonDetailModel();
							andonDetail.ProductStepCode = "CD8";
							andonDetail.PeriodTime = TextUtils.ToInt(Math.Round(periodTime, 0));
							andonDetail.StartTime = fDelayTime;
							andonDetail.EndTime = DateTime.Now;
							andonDetail.MakeTime = andonDetail.PeriodTime + _OAndonModel.Takt;
							andonDetail.Type = 1;
							AndonDetailBO.Instance.Insert(andonDetail);
							isBeginOK = false;
							isBeginDelay = false;
						}
					}
				}
				catch (Exception ex)
				{
					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":checkDelayCD8(): " + ex.ToString() + Environment.NewLine);
				}

			}
		}
		private void checkDelayCD81()
		{
			DateTime fDelayTime = new DateTime();
			bool isBeginDelay = false;
			bool isBeginOK = true;
			while (true)
			{
				Thread.Sleep(300);
				try
				{
					if (_OAndonModel.ID == 0) continue;
					if (!_OAndonModel.IsStart) continue;
					if (_isBreakTime)
					{
						isBeginDelay = false;
						continue;
					}
					if (_OStatusColorCDModel.CD81 == 2)
					{
						if (!isBeginDelay)
						{
							fDelayTime = DateTime.Now;
							isBeginDelay = true;
							isBeginOK = true;
						}
					}

					if (_OStatusColorCDModel.CD81 == 4)
					{
						if (isBeginDelay && isBeginOK)
						{
							//Cất vào bảng AndonDetail
							var timeSpan = DateTime.Now - fDelayTime;
							double periodTime = timeSpan.TotalSeconds;
							AndonDetailModel andonDetail = new AndonDetailModel();
							andonDetail.ProductStepCode = "CD8-1";
							andonDetail.PeriodTime = TextUtils.ToInt(Math.Round(periodTime, 0));
							andonDetail.StartTime = fDelayTime;
							andonDetail.EndTime = DateTime.Now;
							andonDetail.MakeTime = andonDetail.PeriodTime + _OAndonModel.Takt;
							andonDetail.Type = 1;
							AndonDetailBO.Instance.Insert(andonDetail);
							isBeginOK = false;
							isBeginDelay = false;
						}
					}
				}
				catch (Exception ex)
				{
					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":checkDelayCD81(): " + ex.ToString() + Environment.NewLine);
				}

			}
		}
		private void checkDelayCD9()
		{
			DateTime fDelayTime = new DateTime();
			bool isBeginDelay = false;
			bool isBeginOK = true;
			while (true)
			{
				Thread.Sleep(300);
				try
				{
					if (_OAndonModel.ID == 0) continue;
					if (!_OAndonModel.IsStart) continue;
					if (_isBreakTime)
					{
						isBeginDelay = false;
						continue;
					}
					if (_OStatusColorCDModel.CD9 == 2)
					{
						if (!isBeginDelay)
						{
							fDelayTime = DateTime.Now;
							isBeginDelay = true;
							isBeginOK = true;
						}
					}

					if (_OStatusColorCDModel.CD9 == 4)
					{
						if (isBeginDelay && isBeginOK)
						{
							//Cất vào bảng AndonDetail
							var timeSpan = DateTime.Now - fDelayTime;
							double periodTime = timeSpan.TotalSeconds;
							AndonDetailModel andonDetail = new AndonDetailModel();
							andonDetail.ProductStepCode = "CD9";
							andonDetail.PeriodTime = TextUtils.ToInt(Math.Round(periodTime, 0));
							andonDetail.StartTime = fDelayTime;
							andonDetail.EndTime = DateTime.Now;
							andonDetail.MakeTime = andonDetail.PeriodTime + _OAndonModel.Takt;
							andonDetail.Type = 1;
							AndonDetailBO.Instance.Insert(andonDetail);
							isBeginOK = false;
							isBeginDelay = false;
						}
					}
				}
				catch (Exception ex)
				{
					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":checkDelayCD9(): " + ex.ToString() + Environment.NewLine);
				}
			}
		}

		/// <summary>
		/// Cập nhật khi kết thúc ca
		/// </summary>
		private void threadUpdateStatusColorCD()
		{
			while (true)
			{
				Thread.Sleep(1000);
				try
				{
					for (int i = 1; i <= 10; i++)
					{
						string field = "CD";
						if (i == 10)
						{
							field += "81";
						}
						else
						{
							field += i.ToString();
						}
						int value = int.Parse(_frmServer._StatusColorCDModel[field].ToString());
						if (value == 3)
						{
							_frmServer._StatusColorCDModel[field] = 0;
							_OStatusColorCDModel[field] = 3;
							StatusColorCDBO.Instance.Update(_OStatusColorCDModel);

						}
						else if (value == 4)
						{
							_frmServer._StatusColorCDModel[field] = 0;
							_OStatusColorCDModel[field] = 4;
							StatusColorCDBO.Instance.Update(_OStatusColorCDModel);
						}
					}
									
				}
				catch (Exception ex)
				{
					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":threadUpdateStatusColorCD(): " + ex.ToString() + Environment.NewLine);
				}
			}
		}
		private void threadUpdateAndon()
		{
			while (true)
			{
				Thread.Sleep(1000);
				if (_OAndonModel.ID == 0) continue;
				try
				{
					if (_OAndonModel.IsStart == false)
					{
						if (_isStartAndon == 1)
						{
							_EventUpdateAndon.Reset();
							_OAndonModel.IsStart = true;
							AndonBO.Instance.Update(_OAndonModel);
							_EventUpdateAndon.Set();
						}
					}
					if (_isFinishActual == 1)
					{
						_EventUpdateAndon.Reset();
						_isFinishActual = 0;
						_OAndonModel.QtyActual += 1;
						AndonBO.Instance.Update(_OAndonModel);
						_EventUpdateAndon.Set();
					}
				}
				catch (Exception ex)
				{

					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":threadUpdateAndon(): " + ex.ToString() + Environment.NewLine);
				}				
			}
		}
		private void threadChangeBackgroundColor()
		{
			while (true)
			{
				Thread.Sleep(1000);
				try
				{
					this.Invoke((MethodInvoker)delegate
					{
						for (int i = 1; i <= 10; i++)
						{
							string field = "CD";
							if (i == 10)
							{
								field += "81";
							}
							else
							{
								field += i.ToString();
							}
							int value = int.Parse(_OStatusColorCDModel[field].ToString());
							Control btn = this.Controls.Find("btn" + field, true)[0];
							switch (value)
							{
								case 1:
									btn.BackColor = Color.Gray;
									break;
								case 2:
									btn.BackColor = Color.Yellow;
									break;
								case 3:
									btn.BackColor = Color.Red;
									break;
								case 4:
									btn.BackColor = Color.Lime;
									break;
								case 5:
									btn.BackColor = Color.FromArgb(192, 192, 255);
									break;
								default:
									break;
							}
						}
					});
				}
				catch (Exception ex)
				{
					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":threadChangeBackgroundColor(): " + ex.ToString() + Environment.NewLine);
				}

			}
		}
		bool _isBreakTime = false;
		private void resetStatusColor(StatusColorCDModel statusColor, int valueColor)
		{
			try
			{
				statusColor.CD1 = valueColor;
				statusColor.CD2 = valueColor;
				statusColor.CD3 = valueColor;
				statusColor.CD4 = valueColor;
				statusColor.CD5 = valueColor;
				statusColor.CD6 = valueColor;
				statusColor.CD7 = valueColor;
				statusColor.CD8 = valueColor;
				statusColor.CD81 = valueColor;
				statusColor.CD9 = valueColor;
				StatusColorCDBO.Instance.Update(statusColor);
			}
			catch (Exception ex)
			{

				File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":resetStatusColor(): " + ex.ToString() + Environment.NewLine);
			}
			
		}
		private void resetTaktTime()
		{
			try
			{
				_OStatusColorCDModel.CD1 = _frmServer._NotUseCD.CD1 == 1 ? 4 : 1;
				_OStatusColorCDModel.CD2 = _frmServer._NotUseCD.CD2 == 1 ? 4 : 1;
				_OStatusColorCDModel.CD3 = _frmServer._NotUseCD.CD3 == 1 ? 4 : 1;
				_OStatusColorCDModel.CD4 = _frmServer._NotUseCD.CD4 == 1 ? 4 : 1;
				_OStatusColorCDModel.CD5 = _frmServer._NotUseCD.CD5 == 1 ? 4 : 1;
				_OStatusColorCDModel.CD6 = _frmServer._NotUseCD.CD6 == 1 ? 4 : 1;
				_OStatusColorCDModel.CD7 = _frmServer._NotUseCD.CD7 == 1 ? 4 : 1;
				_OStatusColorCDModel.CD8 = _frmServer._NotUseCD.CD8 == 1 ? 4 : 1;
				_OStatusColorCDModel.CD81 = _frmServer._NotUseCD.CD81 == 1 ? 4 : 1;
				_OStatusColorCDModel.CD9 = _frmServer._NotUseCD.CD9 == 1 ? 4 : 1;				
				StatusColorCDBO.Instance.Update(_OStatusColorCDModel);
			}
			catch (Exception ex)
			{

				File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":resetTaktTime(): " + ex.ToString() + Environment.NewLine);
			}
			
		}
		private void threadUpdatePlanCurrent()
		{
			while (true)
			{
				Thread.Sleep(2000);
				try
				{
					if (_OAndonModel.ID == 0) continue;

					TimeSpan hourCurrent = new TimeSpan();
					// Tính plan current theo 2 khoảng thời gian (thời gian bắt đầu ca --> thời gian bắt đầu nghỉ giải lao 
					// và thời gian kết thúc nghỉ --> thời gian kết thúc ca)
					// khoảng break 1
					if (DateTime.Now >= _OAndonModel.ShiftStartTime && DateTime.Now <= _OAndonModel.StartTimeBreak1)
					{
						hourCurrent = (DateTime.Now - _OAndonModel.ShiftStartTime.Value);

					}
					else if (DateTime.Now >= _OAndonModel.EndTimeBreak1 && DateTime.Now <= _OAndonModel.StartTimeBreak2)
					{
						//break 2
						hourCurrent = (DateTime.Now - _OAndonModel.ShiftStartTime.Value)
							- (_OAndonModel.EndTimeBreak1.Value - _OAndonModel.StartTimeBreak1.Value);
					}
					else if (DateTime.Now >= _OAndonModel.EndTimeBreak2 && DateTime.Now <= _OAndonModel.StartTimeBreak3)
					{
						//break 2
						hourCurrent = (DateTime.Now - _OAndonModel.ShiftStartTime.Value)
							- (_OAndonModel.EndTimeBreak1.Value - _OAndonModel.StartTimeBreak1.Value)
							- (_OAndonModel.EndTimeBreak2.Value - _OAndonModel.StartTimeBreak2.Value);
					}
					else if (DateTime.Now >= _OAndonModel.EndTimeBreak3 && DateTime.Now <= _OAndonModel.StartTimeBreak4)
					{
						hourCurrent = (DateTime.Now - _OAndonModel.ShiftStartTime.Value)
							- (_OAndonModel.EndTimeBreak1.Value - _OAndonModel.StartTimeBreak1.Value)
							- (_OAndonModel.EndTimeBreak2.Value - _OAndonModel.StartTimeBreak2.Value)
							- (_OAndonModel.EndTimeBreak3.Value - _OAndonModel.StartTimeBreak3.Value);
					}
					else if (DateTime.Now >= _OAndonModel.EndTimeBreak4 && DateTime.Now <= _OAndonModel.ShiftEndTime)
					{
						hourCurrent = (DateTime.Now - _OAndonModel.ShiftStartTime.Value)
							- (_OAndonModel.EndTimeBreak1.Value - _OAndonModel.StartTimeBreak1.Value)
							- (_OAndonModel.EndTimeBreak2.Value - _OAndonModel.StartTimeBreak2.Value)
							- (_OAndonModel.EndTimeBreak3.Value - _OAndonModel.StartTimeBreak3.Value)
							- (_OAndonModel.EndTimeBreak4.Value - _OAndonModel.StartTimeBreak4.Value);
					}
					else
					{
						_isBreakTime = true;
						continue;
					}

					_isBreakTime = false;
					int secondTotalCurrent = (int)hourCurrent.TotalSeconds;					
					_OAndonModel.QtyPlanCurrent = secondTotalCurrent / _OAndonModel.Takt;
					_OAndonModel.QtyDelay = _OAndonModel.QtyPlanCurrent - _OAndonModel.QtyActual;
					_EventUpdateAndon.WaitOne();
					AndonBO.Instance.Update(_OAndonModel);
				}
				catch (Exception  ex)
				{
					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt", 
						DateTime.Now.ToString("HH:mm:ss") + ":threadUpdatePlanCurrent(): " + ex.ToString() + Environment.NewLine);
				}
			}
		}
		private bool checkStatusCDFinish(StatusColorCDModel statusColor)
		{
			if (statusColor.CD1 == 4 && statusColor.CD2 == 4 && statusColor.CD3 == 4 && statusColor.CD4 == 4 && statusColor.CD5 == 4
				&& statusColor.CD6 == 4 && statusColor.CD7 == 4 && statusColor.CD8 == 4 && statusColor.CD81 == 4 && statusColor.CD9 == 4)
			{
				return true;
			}
			else
			{
				return false;
			}


		}
		/// <summary>
		///Reset lại takt time trên progess bar 
		/// </summary>
		/// 
		private bool _updateDelayFirst = false;
		private int _countTakt = 0;
		private void threadResetTaktTime()
		{
			while (true)
			{
				Thread.Sleep(1000);
				if (_OAndonModel.ID == 0) continue;
				if (_isBreakTime) continue;
				try
				{
					//Kiểm tra trên bảng statusCD
					if (!_OAndonModel.IsStart)
					{						
						resetStatusColor(_OStatusColorCDModel, 5);									
						continue;
					}
					//Bắt đầu chạy progress bar
					if (!_isFisrtStart)
					{
						_isFisrtStart = true;
						resetStatusColor(_OStatusColorCDModel, 1);
						this.Invoke((MethodInvoker)delegate
						{
							progressBar.Position = _OAndonModel.Takt;
							_countTakt = _OAndonModel.Takt;
						});
						continue;
					}
					//Kiểm tra takt time, đếm ngược takt time, reset takt time
					this.Invoke((MethodInvoker)delegate
					{
						if (checkStatusCDFinish(_OStatusColorCDModel))
						{

							// reset lại takt time và cập nhật lại màu
							resetTaktTime();
							progressBar.Position = _OAndonModel.Takt;
							_countTakt = _OAndonModel.Takt;
							_updateDelayFirst = false;
						}
						else
						{
							progressBar.Position--;
							_countTakt--;
							//lblTime.Text = progressBar.Position.ToString();
							lblTime.Text = TextUtils.ToString(_countTakt);
							if (progressBar.Position == 0)
							{
								if (!_updateDelayFirst)
								{
									_updateDelayFirst = true;
									updateStatusColorDelay(_OStatusColorCDModel);
								}
							}
						}
					});
				}
				catch (Exception ex)
				{

					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":threadResetTaktTime(): " + ex.ToString() + Environment.NewLine);
				}				
			}
		}
		private void updateStatusColorDelay(StatusColorCDModel statusColorCD)
		{
			try
			{
				if (statusColorCD.CD1 == 1)
				{
					statusColorCD.CD1 = 2;
				}
				if (statusColorCD.CD2 == 1)
				{
					statusColorCD.CD2 = 2;
				}
				if (statusColorCD.CD3 == 1)
				{
					statusColorCD.CD3 = 2;
				}
				if (statusColorCD.CD4 == 1)
				{
					statusColorCD.CD4 = 2;
				}
				if (statusColorCD.CD5 == 1)
				{
					statusColorCD.CD5 = 2;
				}
				if (statusColorCD.CD6 == 1)
				{
					statusColorCD.CD6 = 2;
				}
				if (statusColorCD.CD7 == 1)
				{
					statusColorCD.CD7 = 2;
				}
				if (statusColorCD.CD8 == 1)
				{
					statusColorCD.CD8 = 2;
				}
				if (statusColorCD.CD81 == 1)
				{
					statusColorCD.CD81 = 2;
				}
				if (statusColorCD.CD9 == 1)
				{
					statusColorCD.CD9 = 2;
				}
				StatusColorCDBO.Instance.Update(statusColorCD);
			}
			catch (Exception ex)
			{

				File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":updateStatusColorDelay(): " + ex.ToString() + Environment.NewLine);
			}		
		}
		//private bool _isFirstProgressBar = false;
		private void LoadAndon()
		{
			while (true)
			{
				Thread.Sleep(500);
				try
				{
					// Store load Andon theo ngày giờ hiện tại
					ArrayList arr = AndonBO.Instance.GetListObject("spGetAndonByDateTimeNow", new string[] { }, new object[] { });
					if (arr.Count > 0)
					{
						_OAndonModel = (AndonModel)arr[0];
						this.Invoke((MethodInvoker)delegate
						{
							progressBar.Properties.Maximum = _OAndonModel.Takt;
						});
					}
					else
					{
						//lblPlanCurrent.Text = lblPlanDay.Text;
						
					}
				}
				catch (Exception ex)
				{
					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":LoadAndon(): " + ex.ToString() + Environment.NewLine);
				}
			}
		}

		//int _isStopCD8 = 0;
		private void LoadStatusColorCD()
		{
			while (true)
			{
				Thread.Sleep(500);
				try
				{
					ArrayList arrStatusColorCD = StatusColorCDBO.Instance.FindAll();
					_OStatusColorCDModel = (StatusColorCDModel)arrStatusColorCD[0];
				}
				catch (Exception ex)
				{
					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":LoadStatusColorCD(): " + ex.ToString() + Environment.NewLine);
				}
			}
		}
		private void fontSizefn(decimal fSize1, decimal fSize2, decimal fSize3, decimal fSize4, decimal fSize5, decimal fSize6, decimal fSize7)
		{
			lblCD1NumDelay.Font = new Font(lblCD1NumDelay.Font.FontFamily, (float)fSize1, lblCD1NumDelay.Font.Style);
			lblCD2NumDelay.Font = new Font(lblCD2NumDelay.Font.FontFamily, (float)fSize1, lblCD2NumDelay.Font.Style);
			lblCD3NumDelay.Font = new Font(lblCD3NumDelay.Font.FontFamily, (float)fSize1, lblCD3NumDelay.Font.Style);
			lblCD4NumDelay.Font = new Font(lblCD4NumDelay.Font.FontFamily, (float)fSize1, lblCD4NumDelay.Font.Style);
			lblCD5NumDelay.Font = new Font(lblCD5NumDelay.Font.FontFamily, (float)fSize1, lblCD5NumDelay.Font.Style);
			lblCD6NumDelay.Font = new Font(lblCD6NumDelay.Font.FontFamily, (float)fSize1, lblCD6NumDelay.Font.Style);
			lblCD7NumDelay.Font = new Font(lblCD7NumDelay.Font.FontFamily, (float)fSize1, lblCD7NumDelay.Font.Style);
			lblCD8NumDelay.Font = new Font(lblCD8NumDelay.Font.FontFamily, (float)fSize1, lblCD8NumDelay.Font.Style);
			lblCD9NumDelay.Font = new Font(lblCD9NumDelay.Font.FontFamily, (float)fSize1, lblCD9NumDelay.Font.Style);
			lblCD10NumDelay.Font = new Font(lblCD10NumDelay.Font.FontFamily, (float)fSize1, lblCD10NumDelay.Font.Style);

			lblCD1NumTrouble.Font = new Font(lblCD1NumTrouble.Font.FontFamily, (float)fSize1, lblCD1NumTrouble.Font.Style);
			lblCD2NumTrouble.Font = new Font(lblCD2NumTrouble.Font.FontFamily, (float)fSize1, lblCD2NumTrouble.Font.Style);
			lblCD3NumTrouble.Font = new Font(lblCD3NumTrouble.Font.FontFamily, (float)fSize1, lblCD3NumTrouble.Font.Style);
			lblCD4NumTrouble.Font = new Font(lblCD4NumTrouble.Font.FontFamily, (float)fSize1, lblCD4NumTrouble.Font.Style);
			lblCD5NumTrouble.Font = new Font(lblCD5NumTrouble.Font.FontFamily, (float)fSize1, lblCD5NumTrouble.Font.Style);
			lblCD6NumTrouble.Font = new Font(lblCD6NumTrouble.Font.FontFamily, (float)fSize1, lblCD6NumTrouble.Font.Style);
			lblCD7NumTrouble.Font = new Font(lblCD7NumTrouble.Font.FontFamily, (float)fSize1, lblCD7NumTrouble.Font.Style);
			lblCD8NumTrouble.Font = new Font(lblCD8NumTrouble.Font.FontFamily, (float)fSize1, lblCD8NumTrouble.Font.Style);
			lblCD9NumTrouble.Font = new Font(lblCD9NumTrouble.Font.FontFamily, (float)fSize1, lblCD9NumTrouble.Font.Style);
			lblCD10NumTrouble.Font = new Font(lblCD10NumTrouble.Font.FontFamily, (float)fSize1, lblCD10NumTrouble.Font.Style);

			lblCD1TimeDelay.Font = new Font(lblCD1TimeDelay.Font.FontFamily, (float)fSize1, lblCD1TimeDelay.Font.Style);
			lblCD2TimeDelay.Font = new Font(lblCD2TimeDelay.Font.FontFamily, (float)fSize1, lblCD2TimeDelay.Font.Style);
			lblCD3TimeDelay.Font = new Font(lblCD3TimeDelay.Font.FontFamily, (float)fSize1, lblCD3TimeDelay.Font.Style);
			lblCD4TimeDelay.Font = new Font(lblCD4TimeDelay.Font.FontFamily, (float)fSize1, lblCD4TimeDelay.Font.Style);
			lblCD5TimeDelay.Font = new Font(lblCD5TimeDelay.Font.FontFamily, (float)fSize1, lblCD5TimeDelay.Font.Style);
			lblCD6TimeDelay.Font = new Font(lblCD6TimeDelay.Font.FontFamily, (float)fSize1, lblCD6TimeDelay.Font.Style);
			lblCD7TimeDelay.Font = new Font(lblCD7TimeDelay.Font.FontFamily, (float)fSize1, lblCD7TimeDelay.Font.Style);
			lblCD8TimeDelay.Font = new Font(lblCD8TimeDelay.Font.FontFamily, (float)fSize1, lblCD8TimeDelay.Font.Style);
			lblCD9TimeDelay.Font = new Font(lblCD9TimeDelay.Font.FontFamily, (float)fSize1, lblCD9TimeDelay.Font.Style);
			lblCD10TimeDelay.Font = new Font(lblCD10TimeDelay.Font.FontFamily, (float)fSize1, lblCD10TimeDelay.Font.Style);

			btnCD1.Font = new Font(btnCD1.Font.FontFamily, (float)fSize2, btnCD1.Font.Style);
			btnCD2.Font = new Font(btnCD2.Font.FontFamily, (float)fSize2, btnCD2.Font.Style);
			btnCD3.Font = new Font(btnCD3.Font.FontFamily, (float)fSize2, btnCD3.Font.Style);
			btnCD4.Font = new Font(btnCD4.Font.FontFamily, (float)fSize2, btnCD4.Font.Style);
			btnCD5.Font = new Font(btnCD5.Font.FontFamily, (float)fSize2, btnCD5.Font.Style);
			btnCD6.Font = new Font(btnCD6.Font.FontFamily, (float)fSize2, btnCD6.Font.Style);
			btnCD7.Font = new Font(btnCD7.Font.FontFamily, (float)fSize2, btnCD7.Font.Style);
			btnCD8.Font = new Font(btnCD8.Font.FontFamily, (float)fSize2, btnCD8.Font.Style);
			btnCD81.Font = new Font(btnCD81.Font.FontFamily, (float)fSize2, btnCD81.Font.Style);
			btnCD9.Font = new Font(btnCD9.Font.FontFamily, (float)fSize2, btnCD9.Font.Style);


			lblPlanDay.Font = new Font(lblPlanDay.Font.FontFamily, (float)fSize3, lblPlanDay.Font.Style);
			lblPlanCurrent.Font = new Font(lblPlanCurrent.Font.FontFamily, (float)fSize3, lblPlanCurrent.Font.Style);
			lblActual.Font = new Font(lblActual.Font.FontFamily, (float)fSize3, lblActual.Font.Style);
			lblDelay.Font = new Font(lblDelay.Font.FontFamily, (float)fSize3, lblDelay.Font.Style);

			lblPlanDayTitle.Font = new Font(lblPlanDayTitle.Font.FontFamily, (float)fSize4, lblPlanDayTitle.Font.Style);
			lblPlanCurrentTitle.Font = new Font(lblPlanCurrentTitle.Font.FontFamily, (float)fSize4, lblPlanCurrentTitle.Font.Style);
			lblActualTitle.Font = new Font(lblActualTitle.Font.FontFamily, (float)fSize4, lblActualTitle.Font.Style);
			lblDelayTitle.Font = new Font(lblDelayTitle.Font.FontFamily, (float)fSize4, lblDelayTitle.Font.Style);

			lblTitleTakt.Font = new Font(lblTitleTakt.Font.FontFamily, (float)fSize6, lblTitleTakt.Font.Style);
			lblTime.Font = new Font(lblTime.Font.FontFamily, (float)fSize7, lblTime.Font.Style);

			lblTitleAndon.Font = new Font(lblTitleAndon.Font.FontFamily, (float)fSize5, lblTitleAndon.Font.Style);

		}
		#endregion

		#region events

		/// <summary>
		/// Lấy giá trị delay công đoạn
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bgwCD1_DoWork(object sender, DoWorkEventArgs e)
		{
			while (true)
			{
				Thread.Sleep(1000);
				try
				{
					if (_OAndonModel.ID == 0)
					{
						this.Invoke((MethodInvoker)delegate
						{
							lblCD1NumDelay.Text = "0";
							lblCD1NumTrouble.Text = "0";
							lblCD1TimeDelay.Text = "0";

							lblCD2NumDelay.Text = "0";
							lblCD2NumTrouble.Text = "0";
							lblCD2TimeDelay.Text = "0";

							lblCD3NumDelay.Text = "0";
							lblCD3NumTrouble.Text = "0";
							lblCD3TimeDelay.Text = "0";

							lblCD4NumDelay.Text = "0";
							lblCD4NumTrouble.Text = "0";
							lblCD4TimeDelay.Text = "0";

							lblCD5NumDelay.Text = "0";
							lblCD5NumTrouble.Text = "0";
							lblCD5TimeDelay.Text = "0";

							lblCD6NumDelay.Text = "0";
							lblCD6NumTrouble.Text = "0";
							lblCD6TimeDelay.Text = "0";

							lblCD7NumDelay.Text = "0";
							lblCD7NumTrouble.Text = "0";
							lblCD7TimeDelay.Text = "0";

							lblCD8NumDelay.Text = "0";
							lblCD8NumTrouble.Text = "0";
							lblCD8TimeDelay.Text = "0";

							lblCD9NumDelay.Text = "0";
							lblCD9NumTrouble.Text = "0";
							lblCD9TimeDelay.Text = "0";

							lblCD10NumDelay.Text = "0";
							lblCD10NumTrouble.Text = "0";
							lblCD10TimeDelay.Text = "0";
						});
						continue;
					}

					if (bgwCD2.CancellationPending) break;

					DataSet dts = TextUtils.GetListDataFromSP("spGetAndonDetails", "AnDonDetails"
						, new string[1] { "@AndonID" }
						, new object[1] { _OAndonModel.ID });
					DataTable dataTableCD1 = dts.Tables[0];
					DataTable dataTableCD2 = dts.Tables[1];
					DataTable dataTableCD3 = dts.Tables[2];
					DataTable dataTableCD4 = dts.Tables[3];
					DataTable dataTableCD5 = dts.Tables[4];
					DataTable dataTableCD6 = dts.Tables[5];
					DataTable dataTableCD7 = dts.Tables[6];
					DataTable dataTableCD8 = dts.Tables[7];
					DataTable dataTableCD9 = dts.Tables[8];
					DataTable dataTableCD10 = dts.Tables[9];

					this.Invoke((MethodInvoker)delegate
					{
						lblCD1NumDelay.Text = TextUtils.ToString(dataTableCD1.Rows[0]["TotalDelayNum"]);
						lblCD1NumTrouble.Text = TextUtils.ToString(dataTableCD1.Rows[0]["TotalRiskNum"]);
						lblCD1TimeDelay.Text = TextUtils.ToString(dataTableCD1.Rows[0]["TotalDelayTime"]);

						lblCD2NumDelay.Text = TextUtils.ToString(dataTableCD2.Rows[0]["TotalDelayNum"]);
						lblCD2NumTrouble.Text = TextUtils.ToString(dataTableCD2.Rows[0]["TotalRiskNum"]);
						lblCD2TimeDelay.Text = TextUtils.ToString(dataTableCD2.Rows[0]["TotalDelayTime"]);

						lblCD3NumDelay.Text = TextUtils.ToString(dataTableCD3.Rows[0]["TotalDelayNum"]);
						lblCD3NumTrouble.Text = TextUtils.ToString(dataTableCD3.Rows[0]["TotalRiskNum"]);
						lblCD3TimeDelay.Text = TextUtils.ToString(dataTableCD3.Rows[0]["TotalDelayTime"]);

						lblCD4NumDelay.Text = TextUtils.ToString(dataTableCD4.Rows[0]["TotalDelayNum"]);
						lblCD4NumTrouble.Text = TextUtils.ToString(dataTableCD4.Rows[0]["TotalRiskNum"]);
						lblCD4TimeDelay.Text = TextUtils.ToString(dataTableCD4.Rows[0]["TotalDelayTime"]);

						lblCD5NumDelay.Text = TextUtils.ToString(dataTableCD5.Rows[0]["TotalDelayNum"]);
						lblCD5NumTrouble.Text = TextUtils.ToString(dataTableCD5.Rows[0]["TotalRiskNum"]);
						lblCD5TimeDelay.Text = TextUtils.ToString(dataTableCD5.Rows[0]["TotalDelayTime"]);

						lblCD6NumDelay.Text = TextUtils.ToString(dataTableCD6.Rows[0]["TotalDelayNum"]);
						lblCD6NumTrouble.Text = TextUtils.ToString(dataTableCD6.Rows[0]["TotalRiskNum"]);
						lblCD6TimeDelay.Text = TextUtils.ToString(dataTableCD6.Rows[0]["TotalDelayTime"]);

						lblCD7NumDelay.Text = TextUtils.ToString(dataTableCD7.Rows[0]["TotalDelayNum"]);
						lblCD7NumTrouble.Text = TextUtils.ToString(dataTableCD7.Rows[0]["TotalRiskNum"]);
						lblCD7TimeDelay.Text = TextUtils.ToString(dataTableCD7.Rows[0]["TotalDelayTime"]);

						lblCD8NumDelay.Text = TextUtils.ToString(dataTableCD8.Rows[0]["TotalDelayNum"]);
						lblCD8NumTrouble.Text = TextUtils.ToString(dataTableCD8.Rows[0]["TotalRiskNum"]);
						lblCD8TimeDelay.Text = TextUtils.ToString(dataTableCD8.Rows[0]["TotalDelayTime"]);

						lblCD9NumDelay.Text = TextUtils.ToString(dataTableCD9.Rows[0]["TotalDelayNum"]);
						lblCD9NumTrouble.Text = TextUtils.ToString(dataTableCD9.Rows[0]["TotalRiskNum"]);
						lblCD9TimeDelay.Text = TextUtils.ToString(dataTableCD9.Rows[0]["TotalDelayTime"]);

						lblCD10NumDelay.Text = TextUtils.ToString(dataTableCD10.Rows[0]["TotalDelayNum"]);
						lblCD10NumTrouble.Text = TextUtils.ToString(dataTableCD10.Rows[0]["TotalRiskNum"]);
						lblCD10TimeDelay.Text = TextUtils.ToString(dataTableCD10.Rows[0]["TotalDelayTime"]);
					});
				}
				catch (Exception ex)
				{
					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":bgwCD1_DoWork(): " + ex.ToString() + Environment.NewLine);
				}
			}
		}

		/// <summary>
		/// Lấy ra và hiển thị dữ liệu trên bảng Andon
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bgwCD2_DoWork(object sender, DoWorkEventArgs e)
		{
			while (true)
			{
				Thread.Sleep(1000);
				try
				{
					if (_OAndonModel.ID == 0)
					{
						this.Invoke((MethodInvoker)delegate
						{
							lblActual.Text = "0";
							lblDelay.Text = "0";
							lblPlanDay.Text = "0";
							lblPlanCurrent.Text = "0";
							lblTime.Text = "0";
						});
						continue;
					}

					if (bgwCD2.CancellationPending) break;
					if(_OAndonModel.ShiftEndTime < DateTime.Now)
					{
						this.Invoke((MethodInvoker)delegate
						{
							lblActual.Text = TextUtils.ToString(_OAndonModel.QtyActual);
							lblDelay.Text = TextUtils.ToString(_OAndonModel.QtyDelay);
							lblPlanDay.Text = TextUtils.ToString(_OAndonModel.QtyPlanDay);
							lblPlanCurrent.Text = TextUtils.ToString(_OAndonModel.QtyPlanDay);
						});
					}
					else
					{
						this.Invoke((MethodInvoker)delegate
						{
							lblActual.Text = TextUtils.ToString(_OAndonModel.QtyActual);
							lblDelay.Text = TextUtils.ToString(_OAndonModel.QtyDelay);
							lblPlanDay.Text = TextUtils.ToString(_OAndonModel.QtyPlanDay);
							lblPlanCurrent.Text = TextUtils.ToString(_OAndonModel.QtyPlanCurrent);
						});
					}
					
				}
				catch (Exception ex)
				{
					File.AppendAllText(Application.StartupPath + "/Error_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
						DateTime.Now.ToString("HH:mm:ss") + ":bgwCD2_DoWork(): " + ex.ToString() + Environment.NewLine);
				}
			}
		}

		//F12 Set ca Andon
		private void configToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmAndonConfigVer4 frmAndonConfig = new frmAndonConfigVer4();
			frmAndonConfig._UpdateAndon = new UpdateAndon(editAndon);
			frmAndonConfig.Show();
		}
		// F11 Config Font
		private void configFontToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmShowAndon frm = new frmShowAndon();
			//frm._FontSize = new FontSize(fontSizefn);
			frm.Show();
		}
		// F10 Config các ca mặc định
		private void configShiftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmShifts frm = new frmShifts();
			frm.Show();
		}
		private void frmAndon_FormClosed(object sender, FormClosedEventArgs e)
		{
			bgwCD1.CancelAsync();
			bgwCD2.CancelAsync();
			if (_threadCD1 != null) _threadCD1.Abort();
			if (_threadCD2 != null) _threadCD2.Abort();
			if (_threadCD3 != null) _threadCD3.Abort();
			if (_threadCD4 != null) _threadCD4.Abort();
			if (_threadCD5 != null) _threadCD5.Abort();
			if (_threadCD6 != null) _threadCD6.Abort();
			if (_threadCD7 != null) _threadCD7.Abort();
			if (_threadCD8 != null) _threadCD8.Abort();
			if (_threadCD81 != null) _threadCD81.Abort();
			if (_threadCD9 != null) _threadCD9.Abort();
			if (_threadLoadAndon != null) _threadLoadAndon.Abort();
			if (_threadUpdateStatusColorCD != null) _threadUpdateStatusColorCD.Abort();
			if (_threadUpdateAndon != null) _threadUpdateAndon.Abort();
			if (_threadChangeColor != null) _threadChangeColor.Abort();
			if (_threadResetTakt != null) _threadResetTakt.Abort();
			if (_threadUpdateCurrent != null) _threadUpdateCurrent.Abort();
			if (_threadLoadStatusColorCD != null) _threadLoadStatusColorCD.Abort();
		}
		#endregion
		// F9 Config Ip/tcp
		private void configSizeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmConfig frm = new frmConfig();
			frm._FontSize = new FontSize(fontSizefn);
			frm.Show();
		}
	}
}
