namespace BattleshipNaval.Domain
{
    public class CoordinateRangeLinear : CoordinateRange
    {
        public CoordinateRangeLinear(string range)
            : base(range)
        {
        }

        protected override void Validate(string range)
        {
            base.Validate(range);

            DomainValidationException.When(IsDiagonal(), $"Coordinate Range Linear value cannot be diagonal ({range})");
        }
    }
}
