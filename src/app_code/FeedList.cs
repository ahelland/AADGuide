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

        var feedItem07 = new SyndicationItem
        {
            Title = SyndicationContent.CreatePlaintextContent("Claims"),
            PublishDate = new DateTime(2016, 03, 17),
            Content = SyndicationContent.CreateHtmlContent("A guide for Claims. ... <a href='http://aadguide.azurewebsites.net/claims/'>http://aadguide.azurewebsites.net/claims/</a>")
        };

        var feedItem06 = new SyndicationItem
        {
            Title = SyndicationContent.CreatePlaintextContent("ADSF 2016"),
            PublishDate = new DateTime(2016, 01, 07),
            Content = SyndicationContent.CreateHtmlContent("A guide for ADFS 2016. ... <a href='http://aadguide.azurewebsites.net/adfs2016/'>http://aadguide.azurewebsites.net/adfs2016/</a>")
        };

        var feedItem05 = new SyndicationItem
        {
            Title = SyndicationContent.CreatePlaintextContent("AAD B2B"),
            PublishDate = new DateTime(2015, 11, 22),
            Content = SyndicationContent.CreateHtmlContent("A guide for Azure AD B2B. ... <a href='http://aadguide.azurewebsites.net/aadb2b/'>http://aadguide.azurewebsites.net/aadb2b/</a>")
        };

        var feedItem04 = new SyndicationItem
        {
            Title = SyndicationContent.CreatePlaintextContent("AAD B2C"),
            PublishDate = new DateTime(2015, 10, 27),
            Content = SyndicationContent.CreateHtmlContent("A guide for Azure AD B2C. ... <a href='http://aadguide.azurewebsites.net/aadb2c/aadb2cintro/'>http://aadguide.azurewebsites.net/aadb2c/aadb2cintro/</a>")
        };

        var feedItem03 = new SyndicationItem
        {
            Title = SyndicationContent.CreatePlaintextContent("RegisterAppv2"),
            PublishDate = new DateTime(2015, 08, 13),
            Content = SyndicationContent.CreateHtmlContent("How to register an app with the v2 model in Azure AD. ... <a href='http://aadguide.azurewebsites.net/integration/registerappv2/'>http://aadguide.azurewebsites.net/integration/registerappv2/</a>")
        };

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
        feedItems.Add(feedItem03);
        feedItems.Add(feedItem04);
        feedItems.Add(feedItem05);
        feedItems.Add(feedItem06);
        feedItems.Add(feedItem07);

        return feedItems;
    }
}