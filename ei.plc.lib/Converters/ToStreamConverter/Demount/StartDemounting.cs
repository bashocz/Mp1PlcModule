using EI.Business;
namespace EI.Plc
{
    class StartDemounting
    {
        public WaferSize Size { get; set; }
        public int Count { get; set; }
        public DemountCassetteStation Station { get; set; }
    }
}
