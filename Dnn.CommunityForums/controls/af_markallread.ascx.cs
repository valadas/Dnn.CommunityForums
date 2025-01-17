//
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
namespace DotNetNuke.Modules.ActiveForums
{

    public partial class af_markread : ForumBase
    {
        public string CSSClass { get; set; }

        #region Event Handlers
        
        protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

            if (btnMarkAllRead == null)
                return;

            btnMarkAllRead.Visible = UserId != -1;

            if (!string.IsNullOrWhiteSpace(CSSClass))
                btnMarkAllRead.CssClass = CSSClass;
        }

        #endregion

        #region  Web Form Designer Generated Code

        //This call is required by the Web Form Designer.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
        }
        //NOTE: The following placeholder declaration is required by the Web Form Designer.
        //Do not delete or move it.
        private object designerPlaceholderDeclaration;

        protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

            //CODEGEN: This method call is required by the Web Form Designer
            //Do not modify it using the code editor.
            LocalResourceFile = Globals.SharedResourceFile;
            InitializeComponent();

            btnMarkAllRead.Click += BtnMarkAllReadClick;
        }

        #endregion

        private void BtnMarkAllReadClick(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated) 
                return;

            DataProvider.Instance().Utility_MarkAllRead(ModuleId, UserId, ForumId > 0 ? ForumId : 0);
            
            Response.Redirect(Request.RawUrl);
        }
    }

}
