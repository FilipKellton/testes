using FluentAssertions;
using NetLead.Helpers;

namespace NetLeadTests;
public class BatchValidatorTests
{
    public class Person(string name, int age)
    {
        public string Name { get; set; } = name;
        public int Age { get; set; } = age;
        public override string ToString() => $"Person: {Name}, Age: {Age}";
    }

    [Fact]
    public void ValidateBatch_ShouldReturnAllValid_WhenAllIntsPassPredicate()
    {
        // Given
        var numbers = new List<int> { 1, 2, 3, 4, 5 };
        Func<int, bool> isPositive = n => n > 0;

        // When
        var results = BatchValidator.ValidateBatch(numbers, isPositive).ToList();

        // Then
        results.Should().NotBeNullOrEmpty();
        results.Should().HaveCount(numbers.Count);
        results.Should().OnlyContain(r => r.IsValid);
        results.Should().AllSatisfy(r => r.ErrorMessage.Should().BeNull());
        results.Select(r => r.Item).Should().BeEquivalentTo(numbers); // Order and content
    }

    [Fact]
    public void ValidateBatch_ShouldReturnAllValid_WhenAllStringsPassPredicate()
    {
        // Given
        var words = new List<string> { "apple", "banana", "cherry" };
        Func<string, bool> isNotEmpty = s => !string.IsNullOrWhiteSpace(s);

        // When
        var results = BatchValidator.ValidateBatch(words, isNotEmpty).ToList();

        // Then
        results.Should().NotBeNullOrEmpty();
        results.Should().HaveCount(words.Count);
        results.Should().OnlyContain(r => r.IsValid);
        results.Should().AllSatisfy(r => r.ErrorMessage.Should().BeNull());
        results.Select(r => r.Item).Should().BeEquivalentTo(words);
    }

    [Fact]
    public void ValidateBatch_ShouldReturnAllValid_WhenAllCustomObjectsPassPredicate()
    {
        // Given
        var people = new List<Person>
        {
            new Person("Alice", 30),
            new Person("Bob", 25)
        };
        Func<Person, bool> isAdult = p => p.Age >= 18;

        // When
        var results = BatchValidator.ValidateBatch(people, isAdult).ToList();

        // Then
        results.Should().NotBeNullOrEmpty();
        results.Should().HaveCount(people.Count);
        results.Should().OnlyContain(r => r.IsValid);
        results.Should().AllSatisfy(r => r.ErrorMessage.Should().BeNull());
        results.Select(r => r.Item).Should().BeEquivalentTo(people);
    }

    [Fact]
    public void ValidateBatch_ShouldReturnAllInvalid_WhenAllIntsFailPredicate()
    {
        // Given
        var numbers = new List<int> { 1, 3, 5 };
        Func<int, bool> isEven = n => n % 2 == 0;
        var failureMessage = "Number is odd.";

        // When
        var results = BatchValidator.ValidateBatch(numbers, isEven, failureMessage).ToList();

        // Then
        results.Should().NotBeNullOrEmpty();
        results.Should().HaveCount(numbers.Count);
        results.Should().OnlyContain(r => !r.IsValid);
        results.Should().AllSatisfy(r => r.ErrorMessage.Should().Be(failureMessage));
        results.Select(r => r.Item).Should().BeEquivalentTo(numbers);
    }

    [Fact]
    public void ValidateBatch_ShouldReturnMixedResults_WhenSomeIntsFailPredicate()
    {
        // Given
        var numbers = new List<int> { 10, 15, 20, 25 };
        Func<int, bool> isMultipleOfTen = n => n % 10 == 0;
        var failureMessage = "Not a multiple of ten.";

        // When
        var results = BatchValidator.ValidateBatch(numbers, isMultipleOfTen, failureMessage).ToList();

        // Then
        results.Should().NotBeNullOrEmpty();
        results.Should().HaveCount(4);

        // Item 1: 10 (Valid)
        results[0].Item.Should().Be(10);
        results[0].IsValid.Should().BeTrue();
        results[0].ErrorMessage.Should().BeNull();

        // Item 2: 15 (Invalid)
        results[1].Item.Should().Be(15);
        results[1].IsValid.Should().BeFalse();
        results[1].ErrorMessage.Should().Be(failureMessage);

        // Item 3: 20 (Valid)
        results[2].Item.Should().Be(20);
        results[2].IsValid.Should().BeTrue();
        results[2].ErrorMessage.Should().BeNull();

        // Item 4: 25 (Invalid)
        results[3].Item.Should().Be(25);
        results[3].IsValid.Should().BeFalse();
        results[3].ErrorMessage.Should().Be(failureMessage);
    }

