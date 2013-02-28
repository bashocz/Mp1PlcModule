using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EI.Business;

namespace EI.Plc.Tests
{
    static class SimulatorHelper
    {
        #region private members

        private static Random _random = new Random();

        #endregion

        #region Polishing Simulator help methods

        public static PolisherHeadSimulator_Accessor GetPolisherHeadSimulator(PlcMemory memory, int polisherNumber, int headNumber,
                                                         int force, double pressure, double backpressure, int rpm, double loadCurret)
        {
            PolisherHeadSimulator privateTarget = new PolisherHeadSimulator(memory, polisherNumber, headNumber)
            {
                Force = force,
                Pressure = pressure,
                Backpressure = backpressure,
                Rpm = rpm,
                LoadCurrent = loadCurret
            };
            return new PolisherHeadSimulator_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherHeadSimulator))));
        }

        public static PolisherLiquidSimulator_Accessor GetPolisherLiquidSimulator(PlcMemory memory, int polisherNumber,
            double padTemp, double coolingWaterInTemp, double coolingWaterOutTemp, double slurryInTemp, double slurryOutTemp,
            double rinseTemp, double collingWafetAmount, double slurryAmount, double rinseAmount)
        {
            PolisherLiquidSimulator privateTarget = new PolisherLiquidSimulator(memory, polisherNumber)
            {
                PadTemp = padTemp,
                CoolingWaterInTemp = coolingWaterInTemp,
                CoolingWaterOutTemp = coolingWaterOutTemp,
                SlurryInTemp = slurryInTemp,
                SlurryOutTemp = slurryOutTemp,
                RinseTemp = rinseTemp,
                CoolingWaterAmount = collingWafetAmount,
                SlurryAmount = slurryAmount,
                RinseAmount = rinseAmount
            };
            return new PolisherLiquidSimulator_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherLiquidSimulator))));
        }

        public static PolisherPlateSimulator_Accessor GetPolisherPlateSimulator(PlcMemory memory, int polisherNumber, int plateNumber, string id)
        {
            PolisherPlateSimulator privateTarget = new PolisherPlateSimulator(memory, polisherNumber, plateNumber) { Id = id };
            PolisherPlateSimulator_Accessor target = new PolisherPlateSimulator_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherPlateSimulator))));

            return target;
        }

        public static PolishingPlateSimulator_Accessor GetPolishingPlateSimulator(PlcMemory memory, int plateNumber, string id, int recipe)
        {
            PolishingPlateSimulator privateTarget = new PolishingPlateSimulator(memory, plateNumber) { Id = id, Recipe = recipe };
            return new PolishingPlateSimulator_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingPlateSimulator))));
        }

        public static Magazine GetDefaultMagazine()
        {
            List<ICarrierPlate> plates = new List<ICarrierPlate>();
            for (int i = 0; i < 4; i++)
            {
                plates.Add(new CarrierPlate { Id = "", Capacity = 5, Recipe = 1 });
            }
            Magazine magazine = new Magazine { Id = "" };
            magazine.NewPlates(plates);
            return magazine;
        }

        public static int GetRandomPolisherId()
        {
            int randomInteger = _random.Next(0, 12);
            if (randomInteger >= 8)
                return 0;
            else if (randomInteger >= 4)
                return 1;
            return 2;
        }

        public static PolisherState GetRandomPolisherState()
        {
            int randomInteger = _random.Next(0, 21);
            if (randomInteger >= 17)
                return PolisherState.AutoProcess;
            else if (randomInteger >= 14)
                return PolisherState.AutoWait;
            else if (randomInteger >= 11)
                return PolisherState.AutoLoad;
            else if (randomInteger >= 8)
                return PolisherState.AutoUnload;
            else if (randomInteger >= 5)
                return PolisherState.Pause;
            else if (randomInteger >= 2)
                return PolisherState.Alarm;
            return PolisherState.EmergencyStop;
        }

        #region ICarrierPlate

        public static List<ICarrierPlate> GetPlateList(int count)
        {
            List<ICarrierPlate> plates = new List<ICarrierPlate>();
            for (int i = 0; i < count; i++)
            {
                plates.Add(new CarrierPlate { Id = GetRandomString(8), Capacity = 5, Recipe = 1 });
            }
            return plates;
        }

        #endregion

        #region IWafer

        public static List<IWafer> GetWaferList(int waferCount, int cassetteCount)
        {
            List<IWafer> wafers = new List<IWafer>();

            for (int idx = 0; idx < waferCount; idx++)
                wafers.Add(GetWaferMock(idx % cassetteCount + 1, idx / cassetteCount + 1));

            return wafers;
        }

        private static IWafer GetWaferMock(int cassetteNumber, int slotNumber)
        {
            Mock<IWafer> wafer = new Mock<IWafer>();
            wafer.Setup(x => x.CassetteNumber).Returns(cassetteNumber);
            wafer.Setup(x => x.SlotNumber).Returns(slotNumber);
            return wafer.Object;
        }

        #endregion

        #endregion

        #region Mount Simulator help methods

        public static MountLine GetRandomMountLine()
        {
            int randomInteger = _random.Next(0, 15);
            if (randomInteger >= 10)
                return MountLine.Both;
            else if (randomInteger >= 5)
                return MountLine.Left;
            return MountLine.Right;
        }

        public static MountState GetRandomMountState()
        {
            int randomInteger = _random.Next(0, 20);
            if (randomInteger >= 15)
                return MountState.AutoMount;
            else if (randomInteger >= 10)
                return MountState.AutoMountAlarm;
            else if (randomInteger >= 5)
                return MountState.Stop;
            return MountState.StopAlarm;
        }

        public static LotDataTransmission GetRandomLotDataTransmission()
        {
            int randomInteger = _random.Next(0, 15);
            if (randomInteger >= 10)
                return LotDataTransmission.BeforeWritingCassetteInfo;
            else if (randomInteger >= 5)
                return LotDataTransmission.BeforeWritingWaferInfo;
            return LotDataTransmission.Cleared;
        }

        #region ILotData

        public static ILotData GetDefaultLotData()
        {
            var lotData = new Mock<ILotData>();
            lotData.Setup(x => x.LotId).Returns("");
            lotData.Setup(x => x.Cassettes).Returns(GetCassetteList(0));
            lotData.Setup(x => x.NGWaferCount).Returns(0);
            lotData.Setup(x => x.WaferSize).Returns(WaferSize.AnySize);
            lotData.Setup(x => x.OfType).Returns(OfType.Cleared);
            lotData.Setup(x => x.PolishDivision).Returns(PolishDivision.Cleared);
            lotData.Setup(x => x.Assembly1.CarrierPlateCount).Returns(0);
            lotData.Setup(x => x.Assembly1.WaferCount).Returns(3);
            lotData.Setup(x => x.Assembly2.CarrierPlateCount).Returns(0);
            lotData.Setup(x => x.Assembly2.WaferCount).Returns(3);
            lotData.Setup(x => x.Wafers).Returns(GetWaferList(0, 0));

            return lotData.Object;
        }

        public static ILotData GetRandomLotData()
        {
            int waferCount = _random.Next(3, 301);

            var lotData = new Mock<ILotData>();
            lotData.Setup(x => x.LotId).Returns(GetRandomString(14));
            lotData.Setup(x => x.Cassettes).Returns(GetCassetteList((_random.Next(0, 13))));
            lotData.Setup(x => x.NGWaferCount).Returns((_random.Next(0, 298)));
            lotData.Setup(x => x.WaferSize).Returns((_random.NextDouble() > 0.5) ? WaferSize.Size6Inches : WaferSize.Size8Inches);
            lotData.Setup(x => x.OfType).Returns((_random.NextDouble() > 0.5) ? OfType.OF : OfType.VNotch);
            lotData.Setup(x => x.PolishDivision).Returns((_random.NextDouble() > 0.5) ? PolishDivision.New : PolishDivision.Repolish);
            lotData.Setup(x => x.Assembly1.CarrierPlateCount).Returns((_random.Next(0, 100)));
            lotData.Setup(x => x.Assembly1.WaferCount).Returns((_random.Next(3, 9)));
            lotData.Setup(x => x.Assembly2.CarrierPlateCount).Returns((_random.Next(0, 100)));
            lotData.Setup(x => x.Assembly2.WaferCount).Returns((_random.Next(3, 9)));
            lotData.Setup(x => x.Wafers).Returns(GetWaferList(waferCount, ((waferCount % 25 == 0) ? waferCount / 25 : waferCount / 25 + 1)));

            return lotData.Object;
        }

        #endregion

        #region ICassette

        public static List<ICassette> GetCassetteList(int count)
        {
            List<ICassette> cassettes = new List<ICassette>();

            for (int i = 0; i < count; i++)
            {
                cassettes.Add(GetCassetteMock(GetRandomString(8)));
            }

            return cassettes;
        }

        private static ICassette GetCassetteMock(string cassetteId)
        {
            Mock<ICassette> cassette = new Mock<ICassette>();
            cassette.Setup(x => x.CassetteId).Returns(cassetteId);
            return cassette.Object;
        }

        #endregion

        #endregion

        #region Demount Simulator help methods

        public static DemountState GetRandomDemountState()
        {
            int randomInteger = _random.Next(0, 20);
            if (randomInteger >= 15)
                return DemountState.AutoDemount;
            else if (randomInteger >= 10)
                return DemountState.Standby;
            else if (randomInteger >= 5)
                return DemountState.Stop;
            return DemountState.Alarm;
        }

        public static DemountCassetteHopper GetRandomDemountCassetteHopper()
        {
            int randomInteger = _random.Next(0, 20);
            if (randomInteger >= 15)
                return DemountCassetteHopper.Hopper1;
            else if (randomInteger >= 10)
                return DemountCassetteHopper.Hopper2;
            else if (randomInteger >= 5)
                return DemountCassetteHopper.Hopper3;
            return DemountCassetteHopper.Hopper4;
        }

        public static DemountCassetteStation GetRandomDemountCassetteStation()
        {
            int randomInteger = _random.Next(0, 20);
            if (randomInteger >= 15)
                return DemountCassetteStation.Station1;
            else if (randomInteger >= 10)
                return DemountCassetteStation.Station2;
            else if (randomInteger >= 5)
                return DemountCassetteStation.Station3;
            return DemountCassetteStation.Station4;
        }

        #endregion

        #region overall simulator help methods

        public static string GetRandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(
                Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)])
                .ToArray());
        }

        public static ushort GetRandomUshort()
        {
            return (ushort)_random.Next(0, 65535);
        }

        public static double GetRandomDouble(int decDigits)
        {
            return _random.Next(0, 65535) / Math.Pow(10, decDigits);
        }

        public static WaferSize GetRandomWaferSize()
        {
            int randomInteger = _random.Next(0, 21);
            if (randomInteger >= 10)
                return WaferSize.Size6Inches;
            return WaferSize.Size8Inches;
        }

        public static bool CheckValuesInArray(ushort[] memory, int from, int to, int expectedValue)
        {
            for (int index = from; index <= to; index++)
            {
                if (memory[index] != expectedValue)
                    return false;
            }
            return true;
        }

        #endregion
    }
}
