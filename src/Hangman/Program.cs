using Hangman;

var game = new GameWithUndo("HANGMAN");
var history = new Stack<GameMemento>();
history.Push(game.CreateSetPoint());

while(game.Status is GameStatus.InProgress)
{
  Console.WriteLine($"Word: {game.MaskedWord}");
  Console.WriteLine($"Remaining guesses: {game.RemainingIncorrectGuesses}");
  Console.Write("Guess (A-Z or '-' to undo or 'quit' to exit): ");
  var input = Console.ReadLine();

  if (input is null)
  {
    Console.WriteLine("Invalid guess!");
    continue;
  }

  if (input is "quit")
  {
    Console.WriteLine("Quitting game...");
    break;
  }

  if (input is "-" && history.Count > 1)
  {
    history.Pop();
    game.Restore(history.Peek());
    continue;
  }

  var guess = input.ToUpper()[0];
  var result = game.MakeGuess(guess);
  history.Push(game.CreateSetPoint());
  
  Console.WriteLine(result switch
  {
    GuessResult.Correct => "Correct!",
    GuessResult.Duplicate => "You've already guessed that letter!",
    GuessResult.Incorrect => "Incorrect!",
    GuessResult.Invalid => "Invalid guess!",
    _ => throw new NotImplementedException()
  });

}

Console.WriteLine(game.Status switch
{
  GameStatus.Won => $"Congratulations! You've won! The word was {game.SecretWord}",
  GameStatus.Lost => $"You've lost! The word was {game.SecretWord}",
  _ => "Game exited"
});