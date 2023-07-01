using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UpdateNeighborAppartementsPlugin.Analyzers.Calculators;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;

namespace UpdateNeighborAppartementsPlugin.Tests.Analyzers
{
    [TestClass]
    public class NeighborApartmentsComparerTests
    {

        private NeighborApartmentsComparer comparer;

        public NeighborApartmentsComparerTests() {
            comparer = new NeighborApartmentsComparer();
        }

        [TestMethod]
        public void Compare_WhenObjectsAreNotAppartments_ReturnsNotEqual()
        {
            int expectedResult = -1;

            var node1 = new DocumentTreeNode() { DisplayName = "Квартира 01", NodeType = "Квартира" };
            var node2 = new DocumentTreeNode() { DisplayName = "Квартира 02", NodeType = "Квартира" };

            var result = comparer.Compare(node1, node2);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Compare_WhenComparingNeighborApartments_ReturnsEqual() {
            int expectedResult = 0;

            var apartment1 = new ApartmentNode()
            {
                Level = "Этаж 01",
                SectionName = "Секция 01",
                ApartmentTypeName = "2к",
                DisplayName = "Квартира 01"
            };
            var apartment2 = new ApartmentNode()
            {
                Level = "Этаж 01",
                SectionName = "Секция 01",
                ApartmentTypeName = "2к",
                DisplayName = "Квартира 02"
            };

            var result = comparer.Compare(apartment1, apartment2);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Compare_WhenApartmentsAreNotNeighbor_ReturnsNotEqual()
        {
            int expectedResult = -1;

            var apartment1 = new ApartmentNode()
            {
                Level = "Этаж 01",
                SectionName = "Секция 01",
                ApartmentTypeName = "2к",
                DisplayName = "Квартира 01"
            };
            var apartment2 = new ApartmentNode()
            {
                Level = "Этаж 01",
                SectionName = "Секция 01",
                ApartmentTypeName = "2к",
                DisplayName = "Квартира 03"
            };

            var result = comparer.Compare(apartment1, apartment2);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Compare_WhenComparingApartmentsFromDifferentGroups_ReturnsNotEqual()
        {
            int expectedResult = -1;

            var apartment = new ApartmentNode()
            {
                Level = "Этаж 01",
                SectionName = "Секция 01",
                ApartmentTypeName = "2к",
                DisplayName = "Квартира 01"
            };
            var anotherLevelApartment = new ApartmentNode()
            {
                Level = "Этаж 02",
                SectionName = "Секция 01",
                ApartmentTypeName = "2к",
                DisplayName = "Квартира 02"
            };
            var anotherSectionApartment = new ApartmentNode()
            {
                Level = "Этаж 01",
                SectionName = "Секция 02",
                ApartmentTypeName = "2к",
                DisplayName = "Квартира 02"
            };
            var anotherTypeApartment = new ApartmentNode()
            {
                Level = "Этаж 01",
                SectionName = "Секция 01",
                ApartmentTypeName = "3к",
                DisplayName = "Квартира 02"
            };

            var anotherLevelresult = comparer.Compare(apartment, anotherLevelApartment);
            var anotherSectionresult = comparer.Compare(apartment, anotherSectionApartment);
            var anotherTypeResult = comparer.Compare(apartment, anotherTypeApartment);


            Assert.AreEqual(expectedResult, anotherLevelresult);
            Assert.AreEqual(expectedResult, anotherSectionresult);
            Assert.AreEqual(expectedResult, anotherTypeResult);
        }
    }
}
