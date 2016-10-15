using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LuceneApplicationForIFN647Project;

namespace TestGUI
{
	public partial class GUIForm : Form
	{
		LuceneApplication lapp;


		public GUIForm()
		{
			InitializeComponent();
			lapp = new LuceneApplication();

		}

		private void IndexAtPathBotton(object sender, EventArgs e)
		{
		}

	}
}
