using BattleshipNaval.Domain;
using BattleshipNaval.Interfaces;
using FluentAssertions;

namespace BattleshipNaval.Tests.Domain
{
    public class MapItemTests
    {
        [Theory(DisplayName = "Hit MapItem With Valid Coordinate Should Return Change Its Status To Destroyed")]
        [InlineData("A1")]
        public void Hit_MapItem_With_Valid_Coordinate_Should_Return_Change_Its_Status_To_Destroyed(string coordinate)
        {
            // Arrange
            MapItem submarine = new Submarine();
            Dictionary<Coordinate, Status> expectedFinalStatus = new Dictionary<Coordinate, Status>()
            {
                { new Coordinate("A1"), coordinate == "A1" ? Status.Destroyed : Status.New },
                { new Coordinate("A2"), coordinate == "A2" ? Status.Destroyed : Status.New },
            };

            // Act
            submarine.Hit(new Coordinate(coordinate));

            // Assert
            submarine.Status.Should().BeEquivalentTo(expectedFinalStatus);
        }

        [Fact(DisplayName = "Hit MapItem With Out Of Limits Coordinate Should Throw Domain Validation Exception")]
        public void Hit_MapItem_With_Out_Of_Limits_Coordinate_Should_Throw_Domain_Validation_Exception()
        {
            // Arrange
            MapItem submarine = new Submarine();
            Coordinate invalidCoordinate = new Coordinate("A3");

            // Act
            Action action = () => submarine.Hit(invalidCoordinate);

            // Assert
            action.Should()
                .Throw<DomainValidationException>()
                .WithMessage("Coordinate is out of limits (A3)");
        }

        [Fact(DisplayName = "Equal With Two Equal Map Itens Should Return True")]
        public void Equal_With_Two_Equal_Map_Itens_Should_Return_True()
        {
            // Arrange
            IMapItem mapItem1 = new Submarine();
            IMapItem mapItem2 = new Submarine();

            // Act
            var result = mapItem1.Equals(mapItem2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Equal With Two Different Map Itens Should Return False")]
        public void Equal_With_Two_Non_Equal_coordinates_Should_Return_False()
        {
            // Arrange
            IMapItem mapItem1 = new Submarine();
            IMapItem mapItem2 = new Tanker();

            // Act
            var result = mapItem1.Equals(mapItem2);

            // Assert
            result.Should().BeFalse();
        }
    }
}
