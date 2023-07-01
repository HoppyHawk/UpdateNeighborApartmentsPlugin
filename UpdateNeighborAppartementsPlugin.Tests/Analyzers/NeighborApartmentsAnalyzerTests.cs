using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using UpdateNeighborAppartementsPlugin.Analyzers;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;
using UpdateNeighborAppartementsPlugin.Model;

namespace UpdateNeighborAppartementsPlugin.Tests.Analyzers
{
    [TestClass]
    public class NeighborApartmentsAnalyzerTests
    {
        [TestMethod]
        public void Analyze_DistinctNodeFilter_FindsAllNeighborApartmentsFromDocumentTree()
        {
            int expectedNeighborCount = 4;

            var nodes = new TestDocumentTreeBuilder().MakeDocumentTree();
            var analyzer = new NeighborApartmentsOfSameTypeAnalyzer();
            var filter = new DistinctNodeFilter();
            var result = analyzer.Analyze(nodes, filter);
            Assert.IsNotNull(result);

            Assert.AreEqual(expectedNeighborCount, result.Count);
        }

        [TestMethod]
        public void Analyze_FirstNeighborFilter_FindsAllFirstNeighborApartmentsFromDocumentTree()
        {
            int expectedNeighborCount = 2;

            var nodes = new TestDocumentTreeBuilder().MakeDocumentTree();
            var analyzer = new NeighborApartmentsOfSameTypeAnalyzer();
            var filter = new FirstNeighborAppartementFilter();
            var result = analyzer.Analyze(nodes, filter);
            Assert.IsNotNull(result);

            Assert.AreEqual(expectedNeighborCount, result.Count);
        }
    }

    class TestDocumentTreeBuilder {
        string levelName = "Этаж 03";
        string sectionName = "Секция 01";

        public List<DocumentTreeNode> MakeDocumentTree() {

            List<DocumentTreeNode> nodes = new List<DocumentTreeNode>();

            DocumentTreeNode sectionNode = new DocumentTreeNode() { 
                DisplayName = sectionName,
                NodeType = ParameterKeys.BS_Block,
                Children = new List<DocumentTreeNode>() { 
                    new DocumentTreeNode()
                    {
                        DisplayName = "1k",
                        NodeType = ParameterKeys.ROM_SubZone,
                        Children = Make1kApartments()
                    },
                    new DocumentTreeNode()
                    {
                        DisplayName = "2k",
                        NodeType = ParameterKeys.ROM_SubZone,
                        Children = Make2kApartments()
                    },
                    new DocumentTreeNode()
                    {
                        DisplayName = "3k",
                        NodeType = ParameterKeys.ROM_SubZone,
                        Children = Make3kApartments()
                    }
                }
            };

            DocumentTreeNode levelNode = new DocumentTreeNode() { 
                DisplayName = levelName,
                NodeType = ParameterKeys.Level,
                Children = new List<DocumentTreeNode>() { sectionNode }
            };

            nodes.Add(levelNode);


            return nodes;
        }

        private List<DocumentTreeNode> Make1kApartments() {
            var apartments = new List<DocumentTreeNode>();
            apartments.Add(new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 01",
                NodeType = ParameterKeys.ROM_ZONE
            });
            apartments.Add(new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 03",
                NodeType = ParameterKeys.ROM_ZONE
            });
            apartments.Add(new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 08",
                NodeType = ParameterKeys.ROM_ZONE
            });
            return apartments;
        }

        private List<DocumentTreeNode> Make2kApartments()
        {
            var apartments = new List<DocumentTreeNode>();
            apartments.Add(new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 02",
                NodeType = ParameterKeys.ROM_ZONE
            });
            apartments.Add(new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 04",
                NodeType = ParameterKeys.ROM_ZONE
            });
            apartments.Add(new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 06",
                NodeType = ParameterKeys.ROM_ZONE
            });
            apartments.Add(new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 07",
                NodeType = ParameterKeys.ROM_ZONE
            });
            apartments.Add(new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 09",
                NodeType = ParameterKeys.ROM_ZONE
            });
            apartments.Add(new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "2k",
                DisplayName = "Квартира 10",
                NodeType = ParameterKeys.ROM_ZONE
            });

            return apartments;
        }

        private List<DocumentTreeNode> Make3kApartments() {
            var apartments = new List<DocumentTreeNode>();
            apartments.Add(new ApartmentNode()
            {
                Level = levelName,
                SectionName = sectionName,
                ApartmentTypeName = "3k",
                DisplayName = "Квартира 05",
                NodeType = ParameterKeys.ROM_ZONE
            });
            return apartments;
        }
    }
}
