# Rest-CLI-Client
Commandline rest client

## CLI usage
Example usage:
```
> GET http://example.com/api/users
HEADERS> Authorization: Bearer 21313154
HEADERS> Content-Type: application/json
HEADERS>
BODY> 
< Request in progress
< Request result with code 200
<<< Response headers
Allow: whatever
<<< Response body
{"users": [{"username":"dupa"}, {"username":"krowa"}]}
<<<
(Response from http://example.com/api/users)> $userName=$.users[0].username
(Response from http://example.com/api/users)> DELETE http://example.com/api/users/{$userName:str}
HEADERS> @auth-jwt 21313154
HEADERS> @json-type
HEADERS>
BODY> {"access": 0}
< Request in progress
< Request result with code 400
<<< Response headers
Allow: whatever
<<< Response body
{}
<<<
```

## Saved requests sequence
App allow to save requests sequence and execute it. It can be saved to files using JSON format

```json
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
```
Example saved sequence usage:
```
> EXECUTE PrepareUser $savedToken "kaczka sraczka" "askjdlasjd asdka"
< Requests sequence in progress
< Executing POST http://example.com/api/users with code 200
<<<
...print całego requesta...
<<<
< Done POST http://example.com/api/users with code 200
<<< Result
...print całego responsa...
<<< Response operations
user-guid=2137
<<<
< All sequences finished successfully
> 
```

### Minimal valid JSON
```json
{
  "sequence": [
    {
      "uri": "https://example.com/api/users",
      "method": "POST"
    }
  ]
}
```
Other fields are optional, except:
- `sequence.headers` if specified requires `name`
- `sequence.response_operations` if specified requires `variable` and `value`

## Common name
App has predefined common text like `Content-Type: application/json`. It can be extended adding new
entry to file
I dodatkowo mieć wbudowane commonname-y, które user będzie mógł rozszerzać (w pliku json):

Example content
```json
[
  {"key": "auth-jwt", "value": "Authorization: Bearer"},
  {"key": "json-type", "value": "Content-Type: application/json"}
]
```
It can be used only in command line interface and works as replace `@key` to `value`
