﻿using System;

using DevExpress.ExpressApp.Editors;

namespace Xenial.Framework.WebView
{
    /// <summary>
    /// Class XenialWebViewModule.
    /// Implements the <see cref="Xenial.Framework.XenialModuleBase" />
    /// </summary>
    /// <seealso cref="Xenial.Framework.XenialModuleBase" />
    /// <autogeneratedoc />
    public class XenialWebViewModule : XenialModuleBase
    {
        /// <summary>
        /// Registers the editor descriptors.
        /// </summary>
        /// <param name="editorDescriptorsFactory">The editor descriptors factory.</param>
        /// <autogeneratedoc />
        protected override void RegisterEditorDescriptors(EditorDescriptorsFactory editorDescriptorsFactory)
        {
            base.RegisterEditorDescriptors(editorDescriptorsFactory);
            editorDescriptorsFactory.UseWebViewUriPropertyEditor();
        }
    }
}