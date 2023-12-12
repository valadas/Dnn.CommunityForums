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
using DotNetNuke.Data;
using DotNetNuke.Modules.ActiveForums.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetNuke.Modules.ActiveForums.Controllers
{
    class TopicController : RepositoryControllerBase<DotNetNuke.Modules.ActiveForums.Entities.TopicInfo>
    {
        readonly IDataContext ctx;
        IRepository<DotNetNuke.Modules.ActiveForums.Entities.TopicInfo> repo;
        internal TopicController()
        {
            
            ctx = DataContext.Instance();
            repo = ctx.GetRepository<DotNetNuke.Modules.ActiveForums.Entities.TopicInfo>();
        }
        internal List<DotNetNuke.Modules.ActiveForums.Entities.TopicInfo> Get()
        {
            return repo.Get().ToList();
        }
        internal DotNetNuke.Modules.ActiveForums.Entities.TopicInfo Get(int topicId)
        {
            return repo.Find("WHERE TopicId = @0", topicId).FirstOrDefault();
        }
        internal void Update(DotNetNuke.Modules.ActiveForums.Entities.TopicInfo topicInfo)
        {
            repo.Update(topicInfo);
        }
        internal void Insert(DotNetNuke.Modules.ActiveForums.Entities.TopicInfo topicInfo)
        {
            repo.Insert(topicInfo);
        }
    }
}
