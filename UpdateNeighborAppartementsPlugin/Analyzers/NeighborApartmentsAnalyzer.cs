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

            var apartmentTypeGroups = CollectNodes(ParameterKeys.ROM_SubZone, nodes);

            var apartmentGroupsByApartmentType = apartmentTypeGroups.Select(at => at.Children);

            var allNeighboringCombinations = apartmentGroupsByApartmentType
                .Select(g => combinationsCalculator.Calculate(g));

            return allNeighboringCombinations.SelectMany(c => combinationsFilter.Apply(c)).ToList();

        }
    }
}
