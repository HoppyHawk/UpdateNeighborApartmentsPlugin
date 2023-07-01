using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes
{
    public class DocumentTreeNode
    {
        public string NodeType { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<DocumentTreeNode> Children { get; set; }
        public IEnumerable<Element> Elements { get; set; }
        public bool RequiresProcessing { get; set; }
    }
}
