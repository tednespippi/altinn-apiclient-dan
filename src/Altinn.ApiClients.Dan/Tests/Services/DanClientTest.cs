using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Altinn.ApiClients.Dan.Interfaces;
using Altinn.ApiClients.Dan.Models;
using Altinn.ApiClients.Dan.Models.Enums;
using Altinn.ApiClients.Dan.Services;
using Moq;
using NUnit.Framework;

namespace Tests.Services
{

    [TestFixture]
    public class DanClientTest
    {

        [Test]
        public async Task DeserializeTypedToSuppliedField_Ok()
        {
            // Setup
            var danApi = new Mock<IDanApi>();
            danApi
                .Setup(x => x.GetDirectharvest(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Dictionary<string, string>>()))
                .ReturnsAsync(new DataSet
                {
                    Values = new List<DataSetValue>
                    {
                        new()
                        {
                            DataSetValueName = "SomeString",
                            Value = "Bar",
                            ValueType = DataSetValueType.String
                        },
                        new()
                        {
                            DataSetValueName = "SomeJson",
                            Value = "{\"SomeString\":\"Bar\",\"SomeNumber\":123,\"SomeDateTime\":\"2021-12-12T04:56:12\"}",
                            ValueType = DataSetValueType.JsonSchema
                        }

                    }
                });

            var danClient = new DanClient(danApi.Object);

            // Act
            MyModel result = await danClient.GetDataSet<MyModel>("a", "a", deserializeField: "SomeJson");

            // Verify
            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(result.SomeString));
            Assert.AreEqual(result.SomeNumber, (decimal)123);
            Assert.IsNotNull(result.SomeDateTime);
            Assert.IsFalse(result.SomeDateTime.Equals(DateTime.MinValue));

        }

        [Test]
        public void DeserializeTypedToSuppliedField_FailedMissing()
        {
            // Setup
            var danApi = new Mock<IDanApi>();
            danApi
                .Setup(x => x.GetDirectharvest(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Dictionary<string, string>>()))
                .ReturnsAsync(new DataSet
                {
                    Values = new List<DataSetValue>
                    {
                        new()
                        {
                            DataSetValueName = "SomeString",
                            Value = "Bar",
                            ValueType = DataSetValueType.String
                        },
                        new()
                        {
                            DataSetValueName = "SomeJson",
                            Value = "{\"SomeString\":\"Bar\",\"SomeNumber\":123,\"SomeDateTime\":\"2021-12-12T04:56:12\"}",
                            ValueType = DataSetValueType.JsonSchema
                        }

                    }
                });

            var danClient = new DanClient(danApi.Object);

            // Act / verify
            Assert.ThrowsAsync<DanException>(async () =>
                await danClient.GetDataSet<MyModel>("a", "a", deserializeField: "SomeJsonThatDoesNotExist"));

        }

        [Test]
        public async Task DeserializeTypedFromFirstJsonField_Ok()
        {
            // Setup
            var danApi = new Mock<IDanApi>();
            danApi
                .Setup(x => x.GetDirectharvest(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Dictionary<string, string>>()))
                .ReturnsAsync(new DataSet
                {
                    Values = new List<DataSetValue>
                    {
                        new()
                        {
                            DataSetValueName = "SomeJson",
                            Value = "{\"SomeString\":\"Bar\",\"SomeNumber\":123,\"SomeDateTime\":\"2021-12-12T04:56:12\"}",
                            ValueType = DataSetValueType.JsonSchema
                        },
                        new()
                        {
                            DataSetValueName = "SomeString",
                            Value = "Bar",
                            ValueType = DataSetValueType.String
                        }
                    }
                });

            var danClient = new DanClient(danApi.Object);

            // Act
            MyModel result = await danClient.GetDataSet<MyModel>("a", "a");

            // Verify
            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(result.SomeString));
            Assert.AreEqual(result.SomeNumber, (decimal)123);
            Assert.IsNotNull(result.SomeDateTime);
            Assert.IsFalse(result.SomeDateTime.Equals(DateTime.MinValue));

        }

        [Test]
        public void DeserializeTypedNoJsonTypes_Fail()
        {
            // Setup
            var danApi = new Mock<IDanApi>();
            danApi
                .Setup(x => x.GetDirectharvest(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Dictionary<string, string>>()))
                .ReturnsAsync(new DataSet
                {
                    Values = new List<DataSetValue>
                    {
                        new()
                        {
                            DataSetValueName = "SomeString",
                            Value = "Bar",
                            ValueType = DataSetValueType.String
                        }
                    }
                });

            var danClient = new DanClient(danApi.Object);

            // Act / verify
            Assert.ThrowsAsync<DanException>(async () =>
                await danClient.GetDataSet<MyModel>("a", "a"));
        }

        private DataSet GetDataSet()
        {
            return new DataSet
            {
                Values = new List<DataSetValue>
                {
                    new()
                    {
                        DataSetValueName = "SomeString",
                        Value = "Bar",
                        ValueType = DataSetValueType.String
                    },
                    new()
                    {
                        DataSetValueName = "SomeNumber",
                        Value = "123",
                        ValueType = DataSetValueType.String
                    },
                    new()
                    {
                        DataSetValueName = "SomeDateTime",
                        Value = "2021-12-12T04:56:12",
                        ValueType = DataSetValueType.String
                    },
                }
            };
        }
    }

    internal class MyModel
    {
        public string SomeString { get; set; }
        public decimal SomeNumber { get; set; }
        public DateTime SomeDateTime { get; set; }
    }
}