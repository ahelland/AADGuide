using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;

/// <summary>
/// Build a list of SyndicationItems for the RSS Feed
/// </summary>
public class FeedList
{
    /// <summary>
    /// Manual approach for now until better a one is sorted out :)
    /// </summary>
    /// <returns></returns>
    public static List<SyndicationItem> GetFeedItems()
    {
        //var feedItem01 = new SyndicationItem
        //{
        //    Title = TextSyndicationContent.CreatePlaintextContent("Hello World"),
        //    PublishDate = new DateTime(2015, 06, 24),
        //    Content = TextSyndicationContent.CreateHtmlContent("Foo ... <a href='http://foo'>http://foo</a>")

        //};
        //Might not need the links if put directly in the content
        //SyndicationLink link01 = new SyndicationLink(new Uri("http://foo"));
        //link01.Title = "Foo";
        //link01.MediaType = "text/html";
        //feedItem01.Links.Add(link01);

        var feedItem02 = new SyndicationItem
        {
            Title = SyndicationContent.CreatePlaintextContent("WebAPISingleTenant"),
            PublishDate = new DateTime(2015, 08, 03),
            Content = SyndicationContent.CreateHtmlContent("How to protect a web api with Azure AD, and combine it with authentication for the API help page. ... <a href='http://aadguide.azurewebsites.net/integration/webapisingletenant/'>http://aadguide.azurewebsites.net/integration/webapisingletenant/</a>")
        };

        var feedItem01 = new SyndicationItem
        {
            Title = SyndicationContent.CreatePlaintextContent("GraphTreeView (Windows Forms)"),
            PublishDate = new DateTime(2015, 06, 25),
            Content = SyndicationContent.CreateHtmlContent("How to build a Windows Forms app for retrieving groups and users from Azure AD, and build a treeview to present the results in a hierarchical view. ... <a href='http://aadguide.azurewebsites.net/integration/graphtreeview/'>http://aadguide.azurewebsites.net/integration/graphtreeview/</a>")
        };


        List<SyndicationItem> feedItems = new List<SyndicationItem>();
        feedItems.Add(feedItem01);
        feedItems.Add(feedItem02);

        return feedItems;
    }
}