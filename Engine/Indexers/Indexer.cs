using System.Collections.Generic;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace Lucene.Engine.Indexers
{
    public abstract class Indexer
    {
        #region Fields

        private readonly Analyzer analyzer;

        #endregion

        #region Properties
        
        public bool HasIndexes => System.IO.Directory.Exists(LuceneConfig.IndexPath);

        #endregion

        #region Constructors

        protected Indexer(Analyzer analyzer)
        {
            this.analyzer = analyzer;
        }

        #endregion

        #region Methods

        protected void WriteIndex(Document document)
        {
            using (var indexDir = FSDirectory.Open(new System.IO.DirectoryInfo(LuceneConfig.IndexPath)))
            {
                var config = new IndexWriterConfig(LuceneConfig.Version, analyzer);

                using (var indexWriter = new IndexWriter(indexDir, config))
                {
                    indexWriter.Commit();
                    indexWriter.AddDocument(document);
                    
                    indexWriter.Flush(true, true);
                    indexWriter.Commit();
                }
            }
        }
        
        protected void WriteIndex(IEnumerable<Document> documents)
        {
            using (var indexDir = FSDirectory.Open(new System.IO.DirectoryInfo(LuceneConfig.IndexPath)))
            {
                var config = new IndexWriterConfig(LuceneConfig.Version, analyzer);
                
                using (var indexWriter = new IndexWriter(indexDir, config))
                {
                    foreach (var document in documents)
                    {
                        indexWriter.Commit();
                        indexWriter.AddDocument(document);
                    }
                    
                    indexWriter.Flush(true, true);
                    indexWriter.Commit();
                }
            }
        }

        public void DeleteIndexes()
        {
            if (HasIndexes)
            {
                System.IO.Directory.Delete(LuceneConfig.IndexPath, true);
            }
        }

        #endregion
    }
}