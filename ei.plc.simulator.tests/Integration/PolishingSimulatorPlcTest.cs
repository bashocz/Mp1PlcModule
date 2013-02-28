using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    [Ignore]
    public class PolishingSimulatorPlcTest
    {
        #region private members

        private Random _random = new Random();

        private void CompareObjects(PolishingSimulatorPlcCommunication simulator, bool isMagazineArrived, IPolishingFullStatus status)
        {
            // Magazine Arrival Flag
            Assert.AreEqual<bool>(simulator.MagazineArrived, isMagazineArrived);

            // HighPressures from address 0x140 to 0x142
            Assert.AreEqual(simulator.Polishers[0].HighPressureGlobal, status.Status[0].HighPressure);
            Assert.AreEqual(simulator.Polishers[1].HighPressureGlobal, status.Status[1].HighPressure);
            Assert.AreEqual(simulator.Polishers[2].HighPressureGlobal, status.Status[2].HighPressure);

            // Status
            Assert.AreEqual<PolisherState>(simulator.Polishers[0].State, status.Status[0].State);
            Assert.AreEqual<PolisherState>(simulator.Polishers[1].State, status.Status[1].State);
            Assert.AreEqual<PolisherState>(simulator.Polishers[2].State, status.Status[2].State);

            // Magazine
            // Magazine's ID
            Assert.AreEqual<string>(simulator.Polishers[0].MagazineId, status.Status[0].Magazine.Id);
            Assert.AreEqual<string>(simulator.Polishers[1].MagazineId, status.Status[1].Magazine.Id);
            Assert.AreEqual<string>(simulator.Polishers[2].MagazineId, status.Status[2].Magazine.Id);

            // Plates's ID
            Assert.AreEqual<string>(simulator.Polishers[0].Plates[0].Id, status.Status[0].Magazine.Plates[0].Id);
            Assert.AreEqual<string>(simulator.Polishers[0].Plates[1].Id, status.Status[0].Magazine.Plates[1].Id);
            Assert.AreEqual<string>(simulator.Polishers[0].Plates[2].Id, status.Status[0].Magazine.Plates[2].Id);
            Assert.AreEqual<string>(simulator.Polishers[0].Plates[3].Id, status.Status[0].Magazine.Plates[3].Id);

            Assert.AreEqual<string>(simulator.Polishers[1].Plates[0].Id, status.Status[1].Magazine.Plates[0].Id);
            Assert.AreEqual<string>(simulator.Polishers[1].Plates[1].Id, status.Status[1].Magazine.Plates[1].Id);
            Assert.AreEqual<string>(simulator.Polishers[1].Plates[2].Id, status.Status[1].Magazine.Plates[2].Id);
            Assert.AreEqual<string>(simulator.Polishers[1].Plates[3].Id, status.Status[1].Magazine.Plates[3].Id);

            Assert.AreEqual<string>(simulator.Polishers[2].Plates[0].Id, status.Status[2].Magazine.Plates[0].Id);
            Assert.AreEqual<string>(simulator.Polishers[2].Plates[1].Id, status.Status[2].Magazine.Plates[1].Id);
            Assert.AreEqual<string>(simulator.Polishers[2].Plates[2].Id, status.Status[2].Magazine.Plates[2].Id);
            Assert.AreEqual<string>(simulator.Polishers[2].Plates[3].Id, status.Status[2].Magazine.Plates[3].Id);

            // HighPressure
            Assert.AreEqual<bool>(simulator.Polishers[0].HighPressure, status.Status[0].HighPressure);
            Assert.AreEqual<bool>(simulator.Polishers[1].HighPressure, status.Status[1].HighPressure);
            Assert.AreEqual<bool>(simulator.Polishers[2].HighPressure, status.Status[2].HighPressure);

            // HighPressureDuration
            Assert.AreEqual<int>(simulator.Polishers[0].HighPressureDuration.Milliseconds, status.Status[0].HighPressureDuration.Milliseconds);
            Assert.AreEqual<int>(simulator.Polishers[1].HighPressureDuration.Milliseconds, status.Status[1].HighPressureDuration.Milliseconds);
            Assert.AreEqual<int>(simulator.Polishers[2].HighPressureDuration.Milliseconds, status.Status[2].HighPressureDuration.Milliseconds);

            // PlateRpm
            Assert.AreEqual<int>(simulator.Polishers[0].PlateRpm, status.Status[0].PlateRpm);
            Assert.AreEqual<int>(simulator.Polishers[1].PlateRpm, status.Status[1].PlateRpm);
            Assert.AreEqual<int>(simulator.Polishers[2].PlateRpm, status.Status[2].PlateRpm);

            // PlateLoadCurrent
            Assert.AreEqual<double>(simulator.Polishers[0].PlateLoadCurrent, status.Status[0].PlateLoadCurrent);
            Assert.AreEqual<double>(simulator.Polishers[1].PlateLoadCurrent, status.Status[1].PlateLoadCurrent);
            Assert.AreEqual<double>(simulator.Polishers[2].PlateLoadCurrent, status.Status[2].PlateLoadCurrent);

            // PolishingHeads
            // Force
            Assert.AreEqual<int>(simulator.Polishers[0].Heads[0].Force, status.Status[0].PolisherHeads[0].Force);
            Assert.AreEqual<int>(simulator.Polishers[0].Heads[1].Force, status.Status[0].PolisherHeads[1].Force);
            Assert.AreEqual<int>(simulator.Polishers[0].Heads[2].Force, status.Status[0].PolisherHeads[2].Force);
            Assert.AreEqual<int>(simulator.Polishers[0].Heads[3].Force, status.Status[0].PolisherHeads[3].Force);

            Assert.AreEqual<int>(simulator.Polishers[1].Heads[0].Force, status.Status[1].PolisherHeads[0].Force);
            Assert.AreEqual<int>(simulator.Polishers[1].Heads[1].Force, status.Status[1].PolisherHeads[1].Force);
            Assert.AreEqual<int>(simulator.Polishers[1].Heads[2].Force, status.Status[1].PolisherHeads[2].Force);
            Assert.AreEqual<int>(simulator.Polishers[1].Heads[3].Force, status.Status[1].PolisherHeads[3].Force);

            Assert.AreEqual<int>(simulator.Polishers[2].Heads[0].Force, status.Status[2].PolisherHeads[0].Force);
            Assert.AreEqual<int>(simulator.Polishers[2].Heads[1].Force, status.Status[2].PolisherHeads[1].Force);
            Assert.AreEqual<int>(simulator.Polishers[2].Heads[2].Force, status.Status[2].PolisherHeads[2].Force);
            Assert.AreEqual<int>(simulator.Polishers[2].Heads[3].Force, status.Status[2].PolisherHeads[3].Force);

            // Pressure
            Assert.AreEqual<double>(simulator.Polishers[0].Heads[0].Pressure, status.Status[0].PolisherHeads[0].Pressure);
            Assert.AreEqual<double>(simulator.Polishers[0].Heads[1].Pressure, status.Status[0].PolisherHeads[1].Pressure);
            Assert.AreEqual<double>(simulator.Polishers[0].Heads[2].Pressure, status.Status[0].PolisherHeads[2].Pressure);
            Assert.AreEqual<double>(simulator.Polishers[0].Heads[3].Pressure, status.Status[0].PolisherHeads[3].Pressure);

            Assert.AreEqual<double>(simulator.Polishers[1].Heads[0].Pressure, status.Status[1].PolisherHeads[0].Pressure);
            Assert.AreEqual<double>(simulator.Polishers[1].Heads[1].Pressure, status.Status[1].PolisherHeads[1].Pressure);
            Assert.AreEqual<double>(simulator.Polishers[1].Heads[2].Pressure, status.Status[1].PolisherHeads[2].Pressure);
            Assert.AreEqual<double>(simulator.Polishers[1].Heads[3].Pressure, status.Status[1].PolisherHeads[3].Pressure);

            Assert.AreEqual<double>(simulator.Polishers[2].Heads[0].Pressure, status.Status[2].PolisherHeads[0].Pressure);
            Assert.AreEqual<double>(simulator.Polishers[2].Heads[1].Pressure, status.Status[2].PolisherHeads[1].Pressure);
            Assert.AreEqual<double>(simulator.Polishers[2].Heads[2].Pressure, status.Status[2].PolisherHeads[2].Pressure);
            Assert.AreEqual<double>(simulator.Polishers[2].Heads[3].Pressure, status.Status[2].PolisherHeads[3].Pressure);

            // Backpressure
            Assert.AreEqual<double>(simulator.Polishers[0].Heads[0].Backpressure, status.Status[0].PolisherHeads[0].Backpressure);
            Assert.AreEqual<double>(simulator.Polishers[0].Heads[1].Backpressure, status.Status[0].PolisherHeads[1].Backpressure);
            Assert.AreEqual<double>(simulator.Polishers[0].Heads[2].Backpressure, status.Status[0].PolisherHeads[2].Backpressure);
            Assert.AreEqual<double>(simulator.Polishers[0].Heads[3].Backpressure, status.Status[0].PolisherHeads[3].Backpressure);

            Assert.AreEqual<double>(simulator.Polishers[1].Heads[0].Backpressure, status.Status[1].PolisherHeads[0].Backpressure);
            Assert.AreEqual<double>(simulator.Polishers[1].Heads[1].Backpressure, status.Status[1].PolisherHeads[1].Backpressure);
            Assert.AreEqual<double>(simulator.Polishers[1].Heads[2].Backpressure, status.Status[1].PolisherHeads[2].Backpressure);
            Assert.AreEqual<double>(simulator.Polishers[1].Heads[3].Backpressure, status.Status[1].PolisherHeads[3].Backpressure);

            Assert.AreEqual<double>(simulator.Polishers[2].Heads[0].Backpressure, status.Status[2].PolisherHeads[0].Backpressure);
            Assert.AreEqual<double>(simulator.Polishers[2].Heads[1].Backpressure, status.Status[2].PolisherHeads[1].Backpressure);
            Assert.AreEqual<double>(simulator.Polishers[2].Heads[2].Backpressure, status.Status[2].PolisherHeads[2].Backpressure);
            Assert.AreEqual<double>(simulator.Polishers[2].Heads[3].Backpressure, status.Status[2].PolisherHeads[3].Backpressure);

            // Rpm
            Assert.AreEqual<int>(simulator.Polishers[0].Heads[0].Rpm, status.Status[0].PolisherHeads[0].Rpm);
            Assert.AreEqual<int>(simulator.Polishers[0].Heads[1].Rpm, status.Status[0].PolisherHeads[1].Rpm);
            Assert.AreEqual<int>(simulator.Polishers[0].Heads[2].Rpm, status.Status[0].PolisherHeads[2].Rpm);
            Assert.AreEqual<int>(simulator.Polishers[0].Heads[3].Rpm, status.Status[0].PolisherHeads[3].Rpm);

            Assert.AreEqual<int>(simulator.Polishers[1].Heads[0].Rpm, status.Status[1].PolisherHeads[0].Rpm);
            Assert.AreEqual<int>(simulator.Polishers[1].Heads[1].Rpm, status.Status[1].PolisherHeads[1].Rpm);
            Assert.AreEqual<int>(simulator.Polishers[1].Heads[2].Rpm, status.Status[1].PolisherHeads[2].Rpm);
            Assert.AreEqual<int>(simulator.Polishers[1].Heads[3].Rpm, status.Status[1].PolisherHeads[3].Rpm);

            Assert.AreEqual<int>(simulator.Polishers[2].Heads[0].Rpm, status.Status[2].PolisherHeads[0].Rpm);
            Assert.AreEqual<int>(simulator.Polishers[2].Heads[1].Rpm, status.Status[2].PolisherHeads[1].Rpm);
            Assert.AreEqual<int>(simulator.Polishers[2].Heads[2].Rpm, status.Status[2].PolisherHeads[2].Rpm);
            Assert.AreEqual<int>(simulator.Polishers[2].Heads[3].Rpm, status.Status[2].PolisherHeads[3].Rpm);

            // LoadCurrent
            Assert.AreEqual<double>(simulator.Polishers[0].Heads[0].LoadCurrent, status.Status[0].PolisherHeads[0].LoadCurrent);
            Assert.AreEqual<double>(simulator.Polishers[0].Heads[1].LoadCurrent, status.Status[0].PolisherHeads[1].LoadCurrent);
            Assert.AreEqual<double>(simulator.Polishers[0].Heads[2].LoadCurrent, status.Status[0].PolisherHeads[2].LoadCurrent);
            Assert.AreEqual<double>(simulator.Polishers[0].Heads[3].LoadCurrent, status.Status[0].PolisherHeads[3].LoadCurrent);

            Assert.AreEqual<double>(simulator.Polishers[1].Heads[0].LoadCurrent, status.Status[1].PolisherHeads[0].LoadCurrent);
            Assert.AreEqual<double>(simulator.Polishers[1].Heads[1].LoadCurrent, status.Status[1].PolisherHeads[1].LoadCurrent);
            Assert.AreEqual<double>(simulator.Polishers[1].Heads[2].LoadCurrent, status.Status[1].PolisherHeads[2].LoadCurrent);
            Assert.AreEqual<double>(simulator.Polishers[1].Heads[3].LoadCurrent, status.Status[1].PolisherHeads[3].LoadCurrent);

            Assert.AreEqual<double>(simulator.Polishers[2].Heads[0].LoadCurrent, status.Status[2].PolisherHeads[0].LoadCurrent);
            Assert.AreEqual<double>(simulator.Polishers[2].Heads[1].LoadCurrent, status.Status[2].PolisherHeads[1].LoadCurrent);
            Assert.AreEqual<double>(simulator.Polishers[2].Heads[2].LoadCurrent, status.Status[2].PolisherHeads[2].LoadCurrent);
            Assert.AreEqual<double>(simulator.Polishers[2].Heads[3].LoadCurrent, status.Status[2].PolisherHeads[3].LoadCurrent);

            // PolishingLiquid
            Assert.AreEqual<double>(simulator.Polishers[0].Liquid.PadTemp, status.Status[0].PolisherLiquid.PadTemp);
            Assert.AreEqual<double>(simulator.Polishers[1].Liquid.PadTemp, status.Status[1].PolisherLiquid.PadTemp);
            Assert.AreEqual<double>(simulator.Polishers[2].Liquid.PadTemp, status.Status[2].PolisherLiquid.PadTemp);

            Assert.AreEqual<double>(simulator.Polishers[0].Liquid.CoolingWaterInTemp, status.Status[0].PolisherLiquid.CoolingWaterInTemp);
            Assert.AreEqual<double>(simulator.Polishers[1].Liquid.CoolingWaterInTemp, status.Status[1].PolisherLiquid.CoolingWaterInTemp);
            Assert.AreEqual<double>(simulator.Polishers[2].Liquid.CoolingWaterInTemp, status.Status[2].PolisherLiquid.CoolingWaterInTemp);

            Assert.AreEqual<double>(simulator.Polishers[0].Liquid.CoolingWaterOutTemp, status.Status[0].PolisherLiquid.CoolingWaterOutTemp);
            Assert.AreEqual<double>(simulator.Polishers[1].Liquid.CoolingWaterOutTemp, status.Status[1].PolisherLiquid.CoolingWaterOutTemp);
            Assert.AreEqual<double>(simulator.Polishers[2].Liquid.CoolingWaterOutTemp, status.Status[2].PolisherLiquid.CoolingWaterOutTemp);

            Assert.AreEqual<double>(simulator.Polishers[0].Liquid.SlurryInTemp, status.Status[0].PolisherLiquid.SlurryInTemp);
            Assert.AreEqual<double>(simulator.Polishers[1].Liquid.SlurryInTemp, status.Status[1].PolisherLiquid.SlurryInTemp);
            Assert.AreEqual<double>(simulator.Polishers[2].Liquid.SlurryInTemp, status.Status[2].PolisherLiquid.SlurryInTemp);

            Assert.AreEqual<double>(simulator.Polishers[0].Liquid.SlurryOutTemp, status.Status[0].PolisherLiquid.SlurryOutTemp);
            Assert.AreEqual<double>(simulator.Polishers[1].Liquid.SlurryOutTemp, status.Status[1].PolisherLiquid.SlurryOutTemp);
            Assert.AreEqual<double>(simulator.Polishers[2].Liquid.SlurryOutTemp, status.Status[2].PolisherLiquid.SlurryOutTemp);

            Assert.AreEqual<double>(simulator.Polishers[0].Liquid.RinseTemp, status.Status[0].PolisherLiquid.RinseTemp);
            Assert.AreEqual<double>(simulator.Polishers[1].Liquid.RinseTemp, status.Status[1].PolisherLiquid.RinseTemp);
            Assert.AreEqual<double>(simulator.Polishers[2].Liquid.RinseTemp, status.Status[2].PolisherLiquid.RinseTemp);

            Assert.AreEqual<double>(simulator.Polishers[0].Liquid.CoolingWaterAmount, status.Status[0].PolisherLiquid.CoolingWaterAmount);
            Assert.AreEqual<double>(simulator.Polishers[1].Liquid.CoolingWaterAmount, status.Status[1].PolisherLiquid.CoolingWaterAmount);
            Assert.AreEqual<double>(simulator.Polishers[2].Liquid.CoolingWaterAmount, status.Status[2].PolisherLiquid.CoolingWaterAmount);

            Assert.AreEqual<double>(simulator.Polishers[0].Liquid.SlurryAmount, status.Status[0].PolisherLiquid.SlurryAmount);
            Assert.AreEqual<double>(simulator.Polishers[1].Liquid.SlurryAmount, status.Status[1].PolisherLiquid.SlurryAmount);
            Assert.AreEqual<double>(simulator.Polishers[2].Liquid.SlurryAmount, status.Status[2].PolisherLiquid.SlurryAmount);

            Assert.AreEqual<double>(simulator.Polishers[0].Liquid.RinseAmount, status.Status[0].PolisherLiquid.RinseAmount);
            Assert.AreEqual<double>(simulator.Polishers[1].Liquid.RinseAmount, status.Status[1].PolisherLiquid.RinseAmount);
            Assert.AreEqual<double>(simulator.Polishers[2].Liquid.RinseAmount, status.Status[2].PolisherLiquid.RinseAmount);

            // PadUsedTime
            Assert.AreEqual<int>(simulator.Polishers[0].PadUsedTime.Milliseconds, status.Status[0].PadUsedTime.Milliseconds);
            Assert.AreEqual<int>(simulator.Polishers[1].PadUsedTime.Milliseconds, status.Status[1].PadUsedTime.Milliseconds);
            Assert.AreEqual<int>(simulator.Polishers[2].PadUsedTime.Milliseconds, status.Status[2].PadUsedTime.Milliseconds);

            // PadUsedCount
            Assert.AreEqual<int>(simulator.Polishers[0].PadUsedCount, status.Status[0].PadUsedCount);
            Assert.AreEqual<int>(simulator.Polishers[1].PadUsedCount, status.Status[1].PadUsedCount);
            Assert.AreEqual<int>(simulator.Polishers[2].PadUsedCount, status.Status[2].PadUsedCount);
        }

        private void CompareObjects(PolishingSimulatorPlcCommunication simulator, bool barcodeError, Magazine magazine)
        {
            // IS error flag
            Assert.AreEqual<bool>(simulator.BarcodeError, barcodeError);

            // Magazine ID
            Assert.AreEqual<string>(simulator.MagazineId, magazine.Id);

            // Plates ID's
            Assert.AreEqual<string>(simulator.Plates[0].Id, magazine.Plates[0].Id);
            Assert.AreEqual<string>(simulator.Plates[1].Id, magazine.Plates[1].Id);
            Assert.AreEqual<string>(simulator.Plates[2].Id, magazine.Plates[2].Id);
            Assert.AreEqual<string>(simulator.Plates[3].Id, magazine.Plates[3].Id);

            // Plates recipe's
            Assert.AreEqual<int>(simulator.Plates[0].Recipe, magazine.Plates[0].Recipe);
            Assert.AreEqual<int>(simulator.Plates[1].Recipe, magazine.Plates[1].Recipe);
            Assert.AreEqual<int>(simulator.Plates[2].Recipe, magazine.Plates[2].Recipe);
            Assert.AreEqual<int>(simulator.Plates[3].Recipe, magazine.Plates[3].Recipe);
        }

        private void ChangeStatusData(PolishingSimulatorPlcCommunication simulator)
        {
            for (int i = 0; i < _random.Next(1, 43); i++)
            {
                switch (_random.Next(1, 43))
                {
                    case 1:
                        simulator.MagazineArrived = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                    case 2:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].State = SimulatorHelper.GetRandomPolisherState();
                        break;
                    case 3:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].MagazineId = SimulatorHelper.GetRandomString(8);
                        break;
                    case 4:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Plates[0].Id = SimulatorHelper.GetRandomString(8);
                        break;
                    case 5:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Plates[1].Id = SimulatorHelper.GetRandomString(8);
                        break;
                    case 6:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Plates[2].Id = SimulatorHelper.GetRandomString(8);
                        break;
                    case 7:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Plates[3].Id = SimulatorHelper.GetRandomString(8);
                        break;
                    case 8:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].HighPressure = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                    case 9:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].HighPressureDuration = TimeSpan.FromMilliseconds(SimulatorHelper.GetRandomUshort());
                        break;
                    case 10:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[0].Force = SimulatorHelper.GetRandomUshort();
                        break;
                    case 11:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[1].Force = SimulatorHelper.GetRandomUshort();
                        break;
                    case 12:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[2].Force = SimulatorHelper.GetRandomUshort();
                        break;
                    case 13:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[3].Force = SimulatorHelper.GetRandomUshort();
                        break;
                    case 14:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Liquid.PadTemp = SimulatorHelper.GetRandomDouble(1);
                        break;
                    case 15:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Liquid.CoolingWaterInTemp = SimulatorHelper.GetRandomDouble(1);
                        break;
                    case 16:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Liquid.CoolingWaterOutTemp = SimulatorHelper.GetRandomDouble(1);
                        break;
                    case 17:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Liquid.SlurryInTemp = SimulatorHelper.GetRandomDouble(1);
                        break;
                    case 18:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Liquid.SlurryOutTemp = SimulatorHelper.GetRandomDouble(1);
                        break;
                    case 19:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Liquid.RinseTemp = SimulatorHelper.GetRandomDouble(1);
                        break;
                    case 20:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Liquid.CoolingWaterAmount = SimulatorHelper.GetRandomDouble(1);
                        break;
                    case 21:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Liquid.SlurryAmount = SimulatorHelper.GetRandomDouble(1);
                        break;
                    case 22:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Liquid.RinseAmount = SimulatorHelper.GetRandomDouble(1);
                        break;
                    case 23:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[0].Pressure = SimulatorHelper.GetRandomDouble(3);
                        break;
                    case 24:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[1].Pressure = SimulatorHelper.GetRandomDouble(3);
                        break;
                    case 25:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[2].Pressure = SimulatorHelper.GetRandomDouble(3);
                        break;
                    case 26:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[3].Pressure = SimulatorHelper.GetRandomDouble(3);
                        break;
                    case 27:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[0].Backpressure = SimulatorHelper.GetRandomDouble(2);
                        break;
                    case 28:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[1].Backpressure = SimulatorHelper.GetRandomDouble(2);
                        break;
                    case 29:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[2].Backpressure = SimulatorHelper.GetRandomDouble(2);
                        break;
                    case 30:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[3].Backpressure = SimulatorHelper.GetRandomDouble(2);
                        break;
                    case 31:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].PlateRpm = SimulatorHelper.GetRandomUshort();
                        break;
                    case 32:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[0].Rpm = SimulatorHelper.GetRandomUshort();
                        break;
                    case 33:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[1].Rpm = SimulatorHelper.GetRandomUshort();
                        break;
                    case 34:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[2].Rpm = SimulatorHelper.GetRandomUshort();
                        break;
                    case 35:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[3].Rpm = SimulatorHelper.GetRandomUshort();
                        break;
                    case 36:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].PlateLoadCurrent = SimulatorHelper.GetRandomDouble(1);
                        break;
                    case 37:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[0].LoadCurrent = SimulatorHelper.GetRandomDouble(1);
                        break;
                    case 38:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[1].LoadCurrent = SimulatorHelper.GetRandomDouble(1);
                        break;
                    case 39:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[2].LoadCurrent = SimulatorHelper.GetRandomDouble(1);
                        break;
                    case 40:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].Heads[3].LoadCurrent = SimulatorHelper.GetRandomDouble(1);
                        break;
                    case 41:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].PadUsedTime = TimeSpan.FromMilliseconds(SimulatorHelper.GetRandomUshort());
                        break;
                    case 42:
                        simulator.Polishers[SimulatorHelper.GetRandomPolisherId()].PadUsedCount = SimulatorHelper.GetRandomUshort();
                        break;
                }
            }
        }

        #endregion

        #region PolishingPlc integration tests

        [TestMethod()]
        [TestCategory("Integration")]
        public void PolishingPlcIntegrationTest()
        {
            PolishingSimulatorPlcCommunication simulator = PolishingSimulatorPlcCommunication.Create();

            IPolishLinePlc target = new PolishLinePlc(simulator);
            target.Open();

            bool barcodeError = false;
            Magazine magazine = SimulatorHelper.GetDefaultMagazine();

            CompareObjects(simulator, barcodeError, magazine);

            bool isMagazineArrived = target.IsMagazineArrived();
            IPolishingFullStatus status = target.GetFullStatus();
            CompareObjects(simulator, isMagazineArrived, status);

            for (int i = 0; i < 10000; i++)
            {
                // "PLC" randomly clear memory
                if (_random.NextDouble() > 0.40)
                {
                    switch (_random.Next(1, 2))
                    {
                        case 1:
                            magazine = SimulatorHelper.GetDefaultMagazine();
                            simulator.MagazineId = magazine.Id;
                            simulator.Plates[0].Id = magazine.Plates[0].Id;
                            simulator.Plates[0].Recipe = magazine.Plates[0].Recipe;
                            simulator.Plates[1].Id = magazine.Plates[1].Id;
                            simulator.Plates[1].Recipe = magazine.Plates[1].Recipe;
                            simulator.Plates[2].Id = magazine.Plates[2].Id;
                            simulator.Plates[2].Recipe = magazine.Plates[2].Recipe;
                            simulator.Plates[3].Id = magazine.Plates[3].Id;
                            simulator.Plates[3].Recipe = magazine.Plates[3].Recipe;
                            break;
                    }
                }

                // "Information system" randomly write to memory
                if (_random.NextDouble() > 0.50)
                {
                    switch (_random.Next(1, 3))
                    {
                        case 1:
                            barcodeError = (_random.NextDouble() > 0.5) ? true : false;
                            target.WriteBarcodeError(barcodeError);
                            CompareObjects(simulator, barcodeError, magazine);
                            break;
                        case 2:
                            magazine = new Magazine { Id = SimulatorHelper.GetRandomString(8) };
                            magazine.NewPlates(SimulatorHelper.GetPlateList(4));
                            target.ProcessRecipe(magazine);
                            CompareObjects(simulator, barcodeError, magazine);
                            break;
                    }
                }

                ChangeStatusData(simulator);
                isMagazineArrived = target.IsMagazineArrived();
                status = target.GetFullStatus();
                CompareObjects(simulator, isMagazineArrived, status);
            }
        }

        #endregion
    }
}