using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Sprites;

public class Mushroom : ISprite
{
   private TextureRegion sprite;
    public Vector2 location {get;set;}
    private float horizontalSpeed = 2f;
    private float verticalSpeed=0f;
    private float gravity = 0.3f;
    private float riseUp = 40f;
    private float startY;
    private bool rising = true;
    public Mushroom(TextureRegion region)
    {
        sprite = region;
        location = new Vector2(300,300);
        startY = location.Y;
    }
    public void Update(GameTime gameTime)
    {
        if (rising)
        {
            location = new Vector2(location.X,location.Y-1f);
            if (startY - location.Y >= riseUp)
            {
                rising=false;
                verticalSpeed = 0f;
            }
        }
        else
        {
            verticalSpeed +=gravity;
            location = new Vector2(location.X,location.Y + verticalSpeed);
        }
        if (location.Y >= 500)
        {
            location = new Vector2(location.X,500);
            verticalSpeed = 0f;
            location = new Vector2(location.X+horizontalSpeed,location.Y);
        }
    }

public void Draw(SpriteBatch spriteBatch)
{
   sprite.Draw(spriteBatch,location,Color.White,0f,Vector2.One,4f,SpriteEffects.None,0f); 
}
}