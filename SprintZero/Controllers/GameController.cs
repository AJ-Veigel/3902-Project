using Microsoft.Xna.Framework.Input;
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
        }
    }
}