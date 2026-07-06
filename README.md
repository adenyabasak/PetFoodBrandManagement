# Pet Food Brand Management System

## Overview

The Pet Food Brand Management System is a comprehensive web application developed using **ASP.NET Core MVC**, **Entity Framework Core Code First**, and the **Unit of Work** design pattern. The project was designed to provide a complete management platform for pet food brands, products, customer orders, reviews, and website content while following modern software architecture principles.

The application follows a **layered architecture**, separating the project into **UI**, **Data**, and **Model** layers. Repository Pattern and Unit of Work were implemented together to create a clean, maintainable, and scalable application structure. Interfaces were used throughout the project to reduce dependency between layers and support loose coupling, making future maintenance and development significantly easier.

The system includes separate administrator and user interfaces. Users can register, log into the system, browse pet food products, create orders, leave product reviews, and access their personal order history. Session-based authentication is used to manage user access and authorization.

The administration panel provides complete CRUD (Create, Read, Update, Delete) functionality for managing brands, categories, products, customer orders, reviews, and website content. Administrators can efficiently control every part of the application through a modern Bootstrap-based dashboard.

The project also includes advanced management features such as product search, statistical reports, PDF document generation, Microsoft Excel export, dashboard statistics, and QR Code generation. LINQ queries were used to generate meaningful reports from the SQL Server database, allowing administrators to monitor business information more effectively.

Entity Framework Core Code First was used to create and maintain the SQL Server database. Relationships between entities were established using navigation properties, providing a well-structured and scalable database design.

Overall, this project demonstrates practical experience with ASP.NET Core MVC, Entity Framework Core, Repository Pattern, Unit of Work, layered architecture, interface-based programming, SQL Server, authentication, reporting, document generation, and responsive web application development. It represents a complete enterprise-style management system developed according to modern software engineering practices.

---

## Technologies

- ASP.NET Core MVC
- C#
- Entity Framework Core (Code First)
- SQL Server
- Unit of Work Pattern
- Generic Repository Pattern
- Layered Architecture
- Interface-Based Programming
- Bootstrap 5
- HTML5
- CSS3
- JavaScript
- LINQ

---

## NuGet Packages

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.Design
- ClosedXML
- QuestPDF
- QRCoder

---

## Features

- Responsive Home Page
- User Registration
- User Login
- Session-Based Authentication
- Admin Dashboard
- Brand Management (CRUD)
- Category Management (CRUD)
- Product Management (CRUD)
- Order Management (CRUD)
- Review Management (CRUD)
- Search Functionality
- Dashboard Statistics
- LINQ Reports
- PDF Export
- Excel Export
- QR Code Generation
- SQL Server Integration
- Repository Pattern
- Unit of Work Pattern
- Layered Architecture
- Interface-Based Development

---

# Project Screenshots

## Home Page

![Home](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/home.png)

The home page introduces visitors to the application with a responsive interface and provides easy access to products and website content.

---

## Products

![Products](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/urunler.png)

Displays all available pet food products in a modern card layout.

---

## Product Categories

![Categories](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/kategori.png)

Administrators can manage product categories through complete CRUD operations.

---

## Brands

![Brands](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/markalar.png)

Provides full management of pet food brands.

---

## User Registration

![Register](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/userkayit.png)

Allows new users to create accounts before accessing the application.

---

## Administrator Login

![Admin Login](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/admingiris.png)

Secure administrator login page using session-based authentication.

---

## Dashboard

![Dashboard](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/dashboard.png)

Displays important business statistics and system summaries.

---

## Admin Products

![Admin Products](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/adminurunler.png)

Administrators can create, update, delete, and manage all products.

---

## Customer Orders

![Orders](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/siparisler.png)

Displays all customer orders for management purposes.

---

## My Orders

![My Orders](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/siparislerim.png)

Users can view their own order history after logging into the system.

---

## Customer Reviews

![Reviews](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/yorumlar.png)

Customers can publish reviews, while administrators can manage all submitted comments.

---

## Admin Reviews

![Admin Reviews](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/adminyorumlar.png)

Dedicated management page for controlling customer reviews.

---

## Reports

![Reports](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/raporlar.png)

Displays statistical reports generated using LINQ queries.

---

## PDF Export

![PDF](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/pdf.png)

Exports application data into professionally formatted PDF documents.

---

## Excel Export

![Excel](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/excel.png)

Allows administrators to export reports into Microsoft Excel format.

---

## QR Code

![QR Code](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/karekod.png)

Generates QR Codes for application data using the QRCoder library.

---

## News Section

![News](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/haberler.png)

Administrators can manage news and announcements displayed on the website.

---

## Home News

![Home News](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/homehaber.png)

Latest news is presented to visitors on the home page.

---

## Contact Page

![Contact](https://github.com/adenyabasak/PetFoodBrandManagement/blob/master/images/iletisim.png)

Provides contact information and communication details for visitors.

---

## Purpose

This project was developed to gain practical experience with enterprise-level ASP.NET Core MVC application development by implementing Entity Framework Core Code First, Repository Pattern, Unit of Work, layered architecture, interface-based programming, SQL Server integration, authentication, reporting, PDF and Excel document generation, QR Code generation, and responsive web application design.
