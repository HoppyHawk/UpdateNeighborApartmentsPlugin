﻿using System;
using System.Collections.Generic;
using System.Linq;
using UpdateNeighborAppartementsPlugin.Analyzers.Calculators;
using UpdateNeighborAppartementsPlugin.DocumentTreeModel.Nodes;
using UpdateNeighborAppartementsPlugin.Model;

namespace UpdateNeighborAppartementsPlugin.Analyzers
{
    public class NeighborApartmentsAnalyzer : DocumentTreeAnalyzerBase
    {
        private INodeCombinationsFilter combinationsFilter;

        public NeighborApartmentsAnalyzer(INodeCombinationsFilter combinationsFilter)
        {
            this.combinationsFilter = combinationsFilter;
        }

        public override List<DocumentTreeNode> Analyze(IEnumerable<DocumentTreeNode> nodes)
        {
            var nodeComparer = new NeighborApartmentsComparer();
            var combinationsCalculator = new NodeCombinationsCalculator(nodeComparer);

            var apartmentNodesByApartmentType = CollectChildNodes(ParameterKeys.ROM_SubZone, nodes);

            var allNeighboringCombinations = apartmentNodesByApartmentType
                .Select(g => combinationsCalculator.Calculate(g));

            return allNeighboringCombinations.SelectMany(c => combinationsFilter.Apply(c)).ToList();

        }
    }
}