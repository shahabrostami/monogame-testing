﻿using Microsoft.Xna.Framework.Graphics;
using MyObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.enemy
{
    class EnemyTypes
    {
        private static EnemyFactory enemyFactory;

        public static void LoadContent(EnemyFactory enemyFactory, Texture2D texture, Sprite sprite)
        {
            SpriteSpec spriteSpec = new SpriteSpec(texture, sprite.offset, sprite.rows, sprite.columns);
        }
    }
}
