using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherTest.Integration;
using WeatherTest.Service;
using Xunit;
using static WeatherTest.Integration.JsonResultObject;

namespace WeatherTest.UnitTests.Service
{
    public class ExtensionMethodsTests
    {
        [Fact]
        public void If_Items_Only_Contains_Periods_With_Dates_Of_Tomorrow()
        {
            //Arrange
            var jsonResultObject = new JsonResultObject()
            {
                properties = new Properties()
                {
                    generatedAt = DateTime.Now,
                    periods = new List<Period>
                    {
                        new Period()
                        {
                            endTime = DateTime.Now.AddHours(3),
                            startTime = DateTime.Now.AddHours(2),
                            temperature = 5,
                            temperatureUnit = "F"
                        },
                        new Period()
                        {
                            endTime = DateTime.Now.AddDays(1).AddHours(4),
                            startTime = DateTime.Now.AddDays(1).AddHours(3),
                            temperature = 3,
                            temperatureUnit = "F"
                        },
                        new Period()
                        {
                            endTime = DateTime.Now.AddDays(1).AddHours(2),
                            startTime = DateTime.Now.AddDays(1).AddHours(1),
                            temperature = 5,
                            temperatureUnit = "F"
                        }
                    }
                }
            };

            //Act
            var result = jsonResultObject.FilterOutWrongDays();

            //Assert
            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public void If_Temperatures_Is_Sorted()
        {
            //Arrange
            var periods = new List<Period>
            {
                new Period()
                {
                    endTime = DateTime.Now.AddHours(3),
                    startTime = DateTime.Now.AddHours(2),
                    temperature = 4,
                    temperatureUnit = "F"
                },
                new Period()
                {
                    endTime = DateTime.Now.AddDays(1).AddHours(4),
                    startTime = DateTime.Now.AddDays(1).AddHours(3),
                    temperature = 7,
                    temperatureUnit = "F"
                },
                new Period()
                {
                    endTime = DateTime.Now.AddDays(1).AddHours(2),
                    startTime = DateTime.Now.AddDays(1).AddHours(1),
                    temperature = 1,
                    temperatureUnit = "F"
                }
            };

            //Act
            var result = periods.GetSortedTemperatures();

            //Assert
            Assert.Equal(1, result[0]);
            Assert.Equal(4, result[1]);
            Assert.Equal(7, result[2]);
        }

        [Fact]
        public void If_Array_Gets_Median_Value()
        {
            //Arrange
            var numbers = new List<int> { 1, 5, 8, 4, 2 };
            var sortedNumbers = new List<int> { 1, 5, 8, 4, 2 };
            sortedNumbers.Sort();

            //Act
            var resultNumbers = numbers.GetMedian();
            var resultSortedNumbers = sortedNumbers.GetMedian();

            //Assert
            Assert.Equal(8, resultNumbers);
            Assert.Equal(4, resultSortedNumbers);
        }
    }
}