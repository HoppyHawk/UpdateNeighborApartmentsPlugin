using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using UpdateNeighborAppartementsPlugin.Analyzers;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;
using UpdateNeighborAppartementsPlugin.Model;

namespace UpdateNeighborAppartementsPlugin.Tests.Analyzers
{
    [TestClass]
    public class FirstNeighborAppartementFilterTests
    {
        [TestMethod]
        public void Apply_WhenCombinationsContainPairs_SelectsLastApartmentFromEachPair()
        {
            int expectedApartmentCount = 2;

            var combinations = TestCombinationsGenerator.GeneratePairsCombinations();

            var combinationsFilter = new FirstNeighborAppartementFilter();

            var result = combinationsFilter.Apply(combinations).ToList();

            Assert.AreEqual(expectedApartmentCount, result.Count());
            Assert.AreEqual("Квартира 02", result[0].DisplayName);
            Assert.AreEqual("Квартира 05", result[1].DisplayName);
        }

        [TestMethod]
        public void Apply_WhenCombinationsContainSingleIntersection_SelectsEvenApartmentsFromLagestCombination()
        {
            int expectedApartmentCount = 1;

            var combinations = TestCombinationsGenerator.GenerateCombinationsWithOneIntersection();

            var combinationsFilter = new FirstNeighborAppartementFilter();

            var result = combinationsFilter.Apply(combinations).ToList();

            Assert.AreEqual(expectedApartmentCount, result.Count());
            Assert.AreEqual("Квартира 02", result[0].DisplayName);
        }

        [TestMethod]
        public void Apply_WhenCombinationsContainMultipleIntersections_SelectsEvenApartmentsFromSuperset()
        {
            int expectedApartmentCount = 2;

            var combinations = TestCombinationsGenerator.GenerateCombinationsWithTwoIntersections();

            var combinationsFilter = new FirstNeighborAppartementFilter();

            var result = combinationsFilter.Apply(combinations).ToList();

            Assert.AreEqual(expectedApartmentCount, result.Count);
            Assert.AreEqual("Квартира 02", result[0].DisplayName);
            Assert.AreEqual("Квартира 04", result[1].DisplayName);
        }
    }

    class TestCombinationsGenerator
    {

        private const string levelName = "Этаж 03";
        private const string sectionName = "Секция 01";

        public static IEnumerable<IEnumerable<DocumentTreeNode>> GeneratePairsCombinations()
        {
            var combinations = new List<List<ApartmentNode>>();

            var apartment1 = new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 01",
                NodeType = ParameterKeys.ROM_ZONE
            };

            var apartment2 = new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 02",
                NodeType = ParameterKeys.ROM_ZONE
            };

            var apartment4 = new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 04",
                NodeType = ParameterKeys.ROM_ZONE
            };

            var apartment5 = new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 05",
                NodeType = ParameterKeys.ROM_ZONE
            };


            combinations.Add(new List<ApartmentNode>() { apartment1, apartment2 });
            combinations.Add(new List<ApartmentNode>() { apartment2, apartment1 });
            combinations.Add(new List<ApartmentNode>() { apartment4, apartment5 });
            combinations.Add(new List<ApartmentNode>() { apartment5, apartment4 });

            return combinations;
        }

        public static IEnumerable<IEnumerable<DocumentTreeNode>> GenerateCombinationsWithOneIntersection()
        {
            var combinations = new List<List<ApartmentNode>>();

            var apartment1 = new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 01",
                NodeType = ParameterKeys.ROM_ZONE
            };

            var apartment2 = new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 02",
                NodeType = ParameterKeys.ROM_ZONE
            };

            var apartment3 = new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 03",
                NodeType = ParameterKeys.ROM_ZONE
            };

            combinations.Add(new List<ApartmentNode>() { apartment1, apartment2 });
            combinations.Add(new List<ApartmentNode>() { apartment1, apartment2, apartment3 });
            combinations.Add(new List<ApartmentNode>() { apartment2, apartment3 });

            return combinations;
        }

        public static IEnumerable<IEnumerable<DocumentTreeNode>> GenerateCombinationsWithTwoIntersections()
        {
            var combinations = new List<List<ApartmentNode>>();

            var apartment1 = new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 01",
                NodeType = ParameterKeys.ROM_ZONE
            };

            var apartment2 = new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 02",
                NodeType = ParameterKeys.ROM_ZONE
            };

            var apartment3 = new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 03",
                NodeType = ParameterKeys.ROM_ZONE
            };

            var apartment4 = new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 04",
                NodeType = ParameterKeys.ROM_ZONE
            };

            combinations.Add(new List<ApartmentNode>() { apartment1, apartment2 });
            combinations.Add(new List<ApartmentNode>() { apartment1, apartment2, apartment3 });
            combinations.Add(new List<ApartmentNode>() { apartment2, apartment3, apartment4 });
            combinations.Add(new List<ApartmentNode>() { apartment3, apartment4 });

            return combinations;
        }
    }
}
