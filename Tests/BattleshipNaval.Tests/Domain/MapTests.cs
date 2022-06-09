using BattleshipNaval.Domain;
using BattleshipNaval.Interfaces;
using BattleshipNaval.Tests.Data;
using FluentAssertions;

namespace BattleshipNaval.Tests.Domain
{
    public class MapTests
    {
        [Fact(DisplayName = "Create Map With Valid Parameters Should Not Throw Domain Validation Exception")]
        public void Create_Map_With_Valid_Parameters_Should_Not_Throw_Domain_Validation_Exception()
        {
            // Arrange
            Map map;

            // Act
            Action action = () => map = new Map(10, 10);

            // Assert
            action.Should().NotThrow<DomainValidationException>();
        }

        [Fact(DisplayName = "Create Map With Width And Length Different Should Throw Domain Validation Exception")]
        public void Create_Map_With_Width_And_Length_Different_Should_Throw_Domain_Validation_Exception()
        {
            // Arrange
            Map map;

            // Act
            Action action = () => map = new Map(10, 9);

            // Assert
            action.Should()
                  .Throw<DomainValidationException>()
                  .WithMessage("Width and Length must be equal");
        }

        [Fact(DisplayName = "Create Map With Width And Length Less Than 4 Should Throw Domain Validation Exception")]
        public void Create_Map_With_Width_And_Length_Less_Than_4_Should_Throw_Domain_Validation_Exception()
        {
            // Arrange
            Map map;

            // Act
            Action action = () => map = new Map(3, 3);

            // Assert
            action.Should()
                  .Throw<DomainValidationException>()
                  .WithMessage("Width and Length must be greater than 3");
        }

        [Fact(DisplayName = "Create Map With Valid Parameters Should Contains One MapItem Only")]
        public void Create_Map_With_Valid_Parameters_Should_Contains_One_MapItem_Only()
        {
            // Arrange
            Map map = new Map(10, 10); ;

            // Act
            var items = map.GetAllItems();

            // Assert
            items.Should().HaveCount(1);
        }

        [Fact(DisplayName = "Create Map With Valid Parameters Should Contains a Sea")]
        public void Create_Map_With_Valid_Parameters_Should_Contains_A_Sea()
        {
            // Arrange            
            int width = 10;
            int length = 10;
            Map map = new Map(width, length);
            List<IMapItem> expectedItem = 
                new List<IMapItem>() { new Sea(width, length) };

            // Act
            var items = map.GetAllItems();

            // Assert
            items.Should().BeEquivalentTo(expectedItem);
        }

        [Fact(DisplayName = "Create Map With Valid Parameters Should Contains a Sea With 100 Coordinates Long")]
        public void Create_Map_With_Valid_Parameters_Should_Contains_A_Sea_With_100_Coordinates_Long()
        {
            // Arrange            
            int width = 10;
            int length = 10;
            Map map = new Map(width, length);
            int expectedNumberOfCoordinates = 100;
            var items = map.GetAllItems();

            // Act
            var numberOfCoordinates = items.First().Coordinates.Count;

            // Assert
            numberOfCoordinates.Should().Be(expectedNumberOfCoordinates);
        }

        [Fact(DisplayName = "Get Item With Valid Coordinate Just After Creating Map Should Returns The Sea With Original Coordinates For All Possible Input Coordinates")]        
        public void Get_Item_With_Valid_Coordinate_Just_After_Creating_Map_Should_Returns_The_Sea_Original_Coordinates_For_All_Possible_Input_Coordinates()
        {
            // Arrange
            Map map;
            int width = 10;
            int length = 10;
            map = new Map(width, length);            
            IMapItem expectedItem = new Sea(width, length);
            List<Coordinate> possibleCoordinates = DataGenerators.GetAllPossibleCoordinates(width, length);            

            // Act
            IEnumerable<IMapItem> items = possibleCoordinates.Select(coordinate => map.GetItem(coordinate));

            // Assert            
            items.Should().AllBeEquivalentTo(expectedItem);
        }

        [Fact(DisplayName = "Is Available Just After Creating Map Should Return True For All Possible Input Coordinate Ranges")]
        public void Is_Available_Just_After_Creating_Map_Should_Return_True_For_All_Possible_Input_Coordinate_Ranges()
        {
            // Arrange
            Map map;
            int width = 10;
            int length = 10;
            int minSizeOfMapItem = 2;
            int maxSizeOfMapItem = 5;
            map = new Map(width, length);
            IMapItem expectedItem = new Sea(width, length);
            List<CoordinateRangeLinear> possibleCoordinates = DataGenerators.GetAllPossibleCoordinatesRange(width, length, minSizeOfMapItem, maxSizeOfMapItem);

            // Act
            IEnumerable<bool> items = possibleCoordinates.Select(coordinate => map.IsAvailable(coordinate));

            // Assert            
            items.Should().OnlyContain(item => item == true);
        }
    }
}
