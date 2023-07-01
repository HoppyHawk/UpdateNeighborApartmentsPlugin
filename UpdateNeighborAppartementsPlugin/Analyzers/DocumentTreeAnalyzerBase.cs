using System;
using System.Collections.Generic;
using System.Linq;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;

namespace UpdateNeighborAppartementsPlugin.Analyzers
{
    public abstract class DocumentTreeAnalyzerBase: IDocumentTreeAnalyzer
    {
        public abstract List<DocumentTreeNode> Analyze(IEnumerable<DocumentTreeNode> nodes);
        
        protected IEnumerable<IEnumerable<DocumentTreeNode>> CollectChildNodes(string parentNodeType, IEnumerable<DocumentTreeNode> nodes)
        {
            foreach (DocumentTreeNode node in nodes)
            {
                if (node.NodeType == parentNodeType)
                    yield return node.Children;
                else
                {
                    foreach (var child in CollectChildNodes(parentNodeType, node.Children))
                        yield return child;
                }

            }
        }
    }
}
