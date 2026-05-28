# Mini SAP - Retail and Stock Management Platform

## 1. Introduction
Mini SAP is a comprehensive, web-based software application designed for multi-store retail businesses to manage their inventory, product assignments, and inter-store stock transfers seamlessly. It offers a secure, role-based ecosystem connecting Store Managers and System Administrators.

## 2. Code Libraries & Components

### Backend Architecture
The backend is built with **.NET 8 (ASP.NET Core Web API)** using a clean architecture approach, dividing responsibilities into API, Business (Application), and Data layers.
- **Entity Framework Core (EF Core)**: Used as the ORM to manage database schemas and perform SQL queries through C# objects. It connects to a **SQL Server (LocalDB)** instance.
- **MediatR**: Implements the CQRS (Command Query Responsibility Segregation) pattern. All requests (Queries and Commands) are sent to Handlers, ensuring the controllers remain thin and business logic is strictly decoupled.
- **JWT (JSON Web Tokens)**: Handles stateless, secure authentication. Users are assigned roles (`Admin` or `StoreManager`), which govern their access to specific endpoints.
- **BCrypt.Net**: Used for securely hashing user passwords in the database.
- **Swagger (Swashbuckle)**: Provides an interactive UI for testing and documenting the API endpoints.

### Frontend Architecture
The frontend is a lightweight, high-performance **Vanilla JavaScript, HTML, and CSS** application. 
- **Component-based UI via Vanilla JS**: Relies on dynamic DOM manipulation to render grids, tables, and modals without the overhead of heavy frameworks like React or Angular.
- **Responsive CSS (Mobile Support)**: Utilizes Flexbox, CSS Grid, and `@media` queries to ensure the platform is perfectly usable on smartphones and tablets.
- **Glassmorphism Design**: Custom UI utilizing backdrop-filters, modern typography (`Inter` font), and hover animations to create a premium, user-friendly interface.
- **JWT LocalStorage**: The frontend stores the JWT token in `localStorage` and automatically attaches it to the `Authorization: Bearer` header of every `fetch` request.

## 3. How to Initialize the Project

Follow these steps to run the project locally on your machine:

### Step 1: Database Setup
1. Open SQL Server Management Studio (SSMS) or Azure Data Studio and connect to your local server (e.g., `(localdb)\MSSQLLocalDB`).
2. Run the `SeedData.sql` file to create the `RetailAndStockManagement` database and initialize the Store, Users, and Roles tables.
3. Run the **`GüncelFotolarVeUrunler.sql`** file. This script is fully updated and contains all the high-quality photos, actual product names, and pricing for the 51 current products in the catalog. It will automatically insert or update existing products without breaking existing tables.

### Step 2: Running the Backend
1. Open the solution `RetailAndStockManagement.API.sln` using **Visual Studio 2022**.
2. Make sure `RetailAndStockManagement.API` is set as the Startup Project.
3. Verify the connection string inside `appsettings.json` points to your active local DB.
4. Press `F5` (or click "Start Debugging") to run the backend. 
5. The Swagger UI will automatically open in your browser, confirming the API is active.

### Step 3: Running the Frontend
1. The frontend consists of static HTML files (`login.html`, `anasayfa.html`, `admin.html`).
2. You can simply double-click `login.html` to open it in your browser.
3. **Important:** The backend must be running in the background for the frontend to authenticate and fetch data.

### Step 4: Login Credentials
Use the following default accounts to test the system:
- **Admin Panel:** Username: `admin`, Password: `admin123`
- **Store Manager (Adana):** Username: `manager1`, Password: `123456`
- **Store Manager (Ankara):** Username: `manager2`, Password: `123456`

## 4. Key Features (Demonstration Checklist)
- [x] **Role-based Dashboards:** Distinct interfaces for Admins (User & Product management) and Store Managers (Stock & Transfers).
- [x] **Product Assignment:** Admins can assign specific products and sizes to different branch stores.
- [x] **Inter-store Transfers:** Store managers can search for products using keywords/barcodes, request stock from other stores, and approve incoming requests.
- [x] **Advertisements:** Integrated banner ads on main pages.
- [x] **Mobile Responsiveness:** Layouts adapt seamlessly to smaller screens.
- [x] **Image Previews:** Clickable thumbnails that enlarge in a dark-mode modal.
