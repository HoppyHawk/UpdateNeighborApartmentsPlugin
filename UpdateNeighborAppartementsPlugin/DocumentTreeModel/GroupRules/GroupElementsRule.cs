using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;

namespace UpdateNeighborAppartementsPlugin.DocumentTreeModel.GroupRules
{
    public abstract class GroupElementsRule
    {
        public GroupElementsRule NextLevelGroupingRule { get; set; }

        public abstract IEnumerable<DocumentTreeNode> Apply(IEnumerable<Element> elements);
    }
}
