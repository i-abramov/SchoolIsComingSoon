using AutoMapper;
using SchoolIsComingSoon.Application.Common.Mappings;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.PostImages.Queries.GetPostImageList
{
    public class PostImageLookupDto : IMapWith<PostImage>
    {
        public Guid Id { get; set; }
        public string Base64Code { get; set; }
        public string FileType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostImage, PostImageLookupDto>()
                .ForMember(postImageDto => postImageDto.Id,
                    opt => opt.MapFrom(postImage => postImage.Id))
                .ForMember(postImageDto => postImageDto.Base64Code,
                    opt => opt.MapFrom(postImage => postImage.Base64Code))
                .ForMember(postImageDto => postImageDto.FileType,
                    opt => opt.MapFrom(postImage => postImage.FileType));
        }
    }
}