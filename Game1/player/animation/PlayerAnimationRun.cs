﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MyObjects;

namespace Game1
{
    class PlayerAnimationRun : BasePlayerAnimation
    {
        private int ms;

        public PlayerAnimationRun(Player player, SpriteSpec spriteSpec, AnimationSpec animationSpec) :
            base(player, spriteSpec, animationSpec)
        {
            cycles[1].frames = cycles[0].frames;
            cycles[1].velocity = new Vector2(cycles[0].velocity.X * -1, cycles[0].velocity.Y);
            cycles[2].frames = cycles[0].frames;
            cycles[3].frames = cycles[1].frames;

            ms = animationSpec.cycles[0].ms;
        }

        override public bool Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            
            if (timeSinceLastFrame > ms)
            {
                timeSinceLastFrame -= ms;

                currentFrameIndex++;

                if (currentFrameIndex > currentCycle.ef)
                    currentFrameIndex = currentCycle.sf;

                currentFrame = currentCycle.frames[currentCycle.ef % currentFrameIndex];
            }

            player.updateLocation(currentCycle.velocity * (timeSinceLastFrame/1000));
            return true;
        }

        public override void updateOnAction(PlayerStateAnimation pState, Action pAction)
        {
            if (player.direction == Direction.RIGHT)
            {
                if (player.shooting)
                    updateCycle(cycles[2]);
                else updateCycle(cycles[0]);
            }
            else if (player.direction == Direction.LEFT)
            {
                if (player.shooting)
                    updateCycle(cycles[3]);
                else updateCycle(cycles[1]);
            }
        }
           
        public override bool hasMovement()
        {
            return true;
        }

        public override void reset()
        {

        }
    }
}
