﻿using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class SortParameter<TDocument> : ISortParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        bool ISearchParameter.AllowMultipleInstances { get; set; }

        bool ISortParameter<TDocument>.Ascendent { get; set; }

        Expression<Func<TDocument, object>> ISortParameter<TDocument>.FieldExpression { get; set; }

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