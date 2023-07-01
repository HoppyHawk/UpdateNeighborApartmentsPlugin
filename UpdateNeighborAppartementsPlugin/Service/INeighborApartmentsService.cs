using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;

namespace UpdateNeighborAppartementsPlugin.Service
{
    public interface INeighborApartmentsService
    {
        Task<List<ApartmentNode>> GetNeighborApartments();

        Task UpdateApartment(ApartmentNode apartment);
        Task UpdateApartments(List<ApartmentNode> apartments);
    }
}
