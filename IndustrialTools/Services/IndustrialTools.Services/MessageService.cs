﻿using IndustrialTools.Services.Interfaces;

namespace IndustrialTools.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
