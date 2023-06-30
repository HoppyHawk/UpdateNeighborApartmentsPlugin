using System;
using System.Collections.Generic;
using System.Linq;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;
using UpdateNeighborAppartementsPlugin.Model;

namespace UpdateNeighborAppartementsPlugin.Analyzers
{
    public class NeighborApartmentsOfSameTypeAnalyzer : DocumentTreeNodeCollector, IDocumentTreeAnalyzer
    {
        public List<DocumentTreeNode> Analyze(IEnumerable<DocumentTreeNode> nodes, 
            INodeCombinationsFilter combinationsFilter)
        {
            var allNeighboringCombinations = CollectAllNeighboringCombinations(nodes);

            var matchingNodes = new List<DocumentTreeNode>();

            foreach (var neighboringCombinations in allNeighboringCombinations)
            {
                var filteredResults = combinationsFilter.Apply(neighboringCombinations);

                foreach (var node in filteredResults)
                {
                    matchingNodes.Add(node);
                }
            }

            return matchingNodes;
        }

        private IEnumerable<IEnumerable<IEnumerable<ApartmentNode>>> CollectAllNeighboringCombinations(
            IEnumerable<DocumentTreeNode> nodes)
        {
            var apartmentNodesByApartmentType = CollectChildNodes(ParameterKeys.ROM_SubZone, nodes);
            foreach (var group in apartmentNodesByApartmentType)
            {
                var apartmentGroup = group.Select(n => n as ApartmentNode);
                yield return FindNeighboringCombinations(apartmentGroup);
            }
        }

        private IEnumerable<IEnumerable<ApartmentNode>> FindNeighboringCombinations(
            IEnumerable<ApartmentNode> apartments)
        {
            var neighborApartments = apartments.Select(
                   a1 => apartments.Where(a2 => IsNeighboringCombination(a1, a2))
                       .OrderBy(a => a.ApartmentNumber)
               )
               .Where(c => c.Count() > 1);
            return neighborApartments;
        }

        private bool IsNeighboringCombination(ApartmentNode apartment1, ApartmentNode apartment2)
        {
            return Math.Abs(apartment1.ApartmentNumber - apartment2.ApartmentNumber) <= 1;
        }
    }
}
