﻿using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpdateNeighborAppartementsPlugin.Analyzers;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;
using UpdateNeighborAppartementsPlugin.Model;

namespace UpdateNeighborAppartementsPlugin.Service
{
    class NeighborApartmentsService : INeighborApartmentsService
    {
        private IElementsLoader elementsLoader;
        private IDocumentTreeBuilder documentTreeBuilder;
        private IDocumentTreeAnalyzer neighborApartmentAnalyzer;
        private IElementsUpdater elementsUpdater;

        private List<Element> elements;
        private IEnumerable<DocumentTreeNode> documentTreeNodes;

        public NeighborApartmentsService(IElementsLoader elementsLoader, IDocumentTreeBuilder documentTreeBuilder, 
            IDocumentTreeAnalyzer neighborApartmentAnalyzer, IElementsUpdater elementsUpdater) { 

            this.elementsLoader = elementsLoader;
            this.documentTreeBuilder = documentTreeBuilder;
            this.neighborApartmentAnalyzer = neighborApartmentAnalyzer;
            this.elementsUpdater = elementsUpdater;
        }

        public async Task<List<ApartmentNode>> GetNeighborApartments() {
            if (documentTreeNodes == null)
                await LoadDocumentDataAsync();

            var nodes = documentTreeNodes;

            var findAppartmentsTask = Task.Run(() =>
                neighborApartmentAnalyzer.Analyze(nodes)
                    .Select(n => n as ApartmentNode)
                    .OrderBy(a => a.Level)
                    .ToList()
            );

            return await findAppartmentsTask;
        }

        public async Task UpdateApartment(ApartmentNode apartment) {
            await elementsUpdater.Update(apartment.Elements);
        }

        public async Task UpdateApartments(List<ApartmentNode> appartements)
        {
            var rooms = appartements.SelectMany(a => a.Elements).ToList();
            await elementsUpdater.Update(rooms);
        }

        private async Task LoadDocumentDataAsync() {
            elements = await LoadElementsAsync();
            documentTreeNodes = await BuildDocumentTreeAsync(elements);
        }

        private Task<List<Element>> LoadElementsAsync() {
            return Task.Run(() => elementsLoader.LoadElements());
        }

        private Task<IEnumerable<DocumentTreeNode>> BuildDocumentTreeAsync(List<Element> elements) {
            return Task.Run(() => documentTreeBuilder.Build(elements)/*.Where(n => n.DisplayName == "Этаж 03")*/);
        }
    }
}
