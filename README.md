# Web API Caching with Redis

## 📌 Overview
This repository contains a **.NET Web API** project designed with **Clean Architecture**, demonstrating how to implement **Redis caching** to enhance performance and reduce database load.

## 🔥 What is Redis?
[Redis](https://redis.io/) is an open-source, in-memory data store used as a **cache**, **message broker**, and **database**. It enables high-speed data retrieval, reducing response times for API requests.

## 🏗️ Project Architecture
This project follows **Clean Architecture** principles, ensuring:
- Separation of concerns
- Maintainability and scalability
- Testability

### Layers
- **API Layer** - Handles HTTP requests and responses.
- **Application Layer** - Contains business logic and services.
- **Infrastructure Layer** - Implements Redis caching and database interactions.
- **Domain Layer** - Defines core business models and logic.

## 🚀 Features
- **Implement Redis caching** for API responses
- **Store frequently accessed data** in Redis
- **Invalidate cache** when data updates
- **Reduce database load** for optimized performance

## 🛠️ Getting Started
### Prerequisites
- .NET SDK (>= .NET 6)
- Redis Server (Locally or via Docker)

### Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/Web-API-Caching-with-Redis.git
   ```
2. Navigate to the project folder:
   ```sh
   cd Web-API-Caching-with-Redis
   ```
3. Restore dependencies:
   ```sh
   dotnet restore
   ```
4. Update **appsettings.json** with Redis configuration.

### Running the Application
1. Start Redis server (via Docker or local installation):
   ```sh
   docker run --name redis-cache -d -p 6379:6379 redis
   ```
2. Apply database migrations (if applicable):
   ```sh
   dotnet ef database update
   ```
3. Run the API:
   ```sh
   dotnet run
   ```
4. Access the API at:
   ```sh
   http://localhost:5000
   ```

## 🏆 Usage Example
### Caching a Response
```csharp
var cachedData = await _cache.GetStringAsync("my-key");
if (cachedData == null)
{
    var data = await _repository.GetDataAsync();
    await _cache.SetStringAsync("my-key", JsonSerializer.Serialize(data), cacheOptions);
}
return Ok(data);
```

## 🎯 Contribution
Contributions are welcome! Feel free to open issues or submit pull requests.

## 📜 License
This project is licensed under the MIT License.

## 📞 Contact
For any questions, reach out or open an issue in the repository.

Happy Coding! 🚀
