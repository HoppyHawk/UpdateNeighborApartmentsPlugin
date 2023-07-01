using System;
using System.Collections.Generic;
using System.Linq;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;

namespace UpdateNeighborAppartementsPlugin.Analyzers.Calculators
{
    public class NeighborApartmentsComparer : IComparer<DocumentTreeNode>
    {
        public int Compare(DocumentTreeNode x, DocumentTreeNode y)
        {
            var apartment1 = x as ApartmentNode;
            var apartment2 = y as ApartmentNode;

            if (apartment1 == null | apartment2 == null)
                return -1;

            return AreNeighbors(apartment1, apartment2) ? 0 : -1;
        }

        private bool AreNeighbors(ApartmentNode apartment1, ApartmentNode apartment2)
        {
            return apartment1.Level == apartment2.Level
                && apartment1.SectionName == apartment2.SectionName
                && apartment1.ApartmentTypeName == apartment2.ApartmentTypeName
                && Math.Abs(apartment1.ApartmentNumber - apartment2.ApartmentNumber) <= 1;
        }
    }
}
