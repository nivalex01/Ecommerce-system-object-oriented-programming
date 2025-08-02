using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_store_gui
{
    public class SpecialProduct : Product
    {
        private int starsRanking; // Stars ranking attribute for special product
        private int packaging_fee; // Packaging fee attribute for special product

        public SpecialProduct(string product_name, int product_price, ProductCategory category_of_product, int starsRanking, int packaging_fee)
            : base(product_name, product_price, category_of_product)
        {
            StarsRanking = starsRanking;
            PackagingFee = packaging_fee;
        }

        public SpecialProduct(SpecialProduct other) : base(other) // Copy constructor
        {
            StarsRanking = other.StarsRanking;
            PackagingFee = other.PackagingFee;
        }

        public int StarsRanking
        {
            get { return starsRanking; }
            set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentException("Stars ranking must be between 1 and 5.");
                }
                starsRanking = value;
            }
        }

        public int PackagingFee
        {
            get { return packaging_fee; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Packaging fee cannot be negative.");
                }
                packaging_fee = value;
            }
        }

        public override string ToString()
        {
            return base.ToString() + $"\nPackaging Fee: {PackagingFee}\nStars Ranking: {StarsRanking}";
        }
    }
}
