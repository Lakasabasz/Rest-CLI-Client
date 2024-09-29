namespace RestCliClient.Tests;

static class TestConsts
{
    public const string ReadMeJson = 
"""
{
  "variables": {
    "in": [
      "token",
      "username",
      "password"
    ],
    "out": [
      "user-id",
      "user-token"
    ]
  },
  "sequence": [
    {
      "uri": "https://example.com/api/users",
      "method": "POST",
      "options": {
        "ignore_ssl": false,
        "timeout": 15
      },
      "headers": [
        {"name": "Authorization", "value": "Bearer $token"},
        {"name": "Content-Type", "value": "application/json"}
      ],
      "body": "{\"username\": \"$username\", \"password\": \"$password\", \"category\": \"$$\"}",
      "response_operations": [
        {"variable": "user-guid", "value": "$.user.id"}
      ]
    }
  ]
}
""";

    public const string ReadMeMinimalJson =
"""
{
  "sequence": [
    {
      "uri": "https://example.com/api/users",
      "method": "POST"
    }
  ]
}
""";

    public const string ReadMeBodyObjectJson =
"""
{
  "sequence": [
    {
      "uri": "https://example.com/api/users",
      "method": "POST",
      "body": {
        "username": "$username",
        "password": "$password",
        "category": "$$"
      }
    }
  ]
}
""";
}