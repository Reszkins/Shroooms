using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using Shrooms.Authentication;

namespace Shrooms.Services
{
    public class CustomVisionService
    {
        private readonly IConfiguration _configuration;
        public CustomVisionService(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        public async Task<(string name, double probability)> ClassifyMushroom(Stream testImage)
        {
            var trainingApi = CustomVisionAuthenticator.AuthenticateTraining(_configuration["CustomVision:Endpoint"], _configuration["CustomVision:TrainingKey"]);
            var projects = await trainingApi.GetProjectsAsync();
            var project = projects.FirstOrDefault(project => project.Name == _configuration["CustomVision:ProjectName"]);

            var predictionApi = CustomVisionAuthenticator.AuthenticatePrediction(_configuration["CustomVision:Endpoint"], _configuration["CustomVision:PredictionKey"]);

            var classificationResult = await GetClassification(predictionApi, project, _configuration["CustomVision:PublishedModelName"], testImage);

            var mushroom = classificationResult.MaxBy(x => x.Probability);
            var prob = mushroom.Probability;

            return (mushroom.TagName.ToString(), prob);
        }
        private async Task<IList<PredictionModel>> GetClassification(CustomVisionPredictionClient predictionApi, Project project, string publishedModelName, Stream testImage)
        {
            var result = await predictionApi.ClassifyImageAsync(project.Id, publishedModelName, testImage);

            return result.Predictions;
        }
    }
}
