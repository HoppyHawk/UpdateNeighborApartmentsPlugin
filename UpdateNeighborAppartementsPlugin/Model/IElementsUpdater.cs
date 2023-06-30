using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpdateNeighborAppartementsPlugin.Model
{
    public interface IElementsUpdater
    {
        Task Update(IEnumerable<Element> elements);
    }
}
