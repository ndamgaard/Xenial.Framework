﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Utils.Reflection;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.Win.Utils;
using DevExpress.XtraEditors;

using Xenial.Framework.StepProgressEditors.Win.Editors;

namespace Xenial.Framework.StepProgressEditors.Win.Editors
{
    /// <summary>
    /// Class StepProgressBarEnumPropertyEditor.
    /// Implements the <see cref="DevExpress.ExpressApp.Win.Editors.WinPropertyEditor" />
    /// </summary>
    /// <seealso cref="DevExpress.ExpressApp.Win.Editors.WinPropertyEditor" />
    /// <autogeneratedoc />
    public class StepProgressBarEnumPropertyEditor : WinPropertyEditor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StepProgressBarEnumPropertyEditor"/> class.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="model">The model.</param>
        /// <autogeneratedoc />
        public StepProgressBarEnumPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
            => ControlBindingProperty = nameof(Control.EditValue);

        /// <summary>
        /// Indicates whether the caption of the current Property Editor should be visible in a UI.
        /// </summary>
        /// <value>true if the current Property's caption is visible; otherwise, false.</value>
        /// <autogeneratedoc />
        public override bool IsCaptionVisible => false;

        /// <summary>
        /// Creates the control core.
        /// </summary>
        /// <returns>System.Object.</returns>
        /// <autogeneratedoc />
        protected override object CreateControlCore() => new XenialEnumStepProgressBar(MemberInfo, NullText);

        /// <summary>
        /// Provides access to the control that represents the current Property Editor in a UI.
        /// </summary>
        /// <value>A <see cref="T:System.Windows.Forms.Control" /> object used to display the current Property Editor in a UI.</value>
        /// <autogeneratedoc />
        public new XenialEnumStepProgressBar Control => (XenialEnumStepProgressBar)base.Control;
    }

    /// <summary>
    /// Class XenialEnumStepProgressBar.
    /// Implements the <see cref="DevExpress.XtraEditors.StepProgressBar" />
    /// </summary>
    /// <seealso cref="DevExpress.XtraEditors.StepProgressBar" />
    /// <autogeneratedoc />
    public class XenialEnumStepProgressBar : StepProgressBar
    {
        private object? editValue;
        /// <summary>
        /// Gets or sets the edit value.
        /// </summary>
        /// <value>The edit value.</value>
        /// <autogeneratedoc />
        public object? EditValue
        {
            get => editValue;
            set
            {
                editValue = value;

                var selectedItem = Items.FirstOrDefault(item => item.Tag == null && editValue == null || item.Tag?.Equals(editValue) == true);
                if (selectedItem is not null)
                {
                    SelectedItemIndex = Items.IndexOf(selectedItem);
                }
                else
                {
                    SelectedItemIndex = -1;
                }
            }
        }

        /// <summary>
        /// Gets the member information.
        /// </summary>
        /// <value>The member information.</value>
        /// <autogeneratedoc />
        public IMemberInfo MemberInfo { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="XenialEnumStepProgressBar"/> class.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <param name="nullText">The null text.</param>
        /// <autogeneratedoc />
        public XenialEnumStepProgressBar(IMemberInfo memberInfo, string? nullText)
        {
            _ = memberInfo ?? throw new ArgumentNullException(nameof(memberInfo));
            MemberInfo = memberInfo;

            var descriptor = new EnumDescriptor(MemberInfo.MemberType);

            foreach (var value in descriptor.Values)
            {
                var caption = descriptor.GetCaption(value);

                if (value == null && !string.IsNullOrEmpty(nullText))
                {
                    caption = nullText;
                }

                var item = new StepProgressBarItem(caption)
                {
                    Tag = value
                };

                var imageInfo = descriptor.GetImageInfo(value);
                ImageOptionsHelper.AssignImage(item.ContentBlock1.ActiveStateImageOptions, imageInfo, new System.Drawing.Size(32, 32));
                ImageOptionsHelper.AssignImage(item.ContentBlock1.InactiveStateImageOptions, imageInfo, new System.Drawing.Size(32, 32));
                var valueName = value != null ? value.ToString() : null;
                if (TryGetDescription(descriptor.EnumType, valueName, out var description))
                {
                    item.ContentBlock2.Description = description;
                }

                Items.Add(item);
            }

            AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.True;
            ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            DistanceBetweenContentBlockElements = 2;
            ItemOptions.Indicator.ActiveStateImageOptions.SvgImageSize = new System.Drawing.Size(14, 14);
            ItemOptions.Indicator.InactiveStateDrawMode = IndicatorDrawMode.Outline;
            ItemOptions.Indicator.Width = 24;
        }

        private static bool TryGetDescription(Type enumType, string? valueName, out string? description)
        {
            //TODO: Localization
            if (valueName is not null)
            {
                var info = enumType.GetField(valueName);
                if (info is not null)
                {
                    var attr = AttributeHelper.GetAttributes<DXDescriptionAttribute>(info, false);
                    if (attr.Length == 1)
                    {
                        description = attr[0].Description;
                        return true;
                    }
                }
            }
            description = null;
            return false;
        }

        /// <summary>
        /// Handles the <see cref="E:MouseMove" /> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        /// <autogeneratedoc />
        protected override void OnMouseMove(MouseEventArgs e)
        {
            var info = CalcHitInfo(e.Location);
            if (info.InItem)
            {
                var item = info.Item;
                if (item is not null)
                {
                    Cursor = Cursors.Hand;
                }
            }
            else
            {
                Cursor = Cursors.Default;
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        /// Handles the <see cref="E:MouseClick" /> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        /// <autogeneratedoc />
        protected override void OnMouseClick(MouseEventArgs e)
        {
            var info = CalcHitInfo(e.Location);
            if (info.InItem)
            {
                var item = info.Item;
                if (item is not null)
                {
                    EditValue = item.Tag;
                    var binding = DataBindings[nameof(EditValue)];
                    if (binding is not null)
                    {
                        binding.WriteValue();
                    }
                }
            }
            base.OnMouseClick(e);
        }
    }
}

namespace DevExpress.ExpressApp.Editors
{
    /// <summary>
    /// Class StepProgressBarEnumPropertyEditorWinExtensions.
    /// </summary>
    /// <autogeneratedoc />
    public static class StepProgressBarEnumPropertyEditorWinExtensions
    {
        /// <summary>
        /// Uses the step progress enum property editors windows forms.
        /// </summary>
        /// <param name="editorDescriptorsFactory">The editor descriptors factory.</param>
        /// <returns>EditorDescriptorsFactory.</returns>
        /// <exception cref="System.ArgumentNullException">editorDescriptorsFactory</exception>
        /// <autogeneratedoc />
        public static EditorDescriptorsFactory UseStepProgressEnumPropertyEditorsWin(this EditorDescriptorsFactory editorDescriptorsFactory)
        {
            _ = editorDescriptorsFactory ?? throw new ArgumentNullException(nameof(editorDescriptorsFactory));

            editorDescriptorsFactory.RegisterPropertyEditor(
                Xenial.Framework.StepProgressEditors.PubTernal.StepProgressEditorAliases.StepProgressEnumPropertyEditor,
                typeof(Enum),
                typeof(StepProgressBarEnumPropertyEditor),
                false
            );

            return editorDescriptorsFactory;
        }
    }
}
