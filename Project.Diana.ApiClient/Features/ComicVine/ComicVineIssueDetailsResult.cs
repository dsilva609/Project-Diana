using System.Collections.Generic;

namespace Project.Diana.ApiClient.Features.ComicVine
{
    public class CharacterCredits
    {
        public string api_detail_url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string site_detail_url { get; set; }
    }

    public class ComicVineIssueDetailsResult
    {
        public string error { get; set; }
        public int limit { get; set; }
        public int number_of_page_results { get; set; }
        public int number_of_total_results { get; set; }
        public int offset { get; set; }
        public Results results { get; set; }
        public int status_code { get; set; }
        public string version { get; set; }
    }

    public class ConceptCredits
    {
        public string api_detail_url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string site_detail_url { get; set; }
    }

    public class DetailImage
    {
        public string icon_url { get; set; }
        public string image_tags { get; set; }
        public string medium_url { get; set; }
        public string original_url { get; set; }
        public string screen_large_url { get; set; }
        public string screen_url { get; set; }
        public string small_url { get; set; }
        public string super_url { get; set; }
        public string thumb_url { get; set; }
        public string tiny_url { get; set; }
    }

    public class DetailVolume
    {
        public string api_detail_url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string site_detail_url { get; set; }
    }

    public class PersonCredits
    {
        public string api_detail_url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string role { get; set; }
        public string site_detail_url { get; set; }
    }

    public class Results
    {
        public object aliases { get; set; }
        public string api_detail_url { get; set; }
        public List<CharacterCredits> character_credits { get; set; }
        public List<ConceptCredits> concept_credits { get; set; }
        public string cover_date { get; set; }
        public string date_added { get; set; }
        public string date_last_updated { get; set; }
        public string deck { get; set; }
        public string description { get; set; }
        public object first_appearance_characters { get; set; }
        public object first_appearance_concepts { get; set; }
        public object first_appearance_locations { get; set; }
        public object first_appearance_objects { get; set; }
        public object first_appearance_storyarcs { get; set; }
        public object first_appearance_teams { get; set; }
        public bool has_staff_review { get; set; }
        public int id { get; set; }
        public DetailImage image { get; set; }
        public string issue_number { get; set; }
        public object name { get; set; }
        public List<PersonCredits> person_credits { get; set; }
        public string site_detail_url { get; set; }
        public string store_date { get; set; }
        public DetailVolume volume { get; set; }
    }
}