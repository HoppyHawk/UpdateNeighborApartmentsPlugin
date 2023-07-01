using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using UpdateNeighborAppartementsPlugin.Analyzers.Calculators;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;

namespace UpdateNeighborAppartementsPlugin.Tests.Analyzers
{
    [TestClass]
    public class NodeCombinationsCalculatorTests
    {
        private NodeCombinationsCalculator calculator;
        private Mock<IComparer<DocumentTreeNode>> comparerMock;

        public NodeCombinationsCalculatorTests() {
            comparerMock = new Mock<IComparer<DocumentTreeNode>>();
            calculator = new NodeCombinationsCalculator(comparerMock.Object);
        }

        [TestMethod]
        public void Calculate_WhenNodesCollectionIsNull_ReturnsEmptyCollection()
        {
            var combinations = calculator.Calculate(null);

            Assert.IsNotNull(combinations);
            Assert.AreEqual(0, combinations.Count());
        }

        [TestMethod]
        public void Calculate_WhenNodesCollectionIsEmpty_ReturnsEmptyCollection()
        {
            IEnumerable<DocumentTreeNode> nodes = Enumerable.Empty<DocumentTreeNode>();

            var combinations = calculator.Calculate(nodes);

            Assert.IsNotNull(combinations);
            Assert.AreEqual(0, combinations.Count());
        }

        [TestMethod]
        public void Calculate_WhenNodesCollectionContainsElements_ReturnsAllCombinationsThatMatchCriteria() {
            int expectedCombinationCount = 3;

            List<DocumentTreeNode> nodes = GenerateTestNodes();

            comparerMock.Setup(c => c.Compare(It.IsAny<DocumentTreeNode>(), It.IsAny<DocumentTreeNode>()))
                .Returns((DocumentTreeNode n1, DocumentTreeNode n2) => n1.NodeType == n2.NodeType ? 0 : -1);

            var combinations = calculator.Calculate(nodes);

            Assert.IsNotNull(combinations);
            Assert.AreEqual(expectedCombinationCount, combinations.Count());
        }

        private List<DocumentTreeNode> GenerateTestNodes() {
            List<DocumentTreeNode> nodes = new List<DocumentTreeNode>();

            nodes.Add(new DocumentTreeNode() { DisplayName = "Квартира 01", NodeType = "Квартира" });
            nodes.Add(new DocumentTreeNode() { DisplayName = "Квартира 02", NodeType = "Квартира" });
            nodes.Add(new DocumentTreeNode() { DisplayName = "Квартира 03", NodeType = "Квартира" });
            nodes.Add(new DocumentTreeNode() { DisplayName = "Секция 01", NodeType = "Секция" });
            nodes.Add(new DocumentTreeNode() { DisplayName = "Этаж 01", NodeType = "Этаж" });

            return nodes;
        }
    }
}
