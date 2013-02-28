using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    [Ignore]
    public class DemountSimulatorPlcTest
    {
        #region private members

        private Random _random = new Random();

        private void CompareObjects(DemountSimulatorPlcCommunication simulator, IDemountStatus status)
        {
            Assert.AreEqual<bool>(simulator.IsCarrierPlateArrived, status.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(simulator.IsCarrierPlateDemountStarted, status.IsCarrierPlateDemountStarted);
            Assert.AreEqual<int>(simulator.WaferDemountCounter, status.DemountInfo.DemountedWaferCount);
            Assert.AreEqual<bool>(simulator.IsCarrierPlateDemounted, status.DemountInfo.Completed);          
            Assert.AreEqual<DemountCassetteHopper>(simulator.CanReadCassetteBarcode, status.CanReadCassetteBarcode);           
            Assert.AreEqual<bool>(simulator.IsCassette[0].IsCassettePositioned, status.AreCassettes[0]);
            Assert.AreEqual<bool>(simulator.IsCassette[1].IsCassettePositioned, status.AreCassettes[1]);
            Assert.AreEqual<bool>(simulator.IsCassette[2].IsCassettePositioned, status.AreCassettes[2]);
            Assert.AreEqual<bool>(simulator.IsCassette[3].IsCassettePositioned, status.AreCassettes[3]);
            Assert.AreEqual<DemountState>(simulator.State, status.State);
        }

        private void CompareObjects(DemountSimulatorPlcCommunication simulator, bool barcodeError, bool isCarrierPlateBarcodeReadedOk,
            WaferSize carrierPlateWaferSize, int waferCount, DemountCassetteStation station, CarrierPlateRoutingType routingType, DemountCassetteStation from,
            WaferSize cassetteWaferSize, DemountCassetteHopper destination, bool isCassetteBarcodeReadedOk, bool spatulaInspectionRequired)
        {
            Assert.AreEqual<bool>(barcodeError, simulator.IsInformationSystemError);
            Assert.AreEqual<bool>(isCarrierPlateBarcodeReadedOk, simulator.IsCarrierPlateBarcodeReadedOk);
            Assert.AreEqual<WaferSize>(carrierPlateWaferSize, simulator.CarrierPlateWaferSize);
            Assert.AreEqual<int>(waferCount, simulator.CarrierPlateWaferCount);
            Assert.AreEqual<DemountCassetteStation>(station, simulator.DemountCassetteStation);
            Assert.AreEqual<CarrierPlateRoutingType>(routingType, simulator.CarrierPlateRoutingType);
            Assert.AreEqual<DemountCassetteStation>(from, simulator.RemoveCassetteFromDemountStation);
            Assert.AreEqual<WaferSize>(cassetteWaferSize, simulator.CassetteWaferSize);
            Assert.AreEqual<DemountCassetteHopper>(destination, simulator.DestinationStation);
            Assert.AreEqual<bool>(isCassetteBarcodeReadedOk, simulator.IsCassetteBarcodeReadedOk);
            Assert.AreEqual<bool>(spatulaInspectionRequired, simulator.ShouldInspectSpatula);
        }

        private void ChangeStatusData(DemountSimulatorPlcCommunication simulator)
        {
            for (int i = 0; i < _random.Next(1, 11); i++)
            {
                switch (_random.Next(1, 11))
                {
                    case 1:
                        simulator.IsCarrierPlateArrived = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                    case 2:
                        simulator.IsCarrierPlateDemountStarted = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                    case 3:
                        simulator.WaferDemountCounter = SimulatorHelper.GetRandomUshort();
                        break;
                    case 4:
                        simulator.IsCarrierPlateDemounted = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                    case 5:
                        simulator.CanReadCassetteBarcode = SimulatorHelper.GetRandomDemountCassetteHopper();
                        break;
                    case 6:
                        simulator.IsCassette[0].IsCassettePositioned = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                    case 7:
                        simulator.IsCassette[1].IsCassettePositioned = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                    case 8:
                        simulator.IsCassette[2].IsCassettePositioned = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                    case 9:
                        simulator.IsCassette[3].IsCassettePositioned = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                    case 10:
                        simulator.State = SimulatorHelper.GetRandomDemountState();
                        break;
                }
            }
        }

        #endregion

        #region DemountPlc integration test

        [TestMethod()]
        [TestCategory("Integration")]
        public void DemountPlcIntegrationTest()
        {
            DemountSimulatorPlcCommunication simulator = DemountSimulatorPlcCommunication.Create();

            IDemountPlc target = new DemountPlc(simulator);
            target.Open();

            bool barcodeError = false;
            bool isCarrierPlateBarcodeReadedOk = false;
            WaferSize carrierPlateWaferSize = WaferSize.AnySize;
            int waferCount = 0;
            DemountCassetteStation station = DemountCassetteStation.Cleared;
            CarrierPlateRoutingType routingType = CarrierPlateRoutingType.Cleared;
            DemountCassetteStation from = DemountCassetteStation.Cleared;
            WaferSize cassetteWaferSize = WaferSize.AnySize;
            DemountCassetteHopper destination = DemountCassetteHopper.Cleared;
            bool isCassetteBarcodeReadedOk = false;
            bool spatulaInspectionRequired = false;

            IDemountStatus status = target.GetStatus();
            CompareObjects(simulator, status);

            for (int i = 0; i < 10000; i++)
            {
                // "PLC" randomly clear memory
                if (_random.NextDouble() > 0.40)
                {
                    switch (_random.Next(2, 8))
                    {
                        case 2:
                            isCarrierPlateBarcodeReadedOk = false;
                            simulator.IsCarrierPlateBarcodeReadedOk = isCarrierPlateBarcodeReadedOk;
                            break;
                        case 3:
                            carrierPlateWaferSize = WaferSize.AnySize;
                            simulator.CarrierPlateWaferSize = carrierPlateWaferSize;
                            
                            waferCount = 0;
                            simulator.CarrierPlateWaferCount = waferCount;
                            
                            station = DemountCassetteStation.Cleared;
                            simulator.DemountCassetteStation = station;
                            break;
                        case 4:
                            routingType = CarrierPlateRoutingType.Cleared;
                            simulator.CarrierPlateRoutingType = routingType;
                            break;
                        case 5:
                            from = DemountCassetteStation.Cleared;
                            simulator.RemoveCassetteFromDemountStation = from;

                            cassetteWaferSize = WaferSize.AnySize;
                            simulator.CassetteWaferSize = cassetteWaferSize; 

                            destination = DemountCassetteHopper.Cleared;
                            simulator.DestinationStation = destination;    
                            break;
                        case 6:
                            isCassetteBarcodeReadedOk = false;
                            simulator.IsCassetteBarcodeReadedOk = isCassetteBarcodeReadedOk;
                            
                            break;
                        case 7:
                            spatulaInspectionRequired = false;
                            simulator.ShouldInspectSpatula = spatulaInspectionRequired;
                            break;
                    }
                }

                // "Information system" randomly write to memory
                if (_random.NextDouble() > 0.50)
                {
                    switch (_random.Next(1, 8))
                    {
                        case 1:
                            barcodeError = (_random.NextDouble() > 0.5) ? true : false;
                            target.WriteBarcodeError(barcodeError);
                            CompareObjects(simulator, barcodeError, isCarrierPlateBarcodeReadedOk, carrierPlateWaferSize, waferCount, station, 
                                           routingType, from, cassetteWaferSize, destination, isCassetteBarcodeReadedOk, spatulaInspectionRequired);
                            break;
                        case 2:
                            target.CarrierPlateBarcodeSuccesfullyReaded();
                            isCarrierPlateBarcodeReadedOk = true;
                            CompareObjects(simulator, barcodeError, isCarrierPlateBarcodeReadedOk, carrierPlateWaferSize, waferCount, station,
                                           routingType, from, cassetteWaferSize, destination, isCassetteBarcodeReadedOk, spatulaInspectionRequired);
                            break;
                        case 3:
                            carrierPlateWaferSize = SimulatorHelper.GetRandomWaferSize();
                            waferCount = _random.Next(3, 6);
                            station = SimulatorHelper.GetRandomDemountCassetteStation();
                            target.StartDemounting(carrierPlateWaferSize, waferCount, station);
                            CompareObjects(simulator, barcodeError, isCarrierPlateBarcodeReadedOk, carrierPlateWaferSize, waferCount, station,
                                           routingType, from, cassetteWaferSize, destination, isCassetteBarcodeReadedOk, spatulaInspectionRequired);
                            break;
                        case 4:
                            routingType = (_random.NextDouble() > 0.5) ? CarrierPlateRoutingType.BackThroughAwps : CarrierPlateRoutingType.InspectionRequired;
                            target.CarrierPlateRouting(routingType);
                            CompareObjects(simulator, barcodeError, isCarrierPlateBarcodeReadedOk, carrierPlateWaferSize, waferCount, station,
                                           routingType, from, cassetteWaferSize, destination, isCassetteBarcodeReadedOk, spatulaInspectionRequired);
                            break;
                        case 5:
                            from = SimulatorHelper.GetRandomDemountCassetteStation();
                            cassetteWaferSize = SimulatorHelper.GetRandomWaferSize();
                            destination = SimulatorHelper.GetRandomDemountCassetteHopper();
                            target.ChangeCassette(from, cassetteWaferSize, destination);
                            CompareObjects(simulator, barcodeError, isCarrierPlateBarcodeReadedOk, carrierPlateWaferSize, waferCount, station,
                                           routingType, from, cassetteWaferSize, destination, isCassetteBarcodeReadedOk, spatulaInspectionRequired);
                            break;
                        case 6:
                            target.CassetteBarcodeSuccesfullyRead();
                            isCassetteBarcodeReadedOk = true;
                            CompareObjects(simulator, barcodeError, isCarrierPlateBarcodeReadedOk, carrierPlateWaferSize, waferCount, station,
                                           routingType, from, cassetteWaferSize, destination, isCassetteBarcodeReadedOk, spatulaInspectionRequired);
                            break;
                        case 7:
                            target.SpatulaInspectionRequired();
                            spatulaInspectionRequired = true;
                            CompareObjects(simulator, barcodeError, isCarrierPlateBarcodeReadedOk, carrierPlateWaferSize, waferCount, station,
                                           routingType, from, cassetteWaferSize, destination, isCassetteBarcodeReadedOk, spatulaInspectionRequired);
                            break;
                    }
                }

                ChangeStatusData(simulator);
                status = target.GetStatus();
                CompareObjects(simulator, status);
            }
        }

        #endregion
    }
}
