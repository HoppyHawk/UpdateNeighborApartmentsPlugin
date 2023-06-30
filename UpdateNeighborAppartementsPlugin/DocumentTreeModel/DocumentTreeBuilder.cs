using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.GroupRules;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;
using UpdateNeighborAppartementsPlugin.Model;

namespace UpdateNeighborAppartementsPlugin.DocumentTreeModel
{
    public interface IDocumentTreeBuilder {
        IEnumerable<DocumentTreeNode> Build(IEnumerable<Element> elements);
    }

    public class DocumentTreeBuilder: IDocumentTreeBuilder
    {
        private GroupElementsRule rootGroupingRule;
        private GroupElementsRule currentGroupingRule;

        public void AddGroupingRule(GroupElementsRule groupingRule)
        {
            if (rootGroupingRule == null)
            {
                rootGroupingRule = groupingRule;
                currentGroupingRule = groupingRule;
            }
            else
            {
                currentGroupingRule.NextLevelGroupingRule = groupingRule;
                currentGroupingRule = currentGroupingRule.NextLevelGroupingRule;
            }
        }

        public IEnumerable<DocumentTreeNode> Build(IEnumerable<Element> elements)
        {
            if (rootGroupingRule == null)
            {
                return BuildPlainDocumentModel(elements);
            }
            return rootGroupingRule.Apply(elements);
        }

        private IEnumerable<DocumentTreeNode> BuildPlainDocumentModel(IEnumerable<Element> elements)
        {
            return new List<DocumentTreeNode>() {
                    new DocumentTreeNode() { NodeType = "Root", DisplayName = "Root", Elements = elements }
                };
        }
    }

    public class DefaultDocumentTreeBuilder : DocumentTreeBuilder {
        public DefaultDocumentTreeBuilder() : base()
        {
            BuildDefaultGroupingChain();   
        }

        private void BuildDefaultGroupingChain() {
            AddGroupingRule(new ParameterAsValueStringRule(ParameterKeys.Level));
            AddGroupingRule(new ParameterAsStringRule(ParameterKeys.BS_Block));
            AddGroupingRule(new ParameterAsStringRule(ParameterKeys.ROM_SubZone));
            AddGroupingRule(new TransformToApartmentNodeRule());
        }
    }
}
