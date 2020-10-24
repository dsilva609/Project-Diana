using System.Collections.Generic;

namespace Project.Diana.ApiClient.Features.Discogs
{
    public class Community
    {
        public int have { get; set; }
        public int want { get; set; }
    }

    public class DiscogsSearchResult
    {
        public Pagination pagination { get; set; }
        public IEnumerable<SearchResult> results { get; set; }
    }

    public class Format
    {
        public IEnumerable<string> descriptions { get; set; }
        public string name { get; set; }
        public string qty { get; set; }
        public string text { get; set; }
    }

    public class Pagination
    {
        public int items { get; set; }
        public int page { get; set; }
        public int pages { get; set; }
        public int per_page { get; set; }
        public Urls urls { get; set; }
    }

    public class SearchResult
    {
        public IEnumerable<string> barcode { get; set; }
        public string catno { get; set; }
        public Community community { get; set; }
        public string country { get; set; }
        public string cover_image { get; set; }
        public IEnumerable<string> format { get; set; }
        public int format_quantity { get; set; }
        public IEnumerable<Format> formats { get; set; }
        public IEnumerable<string> genre { get; set; }
        public int id { get; set; }
        public IEnumerable<string> label { get; set; }
        public int master_id { get; set; }
        public string master_url { get; set; }
        public string resource_url { get; set; }
        public IEnumerable<string> style { get; set; }
        public string thumb { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public User_Data user_data { get; set; }
        public int year { get; set; }
    }

    public class Urls
    {
        public string last { get; set; }
        public string next { get; set; }
    }

    public class User_Data
    {
        public bool in_collection { get; set; }
        public bool in_wantlist { get; set; }
    }
}