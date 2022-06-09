using BattleshipNaval.Domain;
using BattleshipNaval.Interfaces;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipNaval.Tests.Domain
{
    public class ShipTests
    {
        [Fact(DisplayName = "Create Submarine With Default Constructor Should Return IShip Instance")]
        public void Create_Submarine_With_Default_Constructor_Should_Return_IShip_Instance()
        {
            // Arrange
            Submarine submarine;

            // Act
            submarine = new Submarine();

            // Assert
            submarine.Should().BeAssignableTo<IShip>();
        }

        [Fact(DisplayName = "Create Submarine With Default Constructor Should Return IMapItem Instance")]
        public void Create_Submarine_With_Default_Constructor_Should_Return_IMapItem_Instance()
        {
            // Arrange
            Submarine submarine;

            // Act
            submarine = new Submarine();

            // Assert
            submarine.Should().BeAssignableTo<IMapItem>();
        }

        [Fact(DisplayName = "Create Destroyer With Default Constructor Should Return Ship Instance")]
        public void Create_Destroyer_With_Default_Constructor_Should_Return_IShip_Instance()
        {
            // Arrange
            Destroyer destroyer;

            // Act
            destroyer = new Destroyer();

            // Assert
            destroyer.Should().BeAssignableTo<IShip>();
        }

        [Fact(DisplayName = "Create Destroyer With Default Constructor Should Return IMapItem Instance")]
        public void Create_Destroyer_With_Default_Constructor_Should_Return_IMapItem_Instance()
        {
            // Arrange
            Destroyer destroyer;

            // Act
            destroyer = new Destroyer();

            // Assert
            destroyer.Should().BeAssignableTo<IMapItem>();
        }

        [Fact(DisplayName = "Create Tanker With Default Constructor Should Return IShip Instance")]
        public void Create_Tanker_With_Default_Constructor_Should_Return_IShip_Instance()
        {
            // Arrange
            Tanker tanker;

            // Act
            tanker = new Tanker();

            // Assert
            tanker.Should().BeAssignableTo<IShip>();
        }

        [Fact(DisplayName = "Create Tanker With Default Constructor Should Return IMapItem Instance")]
        public void Create_Tanker_With_Default_Constructor_Should_Return_IMapItem_Instance()
        {
            // Arrange
            Tanker tanker;

            // Act
            tanker = new Tanker();

            // Assert
            tanker.Should().BeAssignableTo<IMapItem>();
        }

        [Fact(DisplayName = "Create AirCraft Carrier With Default Constructor Should Return IShip Instance")]
        public void Create_AirCraft_Carrier_With_Default_Constructor_Should_Return_IShip_Instance()
        {
            // Arrange
            AirCraftCarrier airCraftCarrier;

            // Act
            airCraftCarrier = new AirCraftCarrier();

            // Assert
            airCraftCarrier.Should().BeAssignableTo<IShip>();
        }

        [Fact(DisplayName = "Create AirCraft Carrier With Default Constructor Should Return IMapItem Instance")]
        public void Create_AirCraft_Carrier_With_Default_Constructor_Should_Return_IMapItem_Instance()
        {
            // Arrange
            AirCraftCarrier airCraftCarrier;

            // Act
            airCraftCarrier = new AirCraftCarrier();

            // Assert
            airCraftCarrier.Should().BeAssignableTo<IMapItem>();
        }

        [Fact(DisplayName = "Create Submarine With Default Constructor Should Return Ship With Initial Conditions")]
        public void Create_Submarine_With_Default_Constructor_Should_Return_Ship_With_Initial_Conditions()
        {
            // Arrange
            Submarine submarine;
            List<Coordinate> expectedInitialPosition = new List<Coordinate>()
            {
                new Coordinate("A1"),
                new Coordinate("A2"),
            };
            Dictionary<Coordinate, Status> expectedInitialStatus = new Dictionary<Coordinate, Status>()
            {
                { new Coordinate("A1"), Status.New },
                { new Coordinate("A2"), Status.New },
            };
            Dictionary<Status, string> expectedSymbolByStatus = new Dictionary<Status, string>()
            {
                { Status.New , "O" },
                { Status.Destroyed , "X" },
            };

            // Act
            submarine = new Submarine();

            // Assert
            submarine.Name.Should().Be("Submarine");
            submarine.Alias.Should().Be("SB");
            submarine.Size.Should().Be(2);
            submarine.Layer.Should().Be(1);
            submarine.Status.Should().BeEquivalentTo(expectedInitialStatus);
            submarine.Coordinates.Should().BeEquivalentTo(expectedInitialPosition);
            submarine.SymbolByStatus.Should().BeEquivalentTo(expectedSymbolByStatus);
        }

        [Fact(DisplayName = "Create Destroyer With Default Constructor Should Return Ship With Initial Conditions")]
        public void Create_Destroyer_With_Default_Constructor_Should_Return_Ship_With_Initial_Conditions()
        {
            // Arrange
            Destroyer destroyer;
            List<Coordinate> expectedInitialPosition = new List<Coordinate>()
            {
                new Coordinate("A1"),
                new Coordinate("A2"),
                new Coordinate("A3"),
            };
            Dictionary<Coordinate, Status> expectedInitialStatus = new Dictionary<Coordinate, Status>()
            {
                { new Coordinate("A1"), Status.New },
                { new Coordinate("A2"), Status.New },
                { new Coordinate("A3"), Status.New },
            };
            Dictionary<Status, string> expectedSymbolByStatus = new Dictionary<Status, string>()
            {
                { Status.New , "O" },
                { Status.Destroyed , "X" },
            };

            // Act
            destroyer = new Destroyer();

            // Assert
            destroyer.Name.Should().Be("Destroyer");
            destroyer.Alias.Should().Be("DS");
            destroyer.Size.Should().Be(3);
            destroyer.Layer.Should().Be(1);
            destroyer.Status.Should().BeEquivalentTo(expectedInitialStatus);
            destroyer.Coordinates.Should().BeEquivalentTo(expectedInitialPosition);
            destroyer.SymbolByStatus.Should().BeEquivalentTo(expectedSymbolByStatus);
        }

        [Fact(DisplayName = "Create Tanker With Default Constructor Should Return Ship With Initial Conditions")]
        public void Create_Tanker_With_Default_Constructor_Should_Return_Ship_With_Initial_Conditions()
        {
            // Arrange
            Tanker tanker;
            List<Coordinate> expectedInitialPosition = new List<Coordinate>()
            {
                new Coordinate("A1"),
                new Coordinate("A2"),
                new Coordinate("A3"),
                new Coordinate("A4"),
            };
            Dictionary<Coordinate, Status> expectedInitialStatus = new Dictionary<Coordinate, Status>()
            {
                { new Coordinate("A1"), Status.New },
                { new Coordinate("A2"), Status.New },
                { new Coordinate("A3"), Status.New },
                { new Coordinate("A4"), Status.New },
            };
            Dictionary<Status, string> expectedSymbolByStatus = new Dictionary<Status, string>()
            {
                { Status.New , "O" },
                { Status.Destroyed , "X" },
            };

            // Act
            tanker = new Tanker();

            // Assert
            tanker.Name.Should().Be("Tanker");
            tanker.Alias.Should().Be("NT");
            tanker.Size.Should().Be(4);
            tanker.Layer.Should().Be(1);
            tanker.Status.Should().BeEquivalentTo(expectedInitialStatus);
            tanker.Coordinates.Should().BeEquivalentTo(expectedInitialPosition);
            tanker.SymbolByStatus.Should().BeEquivalentTo(expectedSymbolByStatus);
        }

        [Fact(DisplayName = "Create AirCraft Carrier With Default Constructor Should Return Ship With Initial Conditions")]
        public void Create_AirCraft_Carrier_With_Default_Constructor_Should_Return_Ship_With_Initial_Conditions()
        {
            // Arrange
            AirCraftCarrier airCraftCarrier;
            List<Coordinate> expectedInitialPosition = new List<Coordinate>()
            {
                new Coordinate("A1"),
                new Coordinate("A2"),
                new Coordinate("A3"),
                new Coordinate("A4"),
                new Coordinate("A5"),
            };
            Dictionary<Coordinate, Status> expectedInitialStatus = new Dictionary<Coordinate, Status>()
            {
                { new Coordinate("A1"), Status.New },
                { new Coordinate("A2"), Status.New },
                { new Coordinate("A3"), Status.New },
                { new Coordinate("A4"), Status.New },
                { new Coordinate("A5"), Status.New },
            };
            Dictionary<Status, string> expectedSymbolByStatus = new Dictionary<Status, string>()
            {
                { Status.New , "O" },
                { Status.Destroyed , "X" },
            };

            // Act
            airCraftCarrier = new AirCraftCarrier();

            // Assert
            airCraftCarrier.Name.Should().Be("AirCraft Carrier");
            airCraftCarrier.Alias.Should().Be("PS");
            airCraftCarrier.Size.Should().Be(5);
            airCraftCarrier.Layer.Should().Be(1);
            airCraftCarrier.Status.Should().BeEquivalentTo(expectedInitialStatus);
            airCraftCarrier.Coordinates.Should().BeEquivalentTo(expectedInitialPosition);
            airCraftCarrier.SymbolByStatus.Should().BeEquivalentTo(expectedSymbolByStatus);
        }

        [Fact(DisplayName = "Fix Ship Should Change Status of Their Coordinates To New")]
        public void Fix_Ship_Should_Change_Status_of_Their_Coordinates_To_New()
        {
            // Arrange
            Ship submarine = new Submarine();
            Dictionary<Coordinate, Status> expectedFinalStatus = new Dictionary<Coordinate, Status>()
            {
                { new Coordinate("A1"), Status.New },
                { new Coordinate("A2"), Status.New },
            };
            submarine.Hit(new Coordinate("A1"));

            // Act
            submarine.Fix();

            // Assert
            submarine.Status.Should().BeEquivalentTo(expectedFinalStatus);
        }

        [Theory(DisplayName = "Update Position With Valid Coordinates Should Change The Ship Position Succesfully")]
        [InlineData("B1B2", "B1", "B2")]
        [InlineData("C1D1", "C1", "D1")]
        public void Update_Position_With_Valid_Coordinates_Should_Change_The_Ship_Position_Succesfully(string range, params string[] coordinates)
        {
            // Arrange
            Ship submarine = new Submarine();
            CoordinateRangeLinear newPosition = new CoordinateRangeLinear(range);
            List<Coordinate> expectedFinalCoordinates = new List<Coordinate>();
            foreach (var coordinate in coordinates)
            {
                expectedFinalCoordinates.Add(new Coordinate(coordinate));
            }

            // Act
            submarine.UpdatePosition(newPosition);

            // Assert
            submarine.Coordinates.Should().BeEquivalentTo(expectedFinalCoordinates);
        }

        [Theory(DisplayName = "Update Position With Size Different of The Ship Should Throw Domain Validation Exception")]
        [InlineData("A3A5", 3)]
        [InlineData("J3J3", 1)]
        [InlineData("A1E1", 5)]
        public void Update_Position_With_Size_Different_of_The_Ship_Should_Throw_Domain_Validation_Exception(string invalidRange, int invalidSize)
        {
            // Arrange
            Ship submarine = new Submarine();
            CoordinateRangeLinear rangeWithDifferentSizeOfTheShip = new CoordinateRangeLinear(invalidRange);

            // Act
            Action action = () => submarine.UpdatePosition(rangeWithDifferentSizeOfTheShip);

            // Assert
            action.Should()
                .Throw<DomainValidationException>()
                .WithMessage($"Invalid size. The new position has to be 2 instead of {invalidSize}");
        }
    }
}
