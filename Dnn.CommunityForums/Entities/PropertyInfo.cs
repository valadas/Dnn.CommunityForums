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
//
using DotNetNuke.ComponentModel.DataAnnotations;
using System.Web.Caching;

namespace DotNetNuke.Modules.ActiveForums.Entities
{
    [TableName("activeforums_Properties")]
    [PrimaryKey("PropertyId", AutoIncrement = true)]
    [Cacheable("activeforums_Properties", CacheItemPriority.Low)]
    public class PropertyInfo
    {
        public int PropertyId { get; set; }
        public int PortalId { get; set; }
        public int ObjectType { get; set; }
        public int ObjectOwnerId { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public int DefaultAccessControl { get; set; }
        public bool IsHidden { get; set; }
        public bool IsRequired { get; set; }
        public bool IsReadOnly { get; set; }
        public string ValidationExpression { get; set; } = string.Empty;
        public string EditTemplate { get; set; } = string.Empty;
        public string ViewTemplate { get; set; } = string.Empty;
        public int SortOrder { get; set; }
        public string DefaultValue { get; set; }
    }
}
