# StoreCRUD

<div align="center">
<img src="https://user-images.githubusercontent.com/118696036/230460273-7d9336fb-2d1f-419b-9703-540906c9bc6a.png">
<img src="https://user-images.githubusercontent.com/118696036/230460293-58580ee6-f95a-4044-a7b5-e2426146291f.png">
<img src="https://user-images.githubusercontent.com/118696036/230460315-95bc8187-0711-41d1-a6be-0533d19bc6d0.png">
<img src="https://user-images.githubusercontent.com/118696036/230460331-96d862cd-3a1d-4bc0-a363-a465f9afe647.png">
<img src="https://user-images.githubusercontent.com/118696036/230460342-db464ad5-7804-4177-a4ec-5bf4fa9c0ceb.png">
<img src="https://user-images.githubusercontent.com/118696036/230628620-6358e750-3cf6-4674-b357-e23d2dad1d07.png" />
</div>

[Video](https://user-images.githubusercontent.com/118696036/230629997-39c9047b-5261-4d3b-beeb-da89c6ed1a4e.webm)


#
## 🌐 Status
<p>Finished project ✅</p>

#
## 🧰 Prerequisites

- .NET 6.0 or +

- Connection string to SQLServer in StoreCRUD/appsettings.json named as Default

#
## <img src="https://icon-library.com/images/database-icon-png/database-icon-png-13.jpg" width="20" /> Database

_Create a database in SQLServer that contains the table created from the following script:_
```sql
CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(80) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [UserName] nvarchar(80) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Role] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(80) NOT NULL,
    [Description] nvarchar(255) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
);
GO
```
### Relationships
```yaml
+--------------+        +-------------+        +--------------+
|   Categories | 1    * |    Products |        |     Users    |
+--------------+        +-------------+        +--------------+
|     Id       |<-------|      Id     |        |      Id      |
|     Title    |        |     Title   |        |   UserName   |
|              |        | Description |        |   Password   |
|              |        |    Price    |        |     Role     |
+--------------+        |  CategoryId |        +--------------+
                        +-------------+
```

#
## 🔧 Installation

`$ git clone https://github.com/lcsmota/StoreCRUD.git`

`$ cd StoreCRUD/`

`$ dotnet restore`

`$ dotnet run`

**Server listenning at  [https://localhost:7195/swagger](https://localhost:7195/swagger) or [https://localhost:7195/api/v1/Users](https://localhost:7195/api/v1/Users), [https://localhost:7195/api/v1/Products](https://localhost:7195/api/v1/Products) and [https://localhost:7195/api/v1/Categories](https://localhost:7195/api/v1/Categories)**

#

# 📫  Routes for Login

### Return all objects (Users)
```http
  GET https://localhost:7195/api/v1/Users
```
⚙️  **Status Code:**
```http
  (200) - OK
  (401) - Unauthorized
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230514508-bfd85110-621d-408d-ba7a-03ee43c18ae5.png" />
<img src="https://user-images.githubusercontent.com/118696036/230514810-d25827bb-8e97-43b9-8099-f3f64a17a872.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230514516-6f191283-8793-41b5-845e-d3af5ed9bfe0.png" />
<img src="https://user-images.githubusercontent.com/118696036/230514814-86a77717-05d3-4a42-ba31-f1b6b36ead7c.png" />

#
### Return only one object (User)

```http
  GET https://localhost:7195/api/v1/Users/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to view|

⚙️  **Status Code:**
```http
  (200) - OK
  (401) - Unauthorized
  (404) - Not Found
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230515094-8c06f693-1209-4028-ac7e-11eabbcfa3b8.png" />
<img src="https://user-images.githubusercontent.com/118696036/230515104-1543c21e-e2ee-47a2-902a-af85749ef3ef.png" />
<img src="https://user-images.githubusercontent.com/118696036/230515100-8f12fea8-14fd-4ca5-9e14-fa4920456b76.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230515243-d04025df-6a8a-4579-8cf7-7600e5f4bf0f.png" />
<img src="https://user-images.githubusercontent.com/118696036/230515258-b33e3d50-af3e-42e1-bfd4-a3070d7e98de.png" />
<img src="https://user-images.githubusercontent.com/118696036/230515253-f07fd6ab-4b21-4356-b476-91b29497a7b9.png" />

#
### Insert a new object (User)

```http
  POST https://localhost:7195/api/v1/Users
```
📨  **body:**
```json
{
  "userName": "maria",
  "password": "maria123456"
}
```

🧾  **response:**
```json
{
  "id": 1002,
  "userName": "maria",
  "password": "",
  "role": "employee"
}
```

⚙️  **Status Code:**
```http
  (200) - OK
  (400) - Bad Request
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230515812-b86bd17e-7caf-45bc-8dd8-50904bc1e7a9.png" />
<img src="https://user-images.githubusercontent.com/118696036/230515816-9fd9a97a-9f3b-42fe-be15-a4c913a37882.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230515546-58ab954b-26b2-4abc-8103-0fde2ae16e48.png" />
<img src="https://user-images.githubusercontent.com/118696036/230515553-962a4c76-8373-4293-93d8-fed3927df1e3.png" />
<img src="https://user-images.githubusercontent.com/118696036/230515557-649f3246-bb42-4802-8170-f51a1c432054.png" />
<img src="https://user-images.githubusercontent.com/118696036/230515564-f2d3cae2-4c75-4d84-8130-868ce4922fcd.png" />

#
### Login (User)

```http
  POST https://localhost:7195/api/v1/Users/login
```
📨  **body:**
```json
{
  "userName": "maria",
  "password": "M@r1a*&$123"
}
```

🧾  **response:**
```json
{
  "user": {
    "id": 1002,
    "userName": "maria",
    "password": "",
    "role": "employee"
  },
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMDAyIiwidW5pcXVlX25hbWUiOiJtYXJpYSIsInJvbGUiOiJlbXBsb3llZSIsIm5iZiI6MTY4MDgyOTk2MCwiZXhwIjoxNjgwODMxMTYwLCJpYXQiOjE2ODA4Mjk5NjB9.F4U_Xq5IX3nK8L0jnfy7FFYz0Alir_YM_RPRxK4RckE"
}
```

⚙️  **Status Code:**
```http
  (200) - OK
  (404) - Not Found
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230520753-34af326c-7941-44cb-a56b-3b486c5a7191.png" />
<img src="https://user-images.githubusercontent.com/118696036/230520763-54ccbf31-8e5f-47f6-990d-23a8450bd49e.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230520901-ce3be437-c0ff-4d61-a12a-2d345e2a1a1b.png" />
<img src="https://user-images.githubusercontent.com/118696036/230520908-6375716b-79ef-40ed-9124-c98579fafcd6.png" />
<img src="https://user-images.githubusercontent.com/118696036/230521014-4a2f7be3-2c4a-4b04-a23b-57d0073b849c.png" />
<img src="https://user-images.githubusercontent.com/118696036/230521018-6fdd37e6-89ce-4ef8-9453-db764ce20a18.png" />

#
### Update an object (User)

```http
  PUT https://localhost:7195/api/v1/Users/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to update|

📨  **body:**
```json
{
  "id": 1002,
  "userName": "maria",
  "password": "M@r1a*&$123",
  "role": "employee"
}
```
🧾  **response:**

⚙️  **Status Code:**
```http
  (204) - No Content
  (400) - Bad Request
  (401) - Unauthorized
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230516652-767dc7cc-1929-4292-acbb-6a8bd07bfc53.png" />
<img src="https://user-images.githubusercontent.com/118696036/230516655-5ca631e2-9313-4102-852b-ab57a7a3dc1e.png" />
<img src="https://user-images.githubusercontent.com/118696036/230621979-095a3d5f-1819-4f77-9c45-a4cf9d835aa1.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230516366-91858b64-9ed1-4758-9fa1-334070048953.png" />
<img src="https://user-images.githubusercontent.com/118696036/230516369-27d08917-ed19-42ac-a798-3e4e4cd9a686.png" />
<img src="https://user-images.githubusercontent.com/118696036/230622449-4aea0ad5-e7bf-4934-89ad-f62c43d91db8.png" />
<img src="https://user-images.githubusercontent.com/118696036/230622464-84959c6e-7db2-4194-b2a0-4284d7745418.png" />
<img src="https://user-images.githubusercontent.com/118696036/230515258-b33e3d50-af3e-42e1-bfd4-a3070d7e98de.png" />


#
#
# 📫  Routes for Products

### Return all objects (Products)
```http
  GET https://localhost:7195/api/v1/Products
```
⚙️  **Status Code:**
```http
  (200) - OK
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230462906-a62d9803-15d7-4a6d-a986-d8cd4675f2af.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230462958-157be87a-b959-4684-9afc-24d19f02aacb.png" />

#
### Return only one object (Product)

```http
  GET https://localhost:7195/api/v1/Products/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to view|

⚙️  **Status Code:**
```http
  (200) - OK
  (404) - Not Found
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230463462-8e1a73fe-1a42-4f25-8763-6d38b72aa498.png" />
<img src="https://user-images.githubusercontent.com/118696036/230463870-c012bca3-b483-453c-a09d-b482b67fe4d6.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230463484-dbf3289e-1641-4fbb-98e3-fb15bdfcea90.png" />
<img src="https://user-images.githubusercontent.com/118696036/230463883-7c2888f0-2fc2-46ee-872a-136c4243c465.png" />

#
### Return products with category

```http
  GET https://localhost:7195/api/v1/Products/productsWithCateg
```

⚙️  **Status Code:**
```http
  (200) - OK
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230466017-1282d614-b172-41ff-94a1-d118a9699b8c.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230466028-1e3bb166-8429-47c0-bf41-9ea4134026b0.png" />

#
### Return product with category

```http
  GET https://localhost:7195/api/v1/Products/${id}/productsWithCateg
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to view|

⚙️  **Status Code:**
```http
  (200) - OK
  (404) - Not Found
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230465309-c8f6346c-effe-491c-8381-7870f38c9186.png" />
<img src="https://user-images.githubusercontent.com/118696036/230463870-c012bca3-b483-453c-a09d-b482b67fe4d6.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230465320-6e9b58a8-9ad1-4a9f-8654-5013e849cfbe.png" />
<img src="https://user-images.githubusercontent.com/118696036/230463883-7c2888f0-2fc2-46ee-872a-136c4243c465.png" />

#
### Return category with product

```http
  GET https://localhost:7195/api/v1/Products/categories/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to view|

⚙️  **Status Code:**
```http
  (200) - OK
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230466932-24b0dd10-b3d3-44c4-80ac-44265a65bc1c.png" />
<img src="https://user-images.githubusercontent.com/118696036/230466950-9747ce6a-2d3c-462a-bf04-8c43f673777e.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230467400-5b146506-4e0f-49c7-8e80-7c9f9d10784b.png" />
<img src="https://user-images.githubusercontent.com/118696036/230467420-0a2080bd-40cb-4fac-a32e-c3e4368740aa.png" />

#
### Insert a new object (Product)

```http
  POST https://localhost:7195/api/v1/Products
```
📨  **body:**
```json
{
  "title": "C# Basics",
  "description": "Learn C# Fundamentals by Coding",
  "price": 150,
  "categoryId": 1
}
```

🧾  **response:**
```json
{
    "id": 3002,
    "title": "C# Basics",
    "description": "Learn C# Fundamentals by Coding",
    "price": 150,
    "categoryId": 1
}
```

⚙️  **Status Code:**
```http
  (201) - Created
  (400) - Bad Request
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230517411-28f647ea-0f1e-468d-ab51-5e426e057be4.png" />
<img src="https://user-images.githubusercontent.com/118696036/230517417-26a4227f-0e61-4988-a53a-d544ba464d8c.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230517496-0672401c-aa2e-413e-bbbb-4988a298ea93.png" />
<img src="https://user-images.githubusercontent.com/118696036/230517502-3535f5ba-d931-420c-8a4e-c87916f53714.png" />
<img src="https://user-images.githubusercontent.com/118696036/230517601-40e50133-3d47-4332-a9e8-f29d2ddb2e7d.png" />
<img src="https://user-images.githubusercontent.com/118696036/230517611-3d3af799-4e0d-4770-97fa-3a9642524800.png" />

#
### Update an object (Product)

```http
  PUT https://localhost:7195/api/v1/Products/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to update|

📨  **body:**
```json
{
  "id": 3002,
  "title": "C# 10 Basics",
  "description": "Learn C# Fundamentals by Coding",
  "price": 360,
  "categoryId": 1,
  "category": {
    "id": 1,
    "title": "Course"
  }
}
```
🧾  **response:**

⚙️  **Status Code:**
```http
  (204) - No Content
  (400) - Bad Request
  (401) - Unauthorized
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230519159-fa843426-a5fc-4af3-91f5-0ea93efc7463.png" />
<img src="https://user-images.githubusercontent.com/118696036/230519164-619ede53-0624-4145-8784-4fb052d56663.png" />
<img src="https://user-images.githubusercontent.com/118696036/230519314-eb82583a-75a3-42c8-b111-92d1de4a4927.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230519687-77c36e5c-5858-492f-8d00-5144bf2e1243.png" />
<img src="https://user-images.githubusercontent.com/118696036/230519695-d409116d-f88f-4af3-bf1b-df97612df796.png" />
<img src="https://user-images.githubusercontent.com/118696036/230519802-21da4b2d-dd16-4a0a-9ec0-caa3a21f78d9.png" />
<img src="https://user-images.githubusercontent.com/118696036/230519805-a795d287-5c9e-4d7f-b0a4-6c07aaae7115.png" />
<img src="https://user-images.githubusercontent.com/118696036/230519895-70a9cc3f-8950-4470-848a-a88ee94ef719.png" />

#
### Delete an object (Product)
```http
  DELETE https://localhost:7195/api/v1/Products/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to delete|

📨  **body:**

🧾  **response:**

⚙️  **Status Code:**
```http
  (204) - No Content
  (401) - Unauthorized
  (404) - Not Found
  (400) - Bad Request
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230519519-9282f9fe-f73b-45f0-a2b1-151b4382d83f.png" />
<img src="https://user-images.githubusercontent.com/118696036/230519511-27749835-40da-4c9d-95a4-4b6936fee45f.png" />
<img src="https://user-images.githubusercontent.com/118696036/230519515-ad479ddd-0d4a-4787-8d23-3021d9de91e6.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230520217-fa293914-e3f9-4ca2-92d1-31fbf549fde2.png" />
<img src="https://user-images.githubusercontent.com/118696036/230519895-70a9cc3f-8950-4470-848a-a88ee94ef719.png" />
<img src="https://user-images.githubusercontent.com/118696036/230520144-537727e6-2fa4-4750-884e-25bda7089032.png" />

#
#
# 📫  Routes for Categories

### Return all objects (Categories)
```http
  GET https://localhost:7195/api/v1/Categories
```
⚙️  **Status Code:**
```http
  (200) - OK
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230623803-4308cfee-7b5e-4b9b-b250-05e243c04d32.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230623811-cf0a8b96-92e8-4309-9635-3136e3d18f7d.png" />

#
### Return only one object (Category)

```http
  GET https://localhost:7195/api/v1/Categories/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to view|

⚙️  **Status Code:**
```http
  (200) - OK
  (404) - Not Found
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230624037-a6187556-51b2-4a6a-9f73-67ddc994598c.png" />
<img src="https://user-images.githubusercontent.com/118696036/230624049-63cde798-ac7c-4a5a-9612-dbc0cff0332c.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230624245-d60338e7-bf54-4a29-8e27-ba821e06d55e.png" />
<img src="https://user-images.githubusercontent.com/118696036/230624256-91b887bc-9fb8-4155-9dda-e4a7dc03d0e0.png" />

#
### Insert a new object (Category)

```http
  POST https://localhost:7195/api/v1/Categories
```
📨  **body:**
```json
{
  "title": "Bootcamp"
}
```

🧾  **response:**
```json
{
    "id": 2005,
    "title": "Bootcamp"
}
```

⚙️  **Status Code:**
```http
  (200) - Ok
  (400) - Bad Request
  (401) - Unauthorized
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230625149-66dfeff0-4310-4dad-994a-8bb274d8230e.png" />
<img src="https://user-images.githubusercontent.com/118696036/230625156-12229980-6ce1-48b3-9f9b-623f28f4cf4e.png" />
<img src="https://user-images.githubusercontent.com/118696036/230625162-702e9e71-50eb-4981-90b3-dd666aa18464.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230625344-0e1e3c82-a0d8-4cc2-9fe9-d9db3a097589.png" />
<img src="https://user-images.githubusercontent.com/118696036/230625353-051e55be-647e-48eb-a010-90371bb6aea4.png" />
<img src="https://user-images.githubusercontent.com/118696036/230625515-d2617491-58e1-42d5-9029-78e76faae232.png" />
<img src="https://user-images.githubusercontent.com/118696036/230625521-05fb9ca0-ddf2-48d2-955c-8a9de20e89b8.png" />

#
### Update an object (Category)

```http
  PUT https://localhost:7195/api/v1/Categories/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to update|

📨  **body:**
```json
{
  "id": "2005",
  "title": "Bootcamps"
}
```
🧾  **response:**

⚙️  **Status Code:**
```http
  (204) - No Content
  (400) - Bad Request
  (401) - Unauthorized
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230626308-f2e7e34a-efb9-4114-94be-7cb8479788e3.png" />
<img src="https://user-images.githubusercontent.com/118696036/230626144-82c0b1f4-1028-4418-aecf-ba2e3cecc385.png" />
<img src="https://user-images.githubusercontent.com/118696036/230625907-c9bcd425-e0c5-42d0-a37b-e6026c69c675.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230626990-aa0d55d7-2165-41d8-9516-b7bfe75856ae.png" />
<img src="https://user-images.githubusercontent.com/118696036/230626997-10e2f1eb-c9f1-49c9-9769-c22de84e2014.png" />
<img src="https://user-images.githubusercontent.com/118696036/230627142-9c3cb00a-e801-4144-a71a-17f5893ac758.png" />
<img src="https://user-images.githubusercontent.com/118696036/230627224-25186d08-ba5c-4c59-b311-44cd76318267.png" />

#
### Delete an object (Category)
```http
  DELETE https://localhost:7195/api/v1/Categories/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to delete|

📨  **body:**

🧾  **response:**

⚙️  **Status Code:**
```http
  (204) - No Content
  (404) - Not Found
  (401) - Unauthorized
  (400) - Bad Request
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230626546-abbd9a9f-02b5-4215-8f81-3a0be197479f.png" />
<img src="https://user-images.githubusercontent.com/118696036/230626565-f0e112ad-1956-4c54-a3e4-4560c1e20b22.png" />
<img src="https://user-images.githubusercontent.com/118696036/230626676-c6db2b83-8a71-4f24-bf99-4fe210851d8d.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230627700-11d58018-efa2-4348-a5f2-554a46341857.png" />
<img src="https://user-images.githubusercontent.com/118696036/230627800-095fb08e-2174-476c-b242-9dbf5213c638.png" />
<img src="https://user-images.githubusercontent.com/118696036/230627224-25186d08-ba5c-4c59-b311-44cd76318267.png" />

#
## 🔨 Tools used

<div>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" width="80" />
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" width="80" />
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/microsoftsqlserver/microsoftsqlserver-plain-wordmark.svg" width=80/>
</div>

# 🖥️ Technologies and practices used
- [x] C# 10
- [x] .NET CORE 6
- [x] SQL SERVER
- [x] Entity Framework 7
- [x] Code First
- [x] Token JWT
- [x] Swagger
- [x] DTOs
- [x] Dependency injection
- [x] POO

# 📖 Features
Registration, Listing, Update and Removal
