﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    abstract class BasePlayerAnimation
    {
        protected readonly Vector2 gravity = new Vector2(0, 9.8f);

        private SpriteSpec spriteSpec;
        protected Direction direction;
        protected AnimationCycleSpec[] cycles;

        protected bool loopAnimation;
        protected bool loopFinished;

        protected AnimationCycleSpec currentCycle;
        protected AnimationFrameSpec currentFrame;
        protected int currentFrameIndex;
        protected float timeSinceLastFrame = 0;
        protected Vector2 velocity;

        protected Player player;

        public BasePlayerAnimation(Player player, SpriteSpec spriteSpec, AnimationSpec animation)
        {
            this.player = player;
            this.spriteSpec = spriteSpec;
            this.loopFinished = false;
            this.loopAnimation = animation.loop;
            this.cycles = animation.cycles;
            this.direction = Direction.RIGHT;
            this.currentCycle = cycles[0];
            this.currentFrame = currentCycle.frames[0];
            this.currentFrameIndex = currentCycle.sf;
        }

        public abstract bool Update(GameTime gameTime);

        public abstract void reset();

        public abstract bool hasMovement();

        public abstract void updateOnAction(PlayerState pState, PlayerAction pAction);

        public void updateCycle(AnimationCycleSpec cycle)
        {
            if (cycle != currentCycle)
            {
                currentCycle = cycle;
                if (currentFrameIndex > currentCycle.ef)
                    currentFrameIndex = currentCycle.ef;
                else if (currentFrameIndex < currentCycle.sf)
                    currentFrameIndex = currentCycle.sf;
            }
        }

        public bool isLoopFinished()
        {
            return loopFinished;
        }
        

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            spriteSpec.Draw(spriteBatch, location, currentCycle.row, currentFrameIndex);
        }
    }


}