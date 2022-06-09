using BattleshipNaval.Domain;
using BattleshipNaval.Tests.Data;
using FluentAssertions;

namespace BattleshipNaval.Tests.Domain
{
    public class CoordinateRangeTests
    {
        [Fact(DisplayName = "Create Coordinate Range With A Valid Value Should Not Throw Domain Validation Exception")]
        public void Create_Coordinate_Range_With_A_Valid_Value_Should_Not_Throw_Domain_Validation_Exception()
        {
            // Arrange
            CoordinateRange coordinate;
            string value = "A1B2";

            // Act
            Action action = () => coordinate = new CoordinateRange(value);

            // Assert
            action.Should()
                .NotThrow<DomainValidationException>();
        }

        [Fact(DisplayName = "Create Coordinate Range With An Empty Value Should Throw Domain Validation Exception")]
        public void Create_Coordinate_Range_With_An_Empty_Value_Should_Throw_Domain_Validation_Exception()
        {
            // Arrange
            CoordinateRange coordinate;
            string value = string.Empty;

            // Act
            Action action = () => coordinate = new CoordinateRange(value);

            // Assert
            action.Should()
                .Throw<DomainValidationException>()
                .WithMessage("Coordinate Range value cannot be null or empty");
        }

        [Fact(DisplayName = "Create Coordinate Range With A Null Value Should Throw Domain Validation Exception")]
        public void Create_Coordinate_Range_With_A_Null_Value_Should_Throw_Domain_Validation_Exception()
        {
            // Arrange
            CoordinateRange coordinate;
            string value = null;

            // Act
            Action action = () => coordinate = new CoordinateRange(value);

            // Assert
            action.Should()
                .Throw<DomainValidationException>()
                .WithMessage("Coordinate Range value cannot be null or empty");
        }

        [Theory(DisplayName = "Create Coordinate Range With An Out of Limits Value Should Throw Domain Validation Exception")]
        [InlineData("A1A11")]
        [InlineData("K1A1")]
        public void Create_Coordinate_Range_With_An_Out_Of_Limits_Value_Should_Throw_Domain_Validation_Exception(string outOfLimitsValue)
        {
            // Arrange
            CoordinateRange coordinate;
            string value = outOfLimitsValue;

            // Act
            Action action = () => coordinate = new CoordinateRange(value);

            // Assert
            action.Should()
                .Throw<DomainValidationException>()
                .WithMessage($"Coordinate Range value is out of range ({outOfLimitsValue})");
        }

        [Theory(DisplayName = "Create Coordinate Range With Lower Case Letter Should Not Throw Domain Validation Exception")]
        [InlineData("a1A10")]
        [InlineData("A1j1")]
        public void Create_Coordinate_Range_With_Lower_Case_Letter_Should_Not_Throw_Domain_Validation_Exception(string outOfLimitsValue)
        {
            // Arrange
            CoordinateRange coordinate;
            string value = outOfLimitsValue;

            // Act
            Action action = () => coordinate = new CoordinateRange(value);

            // Assert
            action.Should()
                .NotThrow<DomainValidationException>();
        }

        [Fact(DisplayName = "Create Coordinate Range With Decrescent Longitude Should Throw Domain Validation Exception")]
        public void Create_Coordinate_Range_With_Decrescent_Longitude_Should_Throw_Domain_Validation_Exception()
        {
            // Arrange
            CoordinateRange coordinate;
            string value = "B1A1";

            // Act
            Action action = () => coordinate = new CoordinateRange(value);

            // Assert
            action.Should()
                .Throw<DomainValidationException>()
                .WithMessage("Longitude cannot be decrescent (B1A1)");
        }

        [Fact(DisplayName = "Create Coordinate Range With Decrescent Latitude Should Throw Domain Validation Exception")]
        public void Create_Coordinate_Range_With_Decrescent_Latitude_Should_Throw_Domain_Validation_Exception()
        {
            // Arrange
            CoordinateRange coordinate;
            string value = "A2A1";

            // Act
            Action action = () => coordinate = new CoordinateRange(value);

            // Assert
            action.Should()
                .Throw<DomainValidationException>()
                .WithMessage("Latitude cannot be decrescent (A2A1)");
        }

