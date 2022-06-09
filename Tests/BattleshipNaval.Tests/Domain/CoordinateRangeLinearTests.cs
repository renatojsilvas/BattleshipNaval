using BattleshipNaval.Domain;
using FluentAssertions;

namespace BattleshipNaval.Tests.Domain
{
    public class CoordinateRangeLinearTests
    {
        [Fact(DisplayName = "Create Coordinate Range Linear With A Valid Value Should Not Throw Domain Validation Exception")]
        public void Create_Coordinate_Range_Linear_With_A_Valid_Value_Should_Not_Throw_Domain_Validation_Exception()
        {
            // Arrange
            CoordinateRangeLinear coordinate;
            string value = "A1A2";

            // Act
            Action action = () => coordinate = new CoordinateRangeLinear(value);

            // Assert
            action.Should()
                .NotThrow<DomainValidationException>();
        }

        [Fact(DisplayName = "Create Coordinate Range Linear With A Diagonal Range Should Throw Domain Validation Exception")]
        public void Create_Coordinate_Range_Linear_With_A_Diagonal_Range_Should_Throw_Domain_Validation_Exception()
        {
            // Arrange
            CoordinateRangeLinear coordinate;
            string value = "A1B2";

            // Act
            Action action = () => coordinate = new CoordinateRangeLinear(value);

            // Assert
            action.Should()
                .Throw<DomainValidationException>()
                .WithMessage("Coordinate Range Linear value cannot be diagonal (A1B2)");
        }
    }
}
