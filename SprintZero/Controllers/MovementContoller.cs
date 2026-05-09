using Microsoft.Xna.Framework.Input;
using SprintZero;
using SprintZero.Marios;

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
            if (game.IsPaused || game.currentMario.WinState) return;
            game.currentMario.Jump();
        }

        // Crouch
        if ((currentState.IsKeyDown(Keys.S) && previousState.IsKeyUp(Keys.S)) || (currentState.IsKeyDown(Keys.Down) && previousState.IsKeyUp(Keys.Down)))
        {
            if (game.IsPaused || game.currentMario.WinState) return;
            game.currentMario.Crouching = true;
            game.currentMario.Crouch();
        }

        // Stop Crouching
        if ((currentState.IsKeyUp(Keys.S) && previousState.IsKeyDown(Keys.S)) || (currentState.IsKeyUp(Keys.Down) && previousState.IsKeyDown(Keys.Down)))
        {
            if (game.IsPaused || game.currentMario.WinState) return;
            game.currentMario.Crouching = false;
            game.currentMario.Crouch();
        }

        // Right
        if (currentState.IsKeyDown(Keys.D) || currentState.IsKeyDown(Keys.Right))
        {
            if (game.IsPaused || game.currentMario.WinState) return;
            game.currentMario.Direction = true;
            game.currentMario.Move();
        }

        // Stop Right
        if ((currentState.IsKeyUp(Keys.D) && previousState.IsKeyDown(Keys.D)) || (currentState.IsKeyUp(Keys.Right) && previousState.IsKeyDown(Keys.Right)))
        {
            if (game.IsPaused || game.currentMario.WinState) return;
            game.currentMario.Direction = true;
            game.currentMario.StopMove();
        }

        // Left
        if (currentState.IsKeyDown(Keys.A) || currentState.IsKeyDown(Keys.Left))
        {
            if (game.IsPaused || game.currentMario.WinState) return;
            game.currentMario.Direction = false;
            game.currentMario.Move();
        }

        // Stop Left
        if ((currentState.IsKeyUp(Keys.A) && previousState.IsKeyDown(Keys.A)) || (currentState.IsKeyUp(Keys.Left) && previousState.IsKeyDown(Keys.Left)))
        {
            if (game.IsPaused || game.currentMario.WinState) return;
            game.currentMario.Direction = false;
            game.currentMario.StopMove();
        }
    }
}