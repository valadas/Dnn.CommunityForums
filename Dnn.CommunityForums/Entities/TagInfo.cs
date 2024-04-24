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

using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Web.Caching;

namespace DotNetNuke.Modules.ActiveForums.Entities
{
    [TableName("activeforums_Tags")]
    [PrimaryKey("TagId", AutoIncrement = true)]
    [Scope("ModuleId")]
    [Cacheable("activeforums_Tags", CacheItemPriority.Low)]
    public class TagInfo
    {
        public int TagId { get; set; }
        public int PortalId { get; set; }
        public int ModuleId { get; set; }
        public string TagName { get; set; }
        public int Clicks { get; set; }
        public int Items { get; set; }
        public int Priority { get; set; }
        public bool IsCategory { get; set; } = false;  /* CategoryInfo is stored as TagInfo, but with IsCategory = true */
        public int ForumId { get; set; }
        public int ForumGroupId { get; set; }
    }
}