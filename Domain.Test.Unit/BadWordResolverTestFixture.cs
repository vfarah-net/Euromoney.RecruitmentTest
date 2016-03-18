using System;
using System.Collections.Generic;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Domain.Test.Unit
{
    [TestFixture]
    public class BadWordResolverTestFixture
    {
        private IFixture fixture;

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        private IReadOnlyList<string> CreateBadWordList()
        {
            return new List<string>
                {
                    fixture.Create("horrible"),
                    fixture.Create("swine"),
                    fixture.Create("nasty"),
                    fixture.Create("bad")
                };
        }

        [TestFixture]
        private class GetBadWordCountTestFixture: BadWordResolverTestFixture
        {
            private string content;

            [SetUp]
            public void SetUp()
            {
                content = fixture.Create<string>();
            }

            [TestCase("")]
            [TestCase(null)]
            [TestCase(" ")]
            public void GetBadWordCount_WhenEmptyOrNullContentAssigned_ShouldReturnZeroBadWordCount(string contentUnderTest)
            {
                // Arrange
                IBadWordResolver subject = fixture.Create<BadWordResolver>();

                // Act
                var actual = subject.GetBadWordCount(contentUnderTest);

                // Assert
                Assert.That(actual, Is.EqualTo(0));

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
            public void GetBadWordCount_WhenContentContainsSwine_ShouldReturnOneBadWordCount()
            {
                // Arrange
                content = fixture.Create<string>("swine");
                IBadWordResolver subject = fixture.Create<BadWordResolver>();

                // Act
                var actual = subject.GetBadWordCount(content);

                // Assert
                Assert.That(actual, Is.EqualTo(1));

            }          

            [Test]
            public void GetBadWordCount_WhenContentContainsBad_ShouldReturnOneBadWordCount()
            {
                // Arrange
                content = fixture.Create<string>("bad");
                IBadWordResolver subject = fixture.Create<BadWordResolver>();

                // Act
                var actual = subject.GetBadWordCount(content);

                // Assert
                Assert.That(actual, Is.EqualTo(1));
            }

            [Test]
            public void GetBadWordCount_WhenContentContainsNasty_ShouldReturnOneBadWordCount()
            {
                // Arrange
                content = fixture.Create<string>("nasty");
                IBadWordResolver subject = fixture.Create<BadWordResolver>();

                // Act
                var actual = subject.GetBadWordCount(content);

                // Assert
                Assert.That(actual, Is.EqualTo(1));
            }
           
            [Test]
            public void GetBadWordCount_WhenContentContainsHorrible_ShouldReturnOneBadWordCount()
            {
                // Arrange
                content = fixture.Create<string>("horrible");
                IBadWordResolver subject = fixture.Create<BadWordResolver>();

                // Act
                var actual = subject.GetBadWordCount(content);

                // Assert
                Assert.That(actual, Is.EqualTo(1));
            }           

            [Test]
            public void GetBadWordCount_WhenContentContainsAllBadWords_ShouldReturnBadWordCountForAllExpectedWords()
            {
                // Arrange
                IReadOnlyList<string> expectedBadWords = CreateBadWordList();
                content = string.Join(",", expectedBadWords);
                IBadWordResolver subject = fixture.Create<BadWordResolver>();

                // Act
                var actual = subject.GetBadWordCount(content);

                // Assert
                Assert.That(actual, Is.EqualTo(expectedBadWords.Count));
            }
        }
    }
}
