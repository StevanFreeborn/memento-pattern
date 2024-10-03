namespace Hangman.Tests;

public class GameTests
{
  [Fact]
  public void ItShouldAcceptASecretWordAndStoreItInAllCaps()
  {
    var game = new Game("Hello");

    var secretWord = game.SecretWord;

    Assert.Equal("HELLO", secretWord);
    Assert.Equal(6, game.AllowedIncorrectGuesses);
  }

  [Fact]
  public void ItShouldAcceptAnAllowedIncorrectGuessesValue()
  {
    var game = new Game("Hello", 10);

    var allowedIncorrectGuesses = game.AllowedIncorrectGuesses;

    Assert.Equal(10, allowedIncorrectGuesses);
  }

  [Fact]
  public void ItShouldStoreTheNumberOfIncorrectGuesses()
  {
    var game = new Game("Hello");

    var incorrectGuesses = game.IncorrectGuesses;

    Assert.Equal(0, incorrectGuesses);
  }

  [Fact]
  public void ItShouldHaveAnInProgressStatusWhenStarted()
  {
    var game = new Game("Hello");

    var status = game.Status;

    Assert.Equal(GameStatus.InProgress, status);
  }

  [Fact]
  public void ItShouldAllowUserToMakeAGuessAndReturnResult()
  {
    var game = new Game("Hello");

    var result = game.MakeGuess('A');

    Assert.Equal(GuessResult.Incorrect, result);
  }

  [Theory]
  [InlineData('1')]
  [InlineData('!')]
  [InlineData(' ')]
  public void ItShouldReturnInvalidGuessResultIfLetterIsNotAtoZ(char guess)
  {
    var game = new Game("Hello");

    var result = game.MakeGuess(guess);

    Assert.Equal(GuessResult.Invalid, result);
  }
  
  [Fact]
  public void ItShouldIncrementIncorrectGuessesWhenIncorrectGuessIsMade()
  {
    var game = new Game("Hello");

    var result = game.MakeGuess('A');

    Assert.Equal(GuessResult.Incorrect, result);
    Assert.Equal(1, game.IncorrectGuesses);
  }

  [Fact]
  public void ItShouldNotIncrementGuessesWhenDuplicateGuessIsMade()
  {
    var game = new Game("Hello");

    game.MakeGuess('A');
    var result = game.MakeGuess('A');

    Assert.Equal(GuessResult.Duplicate, result);
    Assert.Equal(1, game.IncorrectGuesses);
  }

  [Fact]
  public void ItShouldHaveInProgressStatusWhenIncorrectGuessesRemain()
  {
    var game = new Game("Hello");

    game.MakeGuess('A');

    Assert.Equal(GameStatus.InProgress, game.Status);
  }

  [Fact]
  public void ItShouldHaveLostStatusWhenIncorrectGuessesExceedAllowed()
  {
    var game = new Game("Hello", 1);

    game.MakeGuess('A');
    game.MakeGuess('B');

    Assert.Equal(GameStatus.Lost, game.Status);
  }

  [Fact]
  public void ItShouldHaveWonStatusWhenAllLettersAreGuessed()
  {
    var game = new Game("Hello");

    game.MakeGuess('H');
    game.MakeGuess('E');
    game.MakeGuess('L');
    game.MakeGuess('O');

    Assert.Equal(GameStatus.Won, game.Status);
  }

  [Theory]
  [InlineData('A', "_____")]
  [InlineData('H', "H____")]
  [InlineData('E', "_E___")]
  [InlineData('L', "__LL_")]
  public void ItShouldHaveAMaskedWordWithUnderscoresForUnGuessedLetters(char guess, string expectedMaskedWord)
  {
    var game = new Game("Hello");

    game.MakeGuess(guess);

    Assert.Equal(expectedMaskedWord, game.MaskedWord);
  }
}