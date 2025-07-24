using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
﻿#if HSGE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;
using System.Net.WebSockets;



namespace WebSocketClient
{
    public delegate void WebSocketOpenEventHandler();
    public delegate void WebSocketMessageEventHandler(byte[] data);
    public delegate void WebSocketErrorEventHandler(string errorMsg);
    public delegate void WebSocketCloseEventHandler(DisconnectionType closeCode);

    class WebSocket
    {
        public event WebSocketOpenEventHandler OnOpen;
        public event WebSocketMessageEventHandler OnMessage;
        public event WebSocketErrorEventHandler OnError;
        public event WebSocketCloseEventHandler OnClose;

        private WebsocketClient client;

        public WebSocket(string url)
        {
            client = new WebsocketClient(new Uri(url));

            client.ReconnectionHappened.Subscribe(infp => OnOpen?.Invoke());
            client.DisconnectionHappened.Subscribe(info => OnClose?.Invoke(info.Type));
            client.MessageReceived.Subscribe(msg =>
            {
                if (msg.MessageType == WebSocketMessageType.Binary)
                    OnMessage?.Invoke(msg.Binary);
                else
                    OnMessage?.Invoke(Encoding.UTF8.GetBytes(msg.Text));
            });
        }

        async public Task Connect()
        {
            await client.Start();
        }

        async public Task Close()
        {
            client.Dispose();
        }

        public void SendText(string text)
        {
            client.Send(text);
        }

        public void DispatchMessageQueue() { }
    }
}

#endif