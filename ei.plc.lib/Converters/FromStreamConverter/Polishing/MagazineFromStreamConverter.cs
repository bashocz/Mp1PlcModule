using System.Collections.Generic;
using EI.Business;

namespace EI.Plc
{
    class MagazineFromStreamConverter : BaseFromStreamConverter<Magazine>
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

        #endregion

        #region BaseFromStreamConverter members

        protected override bool CheckParameter(string stream)
        {
            if (stream.Length < 80)
                throw GetPlcExceptionInvalidLength(80, stream.Length);
            return true;
        }

        protected override Magazine GetObject(string stream)
        {
            return ParseMagazine(stream.Substring(0, 80));
        }

        #endregion
    }
}
