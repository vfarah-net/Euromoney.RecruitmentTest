using System;
using System.Linq;
using Domain.Repository;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Domain.Test.Unit.Repository
{
    [TestFixture]
    public class SimpleRepositoryTestFixture
    {
        private IFixture fixture;

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        internal class TestModel
        {
            public string Id { get; set; }
        }

        [TestFixture]
        private class GetAllTestFixture : SimpleRepositoryTestFixture
        {
            [Test]
            public void GetAll_WhenCalled_ShouldNotBeEmpty()
            {
                // Arrange

                IRepository<TestModel> subject = fixture.Create<SimpleRepository<TestModel>>();
                subject.Add(fixture.CreateMany<TestModel>().ToArray());

                // Act
                var actual = subject.GetAll();

                // Assert
                Assert.That(actual, Is.Not.Empty);
            }
        }

        [TestFixture]
        private class AddTestFixture : SimpleRepositoryTestFixture
        {
            [Test]
            public void Add_WhenCalled_ShouldAddDataToRepository()
            {
                // Arrange

                IRepository<TestModel> subject = fixture.Create<SimpleRepository<TestModel>>();

                // Act
                subject.Add(fixture.CreateMany<TestModel>().ToArray());

                // Assert
                Assert.That(subject.GetAll(), Is.Not.Empty);
            }
        }

        [TestFixture]
        private class ContainsTestFixture : SimpleRepositoryTestFixture
        {
            [Test]
            public void Contains_WhenCalledByAContainedSearchId_ShouldContainData()
            {
                // Arrange
                IRepository<TestModel> subject = fixture.Create<SimpleRepository<TestModel>>();
                string searchId = fixture.Create<string>();
                var expectedTestModel = fixture.Build<TestModel>()
                    .With(x => x.Id, searchId)
                    .Create();
                subject.Add(expectedTestModel);

                // Act
                var actual = subject.Contains(x => x.Id.Equals(searchId, StringComparison.OrdinalIgnoreCase));

                // Assert
                Assert.That(actual, Is.True);
            }

            [Test]
            public void Contains_WhenCalledByAnInvalidSearchId_ShouldNotContainData()
            {
                // Arrange
                IRepository<TestModel> subject = fixture.Create<SimpleRepository<TestModel>>();
                string searchId = fixture.Create<string>();
                subject.Add(fixture.CreateMany<TestModel>().ToArray());

                // Act
                var actual = subject.Contains(x => x.Id.Equals(searchId, StringComparison.OrdinalIgnoreCase));

                // Assert
                Assert.That(actual, Is.False);
            }
        }
    }
}

    