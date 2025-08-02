using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Ecommerce_store_gui
{
    public partial class StoreGui : Form
    {
        private EcommerceStore store;
        public StoreGui()
        {
            InitializeComponent();
            store = new EcommerceStore("niv and dan store"); // initialize new store instance
            store.LoadSellersFromFile("sellers_data"); // load the sellers from the file 'sellers_data'
            this.FormClosing += new FormClosingEventHandler(StoreGui_FormClosing);
            rbSpecial.CheckedChanged += new EventHandler(rbSpecial_CheckedChanged); // update the radio buttons
            rbNotSpecial.CheckedChanged += new EventHandler(rbNotSpecial_CheckedChanged); // update the radio buttons                                                                         
        }
        private void StoreGui_FormClosing(object sender, FormClosingEventArgs e)
        {
            store.SaveSellersToFile("sellers_data"); // save sellers to file before exiting
        }

        private void StoreGui_Load(object sender, EventArgs e)
        {
            gpAddSeller.Visible = false;
            gpAddBuyer.Visible = false;
            gpAddProductToSeller.Visible = false;
            gpAddProductToBuyer.Visible = false;
        }

        private void btnAddSellerOption_Click(object sender, EventArgs e)
        {
            gpAddSeller.Visible = true;
            gpAddBuyer.Visible = false;
            gpAddProductToSeller.Visible = false;
            gpAddProductToBuyer.Visible = false;
        }

        private void btnAddProductToSellerOption_Click(object sender, EventArgs e)
        {
            gpAddProductToSeller.Visible = true;
            gpAddSeller.Visible = false;
            gpAddBuyer.Visible = false;
            gpAddProductToBuyer.Visible = false;
        }

        private void btnAddProductToBuyerOption_Click(object sender, EventArgs e)
        {
            gpAddProductToBuyer.Visible = true;
            gpAddProductToSeller.Visible = false;
            gpAddSeller.Visible = false;
            gpAddBuyer.Visible = false;
        }

        private void btnAddByuerOption_Click(object sender, EventArgs e)
        {
            gpAddBuyer.Visible = true;
            gpAddSeller.Visible = false;
            gpAddProductToSeller.Visible = false;
            gpAddProductToBuyer.Visible = false;
        }

        private void btnAddSeller_Click(object sender, EventArgs e)
        {
            string sellerUsername = txtSellerUserName.Text;
            string sellerPassword = txtSellerPassword.Text;
            string sellerStreetName = txtSellerStreetName.Text;
            string sellerBuildingNumber = txtSellerBuildingNumber.Text;
            string sellerCity = txtSellerCity.Text;
            string sellerCountry = txtSellerCountry.Text;

            // Clear any previous errors
            ClearSellerErrorProviders();

            // Validate sellerUsername
            if (string.IsNullOrWhiteSpace(sellerUsername))
            {
                errorProviderSellerUserName.SetError(txtSellerUserName, "Please enter seller username.");
            }

            // Validate sellerPassword (if needed)
            if (string.IsNullOrWhiteSpace(sellerPassword))
            {
                errorProviderSellerPassword.SetError(txtSellerPassword, "Please enter seller password.");
            }

            // Validate sellerStreetName
            if (string.IsNullOrWhiteSpace(sellerStreetName))
            {
                errorProviderSellerStreetName.SetError(txtSellerStreetName, "Please enter street name.");
            }

            // Validate sellerBuildingNumber
            int buildingNumber;
            if (!int.TryParse(sellerBuildingNumber, out buildingNumber) || buildingNumber <= 0)
            {
                errorProviderSellerBuildingNumber.SetError(txtSellerBuildingNumber, "Please enter a valid positive integer for building number.");
            }

            // Validate sellerCity
            if (string.IsNullOrWhiteSpace(sellerCity))
            {
                errorProviderSellerCity.SetError(txtSellerCity, "Please enter city name.");
            }

            // Validate sellerCountry
            if (string.IsNullOrWhiteSpace(sellerCountry))
            {
                errorProviderSellerCountry.SetError(txtSellerCountry, "Please enter country name.");
            }

            // Check if any errors were set
            if (errorProviderSellerUserName.GetError(txtSellerUserName) != "" ||
                errorProviderSellerPassword.GetError(txtSellerPassword) != "" ||
                errorProviderSellerStreetName.GetError(txtSellerStreetName) != "" ||
                errorProviderSellerBuildingNumber.GetError(txtSellerBuildingNumber) != "" ||
                errorProviderSellerCity.GetError(txtSellerCity) != "" ||
                errorProviderSellerCountry.GetError(txtSellerCountry) != "")
            {
                MessageBox.Show("Please fix all errors before adding the seller.");
                return;
            }

            try
            {
                // Create a new Seller object
                Seller seller = new Seller(sellerUsername, sellerPassword, new Address(sellerStreetName, buildingNumber, sellerCity, sellerCountry));

                // Add the seller to the store using the + operator
                store += seller;

                MessageBox.Show("Seller added successfully!");
                ClearSellerForm();
                gpAddSeller.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ClearSellerErrorProviders()
        {
            errorProviderSellerUserName.SetError(txtSellerUserName, "");
            errorProviderSellerPassword.SetError(txtSellerPassword, "");
            errorProviderSellerStreetName.SetError(txtSellerStreetName, "");
            errorProviderSellerBuildingNumber.SetError(txtSellerBuildingNumber, "");
            errorProviderSellerCity.SetError(txtSellerCity, "");
            errorProviderSellerCountry.SetError(txtSellerCountry, "");
        }

        private void ClearSellerForm()
        {
            txtSellerUserName.Clear();
            txtSellerPassword.Clear();
            txtSellerStreetName.Clear();
            txtSellerBuildingNumber.Clear();
            txtSellerCity.Clear();
            txtSellerCountry.Clear();
        }

        private void btnAddBuyer_Click(object sender, EventArgs e)
        {
            string buyerUsername = txtBuyerUserName.Text;
            string buyerPassword = txtBuyerPassword.Text;
            string buyerStreetName = txtBuyerStreetName.Text;
            string buyerBuildingNumber = txtBuyerBuildingNumber.Text;
            string buyerCity = txtBuyerCity.Text;
            string buyerCountry = txtBuyerCountry.Text;

            // Clear any previous errors
            ClearErrorBuyerProviders();

            // Validate buyerUsername
            if (string.IsNullOrWhiteSpace(buyerUsername))
            {
                errorProviderBuyerUserName.SetError(txtBuyerUserName, "Please enter buyer username.");
            }

            // Validate buyerPassword (if needed)
            if (string.IsNullOrWhiteSpace(buyerPassword))
            {
                errorProviderBuyerPassword.SetError(txtBuyerPassword, "Please enter buyer password.");
            }

            // Validate buyerStreetName
            if (string.IsNullOrWhiteSpace(buyerStreetName))
            {
                errorProviderBuyerStreetName.SetError(txtBuyerStreetName, "Please enter street name.");
            }

            // Validate buyerBuildingNumber
            int buildingNumber;
            if (!int.TryParse(buyerBuildingNumber, out buildingNumber) || buildingNumber <= 0)
            {
                errorProviderBuildingNumber.SetError(txtBuyerBuildingNumber, "Please enter a valid positive integer for building number.");
            }

            // Validate buyerCity
            if (string.IsNullOrWhiteSpace(buyerCity))
            {
                errorProviderBuyerCity.SetError(txtBuyerCity, "Please enter city name.");
            }

            // Validate buyerCountry
            if (string.IsNullOrWhiteSpace(buyerCountry))
            {
                errorProviderBuyerCountry.SetError(txtBuyerCountry, "Please enter country name.");
            }

            // Check if any errors were set
            if (errorProviderBuyerUserName.GetError(txtBuyerUserName) != "" ||
                errorProviderBuyerPassword.GetError(txtBuyerPassword) != "" ||
                errorProviderBuyerStreetName.GetError(txtBuyerStreetName) != "" ||
                errorProviderBuildingNumber.GetError(txtBuyerBuildingNumber) != "" ||
                errorProviderBuyerCity.GetError(txtBuyerCity) != "" ||
                errorProviderBuyerCountry.GetError(txtBuyerCountry) != "")
            {
                MessageBox.Show("Please fix all errors before adding the buyer.");
                return;
            }

            try
            {
                // Create a new Buyer object
                Buyer buyer = new Buyer(buyerUsername, buyerPassword, new Address(buyerStreetName, buildingNumber, buyerCity, buyerCountry));

                // Add the buyer to the store using the + operator
                store += buyer;

                MessageBox.Show("Buyer added successfully!");
                ClearBuyerForm();
                gpAddBuyer.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ClearErrorBuyerProviders()
        {
            errorProviderBuyerUserName.Clear();
            errorProviderBuyerPassword.Clear();
            errorProviderBuyerStreetName.Clear();
            errorProviderBuildingNumber.Clear();
            errorProviderBuyerCity.Clear();
            errorProviderBuyerCountry.Clear();
        }

        private void ClearBuyerForm()
        {
            txtBuyerUserName.Clear();
            txtBuyerPassword.Clear();
            txtBuyerStreetName.Clear();
            txtBuyerBuildingNumber.Clear();
            txtBuyerCity.Clear();
            txtBuyerCountry.Clear();
        }

        private void rbSpecial_CheckedChanged(object sender, EventArgs e)
        {
            bool isSpecial = rbSpecial.Checked;
            txtPackagingFee.Visible = isSpecial;
            txtStarsRanking.Visible = isSpecial;
            lblPackgingFee.Visible = isSpecial;
            lblStarsRanking.Visible = isSpecial;
            if (!isSpecial)
            {
                txtPackagingFee.Text = "";
                txtStarsRanking.Text = "";
            }
        }

        private void rbNotSpecial_CheckedChanged(object sender, EventArgs e)
        {
            bool isNotSpecial = rbNotSpecial.Checked;
            txtPackagingFee.Visible = !isNotSpecial; // Hide packaging fee if not special
            txtStarsRanking.Visible = !isNotSpecial; // Hide stars ranking if not special
            lblPackgingFee.Visible = !isNotSpecial;
            lblStarsRanking.Visible=!isNotSpecial;
            if (isNotSpecial)
            {
                txtPackagingFee.Text = "";
                txtStarsRanking.Text = "";
            }
        }

        private void btnAddProductToSeller_Click(object sender, EventArgs e)
        {
            // Clear previous errors
            ClearAddProductToSellerErrorProviders();

            // Validate seller username (assuming it's set elsewhere in your application)
            string sellerToaddProductUserName = txtSellerToaddProductUserName.Text;
            if (string.IsNullOrWhiteSpace(sellerToaddProductUserName))
            {
                errorProviderSellerToaddProductUserName.SetError(txtSellerToaddProductUserName, "Please select a seller first.");
                return;
            }

            // Validate if seller exists in the store
            Seller seller = store.FindSellerByUsername(sellerToaddProductUserName);
            if (seller == null)
            {
                errorProviderSellerToaddProductUserName.SetError(txtSellerToaddProductUserName, "Seller not found.");
                return;
            }

            // Validate product name
            string productName = txtProductName.Text;
            if (string.IsNullOrWhiteSpace(productName))
            {
                errorProviderProductName.SetError(txtProductName, "Please enter product name.");
                return;
            }

            // Validate product price
            if (!int.TryParse(txtProductPrice.Text, out int productPrice) || productPrice <= 0)
            {
                errorProviderProductPrice.SetError(txtProductPrice, "Please enter a valid positive decimal number for product price.");
                return;
            }

            // Validate product category
            if (!Enum.TryParse(txtProductCategory.Text, out Product.ProductCategory productCategory) || !Enum.IsDefined(typeof(Product.ProductCategory), productCategory))
            {
                errorProviderProductCategory.SetError(txtProductCategory, "Please enter a valid product category.");
                return;
            }

            // Validate special attributes if the product is marked as special
            bool isSpecial = rbSpecial.Checked;
            int packagingFee = 0;
            int starsRanking = 0;

            if (isSpecial)
            {
                if (!int.TryParse(txtPackagingFee.Text, out packagingFee) || packagingFee <= 0)
                {
                    errorProviderPackagingFee.SetError(txtPackagingFee, "Please enter a valid positive integer for packaging fee.");
                    return;
                }

                if (!int.TryParse(txtStarsRanking.Text, out starsRanking) || starsRanking < 1 || starsRanking > 5)
                {
                    errorProviderStarsRanking.SetError(txtStarsRanking, "Please enter a valid integer between 1 and 5 for stars ranking.");
                    return;
                }
            }

            // Check if any errors were set
            if (errorProviderSellerToaddProductUserName.GetError(txtSellerToaddProductUserName) != "" ||
                errorProviderProductName.GetError(txtProductName) != "" ||
                errorProviderProductPrice.GetError(txtProductPrice) != "" ||
                errorProviderProductCategory.GetError(txtProductCategory) != "" ||
                errorProviderPackagingFee.GetError(txtPackagingFee) != "" ||
                errorProviderStarsRanking.GetError(txtStarsRanking) != "")
            {
                MessageBox.Show("Please fix all errors before adding the product.");
                return;
            }

            try
            {
                // Add the product to the seller's list
                if (isSpecial)
                {
                    SpecialProduct specialProduct = new SpecialProduct(productName, productPrice, productCategory, starsRanking, packagingFee);
                    store.AddProductToSeller(sellerToaddProductUserName, specialProduct);
                    MessageBox.Show("Special Product added successfully to the seller.");
                }
                else
                {
                    Product product = new Product(productName, productPrice, productCategory);
                    store.AddProductToSeller(sellerToaddProductUserName, product);
                    MessageBox.Show("Product added successfully to the seller.");
                }

                // Clear form and hide group box
                ClearAddProductToSellerForm();
                gpAddProductToSeller.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ClearAddProductToSellerForm()
        {
            txtSellerToaddProductUserName.Clear();
            txtProductName.Clear();
            txtProductPrice.Clear();
            txtProductCategory.Clear();
            txtPackagingFee.Clear();
            txtStarsRanking.Clear();
            rbNotSpecial.Checked = true; // Reset radio buttons
        }

        private void ClearAddProductToSellerErrorProviders()
        {
            errorProviderSellerToaddProductUserName.Clear();
            errorProviderProductName.Clear();
            errorProviderProductPrice.Clear();
            errorProviderProductCategory.Clear();
            errorProviderPackagingFee.Clear();
            errorProviderStarsRanking.Clear();
        }

        private void btnAddProductToBuyer_Click(object sender, EventArgs e)
        {
            // take buyer username and product name from input fields
            string buyerUsername = txtBuyerToAddName.Text.Trim();
            string productName = txtProductToAddToBuyerName.Text.Trim();

            // clear any previous errors
            ClearErrorProviders();

            try
            {
                // check if the buyer exists
                Buyer buyer = store.FindBuyerByUsername(buyerUsername);
                if (buyer == null)
                {
                    // Buyer not found, show error using ErrorProvider
                    errorProviderBuyerToAddName.SetError(txtBuyerToAddName, "Buyer not found.");
                    throw new ArgumentException("Buyer not found.");
                }

                // Check if the product is being sold by any seller
                Seller seller = store.FindSellerByProduct(productName);
                if (seller == null)
                {
                    // Product not found or not being sold, show error using ErrorProvider
                    errorProviderProductToAddToBuyerName.SetError(txtProductToAddToBuyerName, "Product not found or not available.");
                    throw new ArgumentException("Product not found or not available.");
                }

                // Call the method to add product to buyer's cart
                store.AddProductToBuyersCart(buyerUsername, productName);
                MessageBox.Show($"Product '{productName}' added to buyer '{buyerUsername}' shopping cart successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAddProductToBuyerForm(); // Clear the input fields
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product to buyer's cart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearErrorProviders()
        {
            // Clear error providers
            errorProviderBuyerToAddName.Clear();
            errorProviderProductToAddToBuyerName.Clear();
        }

        private void ClearAddProductToBuyerForm()
        {
            // clear the input fields after successful addition
            txtBuyerToAddName.Text = "";
            txtProductToAddToBuyerName.Text = "";

            // clear error providers
            ClearErrorProviders();
        }

        private void btnShowStoreDataOption_Click(object sender, EventArgs e)
        {
            StoreSellerAndBuyersDataForm storeDataForm = new StoreSellerAndBuyersDataForm(store);
            storeDataForm.ShowDialog();
        }

    }
}