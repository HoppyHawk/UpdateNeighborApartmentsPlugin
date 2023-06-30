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

            var maxNodeCount = nodeCombinations.Select(nc => nc.Count()).Max();

            if (maxNodeCount == 2)
                return nodeCombinations.Select(nc => nc.First()).Distinct();

            var longestCombinations = nodeCombinations
                    .Where(nc => nc.Count() == maxNodeCount);

            var notIntersectedCombinations = nodeCombinations
                .Where(nc1 => longestCombinations.Select(nc2 => nc2.Intersect(nc1)).SelectMany(nc => nc).Count() == 0)
                .Concat(longestCombinations);

            var result = notIntersectedCombinations
                .Select(nc => nc.Count() == 2
                    ? nc.First()
                    : nc.Where((n, i) => i % 2 != 0).First()).Distinct();

            return result;
        }
    }
}