    [Fact]
    public void ValidateBatch_ShouldReturnMixedResults_WhenSomeCustomObjectsFailPredicate()
    {
        // Given
        var people = new List<Person>
        {
            new Person("Child", 5),
            new Person("Teenager", 16),
            new Person("Adult", 25),
            new Person("Senior", 65)
        };
        Func<Person, bool> isAdult = p => p.Age >= 18;
        var failureMessage = "Is not an adult.";

        // When
        var results = BatchValidator.ValidateBatch(people, isAdult, failureMessage).ToList();

        // Then
        results.Should().NotBeNullOrEmpty();
        results.Should().HaveCount(4);

        results[0].Item.Should().Be(people[0]); // Check item instance directly
        results[0].IsValid.Should().BeFalse();
        results[0].ErrorMessage.Should().Be(failureMessage);

        results[1].Item.Should().Be(people[1]);
        results[1].IsValid.Should().BeFalse();
        results[1].ErrorMessage.Should().Be(failureMessage);

        results[2].Item.Should().Be(people[2]);
        results[2].IsValid.Should().BeTrue();
        results[2].ErrorMessage.Should().BeNull();

        results[3].Item.Should().Be(people[3]);
        results[3].IsValid.Should().BeTrue();
        results[3].ErrorMessage.Should().BeNull();
    }

    [Fact]
    public void ValidateBatch_ShouldReturnCorrectResult_ForSingleValidItem()
    {
        // Given
        var numbers = new List<int> { 10 };
        Func<int, bool> isPositive = n => n > 0;

        // When
        var results = BatchValidator.ValidateBatch(numbers, isPositive).ToList();

        // Then
        results.Should().HaveCount(1);
        results[0].Item.Should().Be(10);
        results[0].IsValid.Should().BeTrue();
        results[0].ErrorMessage.Should().BeNull();
    }

    [Fact]
    public void ValidateBatch_ShouldReturnCorrectResult_ForSingleInvalidItem()
    {
        // Given
        var numbers = new List<int> { -5 };
        Func<int, bool> isPositive = n => n > 0;
        var failureMessage = "Number is not positive.";

        // When
        var results = BatchValidator.ValidateBatch(numbers, isPositive, failureMessage).ToList();

        // Then
        results.Should().HaveCount(1);
        results[0].Item.Should().Be(-5);
        results[0].IsValid.Should().BeFalse();
        results[0].ErrorMessage.Should().Be(failureMessage);
    }

    [Fact]
    public void ValidateBatch_ShouldReturnEmptyEnumerable_WhenInputCollectionIsEmpty()
    {
        // Given
        var emptyList = new List<string>();
        Func<string, bool> alwaysTrue = s => true;

        // When
        var results = BatchValidator.ValidateBatch(emptyList, alwaysTrue).ToList();

        // Then
        results.Should().BeEmpty();
    }

    [Fact]
    public void ValidateBatch_ShouldThrowArgumentNullException_WhenInputItemsAreNull()
    {
        // Given
        IEnumerable<int> nullItems = null!; // Use null-forgiving operator for clarity
        Func<int, bool> alwaysTrue = n => true;

        // When
        Action act = () => BatchValidator.ValidateBatch(nullItems, alwaysTrue);

        // Then
        act.Should().Throw<ArgumentNullException>()
           .WithParameterName("items");
    }

    [Fact]
    public void ValidateBatch_ShouldThrowArgumentNullException_WhenValidationPredicateIsNull()
    {
        // Given
        var numbers = new List<int> { 1, 2, 3 };
        Func<int, bool> nullPredicate = null!;

        // When
        Action act = () => BatchValidator.ValidateBatch(numbers, nullPredicate);

        // Then
        act.Should().Throw<ArgumentNullException>()
           .WithParameterName("validationPredicate");
    }

    [Fact]
    public void ValidateBatch_ShouldReturnAllValid_WhenPredicateAlwaysTrue()
    {
        // Given
        var words = new List<string> { "a", "b", "c" };
        Func<string, bool> alwaysTrue = s => true;

        // When
        var results = BatchValidator.ValidateBatch(words, alwaysTrue).ToList();

        // Then
        results.Should().NotBeNullOrEmpty();
        results.Should().OnlyContain(r => r.IsValid);
    }

