using System;
using System.IO;
using Lucene.Net.Util;

namespace Lucene.Engine
{
    public static class LuceneConfig
    {
        public const LuceneVersion Version = LuceneVersion.LUCENE_48;

        public static readonly string IndexPath = Path.Combine(Environment.CurrentDirectory, "Lucene_index");

        public const int MaximumSearchResults = 50;
    }
}