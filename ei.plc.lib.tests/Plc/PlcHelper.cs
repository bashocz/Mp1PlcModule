using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using EI.Business;

namespace EI.Plc.Tests
{
    static class PlcHelper
    {
        #region ICommunication

        public static Mock<ICommunication> GetPlcCommunicationMock()
        {
            var plcComm = new Mock<ICommunication>();

            bool connected = false;
            plcComm.Setup(c => c.Connected).Returns(() => { return connected; });

            plcComm.Setup(c => c.Open()).Callback(() => { connected = true; }).Returns(true);
            plcComm.Setup(c => c.Close()).Callback(() => { connected = false; });

            return plcComm;
        }

        #endregion

        #region IPlcAddressSpace

        public static IPlcAddressSpace GetAddressSpace()
        {
            var addressSpace = new Mock<IPlcAddressSpace>();
            addressSpace.Setup(a => a.CheckAddress(It.IsAny<int>())).Returns(true);
            addressSpace.Setup(a => a.CheckAddress(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            return addressSpace.Object;
        }

        public static IPlcAddressSpace GetAddressSpace(int from, int to)
        {
            var addressSpace = new Mock<IPlcAddressSpace>();
            addressSpace.Setup(a => a.CheckAddress(It.IsAny<int>())).Returns(false);
            addressSpace.Setup(a => a.CheckAddress(It.IsInRange<int>(from, to, Range.Inclusive))).Returns(true);
            addressSpace.Setup(a => a.CheckAddress(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>((address, length) => { return (address >= from) && ((address + length) <= to); });
            return addressSpace.Object;
        }

        #endregion

        #region IMagazine

        public static IMagazine GetMagazine(string magazineId, List<ICarrierPlate> plates)
        {
            var magazine = new Mock<IMagazine>();
            magazine.Setup(x => x.Id).Returns(magazineId);
            magazine.Setup(x => x.Plates).Returns(plates);
            return magazine.Object;
        }

        #endregion

        #region IPlate

        public static List<ICarrierPlate> GetPlateList(int recipe, params string[] platesId)
        {
            List<ICarrierPlate> plates = new List<ICarrierPlate>();
            foreach (string plateId in platesId)
                plates.Add(GetPlateMock(recipe, plateId));
            return plates;
        }

        private static ICarrierPlate GetPlateMock(int recipe, string plateId)
        {
            Mock<ICarrierPlate> plate = new Mock<ICarrierPlate>();
            plate.Setup(x => x.Id).Returns(plateId);
            plate.Setup(x => x.Recipe).Returns(recipe);
            return plate.Object;
        }

        #endregion

        #region ICassette

        public static List<ICassette> GetCassetteList(int count)
        {
            List<ICassette> cassettes = new List<ICassette>();

            for (int i = 0; i < count; i++)
            {
                cassettes.Add(GetCassetteMock(GetRandomString(_random.Next(1, 9))));
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

        public static IList<IWafer> GetWaferSubList(IList<IWafer> list, int from, int count)
        {
            IList<IWafer> listOfWafers = new List<IWafer>();

            for (int idx = from; idx < from + count; idx++)
            {
                if (idx >= list.Count)
                    break;
                listOfWafers.Add(list[idx]);
            }

            return listOfWafers;
        }

        #endregion

        #region ILotData

        public static ILotData GetLotData()
        {
            var lotData = new Mock<ILotData>();
            lotData.Setup(x => x.LotId).Returns(GetRandomString(_random.Next(1, 15)));
            lotData.Setup(x => x.Cassettes).Returns(PlcHelper.GetCassetteList(5));
            lotData.Setup(x => x.NGWaferCount).Returns(112);
            lotData.Setup(x => x.WaferSize).Returns(WaferSize.Size8Inches);
            lotData.Setup(x => x.OfType).Returns(OfType.VNotch);
            lotData.Setup(x => x.PolishDivision).Returns(PolishDivision.New);
            lotData.Setup(x => x.Assembly1.CarrierPlateCount).Returns(14);
            lotData.Setup(x => x.Assembly1.WaferCount).Returns(3);
            lotData.Setup(x => x.Assembly2.CarrierPlateCount).Returns(26);
            lotData.Setup(x => x.Assembly2.WaferCount).Returns(8);
            lotData.Setup(x => x.Wafers).Returns(PlcHelper.GetWaferList(120, 5));

            return lotData.Object;
        }

        public static ILotData GetLotData(int nGWaferCount)
        {
            var lotData = new Mock<ILotData>();
            lotData.Setup(x => x.LotId).Returns(GetRandomString(_random.Next(1, 15)));
            lotData.Setup(x => x.Cassettes).Returns(PlcHelper.GetCassetteList(5));
            lotData.Setup(x => x.NGWaferCount).Returns(nGWaferCount);
            lotData.Setup(x => x.WaferSize).Returns(WaferSize.Size8Inches);
            lotData.Setup(x => x.OfType).Returns(OfType.VNotch);
            lotData.Setup(x => x.PolishDivision).Returns(PolishDivision.New);
            lotData.Setup(x => x.Assembly1.CarrierPlateCount).Returns(14);
            lotData.Setup(x => x.Assembly1.WaferCount).Returns(3);
            lotData.Setup(x => x.Assembly2.CarrierPlateCount).Returns(26);
            lotData.Setup(x => x.Assembly2.WaferCount).Returns(8);
            lotData.Setup(x => x.Wafers).Returns(PlcHelper.GetWaferList(120, 5));

            return lotData.Object;
        }

        public static ILotData GetLotData(int assembly1CarrierPlateCount, int assembly1WaferCount, int assembly2CarrierPlateCount, int assembly2WaferCount)
        {
            var lotData = new Mock<ILotData>();
            lotData.Setup(x => x.LotId).Returns(GetRandomString(_random.Next(1, 15)));
            lotData.Setup(x => x.Cassettes).Returns(PlcHelper.GetCassetteList(5));
            lotData.Setup(x => x.NGWaferCount).Returns(112);
            lotData.Setup(x => x.WaferSize).Returns(WaferSize.Size8Inches);
            lotData.Setup(x => x.OfType).Returns(OfType.VNotch);
            lotData.Setup(x => x.PolishDivision).Returns(PolishDivision.New);
            lotData.Setup(x => x.Assembly1.CarrierPlateCount).Returns(assembly1CarrierPlateCount);
            lotData.Setup(x => x.Assembly1.WaferCount).Returns(assembly1WaferCount);
            lotData.Setup(x => x.Assembly2.CarrierPlateCount).Returns(assembly2CarrierPlateCount);
            lotData.Setup(x => x.Assembly2.WaferCount).Returns(assembly2WaferCount);
            lotData.Setup(x => x.Wafers).Returns(PlcHelper.GetWaferList(120, 5));

            return lotData.Object;
        }

        public static ILotData GetLotData(string lotId, IList<ICassette> cassettes, IList<IWafer> wafers)
        {
            var lotData = new Mock<ILotData>();

            lotData.Setup(x => x.LotId).Returns(lotId);
            lotData.Setup(x => x.Cassettes).Returns(cassettes);
            lotData.Setup(x => x.NGWaferCount).Returns(112);
            lotData.Setup(x => x.WaferSize).Returns(WaferSize.Size8Inches);
            lotData.Setup(x => x.OfType).Returns(OfType.VNotch);
            lotData.Setup(x => x.PolishDivision).Returns(PolishDivision.New);
            lotData.Setup(x => x.Assembly1.CarrierPlateCount).Returns(14);
            lotData.Setup(x => x.Assembly1.WaferCount).Returns(3);
            lotData.Setup(x => x.Assembly2.CarrierPlateCount).Returns(26);
            lotData.Setup(x => x.Assembly2.WaferCount).Returns(8);
            lotData.Setup(x => x.Wafers).Returns(wafers);

            return lotData.Object;
        }

        public static ILotData GetLotData(string lotId, IList<ICassette> cassettes, int nGWaferCount, WaferSize wafesize,
                                          OfType ofType, PolishDivision isRepolishing, int assembly1CarrierPlateCount, int assembly1WaferCount,
                                          int assembly2CarrierPlateCount, int assembly2WaferCount, IList<IWafer> wafers)
        {
            var lotData = new Mock<ILotData>();

            lotData.Setup(x => x.LotId).Returns(lotId);
            lotData.Setup(x => x.Cassettes).Returns(cassettes);
            lotData.Setup(x => x.NGWaferCount).Returns(nGWaferCount);
            lotData.Setup(x => x.WaferSize).Returns(wafesize);
            lotData.Setup(x => x.OfType).Returns(ofType);
            lotData.Setup(x => x.PolishDivision).Returns(isRepolishing);
            lotData.Setup(x => x.Assembly1.CarrierPlateCount).Returns(assembly1CarrierPlateCount);
            lotData.Setup(x => x.Assembly1.WaferCount).Returns(assembly1WaferCount);
            lotData.Setup(x => x.Assembly2.CarrierPlateCount).Returns(assembly2CarrierPlateCount);
            lotData.Setup(x => x.Assembly2.WaferCount).Returns(assembly2WaferCount);
            lotData.Setup(x => x.Wafers).Returns(wafers);

            return lotData.Object;
        }

        #endregion

        #region Polishing PLC test streams

        public static List<string> GetPolisherStreams()
        {
            List<string> streams = new List<string>();
            streams.Add(GetPolishingStream(0).Substring(4, 220));
            streams.Add(GetPolishingStream(1).Substring(4, 220));
            streams.Add(GetPolishingStream(2).Substring(4, 220));
            return streams;
        }

        public static string GetPolishingStream(int numberOfStream)
        {
            switch (numberOfStream)
            {
                case 0:
                    return PolishingStatusStream1();
                case 1:
                    return PolishingStatusStream2();
                default:
                    return PolishingStatusStream3();
            }
        }

        private static string PolishingStatusStream1()
        {
            StringConverter stringConverter = new StringConverter();
            string fullInfoStream = "00FF"                                          // station ID + pc ID
                                        + stringConverter.ToStream("DHFTPR8Q")       // Magazine ID
                                        + stringConverter.ToStream("NDFH8PLF")       // Carrier Plate ID (head 1)
                                        + stringConverter.ToStream("DKFYQ8EL")       // Carrier Plate ID (head 2)
                                        + stringConverter.ToStream("WYEJF8F3")       // Carrier Plate ID (head 3)
                                        + stringConverter.ToStream("PRYDGHF4")       // Carrier Plate ID (head 4)
                                        + "0001"            // High Pressure Polishing flag
                                        + "0038"            // High Pressure Polishing duration
                                        + "000A"            // Head 1 Force
                                        + "0014"            // Head 2 Force
                                        + "001E"            // Head 3 Force
                                        + "0028"            // Head 4 Force
                                        + "0084"            // Pad Temperature
                                        + "0062"            // Cooling Water In Temperature
                                        + "0093"            // Cooling Water Out Temperature
                                        + "009F"            // Slurry In Temperature
                                        + "0165"            // Slurry Out Temperature
                                        + "01C8"            // Rinse Temperature
                                        + "0144"            // Cooling Water Amount
                                        + "010B"            // Slurry Amount
                                        + "028C"            // Rinse Amount
                                        + "30A5"            // Head 1 Pressure
                                        + "1D6C"            // Head 2 Pressure
                                        + "1388"            // Head 3 Pressure
                                        + "00FE"            // Head 4 Pressure
                                        + "0223"            // Head 1 Back Pressure
                                        + "000D"            // Head 2 Back Pressure
                                        + "16F2"            // Head 3 Back Pressure
                                        + "0145"            // Head 4 Back Pressure
                                        + "058F"            // Plate RPM
                                        + "04B0"            // Head 1 RPM
                                        + "05DC"            // Head 2 RPM
                                        + "0708"            // Head 3 RPM
                                        + "0834"            // head 4 RPM
                                        + "002D"            // Plate Load Current
                                        + "0008"            // Head 1 Load Current
                                        + "0044"            // Head 2 Load Current
                                        + "008A"            // head 3 Load Current
                                        + "03B4"            // head 4 Load Current
                                        + "03DE"            // Pad Used Time
                                        + "0233"            // Pad Used Count
                                        + "\u0003"          // ETX
                                        ;
            return fullInfoStream;
        }

        private static string PolishingStatusStream2()
        {
            StringConverter stringConverter = new StringConverter();
            string fullInfoStream = "00FF"                                          // station ID + pc ID
                                        + stringConverter.ToStream("RTG8PS1P")       // Magazine ID
                                        + stringConverter.ToStream("VFYEQ41C")       // Carrier Plate ID (head 1)
                                        + stringConverter.ToStream("APSD2EKJ")       // Carrier Plate ID (head 2)
                                        + stringConverter.ToStream("JHDER7PM")       // Carrier Plate ID (head 3)
                                        + stringConverter.ToStream("WKD95UAS")       // Carrier Plate ID (head 4)
                                        + "0000"            // High Pressure Polishing flag
                                        + "0055"            // High Pressure Polishing duration
                                        + "0032"            // Head 1 Force
                                        + "003C"            // Head 2 Force
                                        + "0046"            // Head 3 Force
                                        + "0050"            // Head 4 Force
                                        + "00BD"            // Pad Temperature
                                        + "006F"            // Cooling Water In Temperature
                                        + "00DE"            // Cooling Water Out Temperature
                                        + "0155"            // Slurry In Temperature
                                        + "01A9"            // Slurry Out Temperature
                                        + "0278"            // Rinse Temperature
                                        + "0102"            // Cooling Water Amount
                                        + "0006"            // Slurry Amount
                                        + "000A"            // Rinse Amount
                                        + "1569"            // Head 1 Pressure
                                        + "0038"            // Head 2 Pressure
                                        + "036A"            // Head 3 Pressure
                                        + "03E8"            // Head 4 Pressure
                                        + "0005"            // Head 1 Back Pressure
                                        + "A18A"            // Head 2 Back Pressure
                                        + "2547"            // Head 3 Back Pressure
                                        + "00A2"            // Head 4 Back Pressure
                                        + "0694"            // Plate RPM
                                        + "0960"            // Head 1 RPM
                                        + "0A8C"            // Head 2 RPM
                                        + "0BB8"            // Head 3 RPM
                                        + "0CE4"            // head 4 RPM
                                        + "0292"            // Plate Load Current
                                        + "0014"            // Head 1 Load Current
                                        + "0000"            // Head 2 Load Current
                                        + "009C"            // head 3 Load Current
                                        + "40A3"            // head 4 Load Current
                                        + "036B"            // Pad Used Time
                                        + "0281"            // Pad Used Count
                                        + "\u0003"          // ETX
                                        ;
            return fullInfoStream;
        }

        private static string PolishingStatusStream3()
        {
            StringConverter stringConverter = new StringConverter();
            string fullInfoStream = "00FF"                                          // station ID + pc ID
                                        + stringConverter.ToStream("SDJR8ZS6")       // Magazine ID
                                        + stringConverter.ToStream("AP5KFHEM")       // Carrier Plate ID (head 1)
                                        + stringConverter.ToStream("VES7KGFT")       // Carrier Plate ID (head 2)
                                        + stringConverter.ToStream("CIFGP3KJ")       // Carrier Plate ID (head 3)
                                        + stringConverter.ToStream("WKD95UAS")       // Carrier Plate ID (head 4)
                                        + "0001"            // High Pressure Polishing flag
                                        + "004F"            // High Pressure Polishing duration
                                        + "005A"            // Head 1 Force
                                        + "0064"            // Head 2 Force
                                        + "006E"            // Head 3 Force
                                        + "0078"            // Head 4 Force
                                        + "00D5"            // Pad Temperature
                                        + "0166"            // Cooling Water In Temperature
                                        + "02E5"            // Cooling Water Out Temperature
                                        + "0168"            // Slurry In Temperature
                                        + "0001"            // Slurry Out Temperature
                                        + "0323"            // Rinse Temperature
                                        + "0057"            // Cooling Water Amount
                                        + "0087"            // Slurry Amount
                                        + "03C3"            // Rinse Amount
                                        + "30FF"            // Head 1 Pressure
                                        + "589C"            // Head 2 Pressure
                                        + "0001"            // Head 3 Pressure
                                        + "0591"            // Head 4 Pressure
                                        + "0063"            // Head 1 Back Pressure
                                        + "03A6"            // Head 2 Back Pressure
                                        + "1A5D"            // Head 3 Back Pressure
                                        + "0000"            // Head 4 Back Pressure
                                        + "05FE"            // Plate RPM
                                        + "0E10"            // Head 1 RPM
                                        + "0F3C"            // Head 2 RPM
                                        + "0EA6"            // Head 3 RPM
                                        + "0DDE"            // head 4 RPM
                                        + "0007"            // Plate Load Current
                                        + "000A"            // Head 1 Load Current
                                        + "01AC"            // Head 2 Load Current
                                        + "038E"            // head 3 Load Current
                                        + "0001"            // head 4 Load Current
                                        + "03D0"            // Pad Used Time
                                        + "0247"            // Pad Used Count
                                        + "\u0003"          // ETX
                                        ;
            return fullInfoStream;
        }

        #endregion

        #region Mounter PLC test streams

        public static string GetMountStatusStream(int numberOfStream)
        {
            switch (numberOfStream)
            {
                case 0:
                    return MounterStatusStream1();
                case 1:
                    return MounterStatusStream2();
                default:
                    return MounterStatusStream3();
            }
        }

        private static string MounterStatusStream1()
        {
            StringConverter stringConverter = new StringConverter();
            IntConverter intConverter = new IntConverter();
            string statusStream = "00FF"                                // station ID + pc ID
                                + stringConverter.ToStream("ABCDEFGH")   // cassette ID
                                + "0001"                                // lot data request flag
                                + "0000"                                // cassette ID error flag
                                + "0000"                                // lot data communication timeout flag
                                + stringConverter.ToStream("ABCDEFGHIJKLMN") // new lot ID
                                + "0003"                                // new lot start line
                                + "0000"                                // new lot start flag
                                + "0001"                                // carrier plate arrival flag
                                + "0001"                                // BCR read OK flag
                                + "0000"                                // carrier plate mounting ready flag
                                + intConverter.ToStream(10)            // wafer break information
                                + "0000"                                // wafer break information OK flag
                                + "0000"                                // mounting error carrier plate flag
                                + "0000"                                // lot end flag
                                + "0000"                                // reservation lot cancel flag
                                + "00000000000000000000000000000000"    // ... some data ...
                                + "0001"                                // mounter machine state
                                + "\u0003"                              // ETX
                                ;

            return statusStream;
        }

        private static string MounterStatusStream2()
        {
            StringConverter stringConverter = new StringConverter();
            IntConverter intConverter = new IntConverter();
            string statusStream = "00FF"                                // station ID + pc ID
                                + stringConverter.ToStream("IJKLMNOP")   // cassette ID
                                + "0000"                                // lot data request flag
                                + "0001"                                // cassette ID error flag
                                + "0001"                                // lot data communication timeout flag
                                + stringConverter.ToStream("KCGE8PAQ1HC4HF") // new lot ID
                                + "0002"                                // new lot start line
                                + "0001"                                // new lot start flag
                                + "0000"                                // carrier plate arrival flag
                                + "0000"                                // BCR read OK flag
                                + "0001"                                // carrier plate mounting ready flag
                                + intConverter.ToStream(15)            // wafer break information
                                + "0001"                                // wafer break information OK flag
                                + "0001"                                // mounting error carrier plate flag
                                + "0001"                                // lot end flag
                                + "0001"                                // reservation lot cancel flag
                                + "11111111111111111111111111111111"    // ... some data ...
                                + "0003"                                // mounter machine state
                                + "\u0003"                              // ETX
                                ;

            return statusStream;
        }

        private static string MounterStatusStream3()
        {
            StringConverter stringConverter = new StringConverter();
            IntConverter intConverter = new IntConverter();
            string statusStream = "00FF"                                // station ID + pc ID
                                + stringConverter.ToStream("LDPW8X2D")   // cassette ID
                                + "0001"                                // lot data request flag
                                + "0001"                                // cassette ID error flag
                                + "0000"                                // lot data communication timeout flag
                                + stringConverter.ToStream("MZQPALJFIR2JCS") // new lot ID
                                + "0001"                                // new lot start line
                                + "0000"                                // new lot start flag
                                + "0001"                                // carrier plate arrival flag
                                + "0001"                                // BCR read OK flag
                                + "0001"                                // carrier plate mounting ready flag
                                + intConverter.ToStream(0)             // wafer break information
                                + "0001"                                // wafer break information OK flag
                                + "0000"                                // mounting error carrier plate flag
                                + "0000"                                // lot end flag
                                + "0001"                                // reservation lot cancel flag
                                + "22222222222222222222222222222222"    // ... some data ...
                                + "0004"                                // mounter machine state
                                + "\u0003"                              // ETX
                                ;

            return statusStream;
        }

        #endregion

        #region Stocker PLC test streams

        public static string GetStockerStatusStream(int numberOfStream)
        {
            switch (numberOfStream)
            {
                case 0:
                    return StockerStatusStream1();
                case 1:
                    return StockerStatusStream2();
                default:
                    return StockerStatusStream3();
            }
        }

        private static string StockerStatusStream1()
        {
            BoolConverter boolConverter = new BoolConverter();
            IntHexConverter intConverter = new IntHexConverter();
            string stream = "00FF"                             // station ID + pc ID
                            + boolConverter.ToStream(true)    // carrier plate arrival flag
                            + boolConverter.ToStream(false)   // IS Error Flag
                            + "0001"                           // CP Routing
                            + boolConverter.ToStream(false)   // magazine full flag
                            + boolConverter.ToStream(false)   // operator magazine change request flag
                            + boolConverter.ToStream(false)   // IS magazine change flag
                            + boolConverter.ToStream(true)    // magazine change start flag
                            + boolConverter.ToStream(true)    // input magazine barcode OK Flag
                            + "00000000000000000000000000000000" // ... some data ...
                            + intConverter.ToStream(6)        // wafer size requested
                            + boolConverter.ToStream(true)    // magazine request flag
                            + "0001"                           // stocker inventory
                            + boolConverter.ToStream(true)    // output magazine arrive flag
                            + "0000"                           // magazine selection
                            + intConverter.ToStream(1)        // polishline number
                            + "\u0003"                         // ETX
                            ;

            return stream;
        }

        private static string StockerStatusStream2()
        {
            BoolConverter boolConverter = new BoolConverter();
            IntHexConverter intConverter = new IntHexConverter();
            string stream = "00FF"                             // station ID + pc ID
                            + boolConverter.ToStream(false)   // carrier plate arrival flag
                            + boolConverter.ToStream(true)    // IS Error Flag
                            + "0002"                           // CP Routing
                            + boolConverter.ToStream(true)    // magazine full flag
                            + boolConverter.ToStream(false)   // operator magazine change request flag
                            + boolConverter.ToStream(true)    // IS magazine change flag
                            + boolConverter.ToStream(false)   // magazine change start flag
                            + boolConverter.ToStream(false)   // input magazine barcode OK Flag
                            + "00000000000000000000000000000000" // ... some data ...
                            + intConverter.ToStream(8)        // wafer size requested
                            + boolConverter.ToStream(true)    // magazine request flag
                            + "0002"                           // stocker inventory
                            + boolConverter.ToStream(false)   // output magazine arrive flag
                            + "0001"                           // magazine selection
                            + intConverter.ToStream(2)        // polishline number
                            + "\u0003"                         // ETX
                            ;

            return stream;
        }

        private static string StockerStatusStream3()
        {
            BoolConverter boolConverter = new BoolConverter();
            IntHexConverter intConverter = new IntHexConverter();
            string stream = "00FF"                             // station ID + pc ID
                            + boolConverter.ToStream(true)    // carrier plate arrival flag
                            + boolConverter.ToStream(true)    // IS Error Flag
                            + "0001"                           // CP Routing
                            + boolConverter.ToStream(false)   // magazine full flag
                            + boolConverter.ToStream(true)    // operator magazine change request flag
                            + boolConverter.ToStream(true)    // IS magazine change flag
                            + boolConverter.ToStream(true)    // magazine change start flag
                            + boolConverter.ToStream(true)    // input magazine barcode OK Flag
                            + "00000000000000000000000000000000" // ... some data ...
                            + intConverter.ToStream(6)        // wafer size requested
                            + boolConverter.ToStream(false)   // magazine request flag
                            + "0002"                           // stocker inventory
                            + boolConverter.ToStream(false)   // output magazine arrive flag
                            + "0002"                           // magazine selection
                            + intConverter.ToStream(3)        // polishline number
                            + "\u0003"                         // ETX
                            ;

            return stream;
        }

        #endregion

        #region Demounter PLC test streams

        public static string GetDemountStatusStream(int numberOfStream)
        {
            switch (numberOfStream)
            {
                case 0:
                    return DemounterStatusStream1();
                case 1:
                    return DemounterStatusStream2();
                default:
                    return DemounterStatusStream3();
            }
        }

        private static string DemounterStatusStream1()
        {
            BoolConverter boolConverter = new BoolConverter();
            IntHexConverter intConverter = new IntHexConverter();
            string stream = "00FF"                             // station ID + pc ID
                            + boolConverter.ToStream(false)   // carrier plate arrival flag                         
                            + boolConverter.ToStream(false)   // IS error flag
                            + boolConverter.ToStream(true)    // carrier plate barcode read OK flag
                            + boolConverter.ToStream(false)   // carrier plate demount start flag
                            + intConverter.ToStream(6)        // carrier plate wafer size
                            + intConverter.ToStream(5)        // carrier plate wafer count
                            + "0001"                           // demount cassette station
                            + intConverter.ToStream(2)        // wafer demount counter
                            + boolConverter.ToStream(false)   // CP demount complete
                            + "0001"                           // empty CP routing
                            + "000000000000000000000000"       // ... some data ...
                            + "0003"                           // remove cassette from demount station
                            + intConverter.ToStream(6)        // cassette wafer size
                            + "0002"                           // destination station number of cassette
                            + "0004"                           // cassette barcode read start flag
                            + boolConverter.ToStream(true)    // cassette barcode read OK flag
                            + "00000000000000000000000000000000000000000000" // ... some data ...
                            + boolConverter.ToStream(false)   // spatula check flag
                            + boolConverter.ToStream(true)    // station 1 cassette sensor
                            + boolConverter.ToStream(false)   // station 2 cassette sensor
                            + boolConverter.ToStream(true)    // station 3 cassette sensor
                            + boolConverter.ToStream(false)   // station 4 cassette sensor
                            + "0001"                           // demounter machine state
                            + "\u0003"                         // ETX
                            ;

            return stream;
        }

        private static string DemounterStatusStream2()
        {
            BoolConverter boolConverter = new BoolConverter();
            IntHexConverter intConverter = new IntHexConverter();
            string stream = "00FF"                             // station ID + pc ID
                            + boolConverter.ToStream(true)    // carrier plate arrival flag                         
                            + boolConverter.ToStream(false)   // IS error flag
                            + boolConverter.ToStream(false)   // carrier plate barcode read OK flag
                            + boolConverter.ToStream(true)    // carrier plate demount start flag
                            + intConverter.ToStream(8)        // carrier plate wafer size
                            + intConverter.ToStream(4)        // carrier plate wafer count
                            + "0002"                           // demount cassette station
                            + intConverter.ToStream(3)        // wafer demount counter
                            + boolConverter.ToStream(true)    // CP demount complete
                            + "0002"                           // empty CP routing
                            + "000000000000000000000000"       // ... some data ...
                            + "0001"                           // remove cassette from demount station
                            + intConverter.ToStream(8)        // cassette wafer size
                            + "0004"                           // destination station number of cassette
                            + "0003"                           // cassette barcode read start flag
                            + boolConverter.ToStream(false)   // cassette barcode read OK flag
                            + "00000000000000000000000000000000000000000000" // ... some data ...
                            + boolConverter.ToStream(false)   // spatula check flag
                            + boolConverter.ToStream(false)   // station 1 cassette sensor
                            + boolConverter.ToStream(true)    // station 2 cassette sensor
                            + boolConverter.ToStream(false)   // station 3 cassette sensor
                            + boolConverter.ToStream(false)   // station 4 cassette sensor
                            + "0002"                           // demounter machine state
                            + "\u0003"                         // ETX
                            ;

            return stream;
        }

        private static string DemounterStatusStream3()
        {
            BoolConverter boolConverter = new BoolConverter();
            IntHexConverter intConverter = new IntHexConverter();
            string stream = "00FF"                             // station ID + pc ID
                            + boolConverter.ToStream(false)   // carrier plate arrival flag                         
                            + boolConverter.ToStream(true)    // IS error flag
                            + boolConverter.ToStream(true)    // carrier plate barcode read OK flag
                            + boolConverter.ToStream(false)   // carrier plate demount start flag
                            + intConverter.ToStream(6)        // carrier plate wafer size
                            + intConverter.ToStream(7)        // carrier plate wafer count
                            + "0003"                           // demount cassette station
                            + intConverter.ToStream(1)        // wafer demount counter
                            + boolConverter.ToStream(true)    // CP demount complete
                            + "0001"                           // empty CP routing
                            + "000000000000000000000000"       // ... some data ...
                            + "0002"                           // remove cassette from demount station
                            + intConverter.ToStream(6)        // cassette wafer size
                            + "0001"                           // destination station number of cassette
                            + "0002"                           // cassette barcode read start flag
                            + boolConverter.ToStream(true)    // cassette barcode read OK flag
                            + "00000000000000000000000000000000000000000000" // ... some data ...
                            + boolConverter.ToStream(false)   // spatula check flag
                            + boolConverter.ToStream(true)    // station 1 cassette sensor
                            + boolConverter.ToStream(false)   // station 2 cassette sensor
                            + boolConverter.ToStream(true)    // station 3 cassette sensor
                            + boolConverter.ToStream(true)    // station 4 cassette sensor
                            + "0003"                           // demounter machine state
                            + "\u0003"                         // ETX
                            ;

            return stream;
        }

        #endregion

        #region WriteCommand methods

        public static string GetBoolWriteCommand(bool flag, int address)
        {
            return (new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(), address, new BoolToStreamConverter().TryConvert(flag))).CommandToString();
        }

        public static string GetIntWriteCommand(int value, int address)
        {
            return (new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(), address, new IntToStreamConverter().TryConvert(value))).CommandToString();
        }

        #endregion

        #region RandomString method

        private static Random _random = new Random();

        public static string GetRandomString(int size)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(
                Enumerable.Repeat(chars, size)
                .Select(s => s[_random.Next(s.Length)])
                .ToArray());
        }

        #endregion
    }
}
