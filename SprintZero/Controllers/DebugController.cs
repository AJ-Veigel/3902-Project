using System.Data;
using Microsoft.Xna.Framework.Input;
using SprintZero;

public class DebugController
{

    private Game1 game;
    private KeyboardState previousState, currentState;

    public DebugController(KeyboardState prev, KeyboardState curr, Game1 game)
    {
        previousState = prev;
        currentState = curr;
        this.game = game;
    }
    public void UpdateDebug()
    {
        UpdateMarioDebug(previousState, currentState, game);
        UpdateItemDebug(previousState, currentState, game);
        //UpdateEnemyDebug(previousState, currentState, game);
    }

    public static void UpdateMarioDebug(KeyboardState previousState, KeyboardState currentState, Game1 game)
    {
        // Make Mario Take Damage (only in debug mode)
        if (currentState.IsKeyDown(Keys.E) && previousState.IsKeyUp(Keys.E))
        {
            game.Damage();
        }

        // Set mario to be in small state (only in debug mode)
        if ((currentState.IsKeyDown(Keys.D1) && previousState.IsKeyUp(Keys.D1)) || (currentState.IsKeyDown(Keys.NumPad1) && previousState.IsKeyUp(Keys.NumPad1)))
        {
            int smallMario = 0;
            game.SetMario(smallMario);
        }

        // Set mario to be in big state (only in debug mode)
        if ((currentState.IsKeyDown(Keys.D2) && previousState.IsKeyUp(Keys.D2)) || (currentState.IsKeyDown(Keys.NumPad2) && previousState.IsKeyUp(Keys.NumPad2)))
        {
            int bigMario = 1;
            game.SetMario(bigMario);
        }

        // Set mario to be in fire state (only in debug mode)
        if ((currentState.IsKeyDown(Keys.D3) && previousState.IsKeyUp(Keys.D3)) || (currentState.IsKeyDown(Keys.NumPad3) && previousState.IsKeyUp(Keys.NumPad3)))
        {
            int fireMario = 2;
            game.SetMario(fireMario);
        }
    }

    public static void UpdateItemDebug(KeyboardState previousState, KeyboardState currentState, Game1 game)
    {

        if (currentState.IsKeyDown(Keys.T) && previousState.IsKeyUp(Keys.T))
        {
            game.PreviousBlock();
        }


        if (currentState.IsKeyDown(Keys.Y) && previousState.IsKeyUp(Keys.Y))
        {
            game.NextBlock();
        }

        // Change to "Spawn Item"
        if (currentState.IsKeyDown(Keys.U) && previousState.IsKeyUp(Keys.U))
        {
            game.PreviousItem();
        }

        // Change to "Swap Item"
        if (currentState.IsKeyDown(Keys.I) && previousState.IsKeyUp(Keys.I))
        {
            game.NextItem();
        }
    }


    /*public static void UpdateEnemyDebug(KeyboardState previousState, KeyboardState currentState, Game1 game)
    {
        // Change to "Spawn Enemy"
        if (currentState.IsKeyDown(Keys.O) && previousState.IsKeyUp(Keys.O))
        {
            game.previousEnemy();
        }

        // Change to "Swap Enemy"
        if (currentState.IsKeyDown(Keys.P) && previousState.IsKeyUp(Keys.P))
        {
            game.nextEnemy();
        }
    } */
}