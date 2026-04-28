using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;

namespace SprintZero.Items
{
    public class SpawnItem
    {
        public static void SpawnMushroom(TextureAtlas itemTexture, List<ICollectable> items, Vector2 location)
        {
            TextureRegion mushSprite = itemTexture.GetRegion("mushroom");
            ICollectable mushroom = new Mushroom(mushSprite, location);
            items.Add(mushroom);
        }
        public static void SpawnFlower(TextureAtlas itemTexture, List<ICollectable> items, Vector2 location)
        {
            AnimatedSprite flowerSprite = itemTexture.CreateAnimatedSprite("flower");
            ICollectable flower = new Flower(flowerSprite, location);
            items.Add(flower);
        }
        public static void SpawnCoin(TextureAtlas itemTexture, List<ICollectable> items, Vector2 location, bool stationary)
        {
            AnimatedSprite coinSprite = itemTexture.CreateAnimatedSprite("coin");
            ICollectable coin = new Coin(coinSprite, location, stationary);
            items.Add(coin);
        }
        public static void SpawnStar(TextureAtlas itemTexture, List<ICollectable> items, Vector2 location)
        {
            AnimatedSprite starSprite = itemTexture.CreateAnimatedSprite("star");
            ICollectable star = new Star(starSprite, location);
            items.Add(star);
        }

        public static void SpawnOneUp(TextureAtlas itemTexture, List<ICollectable> items, Vector2 location)
        {
            TextureRegion oneUpSprite = itemTexture.GetRegion("one_up");
            ICollectable oneUp = new OneUp(oneUpSprite, location);
            items.Add(oneUp);
        }
    }
}