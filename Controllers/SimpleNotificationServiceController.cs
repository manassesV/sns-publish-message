using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SimpleNotificationService.Models;
using System.Text.Json;

namespace SimpleNotificationService.Controllers
{

   

    [Route("[controller]")]
    public class SimpleNotificationServiceController : ControllerBase
    {
        private readonly SNSSettings _snssettings;

        public SimpleNotificationServiceController(
            IOptions<SNSSettings> options)
        {
            _snssettings = options.Value;
        }

        [HttpPost]
        public async Task Post(WeatherForecast weather)
        {
            var awsCredentials = new BasicAWSCredentials(_snssettings.accessKey, _snssettings.secretKey);
         

            var client = new AmazonSimpleNotificationServiceClient(awsCredentials, region: Amazon.RegionEndpoint.USEast1);
         
            var request = new PublishRequest()
            {
                Subject = weather.Subject,
                Message = JsonSerializer.Serialize(weather),
                TopicArn = weather.TopicARN,
                
                
            };

            var response = await client.PublishAsync(request);


        }
    }
}
