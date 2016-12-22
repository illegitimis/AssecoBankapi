



namespace AssecoBankapi.DAL
{
  using Newtonsoft.Json;

  /// <summary>
  /// {
  /// "access_token": "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IlZwTDhkdnRibUpDNnI4dTY4cFFrYkxVemZROCIsImtpZCI6IlZwTDhkdnRibUpDNnI4dTY4cFFrYkxVemZROCJ9.eyJpc3MiOiJodHRwczovL3d3dy5iYW5rYXBpLm5ldC9kaWdpdGFsZWRnZWlkZW50aXR5IiwiYXVkIjoiaHR0cHM6Ly93d3cuYmFua2FwaS5uZXQvZGlnaXRhbGVkZ2VpZGVudGl0eS9yZXNvdXJjZXMiLCJleHAiOjE0ODI0NDA2OTgsIm5iZiI6MTQ4MjQzNzA5OCwiY2xpZW50X2lkIjoibXVsdGljaGFubmVscm9wYyIsInNjb3BlIjpbImFkZHJlc3MiLCJtdWx0aWNoYW5uZWxtYW5hZ2VtZW50Iiwib3BlbmlkIiwicHJvZmlsZSIsInJvbGVzIl0sInN1YiI6ImFhZGE3NWFiLWI2YzQtNDYzMS04MWNiLTQxNTI5ZDU5NWIzOSIsImF1dGhfdGltZSI6MTQ4MjQzNzA5NywiaWRwIjoiaWRzcnYiLCJjdXN0b21lcl9udW1iZXIiOiJEVTEyMzQ1NiIsImFtciI6WyJwYXNzd29yZCJdfQ.eu7ChCEmSJ8FCoj_S-FEnyb-68Aia84nNETDioDbRvXnMNVQ0ds1xfRn7PbIZURumzbcEIrv0e8R9NmAF8KLO0V7daV46hZpH4XK-x1PeEHwbVJQ9_N39jfUjOd7JKPsyfF0c5dMJ1vm3VRB_pInK9Kd5h7TPqaqH1JK3RRMNHMaGG6NbePiOBR7squ-xXh5XLy-hjujVQXvNqNhcZS9VGRGw33jRw3uKzFhtkR0_J8PjOj7ZgCjEn9VoKgLSrHzYV_uP7-1YQYOaCTj2bHZXxE_jdg45wD99E5i9YLUwHg36QoNN0cHU4Z_ORcltU6gsKcaVcB6bdWKrTUSihlBkw",
  /// "expires_in": 3600,
  /// "token_type": "Bearer"
  /// }
  /// </summary>
  class ConnectToken
  {
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonProperty("token_type")]
    public string TokenType { get; set; }
  }
}
