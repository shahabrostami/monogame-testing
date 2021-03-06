﻿using Game1.player.bullet;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class BulletFactory
    {
        Texture2D bulletTexture;
        protected Player player;
        protected float timeSinceLastFrame = 500;
        protected float delay = 100;
        protected static float screenWidth;
        protected List<Bullet> bullets = new List<Bullet>();

        public BulletFactory(Player player)
        {
            this.player = player;
        }

        public void LoadContent(GraphicsDevice GraphicsDevice)
        {
            bulletTexture = new Texture2D(GraphicsDevice, 1, 1);
            bulletTexture.SetData(new[] { Color.White });
            screenWidth = GraphicsDevice.Viewport.Width / 2 + 20;
        }

        public void Update(bool isShooting, bool isJumping, bool canShoot, GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            // Create new bullets
            if (timeSinceLastFrame > delay) { 

                if (isShooting && canShoot) {
                    bullets.Add(new Bullet(isJumping, (player.position), player.direction));
                    timeSinceLastFrame = 0;
                }
            }
            
            // Update each bullet
            foreach (var bullet in bullets)
                bullet.Update(gameTime);

            // Check collisions
            var bulletsHitWorld = bullets.Where(i => CollisionHandler.checkBulletCollisions((int) i.position.X, (int) i.position.Y)).ToList();
            foreach (var bullet in bulletsHitWorld)
                bullets.Remove(bullet);

            // Check bullet offscreen
            var bulletsOffScreen = bullets.Where(i => IsOffScreen(i)).ToList();
            foreach (var bullet in bulletsOffScreen)
                bullets.Remove(bullet);
        }

        private Boolean IsOffScreen(Bullet bullet)
        {
            // Bullets only travel horizontally
            if (bullet.position.X <= (player.position.X - screenWidth) || bullet.position.X >= (player.position.X + screenWidth))
                return true;
            return false;
        }

        public string GetDebugInfo()
        {
            return "Bullets: " + bullets.Count;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var bullet in bullets)
                bullet.Draw(spriteBatch, bulletTexture);
        }
    }
}
