﻿using System;
using System.Collections.Generic;
using System.Linq;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;

namespace UpdateNeighborAppartementsPlugin.Analyzers
{
    public class CompositeNodeCombinationsFilter : INodeCombinationsFilter
    {
        private INodeCombinationsFilter distinctNodeFilter;
        private INodeCombinationsFilter firstNeighborFilter;

        public CompositeNodeCombinationsFilter()
        {
            distinctNodeFilter = new DistinctNodeFilter();
            firstNeighborFilter = new FirstNeighborAppartementFilter();
        }

        public IEnumerable<DocumentTreeNode> Apply(IEnumerable<IEnumerable<DocumentTreeNode>> nodeCombinations)
        {
            if (nodeCombinations == null || nodeCombinations.Count() == 0)
                return Enumerable.Empty<DocumentTreeNode>();

            var distinctNodes = distinctNodeFilter.Apply(nodeCombinations).ToList();
            var firstNeighbors = firstNeighborFilter.Apply(nodeCombinations).ToList();
            distinctNodes.ForEach(n => n.RequiresProcessing = firstNeighbors.Contains(n));
            return distinctNodes;
        }
    }
}
