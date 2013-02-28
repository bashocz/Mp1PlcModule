using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    [Ignore]
    public class StockerSimulatorPlcTest
    {
        #region private members

        private Random _random = new Random();

        private void CompareObjects(StockerSimulatorPlcCommunication simulator, IStockerStatus status)
        {
            Assert.AreEqual<bool>(simulator.IsCarrierPlateArrived, status.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(simulator.IsMagazineFull, status.MagazineChangeRequest.IsMagazineFull);
            Assert.AreEqual<bool>(simulator.IsOperatorChangeRequest, status.MagazineChangeRequest.IsOperatorChangeRequest);
            Assert.AreEqual<bool>(simulator.IsMagazineChangeStarted, status.IsMagazineChangeStarted);
            Assert.AreEqual<WaferSize>(simulator.WaferSize, status.MagazineRequest.WaferSize);
            Assert.AreEqual<bool>(simulator.IsMagazineRequested, status.MagazineRequest.IsRequested);
            Assert.AreEqual<bool>(simulator.IsMagazineArrived, status.IsMagazineArrived);
        }

        private void CompareObjects(StockerSimulatorPlcCommunication simulator, bool barcodeError, CarrierPlateRouting routing, 
            bool isMagazineChanged, bool isMagazineBarcodeReadedOk, StockerInventory inventory, MagazineSelection selection)
        {
            Assert.AreEqual<bool>(barcodeError, simulator.BarcodeError);
            Assert.AreEqual<CarrierPlateRouting>(routing, simulator.CarrierPlateRouting);
            Assert.AreEqual<bool>(isMagazineChanged, simulator.IsMagazineChange);
            Assert.AreEqual<bool>(isMagazineBarcodeReadedOk, simulator.IsInputMagazineBarcodeOK);
            Assert.AreEqual<StockerInventory>(inventory, simulator.StockerInventory);
            Assert.AreEqual<MagazineSelection>(selection, simulator.Selection);
        }

        private void ChangeStatusData(StockerSimulatorPlcCommunication simulator)
        {
            for (int i = 0; i < _random.Next(1, 8); i++)
            {
                switch (_random.Next(1, 8))
                {
                    case 1:
                        simulator.IsCarrierPlateArrived = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                    case 2:
                        simulator.IsMagazineFull = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                    case 3:
                        simulator.IsOperatorChangeRequest = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                    case 4:
                        simulator.IsMagazineChangeStarted = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                    case 5:
                        simulator.WaferSize = SimulatorHelper.GetRandomWaferSize();
                        break;
                    case 6:
                        simulator.IsMagazineRequested = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                    case 7:
                        simulator.IsMagazineArrived = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                }
            }
        }

        #endregion

        #region StockerPlc integration test

        [TestMethod()]
        [TestCategory("Integration")]
        public void StockerPlcIntegrationTest()
        {
            StockerSimulatorPlcCommunication simulator = StockerSimulatorPlcCommunication.Create();

            IStockerPlc target = new StockerPlc(simulator);
            target.Open();

            bool barcodeError = false;
            CarrierPlateRouting routing = CarrierPlateRouting.Cleared;
            bool isMagazineChanged = false;
            bool isMagazineBarcodeReadedOk = false;
            StockerInventory inventory = StockerInventory.Cleared;
            MagazineSelection selection = MagazineSelection.Cleared;

            IStockerStatus status = target.GetStatus();
            CompareObjects(simulator, status);

            for (int i = 0; i < 100000; i++)
            {
                // "PLC" randomly clear memory 
                if (_random.NextDouble() > 0.40)
                {
                    switch (_random.Next(2, 7))
                    {
                        case 2:
                            routing = CarrierPlateRouting.Cleared;
                            simulator.CarrierPlateRouting = routing;
                            break;
                        case 3:
                            isMagazineChanged = false;
                            simulator.IsMagazineChange = isMagazineChanged;
                            break;
                        case 4:
                            isMagazineBarcodeReadedOk = false;
                            simulator.IsInputMagazineBarcodeOK = isMagazineBarcodeReadedOk;
                            break;
                        case 5:
                            inventory = StockerInventory.Cleared;
                            simulator.StockerInventory = inventory;
                            break;
                        case 6:
                            selection = MagazineSelection.Cleared;
                            simulator.Selection = selection;
                            break;
                    }
                }

                // "Information system" randomly write to memory
                if (_random.NextDouble() > 0.50)
                {
                    switch (_random.Next(1, 7))
                    {
                        case 1:
                            barcodeError = (_random.NextDouble() > 0.5) ? true : false;
                            target.WriteBarcodeError(barcodeError);
                            CompareObjects(simulator, barcodeError, routing, isMagazineChanged, isMagazineBarcodeReadedOk, inventory, selection);
                            break;
                        case 2:
                            routing = (_random.NextDouble() > 0.5) ? CarrierPlateRouting.InsertIntoMagazine : CarrierPlateRouting.Reject;
                            target.AcceptCarrierPlate(routing);
                            CompareObjects(simulator, barcodeError, routing, isMagazineChanged, isMagazineBarcodeReadedOk, inventory, selection);
                            break;
                        case 3:
                            target.MagazineChange();
                            isMagazineChanged = true;
                            CompareObjects(simulator, barcodeError, routing, isMagazineChanged, isMagazineBarcodeReadedOk, inventory, selection);
                            break;
                        case 4:
                            target.MagazineBarcodeSuccesfullyReaded();
                            isMagazineBarcodeReadedOk = true;
                            CompareObjects(simulator, barcodeError, routing, isMagazineChanged, isMagazineBarcodeReadedOk, inventory, selection);
                            break;
                        case 5:
                            inventory = (_random.NextDouble() > 0.5) ? StockerInventory.SizeAvailable : StockerInventory.SizeNotInStocker;
                            target.SetWaferSizeAvailable(inventory);
                            CompareObjects(simulator, barcodeError, routing, isMagazineChanged, isMagazineBarcodeReadedOk, inventory, selection);
                            break;
                        case 6:
                            selection = (_random.NextDouble() > 0.5) ? MagazineSelection.HasRequestedSize : MagazineSelection.DoesNotHaveRequestedSize;
                            target.AcceptMagazine(selection);
                            CompareObjects(simulator, barcodeError, routing, isMagazineChanged, isMagazineBarcodeReadedOk, inventory, selection);
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