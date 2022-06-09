using BattleshipNaval.Interfaces;

namespace BattleshipNaval.Domain
{
    public abstract class Ship : MapItem, IShip
    {
        protected Ship(string name, string alias, int size,
            string hitSymbol = "X", string newSymbol = "O") 
            : base(name, alias, size, hitSymbol, newSymbol)
        {
            for (int i = 0; i < size; i++)
            {
                _coordinates.Add(new Coordinate(i, 0));
                _status.Add(new Coordinate(i, 0), Domain.Status.New);
            }
        }

        public override int Layer => 1;

        public void Fix()
        {
            foreach (var status in Status)
            {
                _status[status.Key] = Domain.Status.New;
            }
        }

        public void UpdatePosition(CoordinateRangeLinear newPosition)
        {
            DomainValidationException.When(newPosition.Size != Size, $"Invalid size. The new position has to be {Size} instead of {newPosition.Size}");

            _coordinates.Clear();
            _coordinates.AddRange(newPosition.Coordinates);
        }
    }
}
