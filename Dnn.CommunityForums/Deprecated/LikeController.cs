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
using System;
using System.Collections.Generic; 
namespace DotNetNuke.Modules.ActiveForums
{
    [Obsolete("Deprecated in Community Forums. Scheduled removal in 09.00.00. Replace with DotNetNuke.Modules.ActiveForums.Controllers.LikeController")]
    class LikesController : DotNetNuke.Modules.ActiveForums.Controllers.LikeController
    {
        [Obsolete("Deprecated in Community Forums. Scheduled removal in 09.00.00. Replace with DotNetNuke.Modules.ActiveForums.Controllers.LikeController.GetForPost()")]
        public new List<DotNetNuke.Modules.ActiveForums.Likes> GetForPost(int postId)
        {
            IDataContext ctx = DataContext.Instance();
            IRepository<DotNetNuke.Modules.ActiveForums.Entities.LikeInfo> repo = ctx.GetRepository<DotNetNuke.Modules.ActiveForums.Entities.LikeInfo>();
            List<DotNetNuke.Modules.ActiveForums.Likes> likes = new List<DotNetNuke.Modules.ActiveForums.Likes>();
            foreach (DotNetNuke.Modules.ActiveForums.Entities.LikeInfo like in base.GetForPost(postId))
            {
                likes.Add((DotNetNuke.Modules.ActiveForums.Likes)like);
            }
            return likes; 
        }
        [Obsolete("Deprecated in Community Forums. Scheduled removal in 09.00.00. Replace with DotNetNuke.Modules.ActiveForums.Controllers.LikeController.Like()")]
        public new void Like(int contentId, int userId)
        {
            base.Like(contentId, userId);
        }
    }
}