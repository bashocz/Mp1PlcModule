using EI.Business;
namespace EI.Plc
{
    class ProcessRecipeToStreamConverter : BaseToStreamConverter<IMagazine>
    {
        #region constants
        
        private const int magazineIdLengthLL = 1;
        private const int magazineIdLengthUL = 8;
        private const int magazinePlatesCountLL = 1;
        private const int magazinePlatesCountUL = 4;
        private const int plateIdLengthLL = 1;
        private const int plateIdLengthUL = 8;
        private const int recipeValueLL = 1;
        private const int recipeValueUL = 25;

        #endregion

        #region constructors

        public ProcessRecipeToStreamConverter() { }

        #endregion

        #region conversion methods

        private string MagazineDataToStream(IMagazine magazine)
        {
            string result = TextToStream(magazine.Id, "MagazineId").PadRight(16, '0');

            for (int idx = 0; idx < magazine.Plates.Count; idx++)
            {
                result += TextToStream(magazine.Plates[idx].Id, "PlatesId");
                result = result.PadRight(32 + idx * 16, '0');
            }
            foreach (ICarrierPlate plate in magazine.Plates)
            {
                result += IntToStream(plate.Recipe, "Recipe");
            }

            return result;
        }

        #endregion

        #region BaseToStreamConverter members

        protected override bool CheckParameter(IMagazine magazine)
        {
            if ((magazine.Id.Length < magazineIdLengthLL) || (magazine.Id.Length > magazineIdLengthUL))
                throw ThrowPlcExceptionOutOfRangeValue("magazine.Id.Length", magazineIdLengthLL, magazineIdLengthUL, magazine.Id.Length);

            if ((magazine.Plates.Count < magazinePlatesCountLL) || (magazine.Plates.Count > magazinePlatesCountUL))
                throw ThrowPlcExceptionOutOfRangeValue("magazine.Plates.Count", magazinePlatesCountLL, magazinePlatesCountUL, magazine.Plates.Count);

            foreach (ICarrierPlate plate in magazine.Plates)
            {
                if ((plate.Id.Length < plateIdLengthLL) || (plate.Id.Length > plateIdLengthUL))
                    throw ThrowPlcExceptionOutOfRangeValue("plate.Id.Length", plateIdLengthLL, plateIdLengthUL, plate.Id.Length);

                if ((plate.Recipe < recipeValueLL) || (plate.Recipe > recipeValueUL))
                    throw ThrowPlcExceptionOutOfRangeValue("plate.Recipe", recipeValueLL, recipeValueUL, plate.Recipe);
            }
            return true;
        }

        protected override int GetLength(IMagazine magazine)
        {
            return 24;
        }

        protected override string GetStream(IMagazine magazine)
        {
            return MagazineDataToStream(magazine);
        }

        #endregion
    }
}