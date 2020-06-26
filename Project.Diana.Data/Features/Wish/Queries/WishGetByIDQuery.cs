using Kledex.Queries;

namespace Project.Diana.Data.Features.Wish.Queries
{
    public class WishGetByIDQuery : IQuery<string>
    {
        public int UserID { get; }
        public int WishId { get; }

        public WishGetByIDQuery()
        {
            //Guard.Against.Default(userID, nameof(userID));
            //Guard.Against.Default(wishID, nameof(wishID));

            UserID = 1;
            WishId = 1;
        }
    }
}