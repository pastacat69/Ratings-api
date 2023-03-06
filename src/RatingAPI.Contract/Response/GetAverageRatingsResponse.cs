using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingAPI.Contract.Response
{
    public class GetAverageRatingsResponse
    {
        public IEnumerable<AverageRatingsObject> TopRatings { get; set; }

        public GetAverageRatingsResponse(IEnumerable<AverageRatingsObject> topRating)
        {
            TopRatings = topRating; 
        }
    }

    public class AverageRatingsObject 
    {
        public string TitleId { get; set; }
        public double AverageRating { get; set; }
        public AverageRatingsObject(string titleId, double averageRating)
        {
            TitleId = titleId;
            AverageRating = Math.Round(averageRating, 2);
        }
    }
}
