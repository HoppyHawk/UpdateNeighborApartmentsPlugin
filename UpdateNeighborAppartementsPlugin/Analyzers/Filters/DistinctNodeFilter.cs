using System;
using System.Collections.Generic;
using System.Linq;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;

namespace UpdateNeighborAppartementsPlugin.Analyzers
{
    public class DistinctNodeFilter : INodeCombinationsFilter
    {
        public IEnumerable<DocumentTreeNode> Apply(IEnumerable<IEnumerable<DocumentTreeNode>> nodeCombinations)
        {
            return nodeCombinations.SelectMany(c => c.Distinct()).Distinct();
        }
    }
}
