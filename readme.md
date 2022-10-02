
**Practical Test**

This application is implemented regarding to the practical test.

Simply just run the application, it will seed the corresponding data.

**SDKs:**
1. .NET 6.0

**Packages**
1. MediatR
2. SCharpFunctionalExtensions

**Approaches**

1. DDD (Domain Driven Design)
2. CQRS (Command and Query Responsibility Segregation)
3. Domain Events

**Domain Models**
1. Read Models
2. Write Models



**Two sample user has been created as follow:**

```json
[
  {
  "email": "user.test@gmail.com",
  "password": "P@ssw0rd"  
  },
  {
    "email": "user.test2@gmail.com",
    "password": "P@ssw0rd"
  }
]
```


**Endpoints**

AccessToken:
```curl
curl --location --request POST 'https://localhost:7183/api/auth/AccessToken' \
--header 'Content-Type: application/json' \
--data-raw '{
"email": "test.user@gmail.com",
"password": "P@ssw0rd"
}'
```

Addblog:

```curl
curl --location --request POST 'https://localhost:7183/api/blog/AddBlog?name=test12&description=test&content=ContentTest' \
--header 'Authorization: Bearer ' \
--header 'Content-Type: application/json' \
--data-raw '
[
    "C#"
]'
```

UpdateBlog:
```curl
curl --location --request POST 'https://localhost:7183/api/blog/UpdateBlogPost?name=test12&description=test&content=ContentTest&blogPostId=1' \
--header 'Authorization: Bearer ' \
--header 'Content-Type: application/json' \
--data-raw '[
    "C#"
]'
```

AddComment:

```curl
curl --location --request POST 'https://localhost:7183/api/blog/AddComment?content=Comment test44&blogPostId=1' \
--header 'Authorization: Bearer '
```

getAllOwnBlogPosts:
```curl
curl --location --request GET 'https://localhost:7183/api/blog/getAllOwnBlogPosts' \
--header 'Authorization: Bearer'
```

GetlatestTendaysBlogPosts:
```curl
curl --location --request GET 'https://localhost:7183/api/blog/getlatestTendaysBlogPosts' \
--header 'Authorization: Bearer '
```