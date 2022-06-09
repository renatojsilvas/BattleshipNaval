using System.Text.RegularExpressions;

namespace BattleshipNaval.Domain
{
    public class Coordinate
    {
        private Regex regex = new Regex(@"\b([A-Ja-j])([1-9]|10)\b");
        private int _latitude;
        private int _longitude;
        private string _symbol;

        public int Latitude => _latitude;
        public int Longitude => _longitude;

        public Coordinate(int latitude, int longitude)
        {
            Validate(latitude, longitude);
        }

        public Coordinate(string value)
        {
            Validate(value);
        }

        private void Validate(string coordinate)
        {
            DomainValidationException.When(string.IsNullOrEmpty(coordinate), "Value cannot be null or empty");
            DomainValidationException.When(!regex.IsMatch(coordinate), "Value is out of limits");

            coordinate = coordinate.ToUpper();

            var match = regex.Match(coordinate);

            _latitude = Convert.ToInt32(match.Groups[2].Value) - 1;
            _longitude = match.Groups[1].Value[0] - 'A';
            _symbol = coordinate;
        }

        private void Validate(int latitude, int longitude)
        {
            DomainValidationException.When(latitude >= 10 || longitude >= 10, "Value is out of limits");

            _latitude = latitude;
            _longitude = longitude;
            _symbol = ((char)('A' + longitude)).ToString() + (latitude + 1).ToString();
        }

        public override string ToString()
        {
            return _symbol;
        }

        public override bool Equals(object? obj)
        {
            return obj is Coordinate coordinate &&
                   _latitude == coordinate._latitude &&
                   _longitude == coordinate._longitude;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_latitude, _longitude);
        }
    }
}
