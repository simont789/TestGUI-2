using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
		protected int pageCount;


		public GUIForm()
		{
			InitializeComponent();
			lapp = new LuceneApplication();
			pageCount = 0;

		}

		private void CreateIndexAtPathBottonHandler(object sender, EventArgs e)
		{
			lapp.CreateIndexFrom(IndexPathTextBox.Text);
			lapp.ReadTextFilesAndIndexTextFrom(CollectionPathTextBox.Text);
			lapp.CleanUpIndexer();
		}
		private void SearchButtonHandler(object sender, EventArgs e)
		{
			lapp.SetupSearch();
			string expandedQeury = lapp.ExpandQuery(QueryTextBox.Text);
			FinalQueryLabel.Text = expandedQeury;
			topdocs = lapp.SearchFor(expandedQeury);
			// search index with EXPANDED and WEIGHTED querytext from textbox
			// show result in resultlabel, rank 1 to 10
		}
		private void SearchAsIsButtonHandler(object sender, EventArgs e)
		{
			lapp.SetupSearch();
			FinalQueryLabel.Text = QueryTextBox.Text;
			lapp.SearchFor(QueryTextBox.Text);
			// show number of results number of results
			// show index time
			ShowResult();
		}

		private void ViewLastPageButtonHandler(object sender, EventArgs e)
		{
			pageCount += 10;
			ShowResult();
		}
		private void ViewNextPageButtonHandler(object sender, EventArgs e)
		{
			pageCount -= 10;
			ShowResult();
		}

		private void ShowResult()
		{
			for (int i = pageCount; i < (pageCount + 10); i++)
			{
				AcadamicPublication ap = lapp.GetAcadamicPublicationAt(i);
				// print on labels
			}

		}





		private void RestartSectionBottonHandler(object sender, EventArgs e)
		{

		}
		private void SaveAsButtonHandler(object sender, EventArgs e)
		{

		}

	}
}
