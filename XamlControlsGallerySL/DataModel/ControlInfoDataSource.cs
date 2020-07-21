//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


// The data model defined by this file serves as a representative example of a strongly-typed
// model.  The property names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs. If using this model, you might improve app
// responsiveness by initiating the data loading task in the code behind for App.xaml when the app
// is first launched.

namespace AppUIBasics.Data
{
    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class ControlInfoDataItem
    {
        public ControlInfoDataItem(string uniqueId, string title, string subtitle, string imagePath, string page, string badgeString, string description, string content, bool isNew, bool isUpdated, bool isPreview)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.ImagePath = imagePath;
            this.Page = page;
            this.BadgeString = badgeString;
            this.Content = content;
            this.IsNew = isNew;
            this.IsUpdated = isUpdated;
            this.IsPreview = isPreview;
            this.Docs = new ObservableCollection<ControlInfoDocLink>();
            this.RelatedControls = new ObservableCollection<string>();
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        
        public string Page { get; private set; }
        public string BadgeString { get; private set; }
        public string Content { get; private set; }
        public bool IsNew { get; private set; }
        public bool IsUpdated { get; private set; }
        public bool IsPreview { get; private set; }
        public ObservableCollection<ControlInfoDocLink> Docs { get; private set; }
        public ObservableCollection<string> RelatedControls { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    public class ControlInfoDocLink
    {
        public ControlInfoDocLink(string title, string uri)
        {
            this.Title = title;
            this.Uri = uri;
        }
        public string Title { get; private set; }
        public string Uri { get; private set; }
    }


    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class ControlInfoDataGroup
    {
        public ControlInfoDataGroup(string uniqueId, string title, string subtitle, string imagePath, string description, string page)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.ImagePath = imagePath;
            this.Items = new ObservableCollection<ControlInfoDataItem>();
            this.Page = page;
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public ObservableCollection<ControlInfoDataItem> Items { get; private set; }

        public string Page { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    ///
    /// ControlInfoSource initializes with data read from a static json file included in the
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class ControlInfoDataSource
    {
        private static readonly object _lock = new object();

        #region Singleton

        private static ControlInfoDataSource _instance;

        public static ControlInfoDataSource Instance
        {
            get
            {
                return _instance;
            }
        }

        static ControlInfoDataSource()
        {
            _instance = new ControlInfoDataSource();

            _instance.GetControlInfoData();
        }

        private ControlInfoDataSource() { }

        #endregion

        private IList<ControlInfoDataGroup> _groups = new List<ControlInfoDataGroup>();
        public IList<ControlInfoDataGroup> Groups
        {
            get { return this._groups; }
        }

        public IEnumerable<ControlInfoDataGroup> GetGroups()
        {
            _instance.GetControlInfoData();

            return _instance.Groups;
        }

        public ControlInfoDataGroup GetGroup(string uniqueId)
        {
            _instance.GetControlInfoData();
            // Simple linear search is acceptable for small data sets
            var matches = _instance.Groups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public ControlInfoDataItem GetItem(string uniqueId)
        {
            _instance.GetControlInfoData();
            // Simple linear search is acceptable for small data sets
            var matches = _instance.Groups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() > 0) return matches.First();
            return null;
        }

        public ControlInfoDataGroup GetGroupFromItem(string uniqueId)
        {
            _instance.GetControlInfoData();
            var matches = _instance.Groups.Where((group) => group.Items.FirstOrDefault(item => item.UniqueId.Equals(uniqueId)) != null);
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        private void GetControlInfoData()
        {
            lock (_lock)
            {
                if (this.Groups.Count() != 0)
                {
                    return;
                }
            }
            string jsonText = "";
            Uri dataUri = new Uri("/XamlControlsGallerySL;component/DataModel/ControlInfoData.json", UriKind.Relative);
            using (Stream s = Application.GetResourceStream(dataUri).Stream)
            {
                var reader = new StreamReader(s);
                jsonText = reader.ReadToEnd();
            }

            JsonObject jsonObject = (JsonObject)JsonObject.Parse(jsonText);
            JsonArray jsonArray = (JsonArray)jsonObject["Groups"];

            lock (_lock)
            {
                foreach (JsonValue groupValue in jsonArray)
                {
                    JsonObject groupObject = (JsonObject) groupValue;
                    JsonValue grouppage;
                    groupObject.TryGetValue("Page", out grouppage);

                    ControlInfoDataGroup group = new ControlInfoDataGroup(groupObject["UniqueId"],
                                                                          groupObject["Title"],
                                                                          groupObject["Subtitle"],
                                                                          groupObject["ImagePath"],
                                                                          groupObject["Description"],
                                                                          grouppage);

                    foreach (JsonValue itemValue in groupObject["Items"])
                    {
                        JsonObject itemObject = (JsonObject)itemValue;

                        string badgeString = null;

                        bool isNew = itemObject.ContainsKey("IsNew") ? (bool)itemObject["IsNew"] : false;
                        bool isUpdated = itemObject.ContainsKey("IsUpdated") ? (bool)itemObject["IsUpdated"] : false;
                        bool isPreview = itemObject.ContainsKey("IsPreview") ? (bool)itemObject["IsPreview"] : false;

                        if (isNew)
                        {
                            badgeString = "New";
                        }
                        else if (isUpdated)
                        {
                            badgeString = "Updated";
                        }
                        else if (isPreview)
                        {
                            badgeString = "Preview";
                        }
                        JsonValue page;
                        itemObject.TryGetValue("Page", out page);
                        var item = new ControlInfoDataItem(itemObject["UniqueId"],
                                                                itemObject["Title"],
                                                                itemObject["Subtitle"],
                                                                itemObject["ImagePath"],
                                                                page,
                                                                badgeString,
                                                                itemObject["Description"],
                                                                itemObject["Content"],
                                                                isNew,
                                                                isUpdated,
                                                                isPreview);

                        if (itemObject.ContainsKey("Docs"))
                        {
                            foreach (JsonValue docValue in itemObject["Docs"])
                            {
                                JsonObject docObject = (JsonObject)docValue;
                                item.Docs.Add(new ControlInfoDocLink(docObject["Title"], docObject["Uri"]));
                            }
                        }

                        if (itemObject.ContainsKey("RelatedControls"))
                        {
                            foreach (JsonValue relatedControlValue in itemObject["RelatedControls"])
                            {
                                item.RelatedControls.Add(relatedControlValue);
                            }
                        }

                        group.Items.Add(item);
                    }

                    if (!Groups.Any(g => g.Title == group.Title))
                    {
                        Groups.Add(group);
                    }
                }
            }
        }
    }
}
