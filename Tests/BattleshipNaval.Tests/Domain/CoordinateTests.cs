using BattleshipNaval.Domain;
using FluentAssertions;

namespace BattleshipNaval.Tests.Domain
{
    public class CoordinateTests
    {
        [Fact(DisplayName = "Create coordinate With Valid Value Should Not Throw Exception")]
        public void Create_coordinate_With_Valid_Parameters_Should_Not_Throw_Exception()
        {
            // Arrange
            Coordinate coordinate;
            string value = "A1";

            // Act
            Action action = () => coordinate = new Coordinate(value);

            // Assert
            action.Should()
                .NotThrow<DomainValidationException>();
        }

        [Fact(DisplayName = "Create coordinate With Empty Value Should Throw coordinate Invalid Exception")]
        public void Create_coordinate_With_Empty_Value_Should_Throw_coordinate_Invalid_Exception()
        {
            // Arrange
            Coordinate coordinate;
            string value = string.Empty;

            // Act
            Action action = () => coordinate = new Coordinate(value);

            // Assert
            action.Should()
                .Throw<DomainValidationException>()
                .WithMessage("Value cannot be null or empty");
        }

        [Fact(DisplayName = "Create coordinate With Null Value Should Throw coordinate Invalid Exception")]
        public void Create_coordinate_With_Null_Value_Should_Throw_coordinate_Invalid_Exception()
        {
            // Arrange
            Coordinate coordinate;
            string value = null;

            // Act
            Action action = () => coordinate = new Coordinate(value);

            // Assert
            action.Should()
                .Throw<DomainValidationException>()
                .WithMessage("Value cannot be null or empty");
        }

        [Theory(DisplayName = "Create coordinate With Invalid Value Should Throw coordinate Invalid Exception")]
        [InlineData("A11")]
        [InlineData("K1")]
        public void Create_coordinate_With_Invalid_Value_Should_Throw_coordinate_Invalid_Exception(string _value)
        {
            // Arrange
            Coordinate coordinate;
            string value = _value;

            // Act
            Action action = () => coordinate = new Coordinate(value);

            // Assert
            action.Should()
                .Throw<DomainValidationException>()
                .WithMessage("Value is out of limits");
        }

        [Fact(DisplayName = "Create coordinate With Lower Case Column Should Not Throw coordinate Invalid Exception")]
        public void Create_coordinate_With_Lower_Case_Column_Should_Throw_coordinate_Invalid_Exception()
        {
            // Arrange
            Coordinate coordinate;
            string value = "a10";

            // Act
            Action action = () => coordinate = new Coordinate(value);

            // Assert
            action.Should()
                .NotThrow<DomainValidationException>();
        }

        [Fact(DisplayName = "Create coordinate With Lower Case Column Should Capitalize It")]
        public void Create_coordinate_With_Lower_Case_Column_Should_Capitalize_It()
        {
            // Arrange
            Coordinate coordinate;
            string value = "a10";

            // Act
            coordinate = new Coordinate(value);

            // Assert
            coordinate.ToString().Should().Be("A10");
        }

        [Fact(DisplayName = "Create coordinate With Valid Latitude And Longitude Should Not Throw Domain Validation Exception")]
        public void Create_coordinate_With_Valid_Latitude_And_Longitude_Should_Not_Throw_Domain_Validation_Exception()
        {
            // Arrange
            int latitude = 0;
            int longitude = 0;

            // Act
            Action action = () => new Coordinate(latitude, longitude);

            // Assert
            action.Should()
                .NotThrow<DomainValidationException>();
        }

        [Fact(DisplayName = "ToString With Valid Latitude And Longitude Should Return Its Symbol")]
        public void ToString_With_Valid_Latitude_And_Longitude_Should_Return_Its_Symbol()
        {
            // Arrange
            Coordinate coordinate;
            int latitude = 0;
            int longitude = 0;

            // Act
            coordinate = new Coordinate(latitude, longitude);

            // Assert
            coordinate.ToString().Should().Be("A1");
        }

        [Theory(DisplayName = "Create coordinate With Invalid Latitude And Longitude Should Throw Domain Validation Exception")]
        [InlineData(0, 11)]
        [InlineData(11, 0)]
        public void Create_coordinate_With_Invalid_Latitude_And_Longitude_Should_Throw_Domain_Validation_Exception(int latitude, int longitude)
        {
            // Arrange

            // Act
            Action action = () => new Coordinate(latitude, longitude);

            // Assert
            action.Should()
                .Throw<DomainValidationException>()
                .WithMessage("Value is out of limits");
        }

        [Fact(DisplayName = "Equal With Two Equal coordinates Should Return True")]
        public void Equal_With_Two_Equal_coordinates_Should_Return_True()
        {
            // Arrange
            Coordinate coordinate1 = new Coordinate("A1");
            Coordinate coordinate2 = new Coordinate("A1");

            // Act
            var result = coordinate1.Equals(coordinate2);

            // Assert
            result.Should().BeTrue();
        }

        [Theory(DisplayName = "Equal With Two Non Equal coordinates Should Return False")]
        [InlineData("A1", "A2")]
        [InlineData("A1", "B1")]
        public void Equal_With_Two_Non_Equal_coordinates_Should_Return_False(string coordinate1Symbol, string coordinate2Symbol)
        {
            // Arrange
            Coordinate coordinate1 = new Coordinate(coordinate1Symbol);
            Coordinate coordinate2 = new Coordinate(coordinate2Symbol);

            // Act
            var result = coordinate1.Equals(coordinate2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Latitude With A Valid Coordinate Should Return The Latitude Value")]
        public void Latitude_With_A_Valid_Coordinate_Should_Return_The_Latitude_Value()
        {
            // Arrange
            Coordinate coordinate = new Coordinate("A2");

            // Act
            var latitude = coordinate.Latitude;

            // Assert
            latitude.Should().Be(1);
        }

        [Fact(DisplayName = "Longitude With A Valid Coordinate Should Return The Longitude Value")]
        public void Longitude_With_A_Valid_Coordinate_Should_Return_The_Longitude_Value()
        {
            // Arrange
            Coordinate coordinate = new Coordinate("A2");

            // Act
            var longitude = coordinate.Longitude;

            // Assert
            longitude.Should().Be(0);
        }

        [Fact(DisplayName = "Longitude With A Lower Case Should Return The Correct Longitude Value")]
        public void Longitude_With_A_Lower_Case_Should_Return_The_Correct_Longitude_Value()
        {
            // Arrange
            Coordinate coordinate = new Coordinate("a2");

            // Act
            var longitude = coordinate.Longitude;

            // Assert
            longitude.Should().Be(0);
        }
    }
}