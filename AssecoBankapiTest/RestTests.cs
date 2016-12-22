

namespace AssecoBankapi
{
  using System;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using System.Net.Http;
  using System.Threading.Tasks;
  using Newtonsoft.Json;
  using System.Net.Http.Headers;
  using System.Collections.Generic;
  using System.Net;


  [TestClass]
  public class RestTests
  {
    static string token = String.Empty;

    const string bankapiBase = "http://www.bankapi.net";
    const string connectToken = "/digitaledgeidentity/connect/token";
    const string identityMe = "/digitaledgeapi/v1/identity/me";
    const string authorizationHeader = "Basic bXVsdGljaGFubmVscm9wYzpteXJhbmRvbWNsaWVudHNlY3JldA==";


    /// <summary>
    /// POST https://bankapi.net/digitaledgeidentity/connect/token HTTP/1.1
    /// </summary>
    [TestMethod]
    public async Task AuthenticationTest()
    {
      using (var httpClient = new HttpClient { BaseAddress = new Uri(bankapiBase) })
      {
        httpClient.DefaultRequestHeaders.Clear();

        //ACCEPT header
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //Authorization Header is present: Basic bXVsdGljaGFubmVscm9wYzpteXJhbmRvbWNsaWVudHNlY3JldA==
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationHeader);

        // Form Data
        var formDataDictionary = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "username", "demouser" },
            { "password", "Pexim.1" },
            { "scope", "openid profile address multichannelmanagement roles" },
        };

        using (var content = new FormUrlEncodedContent(formDataDictionary))
        {
          Assert.AreEqual(112, content.Headers.ContentLength);
          Assert.AreEqual(MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded"), content.Headers.ContentType);


          using (var response = await httpClient.PostAsync(connectToken, content))
          {
            ActOnResponse(response);

            string jsonResponse = await response.Content.ReadAsStringAsync();
            var connectToken = JsonConvert.DeserializeObject<DAL.ConnectToken>(jsonResponse);
            Assert.IsNotNull(connectToken);
            Assert.AreEqual(connectToken.ExpiresIn, 3600);
            Assert.AreEqual(connectToken.TokenType, "Bearer");

            if (String.IsNullOrEmpty(token))
              token = connectToken.AccessToken;
          }
        }
      }
    }

   
    /// <summary>
    /// GET https://bankapi.net/digitaledgeapi/v1/identity/me HTTP/1.1
    /// </summary>
    [TestMethod]
    public async Task IdentityMeTest()
    {
      if (String.IsNullOrEmpty(token))
        await AuthenticationTest();

      using (var httpClient = new HttpClient { BaseAddress = new Uri(bankapiBase) })
      {
        httpClient.DefaultRequestHeaders.Clear();

        //ACCEPT header
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //Authorization Header
        //httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", String.Format("Bearer {0}", token));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Preferred-Client-Culture", "en-US");
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization-Username", "demouser");

        using (var response = await httpClient.GetAsync(identityMe))
        {
          // fails UNAUTHORIZED
          ActOnResponse(response);
                   
          string jsonResponse = await response.Content.ReadAsStringAsync();
          var identityMe = JsonConvert.DeserializeObject<DAL.IdentityMe>(jsonResponse);
          Assert.IsNotNull(identityMe);
          // TODO
        }
      }
    }

    private static void ActOnResponse(HttpResponseMessage response)
    {
      Assert.IsNotNull(response);
      Assert.IsNotNull(response.Content);

      Assert.IsTrue(response.IsSuccessStatusCode);
      Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
      Assert.AreEqual("OK", response.ReasonPhrase);
      Assert.IsNotNull(response.Version);
      Assert.AreEqual(response.Version.Minor, 1);
      Assert.AreEqual(response.Version.Major, 1);
    }
  }
}
