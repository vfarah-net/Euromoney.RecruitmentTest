using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repository;
using Domain.Resolvers;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Domain.Test.Unit.Resolvers
{
    [TestFixture]
    public class BadWordResolverTestFixture
    {
        private IFixture fixture;
        private Mock<IRepository<BadWord>> negativeWordRepositoryMock;

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());
            negativeWordRepositoryMock = fixture.Freeze<Mock<IRepository<BadWord>>>();
        }

        [TearDown]
        public void TearDown()
        {
            negativeWordRepositoryMock.ResetCalls();
        }

        [TestFixture]
        private class GetBadWordCountTestFixture : BadWordResolverTestFixture
        {
            private string content;
            private IEnumerable<BadWord> negativeWords;

            [SetUp]
            public void SetUp()
            {
                content = fixture.Create<string>();
                negativeWords = fixture.CreateMany<BadWord>();
                negativeWordRepositoryMock.Setup(x => x.GetAll())
                    .Returns(() => negativeWords.ToList());
            }

            [TestCase("")]
            [TestCase(null)]
            [TestCase(" ")]
            public void GetBadWordCount_WhenEmptyOrNullContentAssigned_ShouldReturnZeroBadWordCount(
                string contentUnderTest)
            {
                // Arrange
                IBadWordResolver subject = fixture.Create<BadWordResolver>();

                // Act
                var actual = subject.GetBadWordCount(contentUnderTest);

                // Assert
                Assert.That(actual, Is.EqualTo(0));
            }

            [Test]
            public void GetBadWordCount_WhenCalled_ShouldGetAllBadWordsFromDataStore()
            {
                // Arrange

                IBadWordResolver subject = fixture.Create<BadWordResolver>();

                // Act
                var actual = subject.GetBadWordCount(content);

                // Assert
                negativeWordRepositoryMock.Verify(x => x.GetAll(), Times.Once());
            }

            [Test]
            public void GetBadWordCount_WhenContentWithNoBadWordsAssigned_ShouldReturnZeroBadWordCount()
            {
                // Arrange
                IBadWordResolver subject = fixture.Create<BadWordResolver>();

                // Act
                var actual = subject.GetBadWordCount(content);

                // Assert
                Assert.That(actual, Is.EqualTo(0));
            }

            [Test]
            public void GetBadWordCount_WhenContentContainsBadWords_ShouldReturnBadWordCountGreaterThanZero()
            {
                // Arrange
                negativeWords = new List<BadWord>
                {
                    fixture.Build<BadWord>()
                        .With(x => x.Name, content)
                        .Create()
                };
                IBadWordResolver subject = fixture.Create<BadWordResolver>();

                // Act
                var actual = subject.GetBadWordCount(content);

                // Assert
                Assert.That(actual, Is.GreaterThan(0));
            }
        }

        [TestFixture]
        private class AddBadWordsTestFixture : BadWordResolverTestFixture
        {
            private BadWord badWord;

            [SetUp]
            public void SetUp()
            {
                this.badWord = fixture.Create<BadWord>();
            }

            [Test]
            public void AddBadWords_WhenAddingValidBadWord_ShouldAddBadWordToDataStore()
            {
                // Arrange

                IBadWordResolver subject = fixture.Create<BadWordResolver>();

                // Act
                subject.AddBadWords(badWord);

                // Assert
                negativeWordRepositoryMock.Verify(x => x.Add(It.IsIn(badWord)), Times.Once);
            }
        }

        [TestFixture]
        private class FilterTestFixture : BadWordResolverTestFixture
        {
            private string content;            
            private IEnumerable<BadWord> negativeWords;

            [SetUp]
            public void SetUp()
            {
                content = fixture.Create<string>();
                negativeWords = fixture.CreateMany<BadWord>();
                negativeWordRepositoryMock.Setup(x => x.GetAll())
                    .Returns(() => negativeWords.ToList());
            }

            [Test]
            public void Filter_WhenFilteringContentWithBadWords_ShouldReturnFilteredContentWithExpectedDisplayHash()
            {
                // Arrange
                content = "Test";
                negativeWords = new List<BadWord>
                {
                    fixture.Build<BadWord>()
                        .With(x => x.Name, content)
                        .Create()
                };
                IBadWordResolver subject = fixture.Create<BadWordResolver>();

                // Act
                var actual = subject.Filter(content);

                // Assert
                Assert.That(actual, Is.EqualTo("T##t"));
            }
        }
    }
}