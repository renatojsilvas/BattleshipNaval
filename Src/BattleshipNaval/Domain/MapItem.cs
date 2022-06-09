using BattleshipNaval.Interfaces;

namespace BattleshipNaval.Domain
{
    public abstract class MapItem : IMapItem
    {
        protected readonly List<Coordinate> _coordinates = new List<Coordinate>();
        protected readonly IDictionary<Coordinate, Status> _status = new Dictionary<Coordinate, Status>();
        protected readonly IDictionary<Status, string> _symbolByStatus = new Dictionary<Status, string>();

        public MapItem(string name, string alias, int size,
            string hitSymbol = "X", string newSymbol = "O")
        {
            Name = name;
            Alias = alias;
            Size = size;            

            _symbolByStatus = new Dictionary<Status, string>()
            {
                {  Domain.Status.New, newSymbol },
                {  Domain.Status.Destroyed, hitSymbol },
            };
        }

        public IList<Coordinate> Coordinates => new List<Coordinate>(_coordinates);

        public string Alias { get; }

        public string Name { get; }

        public int Size { get; }

        public abstract int Layer { get; }

        public IDictionary<Coordinate, Status> Status => new Dictionary<Coordinate, Status>(_status);

        public IDictionary<Status, string> SymbolByStatus => new Dictionary<Status, string>(_symbolByStatus);      

        public void Hit(Coordinate coordinate)
        {
            DomainValidationException.When(!_status.ContainsKey(coordinate),
                $"Coordinate is out of limits ({coordinate})");

            _status[coordinate] = Domain.Status.Destroyed;
        }
        public override bool Equals(object? obj)
        {
            var item = obj as MapItem;
            if (item == null)
                return false;

            if (Coordinates.Count != item.Coordinates.Count)
                return false;

            for (int i = 0; i < Coordinates.Count; i++)
            {
                if (!Coordinates[i].Equals(item.Coordinates[i]))
                    return false;                
            }

            if (!CompareDictionaries(Status, item.Status))
                return false;

            if (!CompareDictionaries(SymbolByStatus, item.SymbolByStatus))
                return false;            

            return Alias == item.Alias &&
                   Name == item.Name &&
                   Size == item.Size &&
                   Layer == item.Layer;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Coordinates, Alias, Name, Size, Layer, Status, SymbolByStatus);
        }

        private bool CompareDictionaries<TKey, TValue>(IDictionary<TKey, TValue> dictionary1, IDictionary<TKey, TValue> dictionary2)
        {
            if (dictionary1.Count != dictionary2.Count)
                return false;
            if (dictionary1.Keys.Except(dictionary2.Keys).Any())
                return false;
            if (dictionary1.Keys.Except(dictionary2.Keys).Any())
                return false;
            foreach (var pair in dictionary1)
                if (!EqualityComparer<TValue>.Default.Equals(pair.Value, dictionary2[pair.Key]))
                    return false;
            return true;
        }
    }
}