    [Fact]
    public void ValidateBatch_ShouldReturnAllInvalid_WhenPredicateAlwaysFalse()
    {
        // Given
        var numbers = new List<int> { 1, 2, 3 };
        Func<int, bool> alwaysFalse = n => false;
        var failureMessage = "Always fails.";

        // When
        var results = BatchValidator.ValidateBatch(numbers, alwaysFalse, failureMessage).ToList();

        // Then
        results.Should().NotBeNullOrEmpty();
        results.Should().OnlyContain(r => !r.IsValid);
        results.Should().AllSatisfy(r => r.ErrorMessage.Should().Be(failureMessage));
    }

    [Fact]
    public void ValidateBatch_ShouldUseCustomFailureMessage_WhenProvided()
    {
        // Given
        var numbers = new List<int> { 1 };
        Func<int, bool> alwaysFalse = n => false;
        var customMessage = "This item is definitively wrong!";

        // When
        var results = BatchValidator.ValidateBatch(numbers, alwaysFalse, customMessage).ToList();

        // Then
        results.Should().HaveCount(1);
        results[0].IsValid.Should().BeFalse();
        results[0].ErrorMessage.Should().Be(customMessage);
    }

    [Fact]
    public void ValidateBatch_ShouldUseDefaultFailureMessage_WhenNotProvided()
    {
        // Given
        var numbers = new List<int> { 1 };
        Func<int, bool> alwaysFalse = n => false;
        var expectedDefaultMessage = "Item failed validation."; // As per method signature

        // When
        var results = BatchValidator.ValidateBatch(numbers, alwaysFalse).ToList(); // Omit defaultFailureMessage

        // Then
        results.Should().HaveCount(1);
        results[0].IsValid.Should().BeFalse();
        results[0].ErrorMessage.Should().Be(expectedDefaultMessage);
    }

    [Fact]
    public void ValidateBatch_ShouldHaveNullErrorMessage_WhenFailureMessageIsNullAndItemFails()
    {
        // Given
        var numbers = new List<int> { 1 };
        Func<int, bool> alwaysFalse = n => false;
        string? nullMessage = null;

        // When
        var results = BatchValidator.ValidateBatch(numbers, alwaysFalse, nullMessage).ToList();

        // Then
        results.Should().HaveCount(1);
        results[0].IsValid.Should().BeFalse();
        results[0].ErrorMessage.Should().BeNull();
    }

    [Fact]
    public void ValidateBatch_ShouldReturnResultsWithCorrectCount_MatchingInput()
    {
        // Given
        var numbers = Enumerable.Range(1, 10).ToList();
        Func<int, bool> isEven = n => n % 2 == 0;

        // When
        var results = BatchValidator.ValidateBatch(numbers, isEven).ToList();

        // Then
        results.Should().HaveCount(numbers.Count);
    }

    [Fact]
    public void ValidateBatch_ShouldPreserveOriginalItemInstances_ForReferenceTypes()
    {
        // Given
        var p1 = new Person("Eve", 40);
        var p2 = new Person("Frank", 50);
        var people = new List<Person> { p1, p2 };
        Func<Person, bool> alwaysTrue = p => true;

        // When
        var results = BatchValidator.ValidateBatch(people, alwaysTrue).ToList();

        // Then
        results.Should().HaveCount(2);
        results[0].Item.Should().BeSameAs(p1); // Check if it's the *same* instance
        results[1].Item.Should().BeSameAs(p2);
    }

    [Fact]
    public void ValidateBatch_ShouldPreserveOriginalItemValues_ForValueTypes()
    {
        // Given
        var numbers = new List<int> { 10, 20, 30 };
        Func<int, bool> alwaysTrue = n => true;

        // When
        var results = BatchValidator.ValidateBatch(numbers, alwaysTrue).ToList();

        // Then
        results.Should().HaveCount(3);
        results[0].Item.Should().Be(10); // Check value equality
        results[1].Item.Should().Be(20);
        results[2].Item.Should().Be(30);
    }

    [Fact]
    public void ValidateBatch_ShouldPreserveOrderOfItemsInResults()
    {
        // Given
        var numbers = new List<int> { 5, 2, 8, 1 }; // Deliberately unsorted
        Func<int, bool> isEven = n => n % 2 == 0;

        // When
        var results = BatchValidator.ValidateBatch(numbers, isEven).ToList();

        // Then
        results.Should().HaveCount(4);
        results[0].Item.Should().Be(5);
        results[1].Item.Should().Be(2);
        results[2].Item.Should().Be(8);
        results[3].Item.Should().Be(1);
    }
}
