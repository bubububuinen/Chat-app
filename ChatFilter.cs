public void ProcessRequest (HttpContext context) 
{

    // ****************************************
    if (context.Request["postform"] == "1")
    {

        videomessage myVideoMessage = new videomessage();

        myVideoMessage.video_id = context.Request["video_id"];
        myVideoMessage.first_name_submitter = context.Request["first_name_submitter"];
        myVideoMessage.last_initial_submitter = context.Request["last_initial_submitter"];
        myVideoMessage.message = context.Request["message"];
        myVideoMessage.status = "0";

        myVideoMessage.Save();
    }
    // ****************************************

    // ****************************************
    StringBuilder myStringBuilder = new StringBuilder();


    // PULL VIDEOMESSAGES FOR VIDEO_ID
    videomessage[] myCommentsList = new videomessage().Listing("video_id", context.Request["video_id"], "entry_date" , "DESC");

    // FORM COMMENTS IF MORE THAN ONE COMMENT EXISTS
    foreach (videomessage tmpMessage in myCommentsList)
    {
        if (tmpMessage.status == "0" || tmpMessage.status == "1")
        {
            myStringBuilder.Append("<div class=\"comment_box\">");
            myStringBuilder.Append("<p class=\"comment_date\">");
            myStringBuilder.Append(Utility.FormatShortDate(tmpMessage.entry_date) + " " + tmpMessage.first_name_submitter + " " + tmpMessage.last_initial_submitter + "." + "</p>");

            if (!String.IsNullOrEmpty(tmpMessage.message))
            {
                myStringBuilder.Append("<p>" + tmpMessage.message + "</p>");
                myStringBuilder.Append("</div>");
            }
        }
    }
    string return_str = myStringBuilder.ToString();

    // IF NO COMMENTS RETURN THIS
    if( String.IsNullOrEmpty(return_str) )  return_str = "<p>No comments currently exist for this video.</p>";
    // ****************************************

    // RETURN STRING        
    context.Response.ContentType = "text/plain";
    context.Response.Write(return_str);
  public class ProfanityFilter
{        
    // METHOD: containsProfanity
    public bool containsProfanity(string checkStr)
    {
        bool badwordpresent = false;

        string[] inStrArray = checkStr.Split(new char[] { ' ' });

        string[] words = this.profanityArray();

        // LOOP THROUGH WORDS IN MESSAGE
        for (int x = 0; x < inStrArray.Length; x++)
        {
            // LOOP THROUGH PROFANITY WORDS
            for (int i = 0; i < words.Length; i++)
            {
                // IF WORD IS PROFANITY, SET FLAG AND BREAK OUT OF LOOP
                //if (inStrArray[x].toString().toLowerCase().equals(words[i]))
                if( inStrArray[x].ToLower() == words[i].ToLower() )
                {
                    badwordpresent = true;
                    break;
                }
            }
            // IF FLAG IS SET, BREAK OUT OF OUTER LOOP
            if (badwordpresent == true) break;
        }

        return badwordpresent;
    }
    // ************************************************************************
