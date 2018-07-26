using System;
using System.Collections.Generic;
using Lucene.Engine.Indexers;
using Lucene.Engine.Models;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;

namespace Lucene.Engine
{
    public class LuceneSearch : IDisposable
    {
        #region Fields

        private FSDirectory indexDirectory;
        private IndexSearcher indexSearcher;
        private IndexReader indexReader;
        private QueryParser queryParser;

        #endregion

        #region Methods

        public void RefreshIndexes()
        {
            var carIndexer = new CarIndexer(new StandardAnalyzer(LuceneVersion.LUCENE_48, StandardAnalyzer.STOP_WORDS_SET));
            
            carIndexer.DeleteIndexes();
            carIndexer.BuildIndexes();
        }
        
        public IEnumerable<SearchEntry> Search(string searchQuery)
        {
            indexDirectory = FSDirectory.Open(new System.IO.DirectoryInfo(LuceneConfig.IndexPath));
            indexReader = DirectoryReader.Open(indexDirectory);
            indexSearcher = new IndexSearcher(indexReader);
            
            queryParser = new MultiFieldQueryParser (LuceneConfig.Version, new []{"Id", "Brand", "Type", "Color"}, new StandardAnalyzer(LuceneConfig.Version));
            
            var results = new List<SearchEntry>();
            var query = queryParser.Parse(searchQuery);

            var searchHits = indexSearcher.Search(query, null, LuceneConfig.MaximumSearchResults);

            foreach (var hit in searchHits.ScoreDocs)
            {
                var doc = indexSearcher.Doc(hit.Doc);
                
                results.Add(new SearchEntry
                {
                    Id = doc.GetField("Id").GetInt32ValueOrDefault(),
                    Brand = doc.Get("Brand"),
                    Type = doc.Get("Type"),
                    Color = doc.Get("Color")
                });
            }

            return results;
        }
        
        public void Dispose()
        {
            indexReader?.Dispose();
            indexDirectory?.Dispose();
        }

        #endregion
    }
}