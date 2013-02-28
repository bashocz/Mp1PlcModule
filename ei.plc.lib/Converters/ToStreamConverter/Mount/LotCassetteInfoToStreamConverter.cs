using EI.Business;
namespace EI.Plc
{
    class LotCassetteInfoToStreamConverter : BaseToStreamConverter<ILotData>
    {
        #region constants

        private const int lotAndCassettesIDstreamLength = 220;
        private const int cassetteCountLL = 1;
        private const int cassetteCountUL = 12;
        private const int lotIdLengthLL = 1;
        private const int lotIdLengthUL = 14;
        private const int cassetteIdLengthLL = 1;
        private const int cassetteIdLengthUL = 8;

        #endregion

        #region constructors

        public LotCassetteInfoToStreamConverter() { }

        #endregion

        #region conversion methods

        private string LotDataToStream(ILotData lotData)
        {
            string result = TextToStream(lotData.LotId, "LotId").PadRight(28, '0');
            
            for (int idx = 0; idx < lotData.Cassettes.Count; idx++)
            {
                result += TextToStream(lotData.Cassettes[idx].CassetteId.PadRight(8), "CassetteId");
            }

            return result = result.PadRight(lotAndCassettesIDstreamLength, '0') + IntToStream(lotData.Cassettes.Count, "Cassettes.Count");
        }

        #endregion

        #region BaseToStreamConverter members

        protected override bool CheckParameter(ILotData lotData)
        {
            if ((lotData.LotId.Length < lotIdLengthLL) || (lotData.LotId.Length > lotIdLengthUL))
                throw ThrowPlcExceptionOutOfRangeValue("lotData.LotId.Length", lotIdLengthLL, lotIdLengthUL, lotData.LotId.Length);

            if ((lotData.Cassettes.Count < cassetteCountLL) || (lotData.Cassettes.Count > cassetteCountUL))
                throw ThrowPlcExceptionOutOfRangeValue("lotData.Cassettes.Count", cassetteCountLL, cassetteCountUL, lotData.Cassettes.Count);

            foreach (ICassette cassette in lotData.Cassettes)
            {
                if ((cassette.CassetteId.Length < cassetteIdLengthLL) || (cassette.CassetteId.Length > cassetteIdLengthUL))
                    throw ThrowPlcExceptionOutOfRangeValue("cassette.CassetteId.Length", cassetteIdLengthLL, cassetteIdLengthUL, cassette.CassetteId.Length);
            }

            return true;
        }

        protected override int GetLength(ILotData lotData)
        {
            return 56;
        }

        protected override string GetStream(ILotData lotData)
        {
            return LotDataToStream(lotData);
        }

        #endregion
    }
}
