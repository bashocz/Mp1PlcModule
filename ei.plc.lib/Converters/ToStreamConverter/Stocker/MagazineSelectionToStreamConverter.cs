using System;
using System.Globalization;
using EI.Business;

namespace EI.Plc
{
    class MagazineSelectionToStreamConverter : BaseToStreamConverter<MagazineSelection>
    {
        #region constructors

        public MagazineSelectionToStreamConverter() { }

        #endregion

        #region conversion methods

        private string MagazineSelectionToStream(MagazineSelection selection)
        {
            switch (selection)
            {
                case MagazineSelection.HasRequestedSize:
                    return "0001";
                case MagazineSelection.DoesNotHaveRequestedSize:
                    return "0002";
                default:
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type MagazineSelection, actual value is '{0}'.", selection), "selection");
            }
        }

        #endregion

        #region BaseToStreamConverter members

        protected override bool CheckParameter(MagazineSelection selection)
        {
            if ((selection != MagazineSelection.HasRequestedSize) && (selection != MagazineSelection.DoesNotHaveRequestedSize))
                throw ThrowPlcExceptionInvalidEnumValue("selection", selection);
            return true;
        }

        protected override int GetLength(MagazineSelection selection)
        {
            return 1;
        }

        protected override string GetStream(MagazineSelection selection)
        {
            return MagazineSelectionToStream(selection);
        }

        #endregion
    }
}
