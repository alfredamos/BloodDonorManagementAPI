using AutoMapper;
using BloodDonorModels.Models;

namespace BloodDonorManagementAPI.Mapings
{
    public class Map : Profile
    {
        public Map()
        {
            CreateMap<DonorData, DonorData>();
        }
    }
}
