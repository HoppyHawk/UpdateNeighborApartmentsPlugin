using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;
using UpdateNeighborAppartementsPlugin.Model;

namespace UpdateNeighborAppartementsPlugin.DocumentTreeModel.GroupRules
{
    public class TransformToApartmentNodeRule : GroupElementsRule
    {
        public override IEnumerable<DocumentTreeNode> Apply(IEnumerable<Element> elements)
        {
            var nodes = elements.GroupBy(
                    e => new
                    {
                        Level = e.LookupParameter(ParameterKeys.Level).AsValueString(),
                        Section = e.LookupParameter(ParameterKeys.BS_Block).AsString(),
                        ApartmentType = e.LookupParameter(ParameterKeys.ROM_SubZone).AsString(),
                        ApartmentName = e.LookupParameter(ParameterKeys.ROM_ZONE).AsString()
                    },
                    e => e,
                    (key, value) => new ApartmentNode
                    {
                        NodeType = ParameterKeys.ROM_ZONE,
                        Level = key.Level,
                        SectionName = key.Section,
                        ApartmentTypeName = key.ApartmentType,
                        DisplayName = key.ApartmentName,
                        Elements = value,
                        Children = NextLevelGroupingRule != null ? NextLevelGroupingRule.Apply(value) : null
                    }
                )
                .OrderBy(a => a.ApartmentNumber);

            return nodes;
        }
    }
}
