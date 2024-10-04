using Hangman;

var game = new Game("HANGMAN");

while(game.Status is GameStatus.InProgress)
{
  Console.WriteLine($"Word: {game.MaskedWord}");
  Console.WriteLine($"Remaining guesses: {game.RemainingGuesses}");
  Console.Write("Guess: ");
  var input = Console.ReadLine();

  if (input is null)
  {
    Console.WriteLine("Invalid guess!");
    continue;
  }

  var guess = input.ToUpper()[0];
  var result = game.MakeGuess(guess);
  
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
  _ => throw new NotImplementedException()
});