﻿//
// Community Forums
// Copyright (c) 2013-2021
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
using System.Collections;
using System.Web.Caching;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace DotNetNuke.Modules.ActiveForums.Entities
{
    public partial class ForumGroupInfo
    {
        [Obsolete("Deprecated in Community Forums. Removed in 10.00.00. Not Used.")]
        [IgnoreColumn()]
        public int AttachMaxWidth
        {
            get { return Utilities.SafeConvertInt(GroupSettings[ForumSettingKeys.AttachMaxWidth], 500); }
        }
        [Obsolete("Deprecated in Community Forums. Removed in 10.00.00. Not Used.")]
        [IgnoreColumn()]
        public int AttachMaxHeight
        {
            get { return Utilities.SafeConvertInt(GroupSettings[ForumSettingKeys.AttachMaxHeight], 500); }
        }
        [Obsolete("Deprecated in Community Forums. Removed in 10.00.00. Not Used.")]
        [IgnoreColumn()]
        public int EditorStyle
        {
            get { return Utilities.SafeConvertInt(GroupSettings[ForumSettingKeys.EditorStyle], 1); }
        }
        [Obsolete("Deprecated in Community Forums. Removed in 10.00.00. Not Used.")]
        [IgnoreColumn()]
        public string EditorToolBar
        {
            get { return Utilities.SafeConvertString(GroupSettings[ForumSettingKeys.EditorToolbar], "bold,italic,underline"); }
        }
    }
}
namespace DotNetNuke.Modules.ActiveForums
{
    [Obsolete("Deprecated in Community Forums. Removed in 10.00.00. Use DotNetNuke.Modules.ActiveForums.Entities.ForumGroupInfo.")]
    public partial class ForumGroupInfo : DotNetNuke.Modules.ActiveForums.Entities.ForumGroupInfo
    {
    }
}