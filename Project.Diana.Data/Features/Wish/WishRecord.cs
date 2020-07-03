using System;

namespace Project.Diana.Data.Features.Wish
{
    public class WishRecord
    {
        public string ApiID { get; set; }
        public string Category { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public int ID { get; set; }
        public string ImageUrl { get; set; }
        public int ItemType { get; set; } //--TODO: add enum
        public string Notes { get; set; }
        public bool Owned { get; set; }
        public string Title { get; set; }
        public string UserID { get; set; }
    }
}