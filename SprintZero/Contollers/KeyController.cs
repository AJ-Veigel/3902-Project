using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SprintZero;
using SprintZero.Controllers;

public class KeyController : IController
{
    private Game1 game;
    private KeyboardState previousState;

    public KeyController(Game1 game)
    {
        this.game = game;
        previousState = Keyboard.GetState();
    }
    public void Update(GameTime gameTime)
    {
        KeyboardState current = Keyboard.GetState();
        MovementController movement = new MovementController(previousState, current, game);
        DebugController debug = new DebugController(previousState, current, game);
        ProjectileController projectile = new ProjectileController(previousState, current, game);
        GameController gameCon = new GameController(previousState, current, game);

        // updates the movements of Mario
        movement.UpdateMovement();

        // updates the debug functionality, spawing an item, spawning an enemy etc. Remove later
        debug.UpdateDebug();

        // updates the Game (exits, resets etc.)
        gameCon.UpdateGame();

        // updates mario Projectiles
        projectile.UpdateProjectiles();

        previousState = current;
    }
}
