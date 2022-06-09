using BattleshipNaval.Domain;

namespace BattleshipNaval.Interfaces
{
    public interface IMapItem : IMapItemMetaData, IMapItemFighter, IValueObject
    {
        IList<Coordinate> Coordinates { get; }    
        int Layer { get; }
    }
}
