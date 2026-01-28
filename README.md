# CustomerServiceApp

## 📌 Overview

CustomerServiceApp is a **SOAP-based ASP.NET Web Service (ASMX)** developed using **C# and ADO.NET**. The application provides basic **Customer Management (CRUD)** functionality and demonstrates a **custom token-based authentication mechanism** for securing sensitive operations.

This project is mainly built for **learning and practice purposes**, focusing on SOAP services, database connectivity, and API-level security.

---

## 🛠️ Technology Stack

* **Language:** C#
* **Framework:** ASP.NET (.NET Framework)
* **Web Service Type:** SOAP (ASMX)
* **Database:** SQL Server
* **Data Access:** ADO.NET (`SqlConnection`, `SqlCommand`)
* **Authentication:** Custom Token-Based Authentication
* **Configuration:** `web.config`

---

## 📂 Project Structure

```
CustomerServiceApp/
│
├── CustomerService.asmx.cs   # Customer CRUD operations
├── AuthService.asmx.cs       # Authentication & token generation
├── Web.config                # Database connection string
├── CustomerServiceApp.slnx   # Solution file
```

---

## 🔐 Authentication Flow

1. User logs in using **AuthService**
2. Server generates a **Base64 encoded token**
3. Client sends the token in the HTTP header:

   ```
   Authorization: Bearer <token>
   ```
4. Token is validated before performing protected operations

---

## 🌐 Web Services & Methods

### 🔑 AuthService

#### `Login(string username, string password)`

* Validates user credentials (currently hardcoded)
* Generates and returns a token on success
* Returns `INVALID` on failure

**Sample Credentials (for testing):**

```
Username: admin
Password: admin123
```

---

### 👤 CustomerService

#### `GetAllCustomers()`

* Retrieves all customer records from the database
* Returns a list of formatted customer details

#### `CreateCustomer(string name, string email)`

* Inserts a new customer into the database
* Uses parameterized queries to prevent SQL Injection

#### `UpdateCustomer(int id, string name, string email)`

* **Protected Method**
* Requires valid token in Authorization header
* Updates customer details if token is valid

#### `DeleteCustomer(int id)`

* Deletes a customer record by ID
* Returns status message based on result

---

## 🧾 Database Schema

```sql
CREATE TABLE Customers (
    Id INT IDENTITY PRIMARY KEY,
    Name VARCHAR(100),
    Email VARCHAR(100)
);
```

---

## 🔐 Security Features

* Token validation before update operations
* Parameterized SQL queries
* Authorization via HTTP headers

---

## ⚠️ Limitations

* Hardcoded user credentials
* Token is Base64 encoded (not encrypted)
* No token expiry validation
* SOAP-based (legacy approach)

---

## 🚀 Future Enhancements

* Replace SOAP with REST (ASP.NET Web API)
* Implement JWT authentication
* Add database-based user manag
