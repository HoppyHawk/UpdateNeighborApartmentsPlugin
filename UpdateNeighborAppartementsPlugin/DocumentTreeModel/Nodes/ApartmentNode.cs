using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes
{
    public class ApartmentNode: DocumentTreeNode, IEquatable<ApartmentNode>
    {
        private int apartmentNumber = -1;

        public string Level { get; set; }
        public string SectionName { get; set; }
        public string ApartmentTypeName { get; set; }
        public bool IsSelected { get; set; }

        public int ApartmentNumber => GetApartmentNumber();

        public override bool Equals(object obj)
        {
            return Equals(obj as ApartmentNode);
        }

        public bool Equals(ApartmentNode other)
        {
            return !(other is null) &&
                   NodeType == other.NodeType &&
                   DisplayName == other.DisplayName &&
                   Level == other.Level &&
                   SectionName == other.SectionName &&
                   ApartmentTypeName == other.ApartmentTypeName &&
                   ApartmentNumber == other.ApartmentNumber;
        }

        public override int GetHashCode()
        {
            int hashCode = 1177652159;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NodeType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DisplayName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Level);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SectionName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ApartmentTypeName);
            hashCode = hashCode * -1521134295 + ApartmentNumber.GetHashCode();
            return hashCode;
        }

        private int GetApartmentNumber()
        {
            if (apartmentNumber < 0)
            {
                apartmentNumber = ExtractApartmentNumber(DisplayName);
            }
            return apartmentNumber;
        }

        private int ExtractApartmentNumber(string apartmentName)
        {
            var numberPart = Regex.Match(apartmentName, @"\d+").Value;
            if (int.TryParse(numberPart, out int number))
                return number;
            return -1;
        }
    }
}
