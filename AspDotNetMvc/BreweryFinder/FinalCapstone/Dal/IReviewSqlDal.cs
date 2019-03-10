using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Models;

namespace FinalCapstone.Dal
{
    public interface IReviewSqlDal
    {
        List<Review> GetReviews(int beerId);
        double GetAverageRating(int beerId);
        bool NewReview(Review review);
    }
}
