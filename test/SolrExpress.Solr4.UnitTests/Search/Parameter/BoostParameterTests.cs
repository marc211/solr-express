﻿using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Search.ParameterValue;
using SolrExpress.Core.Utility;
using SolrExpress.Solr4.Search.Parameter;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class BoostParameterTests
    {
        /// <summary>
        /// Where   Using a BoostParameter instance
        /// When    Invoking the method "Execute" using BF function
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void BoostParameter001()
        {
            // Arrange
            var container = new List<string>();
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new BoostParameter<TestDocument>(expressionBuilder);
            parameter.Configure(new Any<TestDocument>("id"), BoostFunctionType.Bf);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("bf=id", container[0]);
        }

        /// <summary>
        /// Where   Using a BoostParameter instance
        /// When    Invoking the method "Execute" using Boost function
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void BoostParameter002()
        {
            // Arrange
            var container = new List<string>();
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new BoostParameter<TestDocument>(expressionBuilder);
            parameter.Configure(new Any<TestDocument>("id"), BoostFunctionType.Boost);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("boost=id", container[0]);
        }
    }
}
