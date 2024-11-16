using MagicVilla_VillaAPI.Models.DTOs;

namespace MagicVilla_VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
        {
            new VillaDTO{Id =1, Name="Pool View", Occupancy=4, Sqrft=200},
            new VillaDTO{Id =2, Name="Beach View", Occupancy=2, Sqrft=50}
        };
    }
}
