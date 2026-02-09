//Packages that are being used
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
    if ((current.IsKeyDown(Keys.D0) && previousState.IsKeyUp(Keys.D0))||(current.IsKeyDown(Keys.NumPad0) && previousState.IsKeyUp(Keys.NumPad0)))
        {
            game.Exit();
        }
    if ((current.IsKeyDown(Keys.D1) && previousState.IsKeyUp(Keys.D1))||(current.IsKeyDown(Keys.NumPad1) && previousState.IsKeyUp(Keys.NumPad1)))
        {
            game.SetSprite(0);
        }
     if   ((current.IsKeyDown(Keys.D2) && previousState.IsKeyUp(Keys.D2))||(current.IsKeyDown(Keys.NumPad2) && previousState.IsKeyUp(Keys.NumPad2))){
            game.SetSprite(1);
        }
     if   ((current.IsKeyDown(Keys.D3) && previousState.IsKeyUp(Keys.D3))||(current.IsKeyDown(Keys.NumPad3) && previousState.IsKeyUp(Keys.NumPad3))){
            game.SetSprite(2);
        }
    if   ((current.IsKeyDown(Keys.D4) && previousState.IsKeyUp(Keys.D4))||(current.IsKeyDown(Keys.NumPad4) && previousState.IsKeyUp(Keys.NumPad4))){
            game.SetSprite(3);
        }
        previousState = current;
    }
}
