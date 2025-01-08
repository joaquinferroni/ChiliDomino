using FluentAssertions;
using System.Text.RegularExpressions;
using Xunit;

namespace Domino.Tests
{   
    public class DominoTest
    {
        [InlineData("{1|1}")]
        [InlineData("(1|1)")]
        [InlineData("[1,1]")]
        [InlineData("[1|1],[1|2]")]
        [InlineData("[a|b]")]
        [InlineData("")]
        [Theory]
        public void Should_ThrowsException_BecauseOfInvalidCharacters(string chain)
        {
            Action act = () => Domino.Create(chain);
            var ae = new ArgumentException("Input has not valid characters", nameof(chain));
            act.Should().Throw<ArgumentException>().WithMessage(ae.Message);
        }


        [InlineData("[1|2] [2|3] [3|1]",true)]
        [InlineData("[1|2] [2|3] [4|1]", false)]
        [InlineData("[1|2] [2|3] [3|1] [2|2] [2|2]", true)]
        [InlineData("[2|2] [2|2]", true)]
        [InlineData("[2|2]", true)]
        [InlineData("[2|4] [1|3]", false)]
        [Theory]
        public void IsCircular_Should_ReturnIfChainCanBeCircular(string chain, bool isCircular)
        {
            var d = Domino.Create(chain);
            d.IsCircular.Should().Be(isCircular);
        }

        [Fact]
        public void GetSolution_Should_ReturnsAValidCircularSolution()
        {
            var chain = "[1|2] [2|3] [3|1] [2|2] [2|2]";
            var d = Domino.Create(chain);
            
            var solution = d.GetSolution();
            //Remove special characters and keep only the numbers
            var cleanSolution = Regex.Replace(solution, @"[\[\]|\s]", "");

            //Compare the first and last characters, they must be the same
            cleanSolution[0].Should().Be(cleanSolution[^1]);

            //Iterate the rest of characters to validate that the end of one stone match the beggining of the other
            for (int i = 2; i < cleanSolution.Length-1; i+=2)
            {
                cleanSolution[i].Should().Be(cleanSolution[i - 1]);
            }   
        }

        [Fact]
        public void GetSolution_Should_ReturnsAValidCircularSolutionIfCalledTwice()
        {
            var chain = "[1|2] [2|3] [3|1] [2|2] [2|2]";
            var d = Domino.Create(chain);

            d.GetSolution();
            var solution = d.GetSolution();
            //Remove special characters and keep only the numbers
            var cleanSolution = Regex.Replace(solution, @"[\[\]|\s]", "");

            //Compare the first and last characters, they must be the same
            cleanSolution[0].Should().Be(cleanSolution[^1]);

            //Iterate the rest of characters to validate that the end of one stone match the beggining of the other
            for (int i = 2; i < cleanSolution.Length - 1; i += 2)
            {
                cleanSolution[i].Should().Be(cleanSolution[i - 1]);
            }
        }

        [Fact]
        public void GetSolution_Should_ReturnAMssageIfIsNotCircular()
        {
            var chain = "[1|2] [2|3] [3|1] [2|2] [2|4]";
            var d = Domino.Create(chain);

            var solution = d.GetSolution();
            solution.Should().Be("This game is not circular, please try with a different game");
            d.IsCircular.Should().BeFalse();
        }
    }
}