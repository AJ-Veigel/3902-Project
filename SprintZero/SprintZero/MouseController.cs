//the packages being used
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SprintZero;
using SprintZero.Controllers;

public class MouseController : IController
{
    //this is for setting everything up 
    private Game1 game;
    private MouseState previous;
    public MouseController(Game1 game)
    {
        this.game = game;

    }

    public void Update(GameTime gameTime)
    {
        MouseState current = Mouse.GetState();
        if(current.RightButton == ButtonState.Pressed)
        game.Exit();
        if (current.LeftButton == ButtonState.Pressed && previous.LeftButton == ButtonState.Released)
        {
            int x = current.X;
            int y = current.Y;
            if (x <640 && y<360 )
                game.SetSprite(0);
            else if (x>=640 && y <360)
                game.SetSprite(1);
            else if (x<640 && y >=360)
                game.SetSprite(2);
            else 
                game.SetSprite(3);
        }

        previous = current;
    }
}