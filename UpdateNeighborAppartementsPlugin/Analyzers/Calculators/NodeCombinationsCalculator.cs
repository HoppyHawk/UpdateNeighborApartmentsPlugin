using System;
using System.Collections.Generic;
using System.Linq;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;

namespace UpdateNeighborAppartementsPlugin.Analyzers.Calculators
{
    public class NodeCombinationsCalculator
    {

        private readonly IComparer<DocumentTreeNode> nodeComparer;

        public NodeCombinationsCalculator(IComparer<DocumentTreeNode> nodeComparer)
        {
            this.nodeComparer = nodeComparer;
        }

        public IEnumerable<IEnumerable<DocumentTreeNode>> Calculate(IEnumerable<DocumentTreeNode> nodes)
        {
            if (nodes == null || nodes.Count() == 0)
                return Enumerable.Empty<IEnumerable<DocumentTreeNode>>();

            var nodeCombinations = nodes
                .Select(n1 => nodes.Where(n2 => nodeComparer.Compare(n1, n2) == 0))
               .Where(c => c.Count() > 1);

            return nodeCombinations;
        }
    }
}
