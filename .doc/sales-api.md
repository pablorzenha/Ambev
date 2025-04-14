[Back to README](../README.md)

### Sales

#### GET /Sales
- Description: Retrieve a list of all sales
- Query Parameters:
  - `_page` (optional): Page number for pagination (default: 1)
  - `_size` (optional): Number of items per page (default: 10)
  - `_order` (optional): Ordering of results (e.g., "asc,desc")
- Response: 
  ```json
  {
    "currentPage": 0,
    "totalPages": 0,
    "pageSize": 0,
    "totalCount": 0,
    "data": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "date": "2025-04-14T19:14:32.427Z",
        "saleNumber": "string",
        "customerId": "string",
        "branchId": "string",
        "totalAmount": 0,
        "status": 0,
        "items": [
          {
            "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "quantity": 0,
            "unitPrice": 0,
            "discount": 0,
            "totalPrice": 0
          }
        ]
      }
    ],
    "hasPrevious": true,
    "hasNext": true
  }
  ```

#### POST /Sales
- Description: Add a new sale
- Request Body:
  ```json
  {
    "saleNumber": "string",
    "date": "2025-04-14T14:57:19.292Z",
    "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "branchId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "status": "(1 - Cancelled, 2 - NotCancelled)",
    "items": [
      {
        "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "quantity": 0,
        "unitPrice": 0
      }
    ]
  }
  ```
- Response: 
  ```json
  {
    "success": true,
    "message": "string",
    "errors": [
      {
        "error": "string",
        "detail": "string"
      }
    ],
    "data": {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "saleNumber": "string",
      "date": "2025-04-14T14:58:52.503Z",
      "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "branchId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "totalAmount": 0,
      "status": 0,
      "items": [
        {
          "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "quantity": 0,
          "unitPrice": 0,
          "totalPrice": 0,
          "discount": 0
        }
      ]
    }
  }
  ```

#### GET /Sales/{id}
- Description: Retrieve a specific sale by ID
- Path Parameters:
  - `id`: Sale ID
- Response: 
  ```json
    {
    "success": true,
    "message": "string",
    "errors": [
      {
        "error": "string",
        "detail": "string"
      }
    ],
    "data": {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "saleNumber": "string",
      "customerId": "string",
      "branchId": "string",
      "totalAmount": 0,
      "status": 0,
      "items": [
        {
          "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "quantity": 0,
          "unitPrice": 0,
          "discount": 0,
          "totalPrice": 0
        }
      ]
    }
  }
  ```

#### PUT /Sales/{id}
- Description: Update a specific sale
- Path Parameters:
  - `id`: Sale ID
- Request Body:
  ```json
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "saleNumber": "string",
    "date": "2025-04-14T15:01:55.273Z",
    "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "branchId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "items": [
      {
        "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "quantity": 0,
        "unitPrice": 0
      }
    ],
    "status": 0
  }
  ```
- Response: 
  ```json
  {
    "success": true,
    "message": "string",
    "errors": [
      {
        "error": "string",
        "detail": "string"
      }
    ],
    "data": {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "date": "2025-04-14T14:58:52.503Z",
      "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "branchId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "totalAmount": 0,
      "status": 0,
      "items": [
        {
          "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "quantity": 0,
          "unitPrice": 0,
          "totalPrice": 0,
          "discount": 0
        }
      ]
    }
  }
  ```

#### DELETE /Sales/{id}
- Description: Delete a specific sale
- Path Parameters:
  - `id`: Sale ID
- Response: 
  ```

  ```
<br/>
<div style="display: flex; justify-content: space-between;">
  <a href="./carts-api.md">Previous: Carts API</a>
  <a href="./auth-api.md">Next: Auth API</a>
</div>
