﻿using Microsoft.AspNetCore.Mvc;
using Sample.Ui.Models;
using SolrExpress.Core;
using SolrExpress.Core.Extension;
using SolrExpress.Core.Search.ParameterValue;
using SolrExpress.Core.Search.Result;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Ui.Controllers
{
    public class SearchController : Controller
    {
        private IDocumentCollection<TechProduct> _documentCollection;

        public SearchController(IDocumentCollection<TechProduct> documentCollection)
        {
            this._documentCollection = documentCollection;
        }

        private List<FacetKeyValue<string>> GetFacetRangeViewModelList(IEnumerable<FacetKeyValue<FacetRange>> facetRangeList)
        {
            return facetRangeList
                .Select(q =>
                {
                    var item = new FacetKeyValue<string>();
                    item.Name = q.Name;
                    item.Data = q
                        .Data
                        .Select(q2 => new FacetItemValue<string>()
                        {
                            Key = string.Format("{0} - {1}", q2.Key.GetMinimumValue() ?? "?", q2.Key.GetMaximumValue() ?? "?"),
                            Quantity = q2.Quantity
                        })
                        .ToList();

                    return item;
                })
                .ToList();
        }

        [HttpGet("api/search")]
        public object Get(int page, string keyWord)
        {
            IEnumerable<TechProduct> documents;
            IEnumerable<FacetKeyValue<string>> facetFieldList;
            IDictionary<string, long> facetQueryList;
            IEnumerable<FacetKeyValue<FacetRange>> facetRangeList;
            Information statistics;

            const int itemsPerPage = 10;

            this._documentCollection
                .Select()
                .QueryField("name^13~3 manu^8~2 id^5")
                .Query(keyWord ?? "*")
                .Limit(itemsPerPage)
                .Offset(page)
                .FacetField(q => q.Manufacturer)
                .FacetField(q => q.InStock)
                .FacetRange("Price", q => q.Price, "10", "10", "100")
                .FacetRange("Popularity", q => q.Popularity, "1", "1", "10")
                .FacetRange("ManufacturedateIn", q => q.ManufacturedateIn, "+1MONTH", "NOW-10YEARS", "NOW")
                .FacetQuery("StoreIn1000km", new Spatial<TechProduct>(SolrSpatialFunctionType.Geofilt, q => q.StoredAt, new GeoCoordinate(35.0752M, -97.032M), 1000M))
                .Execute()
                .Document(out documents)
                .Information(out statistics)
                .FacetField(out facetFieldList)
                .FacetQuery(out facetQueryList)
                .FacetRange(out facetRangeList);

            var resul = new
            {
                documents,
                facets = new
                {
                    field = facetFieldList,
                    query = facetQueryList,
                    range = this.GetFacetRangeViewModelList(facetRangeList)
                },
                statistic = new
                {
                    statistics.ElapsedTime,
                    statistics.DocumentCount,
                    pageCount = Math.Ceiling((decimal)statistics.DocumentCount / itemsPerPage)
                }
            };

            return resul;
        }
    }
}
