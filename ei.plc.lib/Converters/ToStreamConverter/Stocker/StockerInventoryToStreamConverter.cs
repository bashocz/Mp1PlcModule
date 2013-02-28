using System;
using System.Globalization;
using EI.Business;

namespace EI.Plc
{
    class StockerInventoryToStreamConverter : BaseToStreamConverter<StockerInventory>
    {
        #region constructors

        public StockerInventoryToStreamConverter() { }

        #endregion

        #region conversion methods

        private string StockerInventoryToStream(StockerInventory inventory)
        {
            switch (inventory)
            {
                case StockerInventory.SizeAvailable:
                    return "0001";
                case StockerInventory.SizeNotInStocker:
                    return "0002";
                default:
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type StockerInvertory, actual value is '{0}'.", inventory), "inventory");
            }
        }

        #endregion

        #region BaseToStreamConverter members

        protected override bool CheckParameter(StockerInventory inventory)
        {
            if ((inventory != StockerInventory.SizeAvailable) && (inventory != StockerInventory.SizeNotInStocker))
                throw ThrowPlcExceptionInvalidEnumValue("inventory", inventory);
            return true;
        }

        protected override int GetLength(StockerInventory inventory)
        {
            return 1;
        }

        protected override string GetStream(StockerInventory inventory)
        {
            return StockerInventoryToStream(inventory);
        }

        #endregion
    }
}
