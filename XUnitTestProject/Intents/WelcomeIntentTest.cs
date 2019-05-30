using DSEHackatthon.BLL;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace XUnitTestProject.Intents
{
    public class WelcomeIntentTest
    {
        private static readonly JsonParser jsonParser =
            new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));
        [Fact]
        public void HandleTest()
        {
            Conversation c = new Conversation();
            Welcome testIntent = new Welcome(c);
            string a = File.ReadAllText($@"data/welcome.json");           
            WebhookRequest request= jsonParser.Parse<WebhookRequest>(a);
            WebhookResponse res= testIntent.Handle(request);
            string expectedResult = "Hello Naveen Kumar, welcome to challengers service\r\n                what can I do for you? I can provide your challenge progress or I can review your challenges";
            Assert.True(res.FulfillmentText.Contains(expectedResult));
        }
    }
}
