using Entities;

namespace Repository
{
    public interface IRatingRepository
    {
        int AddRating(Rating rating);
    }
}