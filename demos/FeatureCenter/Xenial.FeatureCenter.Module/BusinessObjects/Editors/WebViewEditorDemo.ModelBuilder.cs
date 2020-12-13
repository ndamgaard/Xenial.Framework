﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using DevExpress.ExpressApp.DC;

using Xenial.Utils;
using Xenial.Framework.ModelBuilders;
using Xenial.Framework.WebView.ModelBuilders;

namespace Xenial.FeatureCenter.Module.BusinessObjects.Editors
{
    public class WebViewEditorDemoModelBuilder : ModelBuilder<WebViewEditorDemo>
    {
        public WebViewEditorDemoModelBuilder(ITypeInfo typeInfo) : base(typeInfo) { }

        public override void Build()
        {
            base.Build();

            this.HasCaption("Editors - WebView")
                .WithDefaultClassOptions()
                .IsSingleton(autoCommit: true)
                .HasImage("Business_World");

            For(m => m.Uri)
                .UsingWebViewPropertyEditor();
        }
    }
}
