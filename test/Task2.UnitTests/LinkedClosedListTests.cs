using AutoBogus;
using Bogus;
using FluentAssertions;

namespace budaevaler.Interview.Task2.UnitTests;

public abstract class LinkedClosedListTests<T>
{
    [Fact]
    public void GetIndexer_WhenIndexIsLessThenZero_ShouldThrow()
    {
        // Arrange
        var cut = new LinkedClosedList<T>();
        var index = -1;

        // Act, Asserts
        Assert.Throws<ArgumentOutOfRangeException>(() => cut[index]);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    public void GetIndexer_WhenIndexIsGreaterOrEqualsListLength_ShouldThrow(int index)
    {
        // Arrange
        var cut = new LinkedClosedList<T>();

        // Act, Asserts
        Assert.Throws<ArgumentOutOfRangeException>(() => cut[index]);
    }

    [Fact]
    public void SetIndexer_WhenIndexIsLessThenZero_ShouldThrow()
    {
        // Arrange
        var cut = new LinkedClosedList<T>();
        var index = -1;
        T? value = default;

        // Act, Asserts
        Assert.Throws<ArgumentOutOfRangeException>(() => cut[index] = value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    public void SetIndexer_WhenIndexIsGreaterOrEqualsListLength_ShouldThrow(int index)
    {
        // Arrange
        var cut = new LinkedClosedList<T>();
        T? value = default;

        // Act, Asserts
        Assert.Throws<ArgumentOutOfRangeException>(() => cut[index] = value);
    }

    [Fact]
    public void GetAndSetIndexer_WhenIndexIsInListLimit_ShouldSuccess()
    {
        //Arrange
        var cut = new LinkedClosedList<T>(
            GetRangeOfNotDefaultValues(10));
        var value = GetNotDefaultValue();
        var index = 2;
        
        // Act
        cut[index] = value;
        
        //Assert
        Assert.Equal(value, cut[index]);
    }
    
    [Fact]
    public void GetHead_WhenListIsEmpty_ShouldReturnDefault()
    {
        // Arrange
        var cut = new LinkedClosedList<T>();
        T? expected = default;

        // Act
        var result = cut.Head;

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetCurrent_WhenListIsEmpty_ShouldReturnDefault()
    {
        // Arrange
        var cut = new LinkedClosedList<T>();
        T? expected = default;

        // Act
        var result = cut.Current;

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetPrevious_WhenListIsEmpty_ShouldReturnDefault()
    {
        // Arrange
        var cut = new LinkedClosedList<T>();
        T? expected = default;

        // Act
        var result = cut.Previous;

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetNext_WhenListIsEmpty_ShouldReturnsDefault()
    {
        // Arrange
        var cut = new LinkedClosedList<T>();
        T? expected = default;

        // Act
        var result = cut.Next;

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Clear_ShouldListLengthBeEqualsToZero()
    {
        // Arrange
        var cut = new LinkedClosedList<T> { default };
        var expected = 0;

        // Act
        cut.Clear();

        // Assert
        Assert.Equal(expected, cut.Count);
    }

    [Fact]
    public void Add_ShouldSuccess()
    {
        // Arrange
        var cut = new LinkedClosedList<T>();
        var expectedListLength = cut.Count + 1;
        var value = GetNotDefaultValue();

        //Act
        cut.Add(value);

        //Assert
        Assert.Equal(expectedListLength, cut.Count);
        cut.Should().Contain(value);
        cut.Should().EndWith(value);
    }
    
    [Fact]
    public void Contains_PassDefaultItemWhenListHasDefaultItem_ShouldReturnTrue()
    {
        // Arrange
        var cut = new LinkedClosedList<T> { default };
        var expected = true;

        // Act
        var result = cut.Contains(default);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Contains_PassSomeItemWhenListHasThatItem_ShouldReturnTrue()
    {
        // Arrange
        var cut = new LinkedClosedList<T>();
        var item = GetNotDefaultValue();
        cut.Add(item);
        var expected = true;

        // Act
        var result = cut.Contains(item);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Contains_PassSomeItemWhenListDoesNotContainsThatItem_ShouldReturnFalse()
    {
        // Arrange
        var cut = new LinkedClosedList<T>();
        var item = GetNotDefaultValue();
        var anotherItem = GetNotDefaultValue();
        cut.Add(item);
        var expected = false;

        // Act
        var result = cut.Contains(anotherItem);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CopyTo_WhenListIsEmpty_ShouldArrayStaysTheSame()
    {
        // Arrange
        var cut = new LinkedClosedList<T>();
        var array = new T[10];
        var arrayForCopy = new T[10];

        // Act
        cut.CopyTo(arrayForCopy, 0);

        // Assert
        array.Should().Equal(arrayForCopy);
    }

    [Theory]
    [InlineData(0, 2)]
    [InlineData(5, 3)]
    public void CopyTo_WhenListIsInArrayLimit_ShouldArrayContainsList(
        int index,
        int listLength)
    {
        // Arrange
        var cut = new LinkedClosedList<T>(
            GetRangeOfNotDefaultValues(listLength));

        var arrayForCopy = new T[10];

        // Act
        cut.CopyTo(arrayForCopy, index);

        // Assert
        cut.Should().BeSubsetOf(arrayForCopy);
    }

    [Fact]
    public void CopyTo_WhenListIsLongerThenArray_ShouldThrow()
    {
        // Arrange
        var cut = new LinkedClosedList<T>(
            GetRangeOfNotDefaultValues(10));

        var arrayForCopy = new T[5];

        // Act, Asserts
        Assert.Throws<ArgumentException>(() => cut.CopyTo(arrayForCopy, 0));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    public void IndexOf_WhenListDoesNotContainsItem_ShouldReturnMinusOne(int listLength)
    {
        // Arrange
        var cut = new LinkedClosedList<T>(
            GetRangeOfDefaultValues(listLength));

        var expectedIndex = -1;

        // Act
        var result = cut.IndexOf(GetNotDefaultValue());

        // Assert
        Assert.Equal(expectedIndex, result);
    }

    [Fact]
    public void IndexOf_WhenPassDefaultItemAndListContainsDefaultItem_ShouldReturnIndex()
    {
        // Arrange
        var cut = new LinkedClosedList<T>() { default };
        var expectedIndex = 0;

        // Act
        var result = cut.IndexOf(default);

        // Assert
        Assert.Equal(expectedIndex, result);
    }

    [Fact]
    public void IndexOf_WhenPassItemAndListContainsItem_ShouldReturnIndex()
    {
        // Arrange
        var value = GetNotDefaultValue();
        var cut = new LinkedClosedList<T>() { value };
        var expectedIndex = 0;

        // Act
        var result = cut.IndexOf(value);

        // Assert
        Assert.Equal(expectedIndex, result);
    }

    [Fact]
    public void Insert_WhenIndexGreaterThenListLength_ShouldThrow()
    {
        // Arrange
        var cut = new LinkedClosedList<T>();
        var index = 1;

        // Act, Asserts
        Assert.Throws<ArgumentOutOfRangeException>(() => cut.Insert(index, default));
    }

    [Fact]
    public void Insert_WhenIndexLessThenZero_ShouldThrow()
    {
        // Arrange
        var cut = new LinkedClosedList<T>();
        var index = -1;

        // Act, Asserts
        Assert.Throws<ArgumentOutOfRangeException>(() => cut.Insert(index, default));
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(5, 7)]
    public void Insert_WhenIndexInListsLimits_ShouldSuccess(int index, int listLength)
    {
        // Arrange
        var cut = new LinkedClosedList<T>(
            GetRangeOfNotDefaultValues(listLength));

        var value = GetNotDefaultValue();
        var expectedCount = cut.Count + 1;

        // Act
        cut.Insert(index, value);

        // Assert
        Assert.Equal(value, cut[index]);
        Assert.Equal(expectedCount, cut.Count);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(10)]
    public void MoveNext_OneStepForward_ShouldCurrentMovesToNext_PreviousMovesToCurrent(int listLength)
    {
        // Arrange
        var cut = new LinkedClosedList<T>(
            GetRangeOfNotDefaultValues(listLength));

        var expectedPrevious = cut.Current;
        var expectedCurrent = cut.Next;

        // Act
        cut.MoveNext(1);

        // Assert
        Assert.Equal(expectedPrevious, cut.Previous);
        Assert.Equal(expectedCurrent, cut.Current);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(10)]
    public void MoveNext_OneStepBackward_ShouldCurrentMovesToPrevious_NextMovesToCurrent(int listLength)
    {
        // Arrange
        var cut = new LinkedClosedList<T>(
            GetRangeOfNotDefaultValues(listLength));
        var expectedCurrent = cut.Previous;
        var expectedNext = cut.Current;

        // Act
        cut.MoveNext(-1);

        // Assert
        Assert.Equal(expectedNext, cut.Next);
        Assert.Equal(expectedCurrent, cut.Current);
    }

    [Theory]
    [InlineData(3, 4, 1)]
    [InlineData(3, -4, 2)]
    [InlineData(3, 6, 2)]
    [InlineData(3, -6, 2)]
    public void MoveNext_WhenPassingThroughHead_ShouldInvokeEvent(
        int listLength,
        int step,
        int headReachedCountExpected)
    {
        // Arrange
        var cut = new LinkedClosedList<T>(
            GetRangeOfNotDefaultValues(listLength));
        var headReachedCount = 0;
        cut.HeadReached += (s, e) => headReachedCount++;

        // Act
        cut.MoveNext(step);

        // Assert
        Assert.Equal(headReachedCountExpected, headReachedCount);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(10)]
    public void MoveBack_OneStepForward_ShouldCurrentMovesToPrevious_NextMovesToCurrent(int listLength)
    {
        // Arrange
        var cut = new LinkedClosedList<T>(
            GetRangeOfNotDefaultValues(listLength));
        var expectedCurrent = cut.Previous;
        var expectedNext = cut.Current;

        // Act
        cut.MoveBack(1);

        // Assert
        Assert.Equal(expectedNext, cut.Next);
        Assert.Equal(expectedCurrent, cut.Current);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(10)]
    public void MoveBack_OneStepBackward_ShouldCurrentMovesToNext_PreviousMovesToCurrent(int listLength)
    {
        // Arrange
        var cut = new LinkedClosedList<T>(
            GetRangeOfNotDefaultValues(listLength));
        var expectedPrevious = cut.Current;
        var expectedCurrent = cut.Next;

        // Act
        cut.MoveBack(-1);

        // Assert
        Assert.Equal(expectedPrevious, cut.Previous);
        Assert.Equal(expectedCurrent, cut.Current);
    }

    [Theory]
    [InlineData(3, 4, 2)]
    [InlineData(3, -4, 1)]
    [InlineData(3, 6, 2)]
    [InlineData(3, -6, 2)]
    public void MoveBack_WhenPassingThroughHead_ShouldInvokeEvent(
        int listLength,
        int step,
        int headReachedCountExpected)
    {
        // Arrange
        var cut = new LinkedClosedList<T>(
            GetRangeOfNotDefaultValues(listLength));
        var headReachedCount = 0;
        cut.HeadReached += (s, e) => headReachedCount++;

        // Act
        cut.MoveBack(step);

        // Assert
        Assert.Equal(headReachedCountExpected, headReachedCount);
    }

    [Fact]
    public void Remove_WhenListIsEmpty_ShouldReturnFalse()
    {
        // Arrange
        var cut = new LinkedClosedList<T>();
        var expected = false;

        // Act
        var result = cut.Remove(default);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Remove_WhenListDoesNotContainsItem_ShouldReturnFalse()
    {
        // Arrange
        var cut = new LinkedClosedList<T>(
            GetRangeOfNotDefaultValues(10));
        var expected = false;

        // Act
        var result = cut.Remove(default);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Remove_WhenListContainsDefaultItem_ShouldReturnTrue()
    {
        // Arrange
        var cut = new LinkedClosedList<T>(
            GetRangeOfNotDefaultValues(10));
        T? value = default;
        cut.Add(value);
        var expected = true;
        var expectedListLength = cut.Count - 1;

        // Act
        var result = cut.Remove(value);

        // Assert
        Assert.Equal(expected, result);
        Assert.Equal(expectedListLength, cut.Count);
        cut.Should().NotContain(value);
    }

    [Fact]
    public void Remove_WhenListContainsItem_ShouldReturnTrue()
    {
        // Arrange
        var cut = new LinkedClosedList<T>(
            GetRangeOfNotDefaultValues(10));
        T? value = GetNotDefaultValue();
        cut.Add(value);
        var expected = true;
        var expectedListLength = cut.Count - 1;


        // Act
        var result = cut.Remove(value);

        // Assert
        Assert.Equal(expected, result);
        Assert.Equal(expectedListLength, cut.Count);
        cut.Should().NotContain(value);
    }

    [Fact]
    public void RemoveAt_WhenIndexIsLessThenZero_ShouldThrow()
    {
        // Arrange
        var cut = new LinkedClosedList<T>(
            GetRangeOfDefaultValues(10));
        var index = -1;

        // Act, Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => cut.RemoveAt(index));
    }

    [Theory]
    [InlineData(15)]
    [InlineData(10)]
    public void RemoveAt_WhenIndexIsGreaterOrEqualsListLength_ShouldThrow(int index)
    {
        // Arrange
        var cut = new LinkedClosedList<T>(
            GetRangeOfDefaultValues(10));

        // Act, Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => cut.RemoveAt(index));
    }

    [Theory]
    [InlineData(5, 10)]
    [InlineData(10, 11)]
    [InlineData(0, 1)]
    public void RemoveAt_WhenIndexInListLimit_ShouldSuccess(int index, int listLength)
    {
        // Arrange
        var cut = new LinkedClosedList<T>(
            GetRangeOfNotDefaultValues(listLength));
        var expectedCount = cut.Count - 1;
        var oldValue = cut[index];
        
        // Act
        cut.RemoveAt(index);
        
        // Assert
        Assert.Equal(expectedCount, cut.Count);
        cut.Should().NotContain(oldValue);
    }

    protected abstract T GetNotDefaultValue();

    private IEnumerable<T> GetRangeOfNotDefaultValues(int count) =>
        Enumerable
            .Range(0, count)
            .Select(x => GetNotDefaultValue());

    private IEnumerable<T> GetRangeOfDefaultValues(int count) =>
        Enumerable
            .Range(0, count)
            .Select<int, T>(x => default!);
}

public class IntLinkedClosedListTests : LinkedClosedListTests<int>
{
    protected override int GetNotDefaultValue() => new Faker().Random.Int();
}

public class StringLinkedClosedListTests : LinkedClosedListTests<string>
{
    protected override string GetNotDefaultValue() => new Faker().Person.FullName;
}

public class ProgressLinkedClosedListTests : LinkedClosedListTests<Progress<int>>
{
    protected override Progress<int> GetNotDefaultValue() => new AutoFaker<Progress<int>>().Generate();
}