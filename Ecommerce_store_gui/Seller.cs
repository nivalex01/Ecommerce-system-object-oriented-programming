using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_store_gui
{
    public class Seller : User, IComparable<Seller>
    {
        private List<Product> seller_products;

        // Seller constructor
        public Seller() : base()
        {
            SellerProducts = new List<Product>();
        }

        // Constructor to initialize the seller properties
        public Seller(string seller_username, string seller_password, Address seller_address)
            : base(seller_username, seller_password, seller_address)
        {
            SellerProducts = new List<Product>();
        }

        public Seller(Seller other)
            : base(other.Username, other.Password, other.Address) // copy constructor
        {
            SellerProducts = new List<Product>(other.SellerProducts);
        }

        public List<Product> SellerProducts
        {
            get { return seller_products; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("Seller products cannot be null.");
                seller_products = value;
            }
        }

        // Function to get the list of products
        public Product[] GetSellerProductList()
        {
            return SellerProducts.ToArray();
        }

        // Function to add a product to the seller's product list
        public void AddToProductList(Product product)
        {
            // Check if the product already exists in the seller's product list
            if (SellerProducts.Any(existingProduct => existingProduct != null && existingProduct.Equals(product)))
            {
                Console.WriteLine("Product already exists in the seller's product list.");
                return;
            }

            // Add the product to the seller's product list
            SellerProducts.Add(product);
        }

        public bool SearchProductIfItExists(string name_of_product_to_find)
        {
            return SellerProducts.Any(product => product != null && product.ProductName == name_of_product_to_find);
        }

        public Product FindProductByName(string productName)
        {
            return SellerProducts.FirstOrDefault(product => product != null && product.ProductName.Equals(productName));
        }

        public void PrintSellerProducts()
        {
            Console.WriteLine(ToString());
            if (SellerProducts.Count == 0)
            {
                Console.WriteLine("Seller has no products.");
                return;
            }

            Console.WriteLine("Seller Products:");
            foreach (var product in SellerProducts)
            {
                if (product != null)
                {
                    Console.WriteLine($"{product.ToString()}");
                    Console.WriteLine("-------------------");
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Seller other = (Seller)obj;

            return Username.Equals(other.Username) &&
                   Password.Equals(other.Password) &&
                   Address.Equals(other.Address) &&
                   SellerProducts.SequenceEqual(other.SellerProducts);
        }

        public int CompareTo(Seller other)
        {
            // compare sellers based on the number of products they sell
            return SellerProducts.Count.CompareTo(other.SellerProducts.Count);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(base.GetHashCode(), seller_products).GetHashCode();
        }

    }
}
