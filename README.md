# 🛍️ Ecommerce Store System – C# OOP Project

This is a multi-part academic project developed in C# for building a fully object-oriented E-commerce platform. Throughout the development of this system, we gradually applied theoretical and practical concepts from object-oriented programming, exception handling, collections, operator overloading, file operations, and GUI design.

The project simulates a simplified yet functional online marketplace similar to platforms like Amazon or eBay, where **buyers** can browse products and place orders, and **sellers** can manage product inventories.

![Store vid](https://github.com/user-attachments/assets/5b8c0e12-b042-40fa-a9af-7b0cd5d7a973)


We implemented the project in **five structured stages**, each focusing on a different set of software engineering principles and technologies:

### 🔧 Technologies and Concepts Used

- **C# (.NET Framework)** – Main programming language used for both backend logic and GUI components.
- **OOP (Object-Oriented Programming)** – Heavy use of classes, inheritance (`User → Buyer/Seller`), encapsulation, and polymorphism to create reusable and extensible code.
- **Collections (`List<T>`)** – Replacing static arrays with dynamic lists for more scalable data handling.
- **Custom Exceptions** – Defined and handled domain-specific exceptions (e.g., invalid inputs, invalid orders).
- **Operator Overloading** – Enhanced readability and control logic by overloading `+` and `<` operators for users and cart comparisons.
- **Interfaces** – Used interfaces like `ICloneable` and `IComparable` to implement cloning of past orders and sorting of sellers by number of products.
- **Enum Types** – Applied enumerations to represent product categories for cleaner and safer code.
- **File I/O** – Saved and loaded persistent seller/product data using `.txt` files, simulating a mini-database.
- **WinForms (Windows Forms GUI)** – Built a graphical interface for interacting with the system using buttons, input forms, grids, and error indicators.
- **Visual Studio** – Full project created, compiled, and tested using the Visual Studio IDE.


## 📦 Project Overview

The system allows:
- Managing buyers and sellers
- Managing products and shopping carts
- Creating and tracking orders
- Handling special products with packaging
- Cloning past orders
- GUI-based user interaction with persistent storage

---

## 📂 Features by Project Part

### ✅ Part 1 – Basic Functionality
- Console-based menu for:
  - Adding buyers/sellers
  - Adding products to sellers
  - Adding products to buyer carts
  - Checking out carts (placing orders)
  - Viewing buyer/seller details

### 🚀 Part 2 – Inheritance, Polymorphism, Enums & Static Members
- Introduced abstract `User` base class (with `Buyer` and `Seller` inheriting from it)
- Created `SpecialProduct` class (with `starsRanking` and `packagingFee`)
- Static field for auto-incrementing product IDs
- Enum-based category system (e.g., Kids, Electronics, Office, Clothing)
- Product arrays allow polymorphism: mix of regular and special products

### 🧠 Part 3 – Lists, Exceptions, Operator Overloading
- Migrated from arrays to `List<T>` collections
- Input validation and exception handling:
  - Name/address cannot contain numbers or special characters
  - Building numbers must be non-negative
- Custom exception: `SingleItemOrderException` (prevents ordering single item)
- Operator overloading:
  - `+` to add users (buyers/sellers) to the store
  - `<` to compare buyers by total cart price
- Cloneable order system (`ICloneable`) allows reusing past orders

### 💾 Part 4 – File I/O (Persistence)
- Seller and product data saved to `sellers_data.txt` on exit
- Loaded automatically on startup
- File located in: `bin\Debug\sellers_data.txt`

### 🖥️ Part 5 – GUI with WinForms
- Built using Windows Forms
- Features:
  - Add seller / buyer
  - Add product to seller / buyer
  - Display all store data (sellers, buyers, products)
- Uses `GroupBox`, `DataGridView`, and `ErrorProvider` controls
- Input validation and error messages shown via GUI
- GUI Form: `StoreGui.cs`

---

## 🧪 Input Validation Highlights

- Buyer/Seller names must contain only letters
- Street and city names must not include numbers or special characters
- Product categories selected by number and mapped to enums
- Exception-safe throughout

---

## 🏗️ Project Architecture

The project follows a layered, object-oriented architecture with a clear separation of concerns between **core business logic**, **data management**, and the **user interface**.

### **1. Core Classes (Business Logic Layer)**
Located in the `core_classes/` folder:
- **User (abstract)** – Base class for all users, containing shared attributes like `Username`, `Password`, and `Address`.
- **Buyer** – Inherits from `User`. Manages:
  - `List<Product>` shopping cart
  - `List<Order>` past purchases
  - Methods for adding products, checking out, and comparing total cart value.
- **Seller** – Inherits from `User`. Manages:
  - `List<Product>` inventory
  - Comparable by the number of products (via `IComparable<Seller>`).
- **Product (base)** – Represents standard products with attributes `ProductID`, `Name`, `Price`, and `Category` (enum).
- **SpecialProduct** – Extends `Product` with additional fields `PackagingFee` and `StarsRanking`.
- **Order** – Represents a buyer's purchase, including order details and total price. Implements `ICloneable` for order duplication.
- **EcommerceStore** – The main manager class that holds all `Buyers` and `Sellers`, implements operations like adding users/products, sorting sellers, and retrieving store data.

---

### **2. Data Layer**
- **Persistence** – The `SaveSellersToFile()` and `LoadSellersFromFile()` methods serialize and deserialize seller and product data to/from `sellers_data.txt`.
- **File Location** – All data files are stored in `bin/Debug/` during runtime.

---

### **3. Application Layer**
- **Program Entry Point** – Initially a console menu (`Main`) for text-based interactions, handling:
  - Adding users/products
  - Checking out and displaying data
  - Validating inputs with exceptions.

---

### **4. Presentation Layer (GUI)**
- Built using **WinForms** (`StoreGui.cs`):
  - Buttons for all key operations (add buyer/seller, add products, view data).
  - `DataGridView` for displaying lists of sellers, buyers, and their products.
  - Input validation via `ErrorProvider`.
  - Dynamic form sections (`GroupBox`) for user-friendly data entry.

---

### **5. Utilities**
- **Enums** – Define product categories for type safety and readability.
- **Operator Overloading** – Implements `+` and `<` operators for intuitive operations on buyers, sellers, and carts.
- **Exception Classes** – Custom domain-specific exception handling (e.g., `SingleItemOrderException`).
