using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dice3D.Variables
{
    public class PhysicsVariable : MonoBehaviour
    {
        public enum OneBounceFlip
        {
            probablity = 7,
            velocity = -20,
            angularDrag = 2,
            angleForceMin = 5,
            angleForceMax = 25
        }

        public enum OneBounceNoFlip
        {
            probablity = 3,
            velocity = -25,
            angularDrag = 25,
            angleForceMin = 15,
            angleForceMax = 25
        }
        public enum DiceVelocity
        {
            oneBounceFlip = -20,
            oneBounce = -25
        }

        public enum DiceAnuglarDrag
        {
            oneBounceFlip = 2,
            oneBounce = 25
        }

        public enum DiceMass
        {
            oneBounceFlip = 1000,
            oneBounce = 2000
        }

        public enum DiceFriction
        {
            oneBounceFlip = 200,
            oneBounce = 400
        }

        public enum DiceAngle
        {
            oneBounceFlipMin = 15,
            oneBounceFlipMax = 25,
            oneBounceMin = 5,
            oneBounceMax = 25,
        }
    }
}