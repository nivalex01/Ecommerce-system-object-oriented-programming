namespace Ecommerce_store_gui
{
    partial class StoreSellerAndBuyersDataForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvSellers = new System.Windows.Forms.DataGridView();
            this.dgvBuyers = new System.Windows.Forms.DataGridView();
            this.btnShowSellersData = new System.Windows.Forms.Button();
            this.btnShowBuyersData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSellers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuyers)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSellers
            // 
            this.dgvSellers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSellers.Location = new System.Drawing.Point(29, 273);
            this.dgvSellers.Name = "dgvSellers";
            this.dgvSellers.Size = new System.Drawing.Size(721, 518);
            this.dgvSellers.TabIndex = 0;
            // 
            // dgvBuyers
            // 
            this.dgvBuyers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBuyers.Location = new System.Drawing.Point(838, 273);
            this.dgvBuyers.Name = "dgvBuyers";
            this.dgvBuyers.Size = new System.Drawing.Size(721, 518);
            this.dgvBuyers.TabIndex = 1;
            // 
            // btnShowSellersData
            // 
            this.btnShowSellersData.Font = new System.Drawing.Font("Gabriola", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowSellersData.Location = new System.Drawing.Point(212, 73);
            this.btnShowSellersData.Name = "btnShowSellersData";
            this.btnShowSellersData.Size = new System.Drawing.Size(254, 122);
            this.btnShowSellersData.TabIndex = 2;
            this.btnShowSellersData.Text = "Show Sellers Data";
            this.btnShowSellersData.UseVisualStyleBackColor = true;
            this.btnShowSellersData.Click += new System.EventHandler(this.btnShowSellersData_Click);
            // 
            // btnShowBuyersData
            // 
            this.btnShowBuyersData.Font = new System.Drawing.Font("Gabriola", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowBuyersData.Location = new System.Drawing.Point(1052, 73);
            this.btnShowBuyersData.Name = "btnShowBuyersData";
            this.btnShowBuyersData.Size = new System.Drawing.Size(254, 122);
            this.btnShowBuyersData.TabIndex = 3;
            this.btnShowBuyersData.Text = "Show Buyers Data";
            this.btnShowBuyersData.UseVisualStyleBackColor = true;
            this.btnShowBuyersData.Click += new System.EventHandler(this.btnShowBuyersData_Click);
            // 
            // StoreSellerAndBuyersDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(1584, 887);
            this.Controls.Add(this.btnShowBuyersData);
            this.Controls.Add(this.btnShowSellersData);
            this.Controls.Add(this.dgvBuyers);
            this.Controls.Add(this.dgvSellers);
            this.Name = "StoreSellerAndBuyersDataForm";
            this.Text = "StoreSellerAndBuyersDataForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSellers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuyers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSellers;
        private System.Windows.Forms.DataGridView dgvBuyers;
        private System.Windows.Forms.Button btnShowSellersData;
        private System.Windows.Forms.Button btnShowBuyersData;
    }
}