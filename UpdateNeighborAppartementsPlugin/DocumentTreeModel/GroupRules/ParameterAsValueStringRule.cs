using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;

namespace UpdateNeighborAppartementsPlugin.DocumentTreeModel.GroupRules
{
    public class ParameterAsValueStringRule : GroupElementsRule
    {
        protected readonly string parameterName;

        public ParameterAsValueStringRule(string parameterName)
        {
            this.parameterName = parameterName;
        }

        protected virtual string GetParameterValue(Element element)
        {
            return element.LookupParameter(parameterName).AsValueString();
        }

        public override IEnumerable<DocumentTreeNode> Apply(IEnumerable<Element> elements)
        {
            var nodes = elements.GroupBy(
                    e => GetParameterValue(e),
                    e => e,
                    (key, value) => new DocumentTreeNode
                    {
                        NodeType = parameterName,
                        DisplayName = key,
                        Elements = value,
                        Children = NextLevelGroupingRule != null ? NextLevelGroupingRule.Apply(value) : null
                    }
                );
            return nodes;
        }
    }
}
