using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingAPI.Core.Domain
{
    public class AverageRatingObject
    {
        public string TitleId { get; set; }
        public double Avarage { get; set; }

        public AverageRatingObject()
        {

        }
        public AverageRatingObject(string titleId, double avarage)
        {
            TitleId = titleId;
            Avarage = avarage;
        }
    }
}
