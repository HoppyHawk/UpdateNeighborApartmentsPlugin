using System;
using System.Collections.Generic;
using System.Linq;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;

namespace UpdateNeighborAppartementsPlugin.Analyzers
{
    public interface INodeCombinationsFilter
    {
        IEnumerable<DocumentTreeNode> Apply(IEnumerable<IEnumerable<DocumentTreeNode>> nodeCombinations);
    }
}
