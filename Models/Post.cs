using System.Text.Json.Serialization;

namespace Turtl_TMlinaric.Models
{
    public class Post
    {
        [JsonPropertyName("userId")]
        public int userID { get; set; }
        
        [JsonPropertyName("id")]
        public int postID { get; set; }
        
        [JsonPropertyName("title")]
        public string title { get; set; }
        
        [JsonPropertyName("body")]
        public string body { get; set; }
    
    }
}