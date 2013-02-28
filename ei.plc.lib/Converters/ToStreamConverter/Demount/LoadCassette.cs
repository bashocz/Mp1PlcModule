using EI.Business;
namespace EI.Plc
{
    class LoadCassette
    {
        public WaferSize WaferSize { get; set; }
        public DemountCassetteHopper Destination { get; set; }
    }
}
