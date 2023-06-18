# Dotnet Core 7 API with Docker Image Support

This repository contains a sample project for a .NET Core 7 API with Docker image support. It provides two endpoints for managing users: POST and GET.

## Prerequisites

- [.NET Core 7 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/products/docker-desktop)

## Getting Started

1. Clone the repository:

```
git clone https://github.com/ebenvpaul/WebAPI.git
cd your-repo
```

2. Build the Docker image:

```
docker build -t dotnet-api .
```

3. Run the Docker container:

```
docker run -d -p 5000:5000 dotnet-api
```

4. Test the API endpoints:

- Create a new user (POST request):

  ```
  curl -X POST -H "Content-Type: application/json" -d '{"name":"John Doe"}' http://localhost:5000/api/users
  ```

- Get all users (GET request):

  ```
  curl http://localhost:5000/api/users
  ```

## API Endpoints

### POST /api/users

Create a new user.

- Request:

  ```json
  POST /api/users
  Content-Type: application/json

  {
    "name": "John Doe"
  }
  ```

- Response:

  ```json
  {
    "id": 1,
    "name": "John Doe"
  }
  ```

### GET /api/users

Get all users.

- Request:

  ```json
  GET /api/users
  ```

- Response:

  ```json
  [
    {
      "id": 1,
      "name": "John Doe"
    },
    {
      "id": 2,
      "name": "Jane Smith"
    }
  ]
  ```

## License

This project is licensed under the [MIT License](LICENSE).
