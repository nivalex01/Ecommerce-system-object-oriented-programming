using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_store_gui
{
        public class EcommerceStore
        {
            private string name;
            private List<User> usersList;

            public EcommerceStore(string name)
            {
                this.name = name;
                usersList = new List<User>();
            }

            public List<User> UsersList
            {
                get { return usersList; }
                set
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException(nameof(UsersList), "User list cannot be null.");
                    }
                    usersList = value;
                }
            }

            public string Name
            {
                get { return name; }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentException("Store name cannot be null or empty.", nameof(Name));
                    }
                    name = value;
                }
            }

            // Define the + operator to add a Buyer to the store
            public static EcommerceStore operator +(EcommerceStore store, Buyer buyer)
            {
                if (store == null)
                {
                    throw new ArgumentNullException(nameof(store), "Store instance cannot be null.");
                }

                if (buyer == null)
                {
                    throw new ArgumentNullException(nameof(buyer), "Buyer instance cannot be null.");
                }

                if (store.IsUserAlreadyExists(buyer.Username))
                {
                    Console.WriteLine("User with the same username already exists.");
                    return store;
                }

                store.AddUserToStore(buyer);
                Console.WriteLine("Buyer added successfully.");
                return store;
            }

            // Define the + operator to add a Seller to the store
            public static EcommerceStore operator +(EcommerceStore store, Seller seller)
            {
                if (store == null)
                {
                    throw new ArgumentNullException(nameof(store), "Store instance cannot be null.");
                }

                if (seller == null)
                {
                    throw new ArgumentNullException(nameof(seller), "Seller instance cannot be null.");
                }

                if (store.IsUserAlreadyExists(seller.Username))
                {
                    Console.WriteLine("User with the same username already exists.");
                    return store;
                }

                store.AddUserToStore(seller);
                Console.WriteLine("Seller added successfully.");
                return store;
            }

            // Adds a user (can be buyer or seller) to the store
            private void AddUserToStore(User user)
            {
                if (user == null)
                {
                    return;
                }

                usersList.Add(user);
            }

            public void PrintUsersArrayDetails()
            {
                Console.WriteLine("Users array details:");
                foreach (User user in usersList)
                {
                    if (user != null)
                    {
                        Console.WriteLine(user.ToString());
                        Console.WriteLine();
                    }
                }
                Console.WriteLine("The printing is completed.");
            }

            public void PrintBuyersArrayDetails()
            {
                Console.WriteLine("Buyers array Details:");
                foreach (User user in usersList)
                {
                    if (user is Buyer buyer)
                    {
                        Console.WriteLine($"Username: {buyer.Username}");
                        Console.WriteLine($"Address: {buyer.Address}");
                        Console.WriteLine();
                    }
                }
                Console.WriteLine("The printing is completed.");
            }

            public void PrintSellersArrayDetails()
            {
                Console.WriteLine("Sellers array Details:");

                // Sort the sellers array based on the number of products they sell
                List<Seller> sellersList = usersList.OfType<Seller>().OrderByDescending(s => s.SellerProducts.Count).ToList();

                // Print sorted sellers details
                foreach (Seller seller in sellersList)
                {
                    Console.WriteLine($"Username: {seller.Username}");
                    Console.WriteLine($"Address: {seller.Address}");
                    Console.WriteLine($"Number of products: {seller.SellerProducts.Count}");
                    Console.WriteLine();
                }
                Console.WriteLine("Printing completed.");
            }

            // Adds a product to the seller's product list
            public void AddProductToSeller(string username, Product product)
            {
                Seller seller = FindSellerByUsername(username);

                if (seller == null)
                {
                    Console.WriteLine("Seller not found.");
                    return;
                }

                seller.AddToProductList(product);
            }

            // Adds a product to the buyer's shopping cart
            public void AddProductToBuyersCart(string username, string productName)
            {
                Buyer buyer = FindBuyerByUsername(username);

                if (buyer == null)
                {
                    Console.WriteLine("Buyer not found.");
                    return;
                }

                Seller seller = FindSellerByProduct(productName);

                if (seller == null)
                {
                    Console.WriteLine("There is no seller selling this product.");
                    return;
                }

                Product product = seller.FindProductByName(productName);

                if (product == null)
                {
                    Console.WriteLine("Product not found.");
                    return;
                }

                buyer.AddProductToShoppingCart(product);
                Console.WriteLine("Product added successfully to the buyer's cart.");
            }

            // Checkout for the buyer
            public void CheckoutForBuyer(string username)
            {
                Buyer buyer = FindBuyerByUsername(username);

                if (buyer == null)
                {
                    Console.WriteLine("Buyer not found.");
                    return;
                }

                Console.WriteLine("Buyer's Shopping Cart:");
                foreach (Product product in buyer.ShoppingCart)
                {
                    if (product != null)
                    {
                        Console.WriteLine(product.ToString());
                    }
                }

                int totalPrice = buyer.CalculateTotalPrice();
                Console.WriteLine($"Total Price: {totalPrice}");

                buyer.BuyTheShoppingCart();
            }

            // Prints buyer's shopping cart
            public void PrintBuyerShoppingCart(string username)
            {
                Buyer buyer = FindBuyerByUsername(username);

                if (buyer == null)
                {
                    Console.WriteLine("Buyer not found.");
                    return;
                }

                buyer.PrintCurrentShoppingCart();
            }

            // Prints seller's product list
            public void PrintSellerProductsList(string username)
            {
                Seller seller = FindSellerByUsername(username);

                if (seller == null)
                {
                    Console.WriteLine("Seller not found.");
                    return;
                }

                seller.PrintSellerProducts();
            }

            public Seller FindSellerByUsername(string username)
            {
                foreach (User user in usersList)
                {
                    if (user is Seller seller && seller.Username == username)
                    {
                        return seller;
                    }
                }
                return null;
            }

            public Buyer FindBuyerByUsername(string username)
            {
                foreach (User user in usersList)
                {
                    if (user is Buyer buyer && buyer.Username == username)
                    {
                        return buyer;
                    }
                }
                return null;
            }

            // Views past purchases of the buyer
            public void ViewPastPurchases(string username)
            {
                Buyer buyer = FindBuyerByUsername(username);

                if (buyer == null)
                {
                    Console.WriteLine("Buyer not found.");
                    return;
                }

                Console.WriteLine($"The Past Purchases of Buyer: {buyer.Username}");
                buyer.PrintPastPurchases();
            }

            public void CloneCartFromLastPurchases(string buyerUsername, int orderId)
            {
                Buyer buyer = FindBuyerByUsername(buyerUsername);
                if (buyer == null)
                {
                    Console.WriteLine("Buyer not found.");
                    return;
                }

                Order orderToClone = buyer.FindOrderById(orderId); //find order that we want to clone
                if (orderToClone == null)
                {
                    Console.WriteLine($"Order with ID {orderId} not found in past purchases for buyer {buyerUsername}.");
                    return;
                }

                try
                {
                    // Clone the order by calling the Clone function from ICloneable interface
                    Order clonedOrder = (Order)orderToClone.Clone();

                    // Add the cloned products to the buyer's current shopping cart
                    foreach (Product product in clonedOrder.ProductList)
                    {
                        AddProductToBuyersCart(buyerUsername, product.ProductName);
                    }
                    buyer.BuySpecificOrder(clonedOrder);
                    Console.WriteLine("Shopping cart cloned and added successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error cloning shopping cart: {ex.Message}");
                }
            }

            public void CompareBuyers(string username1, string username2)
            {
                Buyer buyer1 = FindBuyerByUsername(username1);
                Buyer buyer2 = FindBuyerByUsername(username2);

                if (buyer1 == null || buyer2 == null)
                {
                    throw new ArgumentException("One or both of the buyers was not found");
                }
                Console.WriteLine($"Total price in {buyer1.Username}'s shopping cart: {buyer1.CalculateTotalPrice()}");
                Console.WriteLine($"Total price in {buyer2.Username}'s shopping cart: {buyer2.CalculateTotalPrice()}");
                if (buyer1 > buyer2)
                {
                    Console.WriteLine($"{buyer1.Username} has a higher total price in his shopping cart than {buyer2.Username}.");
                }
                else if (buyer1 < buyer2)
                {
                    Console.WriteLine($"{buyer2.Username} has a higher total price in his shopping cart than {buyer1.Username}.");
                }
                else
                {
                    Console.WriteLine($"{buyer1.Username} and {buyer2.Username} have the same total price in their shopping carts.");
                }
            }

            // Saves sellers to a file
            public void SaveSellersToFile(string fileName)
            {
                string TXTfilePath = Path.Combine(Directory.GetCurrentDirectory(), fileName + ".txt");

                try
                {
                    using (StreamWriter sw = new StreamWriter(TXTfilePath))
                    {
                        foreach (var user in usersList)
                        {
                            if (user is Seller seller)
                            {
                                // Write basic seller information
                                sw.WriteLine($"Username: {seller.Username}, password: {seller.Password}, Address: {seller.Address}, Number of Products: {seller.SellerProducts.Count}");

                                // Iterate over each product in the seller's product list
                                foreach (var product in seller.SellerProducts)
                                {
                                    // Construct product line with common details
                                    StringBuilder productLine = new StringBuilder();
                                    productLine.Append($"  - Product Name: {product.ProductName}, ");
                                    productLine.Append($"Product ID: {product.ProductId}, ");
                                    productLine.Append($"Price: {product.ProductPrice}, ");
                                    productLine.Append($"Type: {product.GetType().Name}, "); // Get the type name
                                    productLine.Append($"Category: {product.CategoryOfProduct}");

                                    // Check if the product is special
                                    if (product is SpecialProduct specialProduct)
                                    {
                                        productLine.Append($", Packaging Fee: {specialProduct.PackagingFee}, ");
                                        productLine.Append($"Stars Ranking: {specialProduct.StarsRanking}");
                                    }

                                    // Write the constructed product line to the file
                                    sw.WriteLine(productLine.ToString());
                                }

                                // Add an empty line between sellers for readability
                                sw.WriteLine();
                            }
                        }
                        sw.Close();
                    }

                    Console.WriteLine($"Sellers' data saved to the file: {TXTfilePath}");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error saving sellers' data: {ex.Message}");
                }
            }
            public void LoadSellersFromFile(string fileName)
            {
                string TXTfilePath = Path.Combine(Directory.GetCurrentDirectory(), fileName + ".txt");

                try
                {
                    using (StreamReader sr = new StreamReader(TXTfilePath))
                    {
                        string line;
                        Seller currentSeller = null;

                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.StartsWith("Username:"))
                            {
                                // Extract seller information
                                string[] sellerInfo = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                string username = sellerInfo[0].Split(':')[1].Trim();
                                string password = sellerInfo[1].Split(':')[1].Trim();
                                string addressLine = string.Join(",", sellerInfo.Skip(2)).Trim();

                                // Parse address details
                                string streetName = ParseAddressComponent(addressLine, "Street Name:");
                                int buildingNumber = ParseAddressComponentAsInt(addressLine, "Number of Building:");
                                string cityName = ParseAddressComponent(addressLine, "City:");
                                string countryName = ParseAddressComponent(addressLine, "Country:");

                                // Create address instance
                                Address sellerAddress = new Address(streetName, buildingNumber, cityName, countryName);

                                int numberOfProducts = int.Parse(sellerInfo[sellerInfo.Length - 1].Split(':')[1].Trim());

                                // Create a new seller instance
                                currentSeller = new Seller(username, password, sellerAddress);

                                // Add the seller to the store if not already added
                                if (!IsUserAlreadyExists(username))
                                {
                                    AddUserToStore(currentSeller);
                                }
                            }
                            else if (line.StartsWith("  - Product Name:"))
                            {
                                // Extract product information
                                string[] productInfo = line.Trim().Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                                string productName = productInfo[0].Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1];
                                int productId = int.Parse(productInfo[1].Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1]);
                                int productPrice = int.Parse(productInfo[2].Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1]); // Assuming productPrice is an int
                                string productType = productInfo[3].Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1];
                                string productCategoryStr = productInfo[4].Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1];

                                // Parse the product category
                                if (!Enum.TryParse(productCategoryStr, out Product.ProductCategory productCategory))
                                {
                                    // Default category if parsing fails
                                    productCategory = Product.ProductCategory.Electricity;
                                }

                                // Check if the product is special
                                if (productInfo.Length > 5 && productInfo[5].StartsWith("Packaging Fee"))
                                {
                                    int packagingFee = int.Parse(productInfo[5].Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1]);
                                    int starsRanking = int.Parse(productInfo[6].Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1]);

                                    // Create SpecialProduct instance
                                    SpecialProduct specialProduct = new SpecialProduct(productName, productPrice, productCategory, starsRanking, packagingFee);
                                    currentSeller.AddToProductList(specialProduct);
                                }
                                else
                                {
                                    // Create Product instance
                                    Product regularProduct = new Product(productName, productPrice, productCategory);
                                    currentSeller.AddToProductList(regularProduct);
                                }
                            }
                            else if (line == "")
                            {
                                // End of seller's products section, reset currentSeller
                                currentSeller = null;
                            }
                        }
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error loading sellers' data: {ex.Message}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error parsing data from file: {ex.Message}");
                }
            }
        // Returns list of all sellers in the store
        public List<Seller> GetSellers()
        {
            return usersList.OfType<Seller>().ToList();
        }

        // Returns list of all buyers in the store
        public List<Buyer> GetBuyers()
        {
            return usersList.OfType<Buyer>().ToList();
        }

        private string ParseAddressComponent(string addressLine, string componentKey)
            {
                string componentValue = "";
                if (addressLine.Contains(componentKey))
                {
                    componentValue = addressLine.Split(new string[] { componentKey }, StringSplitOptions.None)[1].Split(',')[0].Trim();
                }
                return componentValue;
            }

            private int ParseAddressComponentAsInt(string addressLine, string componentKey)
            {
                int componentValue = 0;
                if (addressLine.Contains(componentKey))
                {
                    string componentString = addressLine.Split(new string[] { componentKey }, StringSplitOptions.None)[1].Split(',')[0].Trim();
                    int.TryParse(componentString, out componentValue);
                }
                return componentValue;
            }

            public override string ToString()
            {
                return $"Store Name: {name}\nTotal Users: {usersList.Count}";
            }


            // Private functions used only in this class
            private bool IsUserAlreadyExists(string username)
            {
                foreach (User user in usersList)
                {
                    if (user != null && user.Username == username)
                    {
                        return true;
                    }
                }
                return false;
            }

            public Seller FindSellerByProduct(string productName)
            {
                foreach (User user in usersList)
                {
                    if (user is Seller seller && seller.SearchProductIfItExists(productName))
                    {
                        return seller;
                    }
                }
                return null;
            }


        }
    }
