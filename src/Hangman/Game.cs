namespace Hangman;

public class Game(string secretWord, int allowedIncorrectGuesses = 6)
{
  protected readonly List<char> _guessedLetters = [];

  public string SecretWord { get; } = secretWord.ToUpper();
  public int AllowedIncorrectGuesses { get; } = allowedIncorrectGuesses;
  public int IncorrectGuesses => _guessedLetters.Count(letter => SecretWord.Contains(letter) is false);
  public int RemainingIncorrectGuesses => AllowedIncorrectGuesses - IncorrectGuesses;
  public GameStatus Status { get; private set; } = GameStatus.InProgress;
  public string MaskedWord => string.Concat(SecretWord.Select(letter => _guessedLetters.Contains(letter) ? letter : '_'));

  public GuessResult MakeGuess(char letter)
  {
    var result = GetResult(letter);
    Status = GetGameStatus();
    return result;
  }

  private GameStatus GetGameStatus()
  {
    if (IncorrectGuesses >= AllowedIncorrectGuesses)
    {
      return GameStatus.Lost;
    }

    if (SecretWord.All(_guessedLetters.Contains))
    {
      return GameStatus.Won;
    }

    return GameStatus.InProgress;
  }

  private GuessResult GetResult(char letter)
  {
    if (char.IsUpper(letter) is false)
    {
      return GuessResult.Invalid;
    }

    if (_guessedLetters.Contains(letter))
    {
      return GuessResult.Duplicate;
    }

    _guessedLetters.Add(letter);

    if (SecretWord.Contains(letter) is false)
    {
      return GuessResult.Incorrect;
    }

    return GuessResult.Correct;
  }
}

public enum GameStatus
{
  InProgress,
  Won,
  Lost
}

public enum GuessResult
{
  Correct,
  Incorrect,
  Invalid,
  Duplicate,
}