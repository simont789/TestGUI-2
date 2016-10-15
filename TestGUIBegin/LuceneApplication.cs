using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis; // for analyser
using Lucene.Net.Analysis.Standard; // for standard analyser
using Lucene.Net.Documents; // for document
using Lucene.Net.Index; //for index writer
using Lucene.Net.QueryParsers; // for query parser
using Lucene.Net.Search;
using Lucene.Net.Store; //for Directory
using System.IO;


namespace LuceneApplicationForIFN647Project
{
    class LuceneApplication
    {

        Lucene.Net.Store.Directory directory;
        Lucene.Net.Analysis.Analyzer analyzer;
		Lucene.Net.Index.IndexWriter indexWriter;
		IndexSearcher indexSearcher;
		QueryParser queryParser;
		Lucene.Net.Search.TopDocs topdocs;

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;
		const string DOCID_FN = "DocID";
        const string TITLE_FN = "Title";
        const string AUTHOR_FN = "Author";
		const string BIBLIOGRAPHICINFORMATION_FN = "BibliographicInformation";
		const string ABSTRACT_FN = "Abstract";


        public LuceneApplication()
        {
            directory = null;
            indexWriter = null;
            indexSearcher = null;
            queryParser = null;

            analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(VERSION); ;

        }
        /// <summary>
        /// Creates the index at indexPath
        /// </summary>
        /// <param name="indexPath">Directory path to create the index</param>
        public void CreateIndexFrom(string indexPath)
        {

            IndexWriter.MaxFieldLength mfl = new IndexWriter.MaxFieldLength(IndexWriter.DEFAULT_MAX_FIELD_LENGTH);
            directory = Lucene.Net.Store.FSDirectory.Open(indexPath);
            indexWriter = new Lucene.Net.Index.IndexWriter(directory, analyzer, true, mfl);

        }

		public void ReadTextFilesAndIndexTextFrom(string collectionPath)
		{
			
			try
			{
				foreach (string file in System.IO.Directory.EnumerateFiles(collectionPath))
				{
					string acadamicPublicationFile = File.ReadAllText(file);
					indexWriter.AddDocument(CreateDocWith(acadamicPublicationFile));
				}

			}
			catch (Exception e) {
				Console.WriteLine(e);
			}

		}

		public Lucene.Net.Documents.Document CreateDocWith(string fileContent) {
			Lucene.Net.Documents.Document doc = new Lucene.Net.Documents.Document();

			string[] tags = { ".I", "\n.T\n", "\n.A\n", "\n.B\n", "\n.W\n" };
			string[] splitedContentWithTags = fileContent.Split(tags, StringSplitOptions.None);

			// edit indexing method here

			doc.Add(new Field(DOCID_FN, splitedContentWithTags[0], Field.Store.NO, Field.Index.NO));
			doc.Add(new Field(TITLE_FN, splitedContentWithTags[1], Field.Store.YES, Field.Index.ANALYZED));
			doc.Add(new Field(AUTHOR_FN, splitedContentWithTags[2], Field.Store.YES, Field.Index.ANALYZED));
			doc.Add(new Field(BIBLIOGRAPHICINFORMATION_FN, 
			                  splitedContentWithTags[4].Replace(splitedContentWithTags[1] + "\n", ""), // remove title from abstract
			                  Field.Store.YES, Field.Index.NOT_ANALYZED));
			doc.Add(new Field(ABSTRACT_FN, splitedContentWithTags[4], Field.Store.YES, Field.Index.ANALYZED));
			return doc;
		}

        /// <summary>
        /// Flushes the buffer and closes the index
        /// </summary>
        public void CleanUpIndexer()
        {
            indexWriter.Optimize();
            indexWriter.Flush(true, true, true);
            indexWriter.Dispose();
        }

		public string ExpandQuery()
		{
			string result = "";


			return result;
		}

        /// <summary>
        /// Creates objects to start up the search
        /// </summary>
        public void SetupSearch()
        {
            indexSearcher = new IndexSearcher(directory);
//            parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, TITLE_FN, analyzer);
//            parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, AUTHOR_FN, analyzer); // activiy 6
//            parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, PUBLISHER_FN, analyzer);  // activity 6

            queryParser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, new[] { AUTHOR_FN, TITLE_FN }, analyzer); // activity 8
        }

        /// <summary>
        /// Cleans up afer a search
        /// </summary>
        public void CleanupSearch()
        {
            indexSearcher.Dispose();
        }


		public void SearchFor(string querytext)
        {
            querytext = querytext.ToLower();
            Query query = queryParser.Parse(querytext);
            topdocs = indexSearcher.Search(query, 100);
			System.Console.WriteLine("Found " + results.TotalHits + " documents.");
        }

		public AcadamicPublication GetAcadamicPublicationAt(int rank)
		{
			
			ScoreDoc[] results = topdocs.ScoreDocs;

			AcadamicPublication newAP = new AcadamicPublication();
			Lucene.Net.Documents.Document doc = indexSearcher.Doc(results[i].Doc);
			newAP.DocID = doc.Get(DOCID_FN);
			newAP.Title = doc.Get(TITLE_FN);
			newAP.Author = doc.Get(AUTHOR_FN);
			newAP.BibliographicInformation = doc.Get(BIBLIOGRAPHICINFORMATION_FN);
			newAP.Abstract = doc.Get(ABSTRACT_FN);

			return newAP;
		}
    }
}