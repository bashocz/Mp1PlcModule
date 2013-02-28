using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    [Ignore]
    public class MountSimulatorPlcTest
    {
        #region private members

        private Random _random = new Random();

        private void CompareObjects(MountSimulatorPlcCommunication simulator, IMountStatus status)
        {          
            Assert.AreEqual<string>(simulator.CassetteId, status.NewLotCassette.CassetteId);
            Assert.AreEqual<bool>(simulator.IsCassetteId, status.NewLotCassette.IsCassetteId);
            Assert.AreEqual<bool>(simulator.IsLotDataTimeout, status.IsLotDataTimeout);
            Assert.AreEqual<string>(simulator.NewLotId, status.NewLotStarted.LotId);
            Assert.AreEqual<MountLine>(simulator.Line, status.NewLotStarted.Line);
            Assert.AreEqual<bool>(simulator.IsLotStarted, status.NewLotStarted.IsLotStarted);
            Assert.AreEqual<bool>(simulator.IsCarrierPlateArrived, status.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(simulator.IsCarrierPlateMountingReady, status.IsCarrierPlateMountingReady);
            Assert.AreEqual<int>(simulator.WaferBreakNumber, status.WaferBreakNumber);
            Assert.AreEqual<bool>(simulator.IsMountingCarrierPlateError, status.IsMountingErrorCarrierPlate);
            Assert.AreEqual<bool>(simulator.IsLotEnded, status.IsEndLot);
            Assert.AreEqual<bool>(simulator.IsReservationLotCanceled, status.IsReservationLotCanceled);
            Assert.AreEqual<MountState>(simulator.State, status.State);
        }

        private void CompareObjects(MountSimulatorPlcCommunication simulator, bool isCassetteIdError, string newLotId, MountLine line, bool isNewLotStarted,
            bool isBarcodeReadedOk, bool isCarrierPlateMountingReady, bool isWaferBreakInfoOk, bool isMountingCarrierPlateError, bool isLotEnded,
            bool isReservationLotCanceled, bool isInformationSystemError, ILotData lotData)
        {
            Assert.AreEqual<bool>(isCassetteIdError, simulator.IsCassetteIdError);
            Assert.AreEqual<string>(newLotId, simulator.NewLotId);
            Assert.AreEqual<MountLine>(line, simulator.Line);
            Assert.AreEqual<bool>(isNewLotStarted, simulator.IsLotStarted);
            Assert.AreEqual<bool>(isBarcodeReadedOk, simulator.IsBarcodeReadedOk);
            Assert.AreEqual<bool>(isCarrierPlateMountingReady, simulator.IsCarrierPlateMountingReady);
            Assert.AreEqual<bool>(isWaferBreakInfoOk, simulator.IsWaferBreakInfoOk);
            Assert.AreEqual<bool>(isMountingCarrierPlateError, simulator.IsMountingCarrierPlateError);
            Assert.AreEqual<bool>(isLotEnded, simulator.IsLotEnded);
            Assert.AreEqual<bool>(isReservationLotCanceled, simulator.IsReservationLotCanceled);
            Assert.AreEqual<bool>(isInformationSystemError, simulator.InformationSystemError);

            // Check lot data - START
            Assert.AreEqual<string>(lotData.LotId, simulator.LotId);
            for (int idx = 0; idx < lotData.Cassettes.Count; idx++)
            {
                Assert.AreEqual<string>(lotData.Cassettes[idx].CassetteId, simulator.Cassettes[idx].Id);
            }
            for (int idx = lotData.Cassettes.Count; idx < 12; idx++)
            {
                Assert.AreEqual<string>("", simulator.Cassettes[idx].Id);
            }
            Assert.AreEqual<int>(lotData.Cassettes.Count, simulator.CassetteQuantityInLot);
            Assert.AreEqual<LotDataTransmission>(LotDataTransmission.Cleared, simulator.LotDataTransmission);
            Assert.AreEqual<int>(lotData.Wafers.Count, simulator.WaferQuantity);
            Assert.AreEqual<int>(lotData.NGWaferCount, simulator.NotGoodWaferQuantity);
            Assert.AreEqual<WaferSize>(lotData.WaferSize, simulator.Size);
            Assert.AreEqual<OfType>(lotData.OfType, simulator.Type);
            Assert.AreEqual<PolishDivision>(lotData.PolishDivision, simulator.Division);
            Assert.AreEqual<int>(lotData.Assembly1.CarrierPlateCount, simulator.CarrierPlateQuantity1);
            Assert.AreEqual<int>(lotData.Assembly1.WaferCount, simulator.WaferQuantity1);
            Assert.AreEqual<int>(lotData.Assembly2.CarrierPlateCount, simulator.CarrierPlateQuantity2);
            Assert.AreEqual<int>(lotData.Assembly2.WaferCount, simulator.WaferQuantity2);

            for (int idx = 0; idx < lotData.Wafers.Count; idx++)
            {
                Assert.AreEqual<int>(lotData.Wafers[idx].CassetteNumber, simulator.Wafers[idx].WaferCassetteNumber);
                Assert.AreEqual<int>(lotData.Wafers[idx].SlotNumber, simulator.Wafers[idx].WaferSlotNumber);
            }
            // Check lot data - END
        }

        private void ChangeStatusData(MountSimulatorPlcCommunication simulator)
        {
            for (int i = 0; i < _random.Next(1, 7); i++)
            {
                switch (_random.Next(1, 7))
                {
                    case 1:
                        simulator.CassetteId = SimulatorHelper.GetRandomString(8);
                        break;
                    case 2:
                        simulator.IsCassetteId = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                    case 3:
                        simulator.IsLotDataTimeout = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                    case 4:
                        simulator.IsCarrierPlateArrived = (_random.NextDouble() > 0.5) ? true : false;
                        break;
                    case 5:
                        simulator.WaferBreakNumber = SimulatorHelper.GetRandomUshort();
                        break;
                    case 6:
                        simulator.State = SimulatorHelper.GetRandomMountState();
                        break;
                }
            }
        }
        #endregion

        #region MountPlc integration test

        [TestMethod()]
        [TestCategory("Integration")]
        public void MountPlcIntegrationTest()
        {
            MountSimulatorPlcCommunication simulator = MountSimulatorPlcCommunication.Create();

            IMountPlc target = new MountPlc(simulator);
            target.Open();

            bool isCassetteIdError = false;
            string newLotId = "";
            MountLine line = MountLine.Both;
            bool isNewLotStarted = false;

            bool isBarcodeReadedOk = false;
            bool isCarrierPlateMountingReady = false;
            bool isWaferBreakInfoOk = false;
            bool isMountingCarrierPlateError = false;

            bool isLotEnded = false;
            bool isReservationLotCanceled = false;
            bool isInformationSystemError = false;

            ILotData lotData = SimulatorHelper.GetDefaultLotData();

            IMountStatus status = target.GetStatus();
            CompareObjects(simulator, status);

            for (int i = 0; i < 1000; i++)
            {
                // "PLC" randomly clear/setup memory 
                if (_random.NextDouble() > 0.40)
                {
                    switch (_random.Next(1, 10))
                    {
                        case 1:
                            isCassetteIdError = false;
                            simulator.IsCassetteIdError = isCassetteIdError;
                            break;
                        case 2:
                            newLotId = SimulatorHelper.GetRandomString(14);
                            simulator.NewLotId = newLotId; 
                          
                            line = SimulatorHelper.GetRandomMountLine();
                            simulator.Line = line;

                            isNewLotStarted = true;
                            simulator.IsLotStarted = isNewLotStarted;
                            break;
                        case 3:
                            isBarcodeReadedOk = false;
                            simulator.IsBarcodeReadedOk = isBarcodeReadedOk;
                            break;
                        case 4:
                            isCarrierPlateMountingReady = true;
                            simulator.IsCarrierPlateMountingReady = isCarrierPlateMountingReady;
                            break;
                        case 5:
                            isWaferBreakInfoOk = false;
                            simulator.IsWaferBreakInfoOk = isWaferBreakInfoOk;
                            break;
                        case 6:
                            isMountingCarrierPlateError = true;
                            simulator.IsMountingCarrierPlateError = isMountingCarrierPlateError;
                            break;
                        case 7:
                            isLotEnded = true;
                            simulator.IsLotEnded = isLotEnded;                            
                            break;
                        case 8:
                            isReservationLotCanceled = true;
                            simulator.IsReservationLotCanceled = isReservationLotCanceled;     
                            break;
                        case 9:
                            lotData = SimulatorHelper.GetDefaultLotData();
                            simulator.LotId = lotData.LotId;
                            for (int idx = 0; idx < 12; idx++)
                            {
                                simulator.Cassettes[idx].Id = string.Empty;
                            }
                            simulator.CassetteQuantityInLot = lotData.Cassettes.Count;

                            for (int idx = 0; idx < 300; idx++)
                            {
                                simulator.Wafers[idx].WaferCassetteNumber = 0;
                                simulator.Wafers[idx].WaferSlotNumber = 0;
                            }
                            simulator.WaferQuantity = lotData.Wafers.Count;

                            simulator.NotGoodWaferQuantity = lotData.NGWaferCount;
                            simulator.Size = lotData.WaferSize;
                            simulator.Type = lotData.OfType;
                            simulator.Division = lotData.PolishDivision;                           
                            simulator.CarrierPlateQuantity1 = lotData.Assembly1.CarrierPlateCount;
                            simulator.WaferQuantity1 = lotData.Assembly1.WaferCount;
                            simulator.CarrierPlateQuantity2 = lotData.Assembly2.CarrierPlateCount;
                            simulator.WaferQuantity2 = lotData.Assembly2.WaferCount;                          
                            break;
                    }
                }

                // "Information system" randomly write to memory
                if (_random.NextDouble() > 0.50)
                {
                    switch (_random.Next(1, 11))
                    {
                        case 1:
                            target.AcceptNewLot(false);
                            isCassetteIdError = true;
                            CompareObjects(simulator, isCassetteIdError, newLotId, line, isNewLotStarted, isBarcodeReadedOk, isCarrierPlateMountingReady, 
                                           isWaferBreakInfoOk, isMountingCarrierPlateError, isLotEnded, isReservationLotCanceled, isInformationSystemError, lotData);
                            break;
                        case 2:
                            target.ClearNewLotStartData();
                            newLotId = "";
                            line = MountLine.Cleared;
                            isNewLotStarted = false;
                            CompareObjects(simulator, isCassetteIdError, newLotId, line, isNewLotStarted, isBarcodeReadedOk, isCarrierPlateMountingReady,
                                           isWaferBreakInfoOk, isMountingCarrierPlateError, isLotEnded, isReservationLotCanceled, isInformationSystemError, lotData);
                            break;
                        case 3:
                            target.CarrierPlateBarcodeSuccesfullyReaded();
                            isBarcodeReadedOk = true;
                            CompareObjects(simulator, isCassetteIdError, newLotId, line, isNewLotStarted, isBarcodeReadedOk, isCarrierPlateMountingReady,
                                           isWaferBreakInfoOk, isMountingCarrierPlateError, isLotEnded, isReservationLotCanceled, isInformationSystemError, lotData);
                            break;
                        case 4:
                            target.ClearCarrierPlateMountingReadyFlag();
                            isCarrierPlateMountingReady = false;
                            CompareObjects(simulator, isCassetteIdError, newLotId, line, isNewLotStarted, isBarcodeReadedOk, isCarrierPlateMountingReady,
                                           isWaferBreakInfoOk, isMountingCarrierPlateError, isLotEnded, isReservationLotCanceled, isInformationSystemError, lotData);
                            break;
                        case 5:
                            target.AcceptWaferBreakNumber();
                            isWaferBreakInfoOk = true;
                            CompareObjects(simulator, isCassetteIdError, newLotId, line, isNewLotStarted, isBarcodeReadedOk, isCarrierPlateMountingReady,
                                           isWaferBreakInfoOk, isMountingCarrierPlateError, isLotEnded, isReservationLotCanceled, isInformationSystemError, lotData);
                            break;
                        case 6:
                            target.ClearMountingErrorCarrierPlateFlag();
                            isMountingCarrierPlateError = false;
                            CompareObjects(simulator, isCassetteIdError, newLotId, line, isNewLotStarted, isBarcodeReadedOk, isCarrierPlateMountingReady,
                                           isWaferBreakInfoOk, isMountingCarrierPlateError, isLotEnded, isReservationLotCanceled, isInformationSystemError, lotData);
                            break;
                        case 7:
                            target.ClearLotEndFlag();
                            isLotEnded = false;
                            CompareObjects(simulator, isCassetteIdError, newLotId, line, isNewLotStarted, isBarcodeReadedOk, isCarrierPlateMountingReady,
                                           isWaferBreakInfoOk, isMountingCarrierPlateError, isLotEnded, isReservationLotCanceled, isInformationSystemError, lotData);
                            break;
                        case 8:
                            target.ClearReservationLotCancelFlag();
                            isReservationLotCanceled = false; ;
                            CompareObjects(simulator, isCassetteIdError, newLotId, line, isNewLotStarted, isBarcodeReadedOk, isCarrierPlateMountingReady,
                                           isWaferBreakInfoOk, isMountingCarrierPlateError, isLotEnded, isReservationLotCanceled, isInformationSystemError, lotData);
                            break;
                        case 9:
                            isInformationSystemError = (_random.NextDouble() > 0.5) ? true : false;
                            target.WriteBarcodeError(isInformationSystemError);
                            CompareObjects(simulator, isCassetteIdError, newLotId, line, isNewLotStarted, isBarcodeReadedOk, isCarrierPlateMountingReady,
                                           isWaferBreakInfoOk, isMountingCarrierPlateError, isLotEnded, isReservationLotCanceled, isInformationSystemError, lotData);
                            break;
                        case 10:
                            lotData = SimulatorHelper.GetRandomLotData();
                            target.SetLotData(lotData);
                            CompareObjects(simulator, isCassetteIdError, newLotId, line, isNewLotStarted, isBarcodeReadedOk, isCarrierPlateMountingReady,
                                           isWaferBreakInfoOk, isMountingCarrierPlateError, isLotEnded, isReservationLotCanceled, isInformationSystemError, lotData);
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