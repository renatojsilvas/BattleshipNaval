using BattleshipNaval.Interfaces;

namespace BattleshipNaval.Domain
{
    public class Sea : MapItem
    {
        public Sea(int width, int length) :
            base("Sea", "S", width * length, "A", "~")
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    _coordinates.Add(new Coordinate(i, j));
                    _status.Add(new Coordinate(i, j), Domain.Status.New);
                }
            }
        }

        public override int Layer => 0;
    }
}
