using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Post
    {
        int postId;
        User sender;
        DateTime postDateTime;
        string content;
        int numOfLikes;

        public int PostId { get => postId; set => postId = value; }
        public User Sender { get => Sender; set => Sender = value; }
        public DateTime PostDateTime { get => postDateTime; set => postDateTime = value; }
        public string Content { get => content; set => content = value; }
        public int NumOfLikes { get => numOfLikes; set => numOfLikes = value; }

        public Post(int postId, User sender, DateTime postDateTime, string content, int numOfLikes)
        {
            PostId = postId;
            Sender = sender;
            PostDateTime = postDateTime;
            Content = content;
            NumOfLikes = numOfLikes;
        }

    }
}