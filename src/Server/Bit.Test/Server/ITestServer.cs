﻿using IdentityModel.Client;
using Microsoft.AspNet.SignalR.Client;
using OpenQA.Selenium.Remote;
using Simple.OData.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bit.Test.Server
{
    public class RemoteWebDriverOptions
    {
        public string Uri { get; set; }

        public TokenResponse Token { get; set; }

        public bool ClientSideTest { get; set; } = true;
    }

    public interface ITestServer : IDisposable
    {
        RemoteWebDriver BuildWebDriver(RemoteWebDriverOptions options = null);

        Task<TokenResponse> Login(string userName, string password, string clientId, string secret = "secret");

        IODataClient BuildODataClient(Action<HttpRequestMessage> beforeRequest = null,
            Action<HttpResponseMessage> afterResponse = null, TokenResponse token = null, string odataRouteName = "Test");

        ODataBatch BuildODataBatchClient(Action<HttpRequestMessage> beforeRequest = null,
           Action<HttpResponseMessage> afterResponse = null, TokenResponse token = null, string odataRouteName = "Test");

        HttpClient BuildHttpClient(TokenResponse token = null);

        Task<IHubProxy> BuildSignalRClient(TokenResponse token = null, Action<string, dynamic> onMessageReceived = null);

        TokenClient BuildTokenClient(string clientId, string secret);

        TService BuildRefitClient<TService>(TokenResponse token = null);

        void Initialize(string uri);

        string Uri { get; }
    }
}
