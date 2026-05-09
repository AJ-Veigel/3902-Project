using Microsoft.Xna.Framework.Input;
using SprintZero;

public class ProjectileController
{

    private Game1 game;
    private KeyboardState previousState, currentState;

    public ProjectileController(KeyboardState prev, KeyboardState curr, Game1 game)
    {
        previousState = prev;
        currentState = curr;
        this.game = game;
    }
    public void UpdateProjectiles()
    {
        if (currentState.IsKeyDown(Keys.Space) && previousState.IsKeyUp(Keys.Space))
        {
            if (game.currentMarioNum == 2)
            {
                if (game.IsPaused || game.currentMario.WinState) return;
                game.currentMario.Fireball();
                game.SpawnFireball();
            }
        }
    }
}