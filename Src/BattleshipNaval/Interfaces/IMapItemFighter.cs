using BattleshipNaval.Domain;

namespace BattleshipNaval.Interfaces
{
    public interface IMapItemFighter
    {
        IDictionary<Coordinate, Status> Status { get; }
        IDictionary<Status, string> SymbolByStatus { get; }
        void Hit(Coordinate coordinate);
    }
}
