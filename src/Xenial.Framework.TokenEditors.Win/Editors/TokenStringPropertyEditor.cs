﻿using System;

using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

using Xenial.Framework.TokenEditors.Win.Editors;

namespace Xenial.Framework.TokenEditors.Win.Editors
{
    /// <summary>
    /// Class TokenStringPropertyEditor.
    /// Implements the <see cref="DevExpress.ExpressApp.Win.Editors.DXPropertyEditor" />
    /// </summary>
    /// <seealso cref="DevExpress.ExpressApp.Win.Editors.DXPropertyEditor" />
    [XenialCheckLicence]
    public sealed partial class TokenStringPropertyEditor : DXPropertyEditor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenStringPropertyEditor"/> class.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="model">The model.</param>
        /// <autogeneratedoc />
        public TokenStringPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        /// <summary>
        /// Creates the repository item.
        /// </summary>
        /// <returns>RepositoryItem.</returns>
        /// <autogeneratedoc />
        protected override RepositoryItem CreateRepositoryItem() => new RepositoryItemTokenEdit();

        /// <summary>
        /// Creates the control core.
        /// </summary>
        /// <returns>System.Object.</returns>
        /// <autogeneratedoc />
        protected override object CreateControlCore() => new TokenEdit();

        /// <summary>
        /// Provides access to the control that represents the current Property Editor in a UI.
        /// </summary>
        /// <value>A DevExpress.XtraEditors.BaseEdit object representing a control used to display the current Property Editor in a UI.</value>
        /// <autogeneratedoc />
        public new TokenEdit Control => (TokenEdit)base.Control;

        /// <summary>
        /// Setups the repository item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <autogeneratedoc />
        protected override void SetupRepositoryItem(RepositoryItem item)
        {
            base.SetupRepositoryItem(item);
            if (item is RepositoryItemTokenEdit tokenEdit)
            {
                InitTokens();

                void InitTokens()
                {
                    tokenEdit.Tokens.BeginUpdate();
                    try
                    {
                        tokenEdit.EditValueSeparatorChar = ';';

                        if (Model is not null && !string.IsNullOrEmpty(Model.PredefinedValues))
                        {
                            foreach (var predefinedValue in Model.PredefinedValues.Split(';'))
                            {
                                tokenEdit.Tokens.AddToken(predefinedValue);
                            }
                        }
                    }
                    finally
                    {
                        tokenEdit.Tokens.EndUpdate();
                    }
                }

            }
        }
    }
}

namespace DevExpress.ExpressApp.Editors
{
    /// <summary>
    /// Class P.
    /// </summary>
    /// <autogeneratedoc />
    public static class TokenStringPropertyEditorWinExtensions
    {
        /// <summary>
        /// Uses the token objects property editor.
        /// </summary>
        /// <param name="editorDescriptorsFactory">The editor descriptors factory.</param>
        /// <returns>EditorDescriptorsFactory.</returns>
        /// <exception cref="System.ArgumentNullException">editorDescriptorsFactory</exception>
        /// <autogeneratedoc />
        public static EditorDescriptorsFactory UseTokenStringPropertyEditorsWin(this EditorDescriptorsFactory editorDescriptorsFactory)
        {
            _ = editorDescriptorsFactory ?? throw new ArgumentNullException(nameof(editorDescriptorsFactory));

            editorDescriptorsFactory.RegisterPropertyEditor(
                Xenial.Framework.TokenEditors.PubTernal.TokenEditorAliases.TokenStringPropertyEditor,
                typeof(string),
                typeof(TokenStringPropertyEditor),
                false
            );

            return editorDescriptorsFactory;
        }
    }
}
