using AutoMapper;
using SchoolIsComingSoon.Application.Common.Mappings;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.Subscriptions.Queries.GetSubscriptionList
{
    public class SubscriptionLookupDto : IMapWith<Post>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int LVL { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Subscription, SubscriptionLookupDto>()
                .ForMember(subscriptionDto => subscriptionDto.Id,
                    opt => opt.MapFrom(subscription => subscription.Id))
                .ForMember(subscriptionDto => subscriptionDto.Name,
                    opt => opt.MapFrom(subscription => subscription.Name))
                .ForMember(subscriptionDto => subscriptionDto.Price,
                    opt => opt.MapFrom(subscription => subscription.Price))
                .ForMember(subscriptionDto => subscriptionDto.LVL,
                    opt => opt.MapFrom(subscription => subscription.LVL));
        }
    }
}