using BattleshipNaval.Domain;

namespace BattleshipNaval.Interfaces
{
    public interface IShip 
    {
        void Fix();
        void UpdatePosition(CoordinateRangeLinear newPosition);
    }
}
