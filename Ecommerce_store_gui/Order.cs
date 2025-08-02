using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_store_gui
{
    public class SingleItemOrderException : Exception
    {
        public SingleItemOrderException(string message) : base(message) { }

    }

    public class Order : ICloneable
    {
        private static int nextOrderId = 1;
        private int orderId;
        private List<Product> product_list;
        private int total_price;
        private Buyer buyer_details;

        public Order(Buyer buyer_details) // Order constructor
        {
            this.orderId = GetNextOrderId();
            BuyerDetails = buyer_details;
            product_list = new List<Product>();
            total_price = 0;
        }

        public int OrderID
        {
            get { return orderId; }
        }

        private static int GetNextOrderId()
        {
            return nextOrderId++;
        }

        public Buyer BuyerDetails
        {
            get { return buyer_details; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(BuyerDetails), "Buyer details cannot be null.");
                }
                buyer_details = value;
            }
        }

        public int TotalPrice
        {
            get { return total_price; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(TotalPrice), "Total price cannot be negative.");
                }
                total_price = value;
            }
        }

        public List<Product> ProductList
        {
            get { return product_list; }
        }

        public void AddProductToOrder(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            }

            // Add the product to the product list
            product_list.Add(product);

            // Update the total price
            TotalPrice += product.ProductPrice;

            // If the product is a special product, add the packaging fee to the total price
            if (product is SpecialProduct specialProduct)
            {
                TotalPrice += specialProduct.PackagingFee;
            }
        }

        public void ValidateOrder()
        {
            if (product_list.Count == 1)
            {
                throw new SingleItemOrderException("Order cannot contain only one product.");
            }
        }

        public object Clone()
        {
            Order clonedOrder = new Order(this.BuyerDetails);

            // Clone the product list
            clonedOrder.product_list = new List<Product>(this.product_list);

            clonedOrder.total_price = this.total_price;

            return clonedOrder;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Order other = (Order)obj;

            return OrderID == other.OrderID &&
                   TotalPrice == other.TotalPrice &&
                   BuyerDetails.Equals(other.BuyerDetails) &&
                   ProductList.SequenceEqual(other.ProductList);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Order Details:");
            result.AppendLine($"Order ID: {OrderID}");
            result.AppendLine($"Buyer: {BuyerDetails}");
            result.AppendLine("Products:");
            foreach (var product in ProductList)
            {
                result.AppendLine(product.ToString());
            }
            result.AppendLine($"Total Price: {TotalPrice}");
            return result.ToString();
        }
        public override int GetHashCode()
        {
            return Tuple.Create(product_list, total_price, buyer_details).GetHashCode();
        }

    }
}
