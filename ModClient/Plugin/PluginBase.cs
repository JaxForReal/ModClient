﻿using ModClient.MessageService;

namespace ModClient.Plugin
{
    public delegate void PluginOutputDelegate(string message);

    //A plugin is a add-on to a MessageServiceBase
    //it can subscribe to all MessageServiceBase events, and send messages
    //also has the ability to preprocess text before it is sent to the backend/server
    public abstract class PluginBase
    {
        protected MessageServiceBase ParentService;

        //pugins can supply output messages to the user
        internal event PluginOutputDelegate OnPluginOutput;

        //plugins can override the setter here to do certain things on enable/disable
        public virtual bool Enabled { get; set; } = true;

        public PluginBase(MessageServiceBase service)
        {
            ParentService = service;
        }

        //return null if no message should be sent
        //only called if plugin.Enabled == true
        public virtual string PreprocessOutgoingMessage(string message) => message;

        protected void OnPluginOutputInternal(string message) => OnPluginOutput?.Invoke(message);
    }
}