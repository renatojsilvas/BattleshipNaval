using BattleshipNaval.Interfaces;
using System.Text.RegularExpressions;

namespace BattleshipNaval.Domain
{
    public class CoordinateRange : ICoordinateRange
    {
        private string _range;
        private Coordinate _coordinateInitial;
        private Coordinate _coordinateFinal;
        private bool _isDifferentLatitude;
        private bool _isDifferentLongitude;
        private int _size;
        private List<Coordinate> _coordinates;

        public int Size => _size;

        public IEnumerable<Coordinate> Coordinates => new List<Coordinate>(_coordinates);

        public CoordinateRange(string range)
        {
            Validate(range);
            CalculateSize();
            FillCoordinates();
        }

        protected virtual void Validate(string range)
        {
            DomainValidationException.When(string.IsNullOrEmpty(range), "Coordinate Range value cannot be null or empty");
            DomainValidationException.When(ValueIsOutOfRange(range), $"Coordinate Range value is out of range ({range})");
            DomainValidationException.When(HasLatitudeDecrescent(), $"Latitude cannot be decrescent ({range})");
            DomainValidationException.When(HasLongitudeDecrescent(), $"Longitude cannot be decrescent ({range})");

            _range = range;
        }

        private void CalculateSize()
        {
            if (_isDifferentLatitude && !_isDifferentLongitude)
                _size = _coordinateFinal.Latitude - _coordinateInitial.Latitude + 1;
            else if (_isDifferentLongitude && !_isDifferentLatitude)
                _size = _coordinateFinal.Longitude - _coordinateInitial.Longitude + 1;
            else if (_isDifferentLongitude && _isDifferentLatitude)
                _size = (_coordinateFinal.Longitude - _coordinateInitial.Longitude + 1) * (_coordinateFinal.Latitude - _coordinateInitial.Latitude + 1);
            else
                _size = 1;
        }

        private bool HasLatitudeDecrescent()
        {
            return _coordinateInitial.Latitude > _coordinateFinal.Latitude;
        }

        private bool HasLongitudeDecrescent()
        {
            return _coordinateInitial.Longitude > _coordinateFinal.Longitude;
        }

        private bool ValueIsOutOfRange(string range)
        {
            var match = Regex.Match(range, @"\b([A-Ja-j]([1-9]|10))([A-Ja-j]([1-9]|10))\b");

            if (!match.Success)
                return true;

            _coordinateInitial = new Coordinate(match.Groups[1].Value);
            _coordinateFinal = new Coordinate(match.Groups[3].Value);

            _isDifferentLatitude = _coordinateFinal.Latitude != _coordinateInitial.Latitude;
            _isDifferentLongitude = _coordinateFinal.Longitude != _coordinateInitial.Longitude;

            return false;
        }

        protected bool IsDiagonal()
        {
            return _isDifferentLatitude && _isDifferentLongitude;
        }

        private void FillCoordinates()
        {
            _coordinates = new List<Coordinate>();

            if (_isDifferentLatitude && !_isDifferentLongitude)
            {
                for (int i = 0; i < Size; i++)
                {
                    _coordinates.Add(new Coordinate(_coordinateInitial.Latitude + i, _coordinateInitial.Longitude));
                }
            }
            else if (_isDifferentLongitude && !_isDifferentLatitude)
            {
                for (int i = 0; i < Size; i++)
                {
                    _coordinates.Add(new Coordinate(_coordinateInitial.Latitude, _coordinateInitial.Longitude + i));
                }
            }
            else if (_isDifferentLongitude && _isDifferentLatitude)
            {
                for (int i = 0; i < (_coordinateFinal.Latitude - _coordinateInitial.Latitude + 1); i++)
                {
                    for (int j = 0; j < (_coordinateFinal.Longitude - _coordinateInitial.Longitude + 1); j++)
                    {
                        _coordinates.Add(new Coordinate(_coordinateInitial.Latitude + i, _coordinateInitial.Longitude + j));
                    }
                }
            }
            else
            {
                _coordinates.Add(new Coordinate(_coordinateInitial.Latitude, _coordinateInitial.Longitude));
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is CoordinateRange range &&
                   EqualityComparer<Coordinate>.Default.Equals(_coordinateInitial, range._coordinateInitial) &&
                   EqualityComparer<Coordinate>.Default.Equals(_coordinateFinal, range._coordinateFinal);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_coordinateInitial, _coordinateFinal);
        }
    }
}
