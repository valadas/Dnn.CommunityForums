﻿//
// Community Forums
// Copyright (c) 2013-2024
// by DNN Community
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
//
using System;
using System.Web.UI;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Modules.ActiveForums.Constants;

namespace DotNetNuke.Modules.ActiveForums
{
    public partial class ActiveForums : ForumBase, IActionable
    {
        private string currentURL = string.Empty;
        protected string CurrentUrl
        {
            get
            {
                if (string.IsNullOrEmpty(currentURL))
                {
                    currentURL = string.Concat(Request.IsSecureConnection ? SEOConstants.HTTPS : SEOConstants.HTTP,
                        Request.Url.Host, Request.RawUrl);
                }
                return currentURL;
            }
        }

        #region DNN Actions

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                {
                    {
                        GetNextActionID(), Utilities.GetSharedResource("[RESX:ControlPanel]"),
                        ModuleActionType.AddContent, string.Empty, string.Empty, EditUrl(), false,
                        Security.SecurityAccessLevel.Edit, true, false
                    }
                };

                return actions;
            }
        }

        #endregion

        #region Event Handlers

        protected override void  OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var ctl = (ForumBase)(LoadControl(Page.ResolveUrl(Globals.ModulePath + "classic.ascx")));
            ctl.ModuleConfiguration = ModuleConfiguration;
            plhAF.Controls.Add(ctl);

            this.Page.Header.Controls.Add(new LiteralControl(string.Format(SEOConstants.FORMAT_CANONICAL_TAG, CurrentUrl)));
        }

        #endregion
    }
}