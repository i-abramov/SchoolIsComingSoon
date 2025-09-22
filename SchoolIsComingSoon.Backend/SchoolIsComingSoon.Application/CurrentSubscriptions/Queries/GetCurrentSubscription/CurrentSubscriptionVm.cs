using AutoMapper;
using SchoolIsComingSoon.Application.Common.Mappings;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.CurrentSubscriptions.Queries.GetCurrentSubscription
{
    public class CurrentSubscriptionVm : IMapWith<CurrentSubscription>
    {
        public Guid UserId { get; set; }
        public Guid SubscriptionId { get; set; }
        public string ExpiresAfter { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CurrentSubscription, CurrentSubscriptionVm>()
                .ForMember(subscriptionVm => subscriptionVm.UserId,
                    opt => opt.MapFrom(subscription => subscription.UserId))
                .ForMember(subscriptionVm => subscriptionVm.SubscriptionId,
                    opt => opt.MapFrom(subscription => subscription.SubscriptionId))
                .ForMember(subscriptionVm => subscriptionVm.ExpiresAfter,
                    opt => opt.MapFrom(subscription => subscription.ExpiresAfter))
                .ForMember(subscriptionVm => subscriptionVm.Name,
                    opt => opt.MapFrom(subscription => subscription.Subscription.Name))
                .ForMember(subscriptionVm => subscriptionVm.Price,
                    opt => opt.MapFrom(subscription => subscription.Subscription.Price));
        }
    }
}