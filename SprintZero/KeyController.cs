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
        if ((current.IsKeyDown(Keys.R) && previousState.IsKeyUp(Keys.R)))
        {
            game.Reset();
        }
        if ((current.IsKeyDown(Keys.E) && previousState.IsKeyUp(Keys.E)))
        {
            game.Damage();
        }
        if((current.IsKeyDown(Keys.D1) && previousState.IsKeyUp(Keys.D1)) || (current.IsKeyDown(Keys.NumPad1) && previousState.IsKeyUp(Keys.NumPad1)))
        {
            int smallMario = 0;
            game.SetMario(smallMario);
        }
        if((current.IsKeyDown(Keys.D2) && previousState.IsKeyUp(Keys.D2)) || (current.IsKeyDown(Keys.NumPad2) && previousState.IsKeyUp(Keys.NumPad2)))
        {
            int bigMario = 1;
            game.SetMario(bigMario);
        }
        if((current.IsKeyDown(Keys.D3) && previousState.IsKeyUp(Keys.D3)) ||  (current.IsKeyDown(Keys.NumPad3) && previousState.IsKeyUp(Keys.NumPad3)))
        {
            int fireMario = 2;
            game.SetMario(fireMario);
        }
        if(current.IsKeyDown(Keys.Space) && previousState.IsKeyUp(Keys.Space))
        {
            game.MarioFire();
        }
        if((current.IsKeyDown(Keys.W) && previousState.IsKeyUp(Keys.W)) || (current.IsKeyDown(Keys.Up) && previousState.IsKeyUp(Keys.Up)))
        {
            game.MarioJump();
        }
        if(current.IsKeyDown(Keys.D) || current.IsKeyDown(Keys.Right))
        {
            game.MarioRight();
        }
        if((current.IsKeyUp(Keys.D) && previousState.IsKeyDown(Keys.D)) || (current.IsKeyUp(Keys.Right) && previousState.IsKeyDown(Keys.Right)))
        {
            game.StopMarioRight();
        }
        if(current.IsKeyDown(Keys.A) || current.IsKeyDown(Keys.Left))
        {
            game.MarioLeft();
        }
        if((current.IsKeyUp(Keys.A) && previousState.IsKeyDown(Keys.A)) || (current.IsKeyUp(Keys.Left) && previousState.IsKeyDown(Keys.Left)))
        {
            game.StopMarioLeft();
        }
        previousState = current;
    }
}
