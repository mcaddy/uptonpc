<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Twitter.ascx.cs" Inherits=" UptonParishCouncil.Site.News.Twitter" %>

    <script type="text/javascript">
        var oldForEachFunction = Array.forEach,
            undefined;
        Array.forEach = undefined;
    </script>

<script src="http://widgets.twimg.com/j/2/widget.js" type="text/javascript" />

    <script type="text/javascript">
        Array.forEach = oldForEachFunction;
    </script>

<script type="text/javascript">
new TWTR.Widget(
{ version: 2, type: 'profile', rpp: 4, interval: 2000, width: 500, height: 100,
 theme: {
    shell: { background: '#ffffff', color: '#0C6D0C' },
    tweets: { background: '#ffffff', color: '#000000', links: '#E82C21' }
  },
  features: { scrollbar: true, loop: false, live: true, hashtags: true, timestamp: true, avatars: false, behavior: 'all' }
}
).render().setUser('uptonpc').start();
</script>

<span style="font-size:small; font-style:italic;">
To have your comments feature above incude the hashtag #uptonpc in your post.
</span>

<span style="font-size:small; font-style:italic;">
The Twitter comments posted above are the opinion of the comment writer, not that of Upton Parish Council.
</span>