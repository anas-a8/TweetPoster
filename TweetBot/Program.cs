using System;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;

class Program
{
    static async Task Main()
    {
        // ✅ Pre-obtained Twitter API credentials (OAuth 1.0a access tokens)
        string API_KEY = "AJcH5OVobLUX2ubjrCjoEYkc6";
        string API_SECRET = "anwshPXsbudaR06mYWbEe9PnF2b60TUx4Q28bo2RAtbyVYIQwG";
        string ACCESS_TOKEN = "1157101405473333248-dDn2VqWuJijQvPLoRlwIyunfLGfqT4";
        string ACCESS_TOKEN_SECRET = "wMyxKl7dEr2dlMXM0dTv2t7l6syk756qkUIicadXezSzf";

        // ✅ Initialize Twitter client with credentials
        var userClient = new TwitterClient(API_KEY, API_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);

        try
        {
            // ✅ Get the authenticated user's information
            var user = await userClient.Users.GetAuthenticatedUserAsync();
            if (user == null)
            {
                Console.WriteLine("❌ Authentication failed.");
                return;
            }

            Console.WriteLine($"✅ Logged in as {user.Name} (@{user.ScreenName}). Now clearly posting tweet...");

            // ✅ Post a tweet using Twitter API v2
            var tweetResponse = await userClient.Execute.AdvanceRequestAsync(
                (ITwitterRequest request) =>
                {
                    request.Query.Url = "https://api.twitter.com/2/tweets";
                    request.Query.HttpMethod = Tweetinvi.Models.HttpMethod.POST;
                    request.Query.HttpContent = new StringContent("{\"text\":\"Hello Twitter from C# and Tweetinvi! 🚀 #FirstTweet #TwitterAPI\"}",
                                                                  System.Text.Encoding.UTF8,
                                                                  "application/json");
                });

            // ✅ Check if tweet was posted successfully
            if (tweetResponse.Response.IsSuccessStatusCode)
            {
                Console.WriteLine("✅ Tweet posted successfully!");
                Console.WriteLine(tweetResponse.Content);
            }
            else
            {
                Console.WriteLine("❌ Failed to post tweet.");
                Console.WriteLine(tweetResponse.Content);
            }
        }
        catch (Exception ex)
        {
            // ❌ Catch and display any exceptions
            Console.WriteLine("❌ Exception caught:");
            Console.WriteLine(ex.Message);
        }
    }
}
