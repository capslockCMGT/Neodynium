﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Physics;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Bucket : PhysicsMesh
    {
        static List<Box> hitboxes = new List<Box>();
        public static void AddHitbox(Box box)
        { hitboxes.Add(box); }
        public static void RemoveHitbox(Box box)
        { hitboxes.Remove(box); }
        bool filledWithWater;
        ParticleSystem water;
        public Bucket(string modelName, string textureName) : base(modelName, textureName, Vector3.zero)
        {
            renderAs.scale = .8f;
            renderAs.y -= 1f;
            water = new ParticleSystem("neodymium/bucket/water drip.png", 0, 0, 0, mode: ParticleSystem.Mode.force);
            AddChild(water);
            water.enabled = false;
            water.forces.Add(new ParticleSystem.GravityForce(Vector3.down*5));
            water.startSize = .001f;
            water.endSize = .001f;
        }

        void Update()
        {
            water.position = pos;
            foreach (Box box in hitboxes)
            {
                if (!box.collider.HitTest(collider)) continue;
                if (box is WaterHitbox && !filledWithWater)
                {
                    filledWithWater = true;
                    water.enabled = true;
                    Console.WriteLine("bucket is filled (:");
                }
                if (box is LavaHitbox && filledWithWater)
                {
                    (box as LavaHitbox).TurnIntoObsidian();
                    filledWithWater = false;
                    water.enabled = false;
                    Console.WriteLine("bucket is unfilled ):");
                }
            }
        }
    }
}