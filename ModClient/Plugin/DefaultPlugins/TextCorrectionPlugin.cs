﻿using System.Text.RegularExpressions;
using ModClient.MessageService;

namespace ModClient.Plugins
{
    public class TextCorrectionPlugin : Plugin
    {
        private static readonly Regex Regex = new Regex(@"^s/([^/]+)/([^/]+)");
        private string previousMessage = "";

        public TextCorrectionPlugin(ServiceView service) : base(service)
        {
        }

        public override string PreprocessOutgoingMessage(string message)
        {
            var match = Regex.Match(message);
            if (match.Success)
            {
                var replaced = previousMessage.Replace(match.Groups[1].Value, match.Groups[2].Value);
                //is there is no change, dont send a new message
                return replaced != message ? replaced : null;
            }
            previousMessage = message;
            return message;
        }

        public override string ToString() => "Text Corrector";
    }
}