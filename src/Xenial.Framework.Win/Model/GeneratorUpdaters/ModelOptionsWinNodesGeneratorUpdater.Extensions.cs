﻿using Xenial.Framework.Win.Model.GeneratorUpdaters;

namespace DevExpress.ExpressApp.Model.Core
{
    /// <summary>
    /// Class ModelNodesGeneratorUpdatersExtentions.
    /// </summary>
    public static partial class ModelNodesGeneratorUpdatersExtentions
    {
        /// <summary>
        /// Uses the application win options.
        /// </summary>
        /// <param name="updaters">The updaters.</param>
        /// <param name="options">The options.</param>
        /// <returns>ModelNodesGeneratorUpdaters.</returns>
        public static ModelNodesGeneratorUpdaters UseApplicationWinOptions(this ModelNodesGeneratorUpdaters updaters, ApplicationWinOptions options)
        {
            updaters.Add(new ModelOptionsWinNodesGeneratorUpdater(options));
            return updaters;
        }
    }
}
