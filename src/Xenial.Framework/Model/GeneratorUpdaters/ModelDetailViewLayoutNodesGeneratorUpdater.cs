﻿using System;
using System.Collections.Generic;
using System.Linq;

using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;

using Xenial.Framework.Layouts;
using Xenial.Framework.Layouts.Items;
using Xenial.Framework.Layouts.Items.Base;
using Xenial.Framework.Layouts.Items.LeafNodes;

using static Xenial.Framework.Model.GeneratorUpdaters.ModelDetailViewLayoutNodesGeneratorUpdaterMappers;

namespace Xenial.Framework.Model.GeneratorUpdaters
{
    /// <summary>
    /// Class ModelDetailViewLayoutNodesGeneratorUpdater.
    /// Implements the <see cref="DevExpress.ExpressApp.Model.ModelNodesGeneratorUpdater{DevExpress.ExpressApp.Model.NodeGenerators.ModelDetailViewLayoutNodesGenerator}" />
    /// </summary>
    /// <seealso cref="DevExpress.ExpressApp.Model.ModelNodesGeneratorUpdater{DevExpress.ExpressApp.Model.NodeGenerators.ModelDetailViewLayoutNodesGenerator}" />
    /// <autogeneratedoc />
    [XenialCheckLicence]
    public partial class ModelDetailViewLayoutNodesGeneratorUpdater : ModelNodesGeneratorUpdater<ModelDetailViewLayoutNodesGenerator>
    {
        private readonly NodeBuilderFactory nodeBuilderFactory
            = new NodeBuilderFactory()
                .Register<LayoutGroupItem, LayoutGroupItemBuilder>(() => new LayoutGroupItemBuilder())
                .Register<LayoutTabGroupItem, LayoutTabGroupItemBuilder>(() => new LayoutTabGroupItemBuilder())
                .Register<LayoutTabbedGroupItem, TabbedGroupItemBuilder>(() => new TabbedGroupItemBuilder())
                .Register<LayoutEmptySpaceItem, EmptySpaceItemBuilder>(() => new EmptySpaceItemBuilder())
                .Register<LayoutViewItem, LayoutViewItemBuilder>(() => new LayoutViewItemBuilder())
            ;

        /// <summary>
        /// Updates the Application Model node content generated by the Nodes Generator, specified by the <see cref="T:DevExpress.ExpressApp.Model.ModelNodesGeneratorUpdater`1" /> class' type parameter.
        /// </summary>
        /// <param name="modelNode">A ModelNode Application Model node to be updated.</param>
        /// <autogeneratedoc />
#pragma warning disable CA1725 //match identitfier of base class -> would conflict with nodes
        public override void UpdateNode(ModelNode modelNode)
#pragma warning restore CA1725 //match identitfier of base class -> would conflict with nodes
        {
            _ = nodeBuilderFactory ?? throw new InvalidOperationException();

            if (modelNode is IModelViewLayout modelViewLayout)
            {
                if (modelViewLayout.Parent is IModelDetailView modelDetailView)
                {
                    //TODO: check IModelObjectGeneratedView

                    if (modelDetailView.Equals(modelDetailView.ModelClass.DefaultDetailView))
                    {
                        //TODO: multiple views and attributes
                        var attribute = modelDetailView.ModelClass.TypeInfo.FindAttribute<DetailViewLayoutBuilderAttribute>();
                        //TODO: Factory
                        if (attribute.BuildLayoutDelegate is not null)
                        {
                            var builder = attribute.BuildLayoutDelegate;
                            var layout = builder.Invoke()
                                ?? throw new InvalidOperationException($"LayoutBuilder on Type '{modelDetailView.ModelClass.TypeInfo.Type}' for View '{modelDetailView.Id}' must return an object of Type '{typeof(Layout)}'");

                            modelViewLayout.ClearNodes();

                            var modelMainNode = modelViewLayout
                                .AddNode<IModelLayoutGroup>(ModelDetailViewLayoutNodesGenerator.MainLayoutGroupName)
                                ?? throw new InvalidOperationException($"Cannot generate 'Main' node on Type '{modelDetailView.ModelClass.TypeInfo.Type}' for View '{modelDetailView.Id}'");

                            foreach (var groupItemNode in VisitNodes<LayoutGroupItem>(layout))
                            {
                                nodeBuilderFactory.CreateViewLayoutElement(modelMainNode, groupItemNode);
                            }

                            foreach (var tabGroupItemNode in VisitNodes<LayoutTabGroupItem>(layout))
                            {
                                nodeBuilderFactory.CreateViewLayoutElement(modelMainNode, tabGroupItemNode);
                            }

                            foreach (var tabbedGroupItemNode in VisitNodes<LayoutTabbedGroupItem>(layout))
                            {
                                nodeBuilderFactory.CreateViewLayoutElement(modelMainNode, tabbedGroupItemNode);
                            }

                            foreach (var emptySpaceItemNode in VisitNodes<LayoutEmptySpaceItem>(layout))
                            {
                                nodeBuilderFactory.CreateViewLayoutElement(modelMainNode, emptySpaceItemNode);
                            }

                            foreach (var layoutViewItemNode in VisitNodes<LayoutViewItem>(layout))
                            {
                                nodeBuilderFactory.CreateViewLayoutElement(modelMainNode, layoutViewItemNode);

                                if (layoutViewItemNode is LayoutPropertyEditorItem layoutPropertyEditorItem
                                    && layoutPropertyEditorItem.PropertyEditorOptions is not null)
                                {
                                    var modelViewItem = modelDetailView
                                        .Items[layoutPropertyEditorItem.PropertyEditorId];

                                    if (modelViewItem is IModelPropertyEditor modelPropertyEditor)
                                    {
                                        layoutPropertyEditorItem.PropertyEditorOptions(modelPropertyEditor);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {

                    }
                }
            }

            static IEnumerable<TItem> VisitNodes<TItem>(LayoutItemNode node)
                where TItem : LayoutItemNode
            {
                if (node is TItem targetNode)
                {
                    yield return targetNode;
                }

                if (node is IEnumerable<LayoutItemNode> items)
                {
                    foreach (var item in items)
                    {
                        foreach (var nestedItem in VisitNodes<TItem>(item))
                        {
                            yield return nestedItem;
                        }
                    }
                }
            }

        }

    }
}
