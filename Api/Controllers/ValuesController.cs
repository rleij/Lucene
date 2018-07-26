using System.Collections;
using System.Collections.Generic;
using Lucene.Engine;
using Lucene.Engine.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lucene.Api.Controllers
{
    [Route("[controller]")]
    public class SearchController : Controller
    {
        [HttpGet("{query}")]
        public IEnumerable<SearchEntry> Get(string query)
        {
            IEnumerable<SearchEntry> results = new List<SearchEntry>();
            
            using (var lucene = new LuceneSearch())
            {
                results = lucene.Search(query);
            }
            
            return results;
        }

        [HttpGet("refresh")]
        public bool Refresh()
        {
            using (var lucene = new LuceneSearch())
            {
                lucene.RefreshIndexes();
            }

            return true;
        }
    }
}