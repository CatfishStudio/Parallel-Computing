/*
 * Сделано в SharpDevelop.
 * Пользователь: Somov Evgeniy
 * Дата: 01.04.2014
 * Время: 10:05
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TestMethods
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			//MessageBox.Show(Environment.CurrentDirectory.ToString() + ">" + textBox1.Text);
			System.Diagnostics.ProcessStartInfo pInfo = new System.Diagnostics.ProcessStartInfo();
			pInfo.FileName = "cmd.exe";
			pInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(Environment.CurrentDirectory.ToString() + "/");
			pInfo.Arguments = "/C" + textBox1.Text;
			System.Diagnostics.Process.Start(pInfo);
			//System.Diagnostics.Process.Start("cmd.exe", Environment.CurrentDirectory.ToString() + ">" + textBox1.Text);
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			System.Diagnostics.ProcessStartInfo pInfo = new System.Diagnostics.ProcessStartInfo();
			pInfo.FileName = "cmd.exe";
			pInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(Environment.CurrentDirectory.ToString() + "/");
			pInfo.Arguments = "/C" + textBox2.Text;
			System.Diagnostics.Process.Start(pInfo);
			
		}
		
		
	}
}
