using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Scoreboard : IScoreboard
{
    public int MaxEntriesToKeep { get; set; }
    private Dictionary<string, string> registerUser;
    private Dictionary<string, string> registerGame;
    private Dictionary<string, OrderedBag<ScoreboardEntry>> scoreBoard;
    public Scoreboard(int maxEntriesToKeep = 10)
    {
        this.registerUser = new Dictionary<string, string>();
        this.registerGame = new Dictionary<string, string>();
        this.scoreBoard = new Dictionary<string, OrderedBag<ScoreboardEntry>>();
        this.MaxEntriesToKeep = maxEntriesToKeep;

    }

    public bool RegisterUser(string username, string password)
    {
        if (!registerUser.ContainsKey(username))
        {
            registerUser[username] = password;
            return true;
        }
        return false;
    }

    public bool RegisterGame(string game, string password)
    {
        if (!registerGame.ContainsKey(game))
        {
            registerUser[game] = password;
            scoreBoard[game] = new OrderedBag<ScoreboardEntry>();
            return true;
        }
        return false;
    }

    public bool AddScore(string username, string userPassword, string game, string gamePassword, int score)
    {
        if (scoreBoard.ContainsKey(game) && registerGame[game] == gamePassword && registerUser.ContainsKey(username) && registerUser[username] == userPassword)
        {
            scoreBoard[game].Add(new ScoreboardEntry() { Username = username, Score = score});
            return true;
        }
        return false;
    }

    public IEnumerable<ScoreboardEntry> ShowScoreboard(string game)
    {
        return scoreBoard[game].Take(MaxEntriesToKeep);
    }

    public bool DeleteGame(string game, string gamePassword)
    {
        if (registerGame.ContainsKey(game) && registerGame[game] != gamePassword) 
        {
            registerGame.Remove(game);
            scoreBoard.Remove(game);
            return true;
        }
        return false;
    }

    public IEnumerable<string> ListGamesByPrefix(string gameNamePrefix)
    {
        throw new NotImplementedException();
    }
}