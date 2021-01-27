﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;

using Xenial.Framework.Model.Core;
using Xenial.Framework.TokenEditors.Model;
using Xenial.Framework.TokenEditors.PubTernal;

namespace Xenial.Framework.TokenEditors.Model
{
    /// <summary>
    /// Interface ITokenStringModelMember
    /// </summary>
    /// <autogeneratedoc />
    public interface ITokenStringModelMember
    {
        /// <summary>
        /// Gets or sets the token drop down show mode.
        /// </summary>
        /// <value>The token drop down show mode.</value>
        /// <autogeneratedoc />
        [ModelBrowsable(typeof(TokenStringEditorTypeVisibilityCalculator))]
        [Category("Behavior")]
        TokenDropDownShowMode? TokenDropDownShowMode { get; set; }
    }

    /// <summary>
    /// Interface ITokenStringModelPropertyEditor
    /// </summary>
    /// <autogeneratedoc />
    public interface ITokenStringModelPropertyEditor : IModelNode
    {
        /// <summary>
        /// Gets or sets the token drop down show mode.
        /// </summary>
        /// <value>The token drop down show mode.</value>
        /// <autogeneratedoc />
        [ModelValueCalculator(nameof(IModelPropertyEditor.ModelMember), typeof(ITokenStringModelMember), nameof(ITokenStringModelMember.TokenDropDownShowMode))]
        [ModelBrowsable(typeof(TokenStringEditorTypeVisibilityCalculator))]
        [Category("Behavior")]
        TokenDropDownShowMode? TokenDropDownShowMode { get; set; }
    }

    /// <summary>
    /// Class TokenStringEditorTypeVisibilityCalculator. This class cannot be inherited.
    /// Implements the <see cref="Xenial.Framework.Model.Core.EditorTypeVisibilityCalculator" />
    /// </summary>
    /// <seealso cref="Xenial.Framework.Model.Core.EditorTypeVisibilityCalculator" />
    /// <autogeneratedoc />
    public sealed class TokenStringEditorTypeVisibilityCalculator : EditorTypeVisibilityCalculator
    {
        /// <summary>
        /// Determines whether the specified node is visible.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if the specified node is visible; otherwise, <c>false</c>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <autogeneratedoc />
        public override bool IsVisible(IModelNode node, string propertyName)
        {
            var propertyEditorType = FindPropertyEditorType(node, TokenEditorAliases.TokenStringPropertyEditor);
            if (HasEditorAliasAttribute(node, TokenEditorAliases.TokenStringPropertyEditor) == true)
            {
                if (propertyEditorType is null) //Base Module -> No Editor registered
                {
                    return true;
                }
            }
            var editorType = FindEditorType(node);
            if (editorType is not null)
            {
                if (propertyEditorType is not null)
                {
                    return editorType == propertyEditorType;
                }
            }

            return false;
        }
    }

#pragma warning disable CA1707 //Convention by XAF

    /// <summary>
    /// Class TokenStringModelMemberDomainLogic.
    /// </summary>
    /// <autogeneratedoc />
    [DomainLogic(typeof(ITokenStringModelMember))]
    public static class TokenStringModelMemberDomainLogic
    {
        /// <summary>
        /// Gets the token drop down show mode.
        /// </summary>
        /// <param name="modelMember">The model member.</param>
        /// <returns>DevExpress.Persistent.Base.TokenDropDownShowMode?.</returns>
        /// <autogeneratedoc />
        public static TokenDropDownShowMode? Get_TokenDropDownShowMode(IModelMember modelMember)
        {
            _ = modelMember ?? throw new ArgumentNullException(nameof(modelMember));
            var attribute = modelMember.MemberInfo.FindAttribute<TokenStringEditorAttribute>();
            if (attribute is not null)
            {
                return attribute.TokenDropDownShowMode;
            }
            return null;
        }
    }

#pragma warning restore CA1707

}

namespace DevExpress.ExpressApp.Model
{
    /// <summary>
    /// Class ModelInterfaceExtendersLayoutBuilderExtensions.
    /// </summary>
    /// <autogeneratedoc />
    public static partial class ModelInterfaceExtendersTokenEditorsExtensions
    {
        /// <summary>
        /// Uses the token string property editors.
        /// </summary>
        /// <param name="extenders">The extenders.</param>
        /// <returns>DevExpress.ExpressApp.Model.ModelInterfaceExtenders.</returns>
        /// <autogeneratedoc />
        public static ModelInterfaceExtenders UseTokenStringPropertyEditors(this ModelInterfaceExtenders extenders)
        {
            _ = extenders ?? throw new ArgumentNullException(nameof(extenders));

            extenders.Add<IModelMember, ITokenStringModelMember>();
            extenders.Add<IModelPropertyEditor, ITokenStringModelPropertyEditor>();

            return extenders;
        }
    }
}

namespace DevExpress.ExpressApp.Model
{
    /// <summary>
    /// Class XenialTokenEditorsModelTypeList.
    /// </summary>
    /// <autogeneratedoc />
    public static class XenialTokenEditorsModelTypeList
    {
        /// <summary>
        /// Uses the token string editor regular types.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <returns>IEnumerable&lt;Type&gt;.</returns>
        /// <autogeneratedoc />
        public static IEnumerable<Type> UseTokenStringEditorRegularTypes(this IEnumerable<Type> types)
            => types.Concat(new[]
            {
                typeof(ITokenStringModelMember),
                typeof(ITokenStringModelPropertyEditor),
                typeof(TokenStringModelMemberDomainLogic),
                typeof(TokenStringEditorTypeVisibilityCalculator)
            });
    }

}
