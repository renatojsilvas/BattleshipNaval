namespace BattleshipNaval.Interfaces
{
    public interface ICoordinate : IValueObject
    {
        int Latitude { get; }
        int Longitude { get; }
    }
}