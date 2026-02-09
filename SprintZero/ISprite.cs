
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteZero.Sprites
{
    public interface ISprite
    {

       Vector2 location {get;set;}
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}