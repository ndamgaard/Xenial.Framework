﻿using Xenial.Framework.Layouts.Items.Base;

namespace Xenial.Framework.Layouts.Items.LeafNodes
{
    /// <summary>
    /// 
    /// </summary>
    [XenialCheckLicence]
    public partial record LayoutEmptySpaceItem : LayoutItemLeaf
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>Xenial.Framework.Layouts.Items.LayoutEmptySpaceItem.</returns>
        /// <autogeneratedoc />
        public static LayoutEmptySpaceItem Create()
            => new();

        /// <summary>
        /// Creates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Xenial.Framework.Layouts.Items.LayoutEmptySpaceItem.</returns>
        /// <autogeneratedoc />
        public static LayoutEmptySpaceItem Create(string id)
            => new(id);

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutEmptySpaceItem"/> class.
        /// </summary>
        /// <autogeneratedoc />
        public LayoutEmptySpaceItem() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutEmptySpaceItem"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <autogeneratedoc />
        public LayoutEmptySpaceItem(string id)
            => Id = Slugifier.GenerateSlug(id);
    }
}
