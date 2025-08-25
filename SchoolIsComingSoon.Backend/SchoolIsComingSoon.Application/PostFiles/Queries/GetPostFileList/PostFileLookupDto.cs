using AutoMapper;
using SchoolIsComingSoon.Application.Common.Mappings;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.PostFiles.Queries.GetPostFileList
{
    public class PostFileLookupDto : IMapWith<PostFile>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Base64Code { get; set; }
        public string FileType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostFile, PostFileLookupDto>()
                .ForMember(postFileDto => postFileDto.Id,
                    opt => opt.MapFrom(postFile => postFile.Id))
                .ForMember(postFileDto => postFileDto.Name,
                    opt => opt.MapFrom(postFile => postFile.Name))
                .ForMember(postFileDto => postFileDto.Base64Code,
                    opt => opt.MapFrom(postFile => postFile.Base64Code))
                .ForMember(postFileDto => postFileDto.FileType,
                    opt => opt.MapFrom(postFile => postFile.FileType));
        }
    }
}