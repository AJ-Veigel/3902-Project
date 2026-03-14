using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SprintZero;
using SprintZero.Controllers;

public class MouseController : IController
{
    private Game1 game;
    private MouseState previousState;
    public MouseController(Game1 game)
    {
        this.game = game;
        previousState = Mouse.GetState();
    }
    public void Update(GameTime gametime)
    {
        MouseState currentState = Mouse.GetState();
        Point mousePosition = currentState.Position;

        // Top Left
        if(mousePosition.X <= 910 && mousePosition.Y <= 540 && currentState.LeftButton.Equals(ButtonState.Pressed) && previousState.LeftButton.Equals(ButtonState.Released))
        {
        }
        // Bottom Left
        else if(mousePosition.X >= 910 && mousePosition.Y <= 540 && currentState.LeftButton.Equals(ButtonState.Pressed) && previousState.LeftButton.Equals(ButtonState.Released))
        {
            
        }
        // Top Right
        else if(mousePosition.X <= 910 && mousePosition.Y >= 540 && currentState.LeftButton.Equals(ButtonState.Pressed) && previousState.LeftButton.Equals(ButtonState.Released))
        {
            
        }
        // Bottom Right
        else if(mousePosition.X >= 910 && mousePosition.Y >= 540 && currentState.LeftButton.Equals(ButtonState.Pressed) && previousState.LeftButton.Equals(ButtonState.Released))
        {
            
        }
    }
}