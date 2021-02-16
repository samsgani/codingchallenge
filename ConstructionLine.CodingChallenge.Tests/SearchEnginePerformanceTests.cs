﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConstructionLine.CodingChallenge.Tests.SampleData;
using Moq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEnginePerformanceTests : SearchEngineTestsBase
    {
        private List<Shirt> _shirts;
        private SearchEngine _searchEngine;

        [SetUp]
        public void Setup()
        {
            var dataBuilder = new SampleDataBuilder(50000);
            _shirts = dataBuilder.CreateShirts();
            Mock<IShirtBuilder> mockShirtBuilder = new Mock<IShirtBuilder>();
            mockShirtBuilder.Setup(S => S.CreateShirts()).Returns(_shirts);
            _searchEngine = new SearchEngine(mockShirtBuilder.Object);
        }


        [Test]
        public void PerformanceTest()
        {
            var sw = new Stopwatch();
            sw.Start();

            var options = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            };

            var results = _searchEngine.Search(options);

            sw.Stop();
            Console.WriteLine($"Test fixture finished in {sw.ElapsedMilliseconds} milliseconds");

            AssertResults(results.Shirts, options);
            AssertSizeCounts(_shirts, options, results.SizeCounts);
            AssertColorCounts(_shirts, options, results.ColorCounts);
        }
    }
}
