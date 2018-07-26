using System.Collections.Generic;
using Lucene.Data.Model;
using Lucene.Engine.Converters;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;

namespace Lucene.Engine.Indexers
{
    public class CarIndexer : Indexer
    {
        #region Constructors

        public CarIndexer(Analyzer analyzer) : base(analyzer)
        { }

        #endregion

        #region Methods

        public void BuildIndexes()
        {
            IEnumerable<Car> models = CarRepository.GetAll();

            IEnumerable<Document> documents = CarConverter.ConvertModelsToDocuments(models);
            
            WriteIndex(documents);
        }

        #endregion
    }
}