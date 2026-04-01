using System;
using System.ComponentModel;
using System.Net.Mime;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SprintZero;


public class GameController
{

    private Game1 game; 
    private KeyboardState previousState, currentState;

    public GameController(KeyboardState prev, KeyboardState curr, Game1 game)
    {
        previousState = prev;
        currentState = curr;
        this.game = game;
       
    }
    public void UpdateGame()
    {
        if (currentState.IsKeyDown(Keys.Q) && previousState.IsKeyUp(Keys.Q))
        {
            game.Exit();
        }
        if (currentState.IsKeyDown(Keys.R) && previousState.IsKeyUp(Keys.R))
        {
            game.Reset();

        } if(currentState.IsKeyDown(Keys.M) && previousState.IsKeyUp(Keys.M))
        {
            MediaPlayer.Pause();
        }
        if (currentState.IsKeyDown(Keys.N) && previousState.IsKeyUp(Keys.N))
        {
            game.play();
        } 
       
    }
}