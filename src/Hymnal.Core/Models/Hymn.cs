using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Hymnal.Core.Models
{
    public class Hymn
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// Language Id <see cref="HymnalLanguage.Id"/>
        /// </summary>
        public string HymnalLanguageId { get; set; }

        public string PlainContent => Content.Replace("\n", " ");

        public string[] ListContent => Content.Replace("\n\n","¶").Split('¶');
    }
}
