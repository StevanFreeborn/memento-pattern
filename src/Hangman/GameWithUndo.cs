namespace Hangman;

public class GameWithUndo(
  string secretWord, 
  int allowedIncorrectGuesses = 6
) : Game(secretWord, allowedIncorrectGuesses)
{
  public GameMemento CreateSetPoint()
  {
    return new()
    {
      GuessedLetters = [.. _guessedLetters],
    };
  }

  public void Restore(GameMemento memento)
  {
    _guessedLetters.Clear();
    _guessedLetters.AddRange(memento.GuessedLetters);
  }
}
