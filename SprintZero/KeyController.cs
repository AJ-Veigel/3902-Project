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
    if ((current.IsKeyDown(Keys.Q)) && previousState.IsKeyUp(Keys.Q))
        {
            game.Exit();
        }
    if ((current.IsKeyDown(Keys.T) && previousState.IsKeyUp(Keys.T)))
        {
            game.PreviousBlock();
        }
    if ((current.IsKeyDown(Keys.Y) && previousState.IsKeyUp(Keys.Y)))
        {
            game.NextBlock();
        }
    if ((current.IsKeyDown(Keys.U) && previousState.IsKeyUp(Keys.U)))
        {
            game.PreviousItem();
        }
    if ((current.IsKeyDown(Keys.I) && previousState.IsKeyUp(Keys.I)))
        {
            game.NextItem();
        }
        previousState = current;
    }
}
