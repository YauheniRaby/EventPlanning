using AutoMapper;

namespace EventPlanning.AutoMap
{
    public class MapperConfig
    {
        public static MapperConfiguration GetConfiguration()
        {
            var configExpression = new MapperConfigurationExpression();

            configExpression.AddProfile<UserProfile>();
            configExpression.AddProfile<CaseProfile>();
            configExpression.AddProfile<ParticipationProfile>();

            var config = new MapperConfiguration(configExpression);
            config.AssertConfigurationIsValid();

            return config;
        }
    }
}