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

        List<SyndicationItem> feedItems = new List<SyndicationItem>();
        //feedItems.Add(feedItem01);

        return feedItems;
    }
}