using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;

public class RatingService : IRatingService
{
    private IRatingRepository _ratingRepository;

    public RatingService(IRatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    public int AddRating(Rating rating)
    {
        return _ratingRepository.AddRating(rating);
    }
}
