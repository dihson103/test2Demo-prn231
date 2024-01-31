using AutoMapper;
using Tesst.Dtos;
using Tesst.Models;

namespace Tesst.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DummyMaster, MasterReponse>();
            CreateMap<DummyDetail, DetailsResponse>();

            CreateMap<DummyDetail, DetailResponseWithMaster>()
                .ForMember(dest => dest.MasterName, opt => opt.MapFrom(src => src.Master.MasterName));
        }
    }
}
