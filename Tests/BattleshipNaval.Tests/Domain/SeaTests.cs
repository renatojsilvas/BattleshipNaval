using BattleshipNaval.Domain;
using BattleshipNaval.Interfaces;
using BattleshipNaval.Tests.Data;
using FluentAssertions;

namespace BattleshipNaval.Tests.Domain
{
    public class SeaTests
    {
        [Fact(DisplayName = "Create Sea With Default Constructor Should Return IMapItem Instance")]
        public void Create_Sea_With_Default_Constructor_Should_Return_IMapItem_Instance()
        {
            // Arrange
            Sea sea;

            // Act
            sea = new Sea(10, 10);

            // Assert
            sea.Should().BeAssignableTo<IMapItem>();
        }

        [Fact(DisplayName = "Create Sea With Default Constructor Should Return Sea With Initial Conditions")]
        public void Create_Sea_With_Default_Constructor_Should_Return_Ship_With_Initial_Conditions()
        {
            // Arrange
            Sea sea;
            int length = 10;
            int width = 10;
            List<Coordinate> expectedInitialPosition = new List<Coordinate>();
            Dictionary<Coordinate, Status> expectedInitialStatus = new Dictionary<Coordinate, Status>();
            List<Coordinate> possibleCoordinates = DataGenerators.GetAllPossibleCoordinates(length, width);
            possibleCoordinates.ForEach(coordinate =>
            {
                expectedInitialPosition.Add(coordinate);
                expectedInitialStatus.Add(coordinate, Status.New);
            });            
            Dictionary<Status, string> expectedSymbolByStatus = new Dictionary<Status, string>()
            {
                { Status.New , "~" },
                { Status.Destroyed , "A" },
            };

            // Act
            sea = new Sea(length, width);

            // Assert
            sea.Name.Should().Be("Sea");
            sea.Alias.Should().Be("S");
            sea.Size.Should().Be(100);
            sea.Layer.Should().Be(0);
            sea.Status.Should().BeEquivalentTo(expectedInitialStatus);
            sea.Coordinates.Should().BeEquivalentTo(expectedInitialPosition);
            sea.SymbolByStatus.Should().BeEquivalentTo(expectedSymbolByStatus);
        }
    }
}