        [Fact(DisplayName = "Equal With Two Equal Coordinate Range Should Return True")]
        public void Equal_With_Two_Equal_Coordinate_Ranges_Should_Return_True()
        {
            // Arrange
            CoordinateRange coordinateRange1 = new CoordinateRange("A1A2");
            CoordinateRange coordinateRange2 = new CoordinateRange("A1A2");

            // Act
            var result = coordinateRange1.Equals(coordinateRange2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Equal With Two Non Equal Coordinate Range Should Return False")]
        public void Equal_With_Two_Non_Equal_Coordinate_Ranges_Should_Return_False()
        {
            // Arrange
            CoordinateRange coordinateRange1 = new CoordinateRange("A1A2");
            CoordinateRange coordinateRange2 = new CoordinateRange("A1A3");

            // Act
            var result = coordinateRange1.Equals(coordinateRange2);

            // Assert
            result.Should().BeFalse();
        }

        [Theory(DisplayName = "Size With Valid Range Should Return The Number Of Coordinates Within The Range")]
        [InlineData("A1A1", 1)]
        [InlineData("A1A2", 2)]
        [InlineData("A1A10", 10)]
        [InlineData("A1B2", 4)]
        [InlineData("A1B3", 6)]
        [InlineData("A1J10", 100)]
        public void Size_With_Valid_Range_Should_Return_The_Number_Of_Coordinates_Within_The_Range(string range, int size)
        {
            // Arrange
            CoordinateRange coordinateRange = new CoordinateRange(range);

            // Act
            var result = coordinateRange.Size;

            // Assert
            result.Should().Be(size);
        }

        [Theory(DisplayName = "Coordinates With Valid Range With Same Latitude Should Return A List Of Coordinates Within The Range")]
        [InlineData("A1A5", "A1", "A2", "A3", "A4", "A5")]
        [InlineData("A1A2", "A1", "A2")]
        [InlineData("B1B2", "B1", "B2")]
        [InlineData("C1C1", "C1")]
        public void Coordinates_With_Valid_Range_With_Same_Latitude_Should_Return_A_List_Of_Coordinates_Within_The_Range(string range, params string[] coordinates)
        {
            // Arrange
            CoordinateRange coordinateRange = new CoordinateRange(range);
            List<Coordinate> expected = new List<Coordinate>();
            foreach (var coordinate in coordinates)
            {
                expected.Add(new Coordinate(coordinate));
            }

            // Act
            IEnumerable<Coordinate> result = coordinateRange.Coordinates;

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory(DisplayName = "Coordinates With Valid Range With Same Longitude Should Return A List Of Coordinates Within The Range")]
        [InlineData("A1C1", "A1", "B1", "C1")]
        [InlineData("B2E2", "B2", "C2", "D2", "E2")]
        [InlineData("J10J10", "J10")]
        public void Coordinates_With_Valid_Range_With_Same_Longitude_Should_Return_A_List_Of_Coordinates_Within_The_Range(string range, params string[] coordinates)
        {
            // Arrange
            CoordinateRange coordinateRange = new CoordinateRange(range);
            List<Coordinate> expected = new List<Coordinate>();
            foreach (var coordinate in coordinates)
            {
                expected.Add(new Coordinate(coordinate));
            }

            // Act
            IEnumerable<Coordinate> result = coordinateRange.Coordinates;

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory(DisplayName = "Coordinates With Valid Range With Different Longitude And Latitude Should Return A List Of Coordinates Within The Range")]
        [InlineData("A1B2", "A1", "A2", "B1", "B2")]
        [InlineData("A1C2", "A1", "A2", "B1", "B2", "C1", "C2")]
        [InlineData("A1B3", "A1", "A2", "A3", "B1", "B2", "B3")]
        public void Coordinates_With_Valid_Range_With_Different_Longitude_And_Latitude_Should_Return_A_List_Of_Coordinates_Within_The_Range(string range, params string[] coordinates)
        {
            // Arrange
            CoordinateRange coordinateRange = new CoordinateRange(range);
            List<Coordinate> expected = new List<Coordinate>();
            foreach (var coordinate in coordinates)
            {
                expected.Add(new Coordinate(coordinate));
            }

            // Act
            IEnumerable<Coordinate> result = coordinateRange.Coordinates;

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "Coordinates With Full Range Should Return All Possible Spacial Coordinates")]
        public void Coordinates_With_Full_Range_Should_Return_All_Possible_Coordinate()
        {
            // Arrange
            CoordinateRange coordinateRange = new CoordinateRange("A1J10");
            List<Coordinate> expected = DataGenerators.GetAllPossibleCoordinates(10, 10);

            // Act
            IEnumerable<Coordinate> result = coordinateRange.Coordinates;

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
