using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Resolvers;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace ContentConsole.Test.Unit
{
    [TestFixture]
    public class BadWordConsoleApplicationTestFixture
    {
        private IFixture fixture;
        private Mock<IBadWordResolver> badWordResolverMock;

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());
            badWordResolverMock = fixture.Freeze<Mock<IBadWordResolver>>();
        }

        [TearDown]
        public void TearDown()
        {
            badWordResolverMock.ResetCalls();
        }

        private bool CheckMandatoryBadWordsContained(BadWord[] badwords)
        {
            var mandatoryBadWords = new List<string>{ "swine" ,"bad" , "nasty", "horrible" };
            return badwords != null && 
                mandatoryBadWords.TrueForAll(each => badwords.Any(item => item.Name.Equals(each,StringComparison.OrdinalIgnoreCase)));
        }

        [TestFixture]
        public class RunTestFixture : BadWordConsoleApplicationTestFixture
        {
            private bool ignoreFiltering;

            [SetUp]
            public void SetUp()
            {
                ignoreFiltering = fixture.Create<bool>();
            }

            [Test]
            public void Constructor_WhenCalled_ShouldInitializeTheMandatoryBadWordList()
            {
                // Arrange
                IApplicationShell subject = fixture.Create<BadWordConsoleApplication>();

                // Act
                subject.Run(ignoreFiltering);

                // Assert
                badWordResolverMock.Verify(x => x.AddBadWords(It.Is<BadWord[]>(badwords => CheckMandatoryBadWordsContained(badwords))), Times.Once);
            }


            [Test]
            public void Run_WhenCalled_ShouldGetBadWordCount()
            {
                // Arrange
                IApplicationShell subject = fixture.Create<BadWordConsoleApplication>();

                // Act
                subject.Run(ignoreFiltering);

                // Assert
                badWordResolverMock.Verify(x => x.GetBadWordCount(It.IsAny<string>()), Times.Once);
            }

            [Test]
            public void Run_WhenCalledWithFiltering_ShouldCallBadWordFilter()
            {
                // Arrange
                IApplicationShell subject = fixture.Create<BadWordConsoleApplication>();

                // Act
                subject.Run(ignoreFiltering: false);

                // Assert
                badWordResolverMock.Verify(x => x.Filter(It.IsAny<string>()), Times.Once);
            }

            [Test]
            public void Run_WhenCalledWithFilteringTurnedOff_ShouldNotUtiliseBadWordFilter()
            {
                // Arrange
                IApplicationShell subject = fixture.Create<BadWordConsoleApplication>();

                // Act
                subject.Run(ignoreFiltering: true);

                // Assert
                badWordResolverMock.Verify(x => x.Filter(It.IsAny<string>()), Times.Never);
            }
        }
    }
}
