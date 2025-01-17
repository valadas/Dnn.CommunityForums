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
using System.Collections;
using System.Collections.Generic;
using System.Data;

using System.Web;
using System.Web.Services;
using System.Text;

namespace DotNetNuke.Modules.ActiveForums.Handlers
{
	public class adminhelper : HandlerBase
	{
		public enum Actions: int
		{
			None,
			PropertySave = 2,
			PropertyDelete = 3,
			PropertyList = 4,
			PropertySortSwitch = 5,
			ListOfLists = 6,
			LoadView = 7,
			RankGet = 8,
			RankSave = 9,
			RankDelete = 10,
			FilterGet = 11,
			FilterSave = 12,
			FilterDelete = 13



		}
		public override void ProcessRequest(HttpContext context)
		{
			AdminRequired = true;
			base.AdminRequired = true;
			base.ProcessRequest(context);
			string sOut = string.Empty;
			Actions action = Actions.None;
			if (IsValid)
			{
				if (Params != null)
				{
					if (Params["action"] != null && SimulateIsNumeric.IsNumeric(Params["action"]))
					{
						action = (Actions)(Convert.ToInt32(Params["action"].ToString()));
					}
				}
				try
				{
					sOut = "{\"result\":\"success\"}";
					switch (action)
					{
						case Actions.PropertySave:
							PropertySave();
							break;
						case Actions.PropertyDelete:
							PropertyDelete();
							break;
						case Actions.PropertyList:
							sOut = "[" + Utilities.LocalizeControl(PropertyList()) + "]";
							break;
						case Actions.PropertySortSwitch:
							UpdateSort();
							break;
						case Actions.ListOfLists:
							sOut = GetLists();
							break;
						case Actions.LoadView:
							sOut = LoadView();
							break;
						case Actions.RankGet:
							sOut = GetRank();
							break;
						case Actions.RankSave:
							RankSave();
							break;
						case Actions.RankDelete:
							RankDelete();
							break;
						case Actions.FilterGet:
							sOut = FilterGet();
							break;
						case Actions.FilterSave:
							FilterSave();
							break;
						case Actions.FilterDelete:
							FilterDelete();




							break;
					}
				}
				catch (Exception ex)
				{
					Exceptions.LogException(ex);
					sOut = "{";
					sOut += Utilities.JSON.Pair("result", "failed");
					sOut += ",";
					sOut += Utilities.JSON.Pair("message", ex.Message);
					sOut += "}";
				}

			}
			else
			{
				sOut = "{";
				sOut += Utilities.JSON.Pair("result", "failed");
				sOut += ",";
				sOut += Utilities.JSON.Pair("message", "Invalid Request");
				sOut += "}";
			}
			context.Response.ContentType = "text/plain";
			context.Response.Write(sOut);
		}
		private string FilterGet()
		{
			int FilterId = -1;
			if (Params.ContainsKey("FilterId"))
			{
				FilterId = Convert.ToInt32(Params["FilterId"]);
			}
            DotNetNuke.Modules.ActiveForums.Entities.FilterInfo filter = new DotNetNuke.Modules.ActiveForums.Controllers.FilterController().GetById(FilterId, ModuleId);
            string sOut = "{";
			sOut += Utilities.JSON.Pair("FilterId", filter.FilterId.ToString());
			sOut += ",";
			sOut += Utilities.JSON.Pair("FilterType", filter.FilterType.ToString());
			sOut += ",";
			sOut += Utilities.JSON.Pair("Find", HttpUtility.UrlEncode(filter.Find.Replace(" ", "-|-")));
			sOut += ",";
			sOut += Utilities.JSON.Pair("Replacement", HttpUtility.UrlEncode(filter.Replace.Replace(" ", "-|-")));
			sOut += "}";
			return sOut;
		}
		private void FilterSave()
		{
			DotNetNuke.Modules.ActiveForums.Entities.FilterInfo filter = new DotNetNuke.Modules.ActiveForums.Entities.FilterInfo();
			filter.FilterId = -1;
			filter.ModuleId = ModuleId;
			filter.PortalId = PortalId;
			if (Params.ContainsKey("FilterId"))
			{
				filter.FilterId = Convert.ToInt32(Params["FilterId"]);
			}
			if (Params.ContainsKey("Find"))
			{
				filter.Find = Params["Find"].ToString();
			}
			if (Params.ContainsKey("Replacement"))
			{
				filter.Replace = Params["Replacement"].ToString();
			}
			if (Params.ContainsKey("FilterType"))
			{
				filter.FilterType = Params["FilterType"].ToString();
			}
            if (filter.FilterId == -1)
            {	
				new DotNetNuke.Modules.ActiveForums.Controllers.FilterController().Insert(filter); 
			}
            else 
			{ 
				new DotNetNuke.Modules.ActiveForums.Controllers.FilterController().Update(filter); 
			}
		}
		private void FilterDelete()
		{
			int FilterId = -1;
			if (Params.ContainsKey("FilterId"))
			{
				FilterId = Convert.ToInt32(Params["FilterId"]);
			}
			if (FilterId == -1)
			{
				return;
			}
			new DotNetNuke.Modules.ActiveForums.Controllers.FilterController().DeleteById(FilterId);

		}
		private string GetRank()
		{
			int RankId = -1;
			if (Params.ContainsKey("RankId"))
			{
				RankId = Convert.ToInt32(Params["RankId"]);
			}
			RewardController rc = new RewardController();
			RewardInfo rank = rc.Reward_Get(PortalId, ModuleId, RankId);
			string sOut = "{";
			sOut += Utilities.JSON.Pair("RankId", rank.RankId.ToString());
			sOut += ",";
			sOut += Utilities.JSON.Pair("RankName", rank.RankName);
			sOut += ",";
			sOut += Utilities.JSON.Pair("MinPosts", rank.MinPosts.ToString());
			sOut += ",";
			sOut += Utilities.JSON.Pair("MaxPosts", rank.MaxPosts.ToString());
			sOut += ",";
			sOut += Utilities.JSON.Pair("Display", rank.Display.ToLowerInvariant().Replace("activeforums/ranks", "ActiveForums/images/ranks"));
			sOut += "}";
			return sOut;
		}
		private void RankSave()
		{
			RewardInfo rank = new RewardInfo();
			rank.RankId = -1;
			rank.ModuleId = ModuleId;
			rank.PortalId = PortalId;
			if (Params.ContainsKey("RankId"))
			{
				rank.RankId = Convert.ToInt32(Params["RankId"]);
			}
			if (Params.ContainsKey("RankName"))
			{
				rank.RankName = Params["RankName"].ToString();
			}
			if (Params.ContainsKey("MinPosts"))
			{
				rank.MinPosts = Convert.ToInt32(Params["MinPosts"]);
			}
			if (Params.ContainsKey("MaxPosts"))
			{
				rank.MaxPosts = Convert.ToInt32(Params["MaxPosts"]);
			}
			if (Params.ContainsKey("Display"))
			{
				rank.Display = Params["Display"].ToString();
			}
			RewardController rc = new RewardController();
			rank = rc.Reward_Save(rank);
		}
		private void RankDelete()
		{
			int RankId = -1;
			if (Params.ContainsKey("RankId"))
			{
				RankId = Convert.ToInt32(Params["RankId"]);
			}
			if (RankId == -1)
			{
				return;
			}
			RewardController rc = new RewardController();
			rc.Reward_Delete(PortalId, ModuleId, RankId);

		}
		private void PropertySave()
		{

			DotNetNuke.Modules.ActiveForums.Entities.PropertyInfo pi = new DotNetNuke.Modules.ActiveForums.Entities.PropertyInfo();
			pi.PropertyId = -1;
			pi.PortalId = PortalId;
			pi = (DotNetNuke.Modules.ActiveForums.Entities.PropertyInfo)(Utilities.ConvertFromHashTableToObject(Params, pi));
			pi.Name = Utilities.CleanName(pi.Name);
			if (! (string.IsNullOrEmpty(pi.ValidationExpression)))
			{
				pi.ValidationExpression = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(pi.ValidationExpression));
			}
			if (pi.PropertyId == -1)
			{
				string lbl = Params["Label"].ToString();
				LocalizationUtils lcUtils = new LocalizationUtils();
				lcUtils.SaveResource("[RESX:" + pi.Name + "].Text", lbl, PortalId);
			}
			else
			{
				if (Utilities.GetSharedResource("[RESX:" + pi.Name + "]").ToLowerInvariant().Trim() != Params["Label"].ToString().ToLowerInvariant().Trim())
				{
					LocalizationUtils lcUtils = new LocalizationUtils();
					lcUtils.SaveResource("[RESX:" + pi.Name + "].Text", Params["Label"].ToString(), PortalId);
				}

			}
            new DotNetNuke.Modules.ActiveForums.Controllers.PropertyController().Save<int>(pi, pi.PropertyId);
			var fc = new DotNetNuke.Modules.ActiveForums.Controllers.ForumController();
            DotNetNuke.Modules.ActiveForums.Entities.ForumInfo fi = fc.GetById(pi.ObjectOwnerId, ModuleId);
			fi.HasProperties = true;
            fc.Forums_Save(PortalId, fi, false, fi.InheritSettings, fi.InheritSecurity);

		}
		private string PropertyList()
		{
            return ! (string.IsNullOrEmpty(Params["ObjectOwnerId"].ToString()))
                ? new DotNetNuke.Modules.ActiveForums.Controllers.PropertyController().ListPropertiesJSON(PortalId, Convert.ToInt32(Params["ObjectType"]), Convert.ToInt32(Params["ObjectOwnerId"]))
                : string.Empty;


        }
		private void UpdateSort()
		{

			int propertyId = -1;
			int sortOrder = -1;
			DotNetNuke.Modules.ActiveForums.Controllers.PropertyController pc = new DotNetNuke.Modules.ActiveForums.Controllers.PropertyController();

            string props = Params["props"].ToString();
			props = props.Remove(props.LastIndexOf("^"));
			foreach (string s in props.Split('^'))
			{
				if (! (string.IsNullOrEmpty(props)))
				{
					propertyId = Convert.ToInt32(s.Split('|')[0]);
					sortOrder = Convert.ToInt32(s.Split('|')[1]);
					DotNetNuke.Modules.ActiveForums.Entities.PropertyInfo pi = new DotNetNuke.Modules.ActiveForums.Controllers.PropertyController().GetById(propertyId);
					if (pi != null)
					{
						pi.SortOrder = sortOrder;
						pc.Save<int>(pi, pi.PropertyId);
					}
				}
			}
		}
		internal void PropertyDelete()
		{
            DotNetNuke.Modules.ActiveForums.Controllers.PropertyController pc = new DotNetNuke.Modules.ActiveForums.Controllers.PropertyController();
            DotNetNuke.Modules.ActiveForums.Entities.PropertyInfo prop = pc.GetById(Convert.ToInt32(Params["propertyid"]));
			if (prop != null)
            {
                pc.DeleteById(Convert.ToInt32(Params["propertyid"]));
				if (! (pc.Count("WHERE PortalId = @0 AND ObjectType = @1 AND ObjectOwnerId = @2" ,PortalId, prop.ObjectType, prop.ObjectOwnerId) > 0))
                {
                    var fc = new DotNetNuke.Modules.ActiveForums.Controllers.ForumController();
                    DotNetNuke.Modules.ActiveForums.Entities.ForumInfo fi = fc.GetById(prop.ObjectOwnerId, ModuleId);
					fi.HasProperties = false;
                    fc.Forums_Save(PortalId, fi, false, fi.InheritSettings, fi.InheritSecurity);
                }
            }


		}
		private string GetLists()
		{
			StringBuilder sb = new StringBuilder();
			DotNetNuke.Common.Lists.ListController lists = new DotNetNuke.Common.Lists.ListController();
			DotNetNuke.Common.Lists.ListInfoCollection lc = lists.GetListInfoCollection(string.Empty, string.Empty, PortalId);
			foreach (DotNetNuke.Common.Lists.ListInfo l in lc)
			{
				if (l.PortalID == PortalId)
				{
					sb.Append("{");
					sb.Append(Utilities.JSON.Pair("listname", l.Name));
					sb.Append(",");
					sb.Append(Utilities.JSON.Pair("listid", l.Key));
					sb.Append("},");
				}
			}
			string sOut = sb.ToString();
			if (sOut.EndsWith(","))
			{
				sOut = sOut.Substring(0, sOut.Length - 1);
				sOut = "[" + sOut + "]";
			}
			return sOut;
		}
		private string LoadView()
		{
			StringBuilder sb = new StringBuilder();
			string view = "home";
			if (Params["view"] != null)
			{
				view = Params["view"].ToString().ToLowerInvariant();
			}
            string sPath = DotNetNuke.Modules.ActiveForums.Utilities.MapPath(Globals.ModulePath);
            string sFile = string.Empty;
			switch (view)
			{
				case "forumeditor":
					sFile = Utilities.GetFile(sPath + "\\admin\\forumeditor.ascx");
					break;
			}
			Controls.ControlPanel cpControls = new Controls.ControlPanel(PortalId, ModuleId);
			sFile = sFile.Replace("[AF:CONTROLS:SELECTTOPICSTEMPLATES]", cpControls.TemplatesOptions(Templates.TemplateTypes.TopicsView));
			sFile = sFile.Replace("[AF:CONTROLS:SELECTTOPICTEMPLATES]", cpControls.TemplatesOptions(Templates.TemplateTypes.TopicView));
			sFile = sFile.Replace("[AF:CONTROLS:SELECTTOPICFORMTEMPLATES]", cpControls.TemplatesOptions(Templates.TemplateTypes.TopicForm));
			sFile = sFile.Replace("[AF:CONTROLS:SELECTREPLYFORMTEMPLATES]", cpControls.TemplatesOptions(Templates.TemplateTypes.ReplyForm));
			sFile = sFile.Replace("[AF:CONTROLS:SELECTPROFILETEMPLATES]", cpControls.TemplatesOptions(Templates.TemplateTypes.Profile));
			sFile = sFile.Replace("[AF:CONTROLS:SELECTEMAILTEMPLATES]", cpControls.TemplatesOptions(Templates.TemplateTypes.Email));
			sFile = sFile.Replace("[AF:CONTROLS:SELECTMODEMAILTEMPLATES]", cpControls.TemplatesOptions(Templates.TemplateTypes.ModEmail));
			sFile = sFile.Replace("[AF:CONTROLS:GROUPFORUMS]", cpControls.ForumGroupOptions());
			sFile = sFile.Replace("[AF:CONTROLS:SECGRID:ROLES]", cpControls.BindRolesForSecurityGrid(DotNetNuke.Modules.ActiveForums.Utilities.MapPath("~/")));

			sFile = Utilities.LocalizeControl(sFile, true);
			return sFile;
		}




	}
}
