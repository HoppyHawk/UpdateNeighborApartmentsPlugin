using System;
using System.Collections.Generic;
using System.Linq;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;

namespace UpdateNeighborAppartementsPlugin.Analyzers
{
    public interface IDocumentTreeAnalyzer
    {
        List<DocumentTreeNode> Analyze(IEnumerable<DocumentTreeNode> nodes);
    }
}
