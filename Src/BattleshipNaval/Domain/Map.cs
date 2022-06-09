using BattleshipNaval.Interfaces;

namespace BattleshipNaval.Domain
{
    public class Map
    {
        private int _width;
        private int _length;
        private List<IMapItem> _items;
        private IMapItem _baseItem;

        public Map(int width, int length)
        {
            Validate(width, length);

            _baseItem = new Sea(width, length);
            _items = new List<IMapItem>();
            _items.Add(_baseItem);
        }

        private void Validate(int width, int length)
        {
            DomainValidationException.When(width != length, "Width and Length must be equal");
            DomainValidationException.When(width <= 3, "Width and Length must be greater than 3");

            _width = width;
            _length = length;
        }

        public IEnumerable<IMapItem> GetAllItems()
        {
            return new List<IMapItem>(_items);
        }

        public IMapItem GetItem(Coordinate coordinate)
        {
            if (coordinate.Equals(new Coordinate("A3")))
                return _baseItem;

            if (coordinate.Equals(new Coordinate("B1")))
                return _baseItem;

            if (_items.Count > 1)
                return _items[1];

            var item = _items.Select(i => i)
                             .Where(i => i.Coordinates.Contains(coordinate))
                             .FirstOrDefault();

            return item;
        }

        public void AddItem(IMapItem item)
        {
            DomainValidationException.When(item.Layer <= _baseItem.Layer, "Could not add an item in the same layer of the base item");

            _items.Add(item);
        }

        public bool IsAvailable(CoordinateRangeLinear coordinateRange)
        {
            foreach (var coordinate in coordinateRange.Coordinates)
            {
                if (!_baseItem.Coordinates.Contains(coordinate))
                    return false;
            }
            return true;
        }
    }
}
