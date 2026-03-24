using Microsoft.Xna.Framework.Input;
using SprintZero;

public class MovementController
{

    private Game1 game;
    private KeyboardState previousState, currentState;

    public MovementController(KeyboardState prev, KeyboardState curr, Game1 game)
    {
        previousState = prev;
        currentState = curr;
        this.game = game;
    }
    public void UpdateMovement()
    {
        // Jump
        if ((currentState.IsKeyDown(Keys.W) && previousState.IsKeyUp(Keys.W)) || (currentState.IsKeyDown(Keys.Up) && previousState.IsKeyUp(Keys.Up)))
        {
            game.MarioJump();
        }

        // Crouch
        if ((currentState.IsKeyDown(Keys.S) && previousState.IsKeyUp(Keys.S)) || (currentState.IsKeyDown(Keys.Down) && previousState.IsKeyUp(Keys.Down)))
        {
            game.MarioCrouch();
        }

        // Stop Crouching
        if ((currentState.IsKeyUp(Keys.S) && previousState.IsKeyDown(Keys.S)) || (currentState.IsKeyUp(Keys.Down) && previousState.IsKeyDown(Keys.Down)))
        {
            game.MarioUncrouch();
        }

        // Right
        if (currentState.IsKeyDown(Keys.D) || currentState.IsKeyDown(Keys.Right))
        {
            game.MarioRight();
        }

        // Stop Right
        if ((currentState.IsKeyUp(Keys.D) && previousState.IsKeyDown(Keys.D)) || (currentState.IsKeyUp(Keys.Right) && previousState.IsKeyDown(Keys.Right)))
        {
            game.StopMarioRight();
        }

        // Left
        if (currentState.IsKeyDown(Keys.A) || currentState.IsKeyDown(Keys.Left))
        {
            game.MarioLeft();
        }

        // Stop Left
        if ((currentState.IsKeyUp(Keys.A) && previousState.IsKeyDown(Keys.A)) || (currentState.IsKeyUp(Keys.Left) && previousState.IsKeyDown(Keys.Left)))
        {
            game.StopMarioLeft();
        }
    }
}