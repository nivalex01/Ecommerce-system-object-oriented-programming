using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Ecommerce_store_gui
{
    public partial class StoreSellerAndBuyersDataForm : Form
    {
        private EcommerceStore store;
        public StoreSellerAndBuyersDataForm(EcommerceStore store)
        {
            InitializeComponent();
            this.store = store; // Assign the store passed from StoreGui to the local store field
            dgvSellers.Visible = false;
            dgvBuyers.Visible = false;
            // Define columns for dgvSellers
            dgvSellers.Columns.Add("Username", "Username");
            dgvSellers.Columns.Add("StreetName", "Street Name");
            dgvSellers.Columns.Add("NumberOfBuilding", "Building Number");
            dgvSellers.Columns.Add("CityName", "City");
            dgvSellers.Columns.Add("CountryName", "Country");
            dgvSellers.Columns.Add("ProductsList", "Products List"); // Products list column

            // Define columns for dgvBuyers
            dgvBuyers.Columns.Add("Username", "Username");
            dgvBuyers.Columns.Add("StreetName", "Street Name");
            dgvBuyers.Columns.Add("NumberOfBuilding", "Building Number");
            dgvBuyers.Columns.Add("CityName", "City");
            dgvBuyers.Columns.Add("CountryName", "Country");
            dgvBuyers.Columns.Add("ShoppingCart", "Shopping Cart"); // Shopping cart column

            LoadStoreData();
        }
        private void LoadStoreData()
        {
            // Clear existing rows
            dgvSellers.Rows.Clear();
            dgvBuyers.Rows.Clear();

            // Load seller data into DataGridView
            foreach (var seller in store.GetSellers())
            {
                // Add row for seller
                int rowIndex = dgvSellers.Rows.Add(seller.Username, seller.Address.StreetName, seller.Address.NumberOfBuilding, seller.Address.CityName, seller.Address.CountryName);

                // Add products list as a cell value in the "Products List" column
                DataGridViewCell productsCell = dgvSellers.Rows[rowIndex].Cells["ProductsList"];
                productsCell.Value = string.Join(", ", seller.SellerProducts.Select(p => p.ProductName));
            }

            // Load buyer data into DataGridView
            foreach (var buyer in store.GetBuyers())
            {
                // Add row for buyer
                int rowIndex = dgvBuyers.Rows.Add(buyer.Username, buyer.Address.StreetName, buyer.Address.NumberOfBuilding, buyer.Address.CityName, buyer.Address.CountryName);

                // Add shopping cart as a cell value in the "Shopping Cart" column
                DataGridViewCell cartCell = dgvBuyers.Rows[rowIndex].Cells["ShoppingCart"];
                cartCell.Value = string.Join(", ", buyer.ShoppingCart.Select(p => p.ProductName));
            }
        }

        private void btnShowSellersData_Click(object sender, EventArgs e)
        {
            // Show seller data in DataGridView
            dgvSellers.Visible = true;
            dgvBuyers.Visible = false;
        }

        private void btnShowBuyersData_Click(object sender, EventArgs e)
        {
            dgvSellers.Visible = false;
            dgvBuyers.Visible = true;
        }
    }
}
