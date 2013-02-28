namespace EI.Business
{
    public interface IPolisherLiquid
    {
        double PadTemp { get; }
        double CoolingWaterInTemp { get; }
        double CoolingWaterOutTemp { get; }
        double SlurryInTemp { get; }
        double SlurryOutTemp { get; }
        double RinseTemp { get; }
        double CoolingWaterAmount { get; }
        double SlurryAmount { get; }
        double RinseAmount { get; }
    }
}
