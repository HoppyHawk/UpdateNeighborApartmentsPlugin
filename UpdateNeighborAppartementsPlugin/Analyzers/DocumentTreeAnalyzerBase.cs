using System;
using System.Collections.Generic;
using System.Linq;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;

namespace UpdateNeighborAppartementsPlugin.Analyzers
{
    public abstract class DocumentTreeAnalyzerBase: IDocumentTreeAnalyzer
    {
        public abstract List<DocumentTreeNode> Analyze(IEnumerable<DocumentTreeNode> nodes);

        protected IEnumerable<DocumentTreeNode> CollectNodes(string targetNodeType,  IEnumerable<DocumentTreeNode> nodes)
        {
            foreach (DocumentTreeNode node in nodes)
            {
                if (node.NodeType == targetNodeType)
                    yield return node;
                else
                {
                    foreach (var child in CollectNodes(targetNodeType, node.Children))
                        yield return child;
                }

            }
        }
    }
}
