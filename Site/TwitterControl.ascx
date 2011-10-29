<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TwitterControl.ascx.cs" Inherits="UptonParishCouncil.Site.TwitterControl" %>

<div id="twitter" style="min-width:200px; max-width:350px;"></div>
<div style="width:350px; clear:both;">
                <span style="font-size:xx-small; font-style:italic;">
To have your comments feature above incude the hashtag #uptonpc in your post.
</span>

<span style="font-size:xx-small; font-style:italic;">
The Twitter comments posted above are the opinion of the comment writer, not that of Upton Parish Council.
</span>
</div>
<script type="text/javascript">
    function twitterSearchCallback(obj) {
        var twitterDiv = document.getElementById('twitter');
        twitterDiv.innerHTML += '<div style="clear:left; border-bottom:1px solid gray; margin-bottom:4px;"/>';
        for (i = 0; i < obj.results.length; i++) {
            twitterDiv.innerHTML += '<div style="clear:left;">';
            twitterDiv.innerHTML += '<span style="float:left; padding-right:4px;"><img src="' + obj.results[i].profile_image_url + '" alt="userImage"></span>';
            twitterDiv.innerHTML += '<span style="font-weight:bold;"><a href="http://twitter.com/' + obj.results[i].from_user + '">' + obj.results[i].from_user + '</a></span> : ' + obj.results[i].text + '<br />';
            twitterDiv.innerHTML += '<div style="text-align:right; font-size:smaller;">' + obj.results[i].created_at.replace('+0000', '') + '</div>';
            twitterDiv.innerHTML += '</div>';
            twitterDiv.innerHTML += '<div style="clear:left; border-bottom:1px solid gray; margin-bottom:4px;"/>';
        }
        if (obj.results.length == 0) {
            twitterDiv.innerHTML += '<div style="clear:left; border-bottom:1px solid gray; text-align:center;padding-bottom:4px; margin-bottom:4px;">No Recent Mentions</div>';
        }
        
    }

    function twitterSearch(searchTerm) 
    {
        var twitterJSON = document.createElement('script');
        twitterJSON.type = 'text/javascript';
        twitterJSON.src = 'http://search.twitter.com/search.json?callback=twitterSearchCallback&q=' + searchTerm;
        document.getElementsByTagName('head')[0].appendChild(twitterJSON);
        return false;
     }
</script>