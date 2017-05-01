﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace Game1.enemy
{
    class EnemyFactory
    {
        static List<EnemyWalker> enemyWalkers;

        public EnemyFactory()
        {
            enemyWalkers = new List<EnemyWalker>
                    {
                        new EnemyWalker(){},
                        new EnemyWalker(){},
                        new EnemyWalker(){},
                        new EnemyWalker(){},
                        new EnemyWalker(){}
                    };
        }

        internal static void setEnemyMapping(TmxList<TmxObject> objects, int heightDiff)
        {
            // foreach (TmxObject tmxObj in objects)
            enemyWalkers[0].position = new Vector2((int)objects[0].X, (int)objects[0].Y - heightDiff);
        }

        public void LoadContent(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            Sprite enemySprite = Content.Load<Sprite[]>("spritetest")[1];
            Texture2D enemyTexture = Content.Load<Texture2D>(enemySprite.textureName);

            SpriteSpec spriteSpec = new SpriteSpec(enemyTexture, enemySprite.rows, enemySprite.columns);
            
            WalkerEnemyAnimation walkerAnimation = new WalkerEnemyAnimation(spriteSpec, enemySprite.animations);
            foreach (EnemyWalker enemyWalker in enemyWalkers)
                enemyWalker.setAnimation(walkerAnimation);
        }

        public void Update(GameTime gameTime)
        {
            foreach (EnemyWalker enemyWalker in enemyWalkers)
                enemyWalker.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // foreach (EnemyWalker enemyWalker in enemyWalkers)
            enemyWalkers[0].Draw(spriteBatch);
        }
    }
}
