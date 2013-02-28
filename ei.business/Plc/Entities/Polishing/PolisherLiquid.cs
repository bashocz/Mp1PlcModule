namespace EI.Business
{
    public class PolisherLiquid : IPolisherLiquid
    {
        #region public members

        public double PadTemp { get; set; }
        public double CoolingWaterInTemp { get; set; }
        public double CoolingWaterOutTemp { get; set; }
        public double SlurryInTemp { get; set; }
        public double SlurryOutTemp { get; set; }
        public double RinseTemp { get; set; }
        public double CoolingWaterAmount { get; set; }
        public double SlurryAmount { get; set; }
        public double RinseAmount { get; set; }

        public override string ToString()
        {
            return string.Format("PadTemp:{0},CoolWatInTemp:{1},CoolWatOutTemp:{2},SluInTemp:{3},SluOuTemp:{4},RinTemp:{5},CoolWatAmo:{6},SluAmo:{7},RinAmo:{8}",
                PadTemp,
                CoolingWaterInTemp,
                CoolingWaterOutTemp,
                SlurryInTemp,
                SlurryOutTemp,
                RinseTemp,
                CoolingWaterAmount,
                SlurryAmount,
                RinseAmount);
        }

        #endregion
    }
}
