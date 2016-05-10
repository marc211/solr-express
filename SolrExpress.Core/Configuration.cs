﻿namespace SolrExpress.Core
{
    /// <summary>
    /// Configurations to control SOLR Query behavior
    /// </summary>
    public sealed class Configuration
    {
        public Configuration()
        {
            this.FailFast = true;
            this.CheckAnyParameter = true;
        }

        /// <summary>
        /// If true, check for possibles fails in the use of the Solr Queriable (using SolrFieldAttribute), otherwise false. Default is true
        /// </summary>
        public bool FailFast { get; set; }

        /// <summary>
        /// If true, check for possibles misstakes in use of IANyParameter
        /// </summary>
        public bool CheckAnyParameter { get; set; }
    }
}
