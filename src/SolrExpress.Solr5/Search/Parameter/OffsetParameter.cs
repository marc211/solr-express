﻿using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class OffsetParameter<TDocument> : IOffsetParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        bool ISearchParameter.AllowMultipleInstances { get; set; }

        long IOffsetParameter<TDocument>.Value { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            throw new NotImplementedException();
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            throw new NotImplementedException();
        }
    }
}