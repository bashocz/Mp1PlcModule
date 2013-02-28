using EI.Business;
namespace EI.Plc
{
    public interface IPolishLinePlc : IBasePlc
    {
        IPolishingShortStatus GetShortStatus();
        IMagazine GetMagazine(Polisher polisher);
        IPolishingFullStatus GetFullStatus();

        bool IsMagazineArrived();
        void ProcessRecipe(IMagazine magazine);
        void WriteBarcodeError(bool error);
    }
}