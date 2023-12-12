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
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using DotNetNuke.Modules.ActiveForums.Entities;

namespace DotNetNuke.Modules.ActiveForums.Controllers
{
    internal static class FilterController
    {

        public static string FilterWords(int portalId, int moduleId, string themePath, string strMessage, bool processEmoticons, bool removeHTML = false)
        {
            if (removeHTML)
            {
                var newSubject = DotNetNuke.Modules.ActiveForums.Utilities.StripHTMLTag(strMessage);
                if (newSubject == string.Empty)
                {
                    newSubject = strMessage.Replace("<", string.Empty);
                    newSubject = newSubject.Replace(">", string.Empty);
                }
                strMessage = newSubject;
            }

            var dr = DataProvider.Instance().Filters_List(portalId, moduleId, 0, 100000, "ASC", "FilterId");
            dr.NextResult();

            while (dr.Read())
            {
                var sReplace = dr["Replace"].ToString();
                var sFind = dr["Find"].ToString();
                switch (dr["FilterType"].ToString().ToUpper())
                {
                    case "MARKUP":
                        strMessage = strMessage.Replace(sFind, sReplace.Trim());
                        break;

                    case "EMOTICON":
                        if (processEmoticons)
                        {
                            if (sReplace.IndexOf("/emoticons", StringComparison.Ordinal) >= 0)
                                sReplace = string.Format("<img src='{0}{1}' align=\"absmiddle\" border=\"0\" class=\"afEmoticon\" />", themePath, sReplace);

                            strMessage = strMessage.Replace(sFind, sReplace);
                        }
                        break;

                    case "REGEX":
                        strMessage = Regex.Replace(strMessage, sFind.Trim(), sReplace, RegexOptions.IgnoreCase);
                        break;
                }

            }
            dr.Close();

            return strMessage;
        }

        public static string RemoveFilterWords(int portalId, int moduleId, string themePath, string strMessage)
        {
            var dr = DataProvider.Instance().Filters_List(portalId, moduleId, 0, 100000, "ASC", "FilterId");
            dr.NextResult();

            while (dr.Read())
            {
                var sReplace = dr["Replace"].ToString();
                var sFind = dr["Find"].ToString();
                switch (dr["FilterType"].ToString().ToUpper())
                {
                    case "MARKUP":
                        strMessage = strMessage.Replace(sReplace, sFind.Trim());
                        break;

                    case "EMOTICON":
                        if (sReplace.IndexOf("/emoticons", StringComparison.Ordinal) >= 0)
                        {
                            sReplace = string.Format("<img src='{0}{1}' align=\"absmiddle\"  border=\"0\"  class=\"afEmoticon\" />", themePath, sReplace);
                            strMessage = strMessage.Replace(sReplace, sFind);
                        }
                        break;
                }

            }
            dr.Close();

            strMessage = DotNetNuke.Modules.ActiveForums.Utilities.ManageImagePath(strMessage);

            return strMessage;
        }

        public static string ImportFilter(int portalID, int moduleID)
        {
            string @out;
            try
            {
                var myFile = HttpContext.Current.Server.MapPath(string.Concat(Globals.DefaultTemplatePath, "/Filters.txt"));
                if (File.Exists(myFile))
                {
                    StreamReader objStreamReader;
                    try
                    {
                        objStreamReader = File.OpenText(myFile);
                    }
                    catch (Exception exc)
                    {
                        @out = exc.Message;
                        return @out;
                    }
                    var strFilter = objStreamReader.ReadLine();
                    while (strFilter != null)
                    {
                        var row = Regex.Split(strFilter, ",,");
                        var sFind = row[0].Substring(1, row[0].Length - 2);
                        var sReplace = row[1].Trim(' ');
                        sReplace = sReplace.Substring(1, sReplace.Length - 2);
                        var sType = row[2].Substring(1, row[2].Length - 2);
                        DataProvider.Instance().Filters_Save(portalID, moduleID, -1, sFind, sReplace, sType);
                        strFilter = objStreamReader.ReadLine();
                    }
                    objStreamReader.Close();
                    @out = "Success";
                }
                else
                {
                    @out = string.Concat("File Not Found<br />Path:", myFile);
                }
            }
            catch (Exception exc)
            {
                @out = exc.Message;
            }

            return @out;
        }
    }
}

