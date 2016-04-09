using System.Collections.Generic;
using System.Linq;
using Tweetinvi;
using Tweetinvi.Core.Credentials;
using Tweet = Xeromatic.Models.Tweet;

namespace Xeromatic.Services
{
    public class TwitterApiService : ITwitterService
    {
        // Get keys from: https://apps.twitter.com
        // Wiki for tweetinvi: https://github.com/linvi/tweetinvi/wiki

        readonly TwitterCredentials _creds;

        public TwitterApiService()
        {
            _creds = new TwitterCredentials
            {
                ConsumerKey = "r1GXVFf5TyeMHxAebwhPdPdHQ",
                ConsumerSecret = "8JiB5vYAB4zQWRjOw09xgoJQZ5zJk12iFjy9AxLAGdQGDzQH97",
                AccessToken = "14047222-EqjZFAGFPsQ4MgVUlIszuc48bUsa9gzmYOCOJa07I",
                AccessTokenSecret = "VpUiegbJFau3srchduacXQQWMwSh4ZkFGxEzt1tVs0hOp"
            };
        }

        public IEnumerable<Tweet> GetTweets()
        {
            var tweets = Auth.ExecuteOperationWithCredentials(_creds, () => Timeline.GetUserTimeline("xero", 10))?.ToList();

            if (tweets != null && tweets.Any())
            {
                return tweets.Select(t =>
                    new Tweet()
                    {
                        Id = t.Id,
                        Text = t.Text
                    });
            }

            return new List<Tweet>();
        }

    }
}