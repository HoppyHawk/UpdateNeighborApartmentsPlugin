using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UpdateNeighborAppartementsPlugin.Model
{
    public interface IElementsLoader
    {
        List<Element> LoadElements();
    }
}
