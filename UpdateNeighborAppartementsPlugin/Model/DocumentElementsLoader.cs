using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UpdateNeighborAppartementsPlugin.Model
{
    public class DocumentElementsLoader : IElementsLoader
    {
        private readonly Document document;

        public DocumentElementsLoader(Document document)
        {
            this.document = document;
        }

        public List<Element> LoadElements()
        {
            using (FilteredElementCollector roomFilter = new FilteredElementCollector(document))
            {
                var allRoomElements = roomFilter.OfCategory(BuiltInCategory.OST_Rooms)
                    .WhereElementIsNotElementType()
                    .Where(r => r.LookupParameter(ParameterKeys.ROM_ZONE) != null 
                                    && r.LookupParameter(ParameterKeys.ROM_ZONE).AsString().Contains("Квартира"));
                return allRoomElements.ToList();
            }
        }
    }
}
