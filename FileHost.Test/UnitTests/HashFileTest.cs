using FileHost.WebApi.Entities;
using FileHost.WebApi.Helpers;
using Xunit;

namespace FileHost.Test.UnitTests
{
    public class HashFileTest
    {
        [Fact]
        public void Successfull_Hashing_File()
        {
            //Arrange
            var model = new FileData
            {
                FileName = "test",
                Format = "test",
                Source = "test",
                Data = new byte[] { 74, 65, 73, 74 }
            };

            //Act
            var hash = HashFile.GetHashString(model.FileName + model.Format + model.Source +
                System.Text.Encoding.UTF8.GetString(model.Data));

            //Assert
            Assert.NotNull(hash);
            Assert.Equal(6, hash.Length);
        }
    }
}
