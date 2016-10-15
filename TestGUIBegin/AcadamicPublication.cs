using System;

namespace LuceneApplicationForIFN647Project
{
	public class AcadamicPublication
	{
		public string DocID { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string BibliographicInformation { get; set; }
		public string Abstract { get; set; }

		public AcadamicPublication() {}
		public AcadamicPublication(string docID, string title, string author, string bibliographicInformation, string words) {
			DocID = docID;
			Title = title;
			Author = author;
			BibliographicInformation = bibliographicInformation;
			Abstract = words;
		}

	}
}
