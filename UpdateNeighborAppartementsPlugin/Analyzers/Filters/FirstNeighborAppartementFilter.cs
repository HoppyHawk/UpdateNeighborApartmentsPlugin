using System;
using System.Collections.Generic;
using System.Linq;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;

namespace UpdateNeighborAppartementsPlugin.Analyzers
{
    public class FirstNeighborAppartementFilter : INodeCombinationsFilter
    {
        public IEnumerable<DocumentTreeNode> Apply(IEnumerable<IEnumerable<DocumentTreeNode>> nodeCombinations)
        {
            if (nodeCombinations.Count() == 0)
                return Enumerable.Empty<DocumentTreeNode>();


            var supersets = nodeCombinations
                .Select(nc1 => nodeCombinations.Where(nc2 => nc2.Intersect(nc1).Count() > 0)
                                                .Select(nc2 => nc2.Union(nc1))
                                                .OrderBy(nc => nc.Count()).Last()).ToList();

            var result = supersets
                .SelectMany(s => s.OrderBy(n => n.DisplayName).Where((n, i) => i % 2 != 0))
                .Distinct();

            return result;
        }
    }
}
