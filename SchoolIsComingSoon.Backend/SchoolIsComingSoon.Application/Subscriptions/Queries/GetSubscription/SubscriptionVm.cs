using AutoMapper;
using SchoolIsComingSoon.Application.Common.Mappings;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.Subscriptions.Queries.GetSubscription
{
    public class SubscriptionVm : IMapWith<Subscription>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Subscription, SubscriptionVm>()
                .ForMember(subscriptionVm => subscriptionVm.Id,
                    opt => opt.MapFrom(subscription => subscription.Id))
                .ForMember(subscriptionVm => subscriptionVm.Name,
                    opt => opt.MapFrom(subscription => subscription.Name))
                .ForMember(subscriptionVm => subscriptionVm.Price,
                    opt => opt.MapFrom(subscription => subscription.Price));
        }
    }
}