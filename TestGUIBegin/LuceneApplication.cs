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


namespace LuceneApplicationForIFN647Project
{
    class LuceneApplication
    {

        Lucene.Net.Store.Directory directory;
        Lucene.Net.Analysis.Analyzer analyzer;
		Lucene.Net.Index.IndexWriter indexWriter;
		IndexSearcher indexSearcher;
		QueryParser queryParser;

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;
        const string TITLE_FN = "Title";
        const string AUTHOR_FN = "Author";
        const string PUBLISHER_FN = "Publisher";

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
        public void CreateIndex(string indexPath)
        {

            IndexWriter.MaxFieldLength mfl = new IndexWriter.MaxFieldLength(IndexWriter.DEFAULT_MAX_FIELD_LENGTH);
            directory = Lucene.Net.Store.FSDirectory.Open(indexPath);
            indexWriter = new Lucene.Net.Index.IndexWriter(directory, analyzer, true, mfl);

        }

        // Activity 4
        /// <summary>
        /// Indexes information relating to books
        /// </summary>
        /// <param name="author">The Book's author</param>
        /// <param name="title">The Book's title</param>
        /// <param name="publisher">The Book's publisher</param>
        public void IndexBook(string author, string title, string publisher){
            Lucene.Net.Documents.Field authorField = new Field(AUTHOR_FN, author, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
            Lucene.Net.Documents.Field titleField = new Field(TITLE_FN, title, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
//            Lucene.Net.Documents.Field publisherField = new Field(PUBLISHER_FN, publisher, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
            Lucene.Net.Documents.Field publisherField = new Field(PUBLISHER_FN, publisher, Field.Store.NO, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
            Lucene.Net.Documents.Document doc = new Document();
            authorField.Boost = 2; // activity 9
            doc.Add(authorField);
            doc.Add(titleField);
            doc.Add(publisherField);
            indexWriter.AddDocument(doc);
        }

        // Activity 10
        /// <summary>
        /// Indexes information relating to books
        /// </summary>
        /// <param name="book">The book</param>
        public void IndexBook(Book book)
        {

            // TODO: Enter code to index text
            Lucene.Net.Documents.Field authorField = new Field(AUTHOR_FN, book.Author, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
            Lucene.Net.Documents.Field textField = new Field(TITLE_FN, book.Title, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
            Lucene.Net.Documents.Field publisherField = new Field(PUBLISHER_FN, book.Publisher, Field.Store.NO, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
            Lucene.Net.Documents.Document doc = new Document();
            authorField.Boost = 2; // activity 9
            doc.Add(authorField);
            doc.Add(textField);
            doc.Add(publisherField);
            indexWriter.AddDocument(doc);
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


        public void SearchAndDisplayResults(string querytext)
        {
            querytext = querytext.ToLower();
            Query query = queryParser.Parse(querytext);
            TopDocs results = indexSearcher.Search(query, 100);
           
            System.Console.WriteLine("Found " + results.TotalHits + " documents.");


            int rank = 0;
            foreach (ScoreDoc scoreDoc in results.ScoreDocs)
            {
                rank++;
                Lucene.Net.Documents.Document doc = indexSearcher.Doc(scoreDoc.Doc);
                string titleValue = doc.Get(TITLE_FN).ToString();
                string authorValue = doc.Get(AUTHOR_FN).ToString(); // activity 5
//                string publisherValue = doc.Get(PUBLISHER_FN).ToString(); // activity 5, 7
//                Console.WriteLine("Rank " + rank + " title " + titleValue);
//                Console.WriteLine("Rank " + rank + " title " + titleValue + " author " + authorValue + " Publisher " + publisherValue); // Activity 5
                Console.WriteLine("Rank " + rank + " title " + titleValue + " author " + authorValue); // Activity 7

            }
        }
    }
}