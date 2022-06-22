using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Vonage.Request;
using Vonage;

namespace kaleyra_WhatsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {

        public SMSController()
        {

        }

        [HttpPost]
        [Route("SendMessage")]
        public async Task<IActionResult> SendMessage(WhatsAppModel model)
        {
            try
            {
                var url = "https://global.kaleyra.com/api/v4/?";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AlwaysMultipartFormData = true;
                request.AddParameter("method", "wa");
                request.AddParameter("api_key", "A70bbb6d950dc95325c10f83a0e6e60c2");
                string jsonString = JsonSerializer.Serialize(model);
                request.AddParameter("body", jsonString);
                request.AddParameter("from", "wa business number");
                RestResponse response = await client.ExecuteAsync(request);
                var output = response.Content;
                return Ok(output);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }

        [HttpGet]
        [Route("SendSMS")]
        public async Task<IActionResult> SendSMS(string PhoneNumber)
        {
            try
            {
                var url = "https://api-alerts.kaleyra.com/v4/?api_key=A145b92d70767a57ba83589bc348efaff&method=sms&message=hello&to=7508850504&sender=KALERA";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AlwaysMultipartFormData = true;
                RestResponse response = await client.ExecuteAsync(request);
                var output = response.Content;
                return Ok(output);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("TwillioSMS")]
        public async Task<IActionResult> SendTwillioSMS(string? Message,string Number)
        {
            try
            {

                string accountSid = "ACd709634f015885640d3e42d121e35876";
                string authToken = "bfdc73e1a1c7e1ce3ac4dc4618419eee";
                TwilioClient.Init(accountSid, authToken);
                var message = await MessageResource.CreateAsync(
                    body: Message,
                    from: new Twilio.Types.PhoneNumber("+19805751526"),
                    to: new Twilio.Types.PhoneNumber("+91" + Number +"")
                );

                Console.WriteLine(message.Sid);
                return Ok(message.Sid);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("SendVonageSMS")]
        public async Task<IActionResult> SendVonageSMS(string? Message, string Number)
        {
            try
            {

                var credentials = Credentials.FromApiKeyAndSecret(
    "9226edf5",
    "14Uqsee7jsHVsFi3"
    );

                var VonageClient = new VonageClient(credentials);
                var response =await VonageClient.SmsClient.SendAnSmsAsync(new Vonage.Messaging.SendSmsRequest()
                {
                    To = "+91" + Number,
                    From = "Vonage APIs",
                    Text = Message
                });
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
