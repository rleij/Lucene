using System;
using System.Collections.Generic;
using Lucene.Data.Model;
using Lucene.Net.Documents;

namespace Lucene.Engine.Converters
{
    public static class CarConverter
    {
        public static Document ConvertModelToDocument(Car model)
        {
            // Sanity check
            if(model == null) throw new ArgumentNullException();
            
            return new Document
            {
                new StoredField("Id", model.Id),
                new TextField("Brand", model.Brand, Field.Store.YES),
                new TextField("Type", model.Type, Field.Store.YES),
                new TextField("Color", model.Color, Field.Store.YES)
            };
        }

        public static IEnumerable<Document> ConvertModelsToDocuments(IEnumerable<Car> models)
        {
            foreach (var model in models)
            {
                yield return ConvertModelToDocument(model);
            }
        }
    }
}