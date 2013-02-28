using EI.Business;
namespace EI.Plc
{
    class ChangeCassette
    {
        public DemountCassetteStation Source { get; set; }
        public WaferSize WaferSize { get; set; }
        public DemountCassetteHopper Destination { get; set; }
    }
}
