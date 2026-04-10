using System.Collections.Generic;
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
    }
}