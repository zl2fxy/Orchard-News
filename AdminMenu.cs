using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.UI.Navigation;
using Castle.NewsManagement.Models;
using Orchard.Core.Title.Models;

namespace Castle.NewsManagement
{
    public class AdminMenu : INavigationProvider
    {
        private readonly IContentManager _contentManager;

        public Localizer T { get; set; }

        public string MenuName { get { return "admin"; } }

        public AdminMenu(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }


        public void GetNavigation(NavigationBuilder builder)
        {
           var newsgroups= _contentManager.Query<NewsGroupPart>().List();

           builder.Add(T("组管理"), "1.8",
               menu => menu.Add(T("组管理"), "2.2", item => item.Action("Index", "NewsGroupAdmin", new { area = "Castle.NewsManagement" }).LocalNav().Permission(Permissions.NewsAdmin)));
            builder.Add(T("新闻添加"), "1.7",
             menu => menu.Add(T("新闻添加"), "2.1", item => item.Action("NewsEdit", "Admin", new { area = "Castle.NewsManagement", id = 0 }).LocalNav().Permission(Permissions.NewsAdmin)));
            builder.Add(T("新闻列表"), "1.6",
             menu =>
             {
                foreach(var newsgroup in newsgroups)
                 {
                     menu.Add(T(newsgroup.GroupName), "2.0", item => item.Action("List", "Admin", new { area = "Castle.NewsManagement", newsgroupid = newsgroup.Id }).LocalNav().Permission(Permissions.NewsAdmin));
                };
             }
             );

        }

    }

}



       