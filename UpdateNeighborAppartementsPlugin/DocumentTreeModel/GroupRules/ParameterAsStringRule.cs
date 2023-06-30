using Autodesk.Revit.DB;
using System;
using System.Linq;

namespace UpdateNeighborAppartementsPlugin.DocumentTreeModel.GroupRules
{
    public class ParameterAsStringRule : ParameterAsValueStringRule
    {
        public ParameterAsStringRule(string parameterName) : base(parameterName) { }

        protected override string GetParameterValue(Element element)
        {
            return element.LookupParameter(parameterName).AsString();
        }
    }
}
