using System;
using System.Collections.Generic;
using EI.Business;

namespace EI.Plc
{
    class PolisherStatusFromStreamConverter : BaseFromStreamConverter<PolisherFullStatus>
    {
        #region conversion methods

        private IList<ICarrierPlate> ParsePlates(string stream)
        {
            List<ICarrierPlate> plates = new List<ICarrierPlate>();
            for (int idx = 0; idx < 4; idx++)
                plates.Add(new CarrierPlate { Id = ParseString(stream.Substring(idx * 16, 16), "CarrierPlateId") });
            return plates.AsReadOnly();
        }

        private Magazine ParseMagazine(string stream)
        {
            Magazine magazine = new Magazine { Id = ParseString(stream.Substring(0, 16), "MagazineId") };
            magazine.NewPlates(ParsePlates(stream.Substring(16)));
            return magazine;
        }

        private IPolisherHead ParsePolisherHead(string stream, int head)
        {
            return new PolisherHead
            {
                Force = ParseHexInt(stream.Substring(88 + head * 4, 4), "Force"),
                Pressure = ((double)ParseHexInt(stream.Substring(140 + head * 4, 4), "Pressure")) / 1000.0,
                Backpressure = ((double)ParseHexInt(stream.Substring(156 + head * 4, 4), "Backpressure")) / 100.0,
                Rpm = ParseHexInt(stream.Substring(176 + head * 4, 4), "Rpm"),
                LoadCurrent = ((double)ParseHexInt(stream.Substring(196 + head * 4, 4), "LoadCurrent")) / 10.0
            };
        }

        private IList<IPolisherHead> ParsePolisherHeads(string stream)
        {
            List<IPolisherHead> polishingHeads = new List<IPolisherHead>();
            for (int idx = 0; idx < 4; idx++)
                polishingHeads.Add(ParsePolisherHead(stream, idx));
            return polishingHeads.AsReadOnly();
        }

        private IPolisherLiquid ParsePolisherLiquid(string stream)
        {
            return new PolisherLiquid
            {
                PadTemp = ((double)ParseHexInt(stream.Substring(0, 4), "PadTemp")) / 10.0,
                CoolingWaterInTemp = ((double)ParseHexInt(stream.Substring(4, 4), "CoolingWaterInTemp")) / 10.0,
                CoolingWaterOutTemp = ((double)ParseHexInt(stream.Substring(8, 4), "CoolingWaterOutTemp")) / 10.0,
                SlurryInTemp = ((double)ParseHexInt(stream.Substring(12, 4), "SlurryInTemp")) / 10.0,
                SlurryOutTemp = ((double)ParseHexInt(stream.Substring(16, 4), "SlurryOutTemp")) / 10.0,
                RinseTemp = ((double)ParseHexInt(stream.Substring(20, 4), "RinseTemp")) / 10.0,
                CoolingWaterAmount = ((double)ParseHexInt(stream.Substring(24, 4), "CoolingWaterAmount")) / 10.0,
                SlurryAmount = ((double)ParseHexInt(stream.Substring(28, 4), "SlurryAmount")) / 10.0,
                RinseAmount = ((double)ParseHexInt(stream.Substring(32, 4), "RinseAmount")) / 10.0
            };
        }

        #endregion

        #region BaseFromStreamConverter members

        protected override bool CheckParameter(string stream)
        {
            if (stream.Length < 220)
                throw GetPlcExceptionInvalidLength(220, stream.Length);
            return true;
        }

        protected override PolisherFullStatus GetObject(string stream)
        {
            PolisherFullStatus status = new PolisherFullStatus
            {
                HighPressure = false, // default, will be rewritten with actual value in higher level
                State = PolisherState.AutoProcess, // default, will be rewritten with actual value in higher level
                Magazine = ParseMagazine(stream.Substring(0, 80)),
                HighPressureDuration = TimeSpan.FromMilliseconds(ParseHexInt(stream.Substring(84, 4), "HighPressureDuration")),
                PlateRpm = ParseHexInt(stream.Substring(172, 4), "PlateRpm"),
                PlateLoadCurrent = ((double)ParseHexInt(stream.Substring(192, 4), "PlateLoadCurrent")) / 10.0,
                PolisherLiquid = ParsePolisherLiquid(stream.Substring(104, 36)),
                PadUsedTime = TimeSpan.FromMilliseconds(ParseHexInt(stream.Substring(212, 4), "PadUsedTime")),
                PadUsedCount = ParseHexInt(stream.Substring(216, 4), "PadUsedCount")
            };
            status.NewPolisherHeads(ParsePolisherHeads(stream));
            return status;
        }

        #endregion
    }
}
