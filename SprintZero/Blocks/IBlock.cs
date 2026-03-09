using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteZero.Marios;

namespace SpriteZero.blocks
{
    public enum CollisionSide {None, Top,Bottom,Left,Right}
    public interface IBlock
    {
        
        Vector2 location {get;set;}
        Rectangle Collider {get; set;}
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);

        void onHit(IMario mario, CollisionSide side);
    }
}