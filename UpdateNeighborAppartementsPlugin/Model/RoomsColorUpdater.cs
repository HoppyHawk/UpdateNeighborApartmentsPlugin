using Autodesk.Revit.DB;
using Revit.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpdateNeighborAppartementsPlugin.Model
{
    public class RoomsColorUpdater : IElementsUpdater
    {
        private const string colorSuffix = ".Полутон";
        private const string transactionName = "UpdateRoomsColor";
        private readonly Document document;

        public RoomsColorUpdater(Document document)
        {
            this.document = document;
        }

        public async Task Update(IEnumerable<Element> elements)
        {
            var updateElementsColorTask = RevitTask.RunAsync(
                    app => {
                        using (Transaction updateColorTransation = new Transaction(document))
                        {
                            updateColorTransation.Start(transactionName);
                            foreach (var element in elements)
                            {
                                UpdateElementColor(element);
                            }
                            updateColorTransation.Commit();
                        }
                    }
                );

            await updateElementsColorTask;
        }

        private void UpdateElementColor(Element element) {
            string colorParameter = element.LookupParameter(ParameterKeys.ROM_Calculated_Subzone_ID).AsString()
                            + colorSuffix;
            element.LookupParameter(ParameterKeys.ROM_SubZone_Index).Set(colorParameter);
        }

    }
}
