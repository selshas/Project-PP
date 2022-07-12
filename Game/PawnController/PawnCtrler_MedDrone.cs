using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PP.Game.PawnController
{
    public class PawnCtrler_MedDrone : PawnController
    {
        public Pawn_MedDrone medDrone;
        public float preferDistance = 3.0f;
        public float preferAltitude = 3.0f;

        Behaviour_Heal behaviour_heal;
        Behaviour_Follow behaviour_follow;
        class Behaviour_Heal : IPawnCtrler_Behaviour
        {
            PawnCtrler_MedDrone controller;
            public Behaviour_Heal(PawnCtrler_MedDrone _controller) =>  controller = _controller;
            public void OnBegin()
            {
                controller.medDrone.ActivateHealRay();

            }
            public void OnLoop()
            {
                Vector3 deltaPos_toOwner = (controller.medDrone.pawn_owner.transform.position - controller.transform.position);
                controller.medDrone.faceDirection = System.Math.Sign(deltaPos_toOwner.x);

                controller.AdjustAltitude();
                float xDist = Mathf.Abs(deltaPos_toOwner.x);
                float zDist = Mathf.Abs(deltaPos_toOwner.z);
                if (xDist > controller.preferDistance || zDist > controller.preferDistance)
                    controller.currentBehaviour = controller.behaviour_follow;
                else
                {
                    controller.medDrone.inputVector_moving.x = 0;
                    controller.medDrone.inputVector_moving.z = 0;
                }
            }
            public void OnEnd()
            {
                controller.medDrone.DeactivateHealRay();
            }
        }

        class Behaviour_Follow : IPawnCtrler_Behaviour
        {
            PawnCtrler_MedDrone controller;
            public Behaviour_Follow(PawnCtrler_MedDrone _controller) => controller = _controller;
            public void OnBegin() {}
            public void OnLoop()
            {
                controller.AdjustAltitude();

                Vector3 deltaPos_toOwner = (controller.medDrone.pawn_owner.transform.position - controller.transform.position);
                controller.medDrone.faceDirection = System.Math.Sign(deltaPos_toOwner.x);

                float xDist = Mathf.Abs(deltaPos_toOwner.x);
                float zDist = Mathf.Abs(deltaPos_toOwner.z);
                if (xDist < controller.preferDistance && zDist < controller.preferDistance)
                    controller.currentBehaviour = controller.behaviour_heal;
                else
                {
                    if (xDist > controller.preferDistance)
                        controller.medDrone.inputVector_moving.x = Mathf.Sign(deltaPos_toOwner.x);
                    else controller.medDrone.inputVector_moving.x = 0;

                    if (zDist > controller.preferDistance)
                        controller.medDrone.inputVector_moving.z = Mathf.Sign(deltaPos_toOwner.z);
                    else controller.medDrone.inputVector_moving.z = 0;
                }
            }
            public void OnEnd() {}
        }

        void AdjustAltitude()
        {
            bool floorDetection = Physics.BoxCast(
                transform.position + new Vector3(0, 0.75f, 0),
                new Vector3(2.0f, 1.0f, 1.0f),
                -Vector3.up,
                transform.rotation,
                preferAltitude,
                0b10000000
            );

            Vector3 deltaPos_toOwner = (medDrone.pawn_owner.transform.position - transform.position);

            if (floorDetection) medDrone.inputVector_moving.y = 1.0f;
            else
            {
                if (Mathf.Abs(deltaPos_toOwner.y) < medDrone.elevationThreshord)
                    medDrone.inputVector_moving.y = 0.0f;
                else if (deltaPos_toOwner.y < 0)
                    medDrone.inputVector_moving.y = -1.0f;
                else if (deltaPos_toOwner.y > 0)
                    medDrone.inputVector_moving.y = 1.0f;
            }
        }

        private void Start()
        {
            behaviour_heal = new Behaviour_Heal(this);
            behaviour_follow = new Behaviour_Follow(this);
        }

        private void OnEnable() => StartCoroutine(BootSequence());
        void FixedUpdate() => currentBehaviour?.OnLoop();

        IEnumerator BootSequence()
        {
            yield return new WaitForSecondsRealtime(0.25f);
            currentBehaviour = behaviour_follow;
            medDrone.isOperating = true;
        }
    }
}