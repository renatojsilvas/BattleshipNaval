using BattleshipNaval.Domain;

namespace BattleshipNaval.Interfaces
{
    public interface ICoordinateRange : IValueObject
    {
        IEnumerable<Coordinate> Coordinates { get; }
        int Size { get; }
    }
}