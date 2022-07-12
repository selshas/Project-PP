using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZakoCtrl : MonoBehaviour
{
    public PP.Game.Pawn_Character pawn_owned;
    public PP.Game.Pawn_Character target;

    Vector3 distance_sense = new Vector3(20.0f, 1.0f, 16f);
    Vector3 distance_combat = new Vector3(10.0f, 1.0f, 1.5f);

    System.Action currentBehaviour = null;

    void Awake()
    {
        currentBehaviour = Behaviour_HoldPosition;
        pawn_owned ??= gameObject.GetComponent<PP.Game.Pawn_Character>();
    }

    void FixedUpdate()
    {
        if (pawn_owned.isActive) currentBehaviour();
        else
        {
            pawn_owned.inputVector_moving.x = 0;
            pawn_owned.inputVector_moving.z = 0;
        }
    }

    void Behaviour_Combat()
    {
        pawn_owned.inputVector_moving.x = 0;
        pawn_owned.inputVector_moving.z = 0;

        ((PP.Game.Pawn_Zako)pawn_owned).Fire();

        Vector3 distanceToPC = (target.transform.position - pawn_owned.transform.position);
        Vector3 toPC_plane = new Vector3(distanceToPC.x, 0, distanceToPC.z);

        pawn_owned.faceDirection = System.Math.Sign(distanceToPC.x);

        if (Mathf.Abs(distanceToPC.x) > distance_combat.x || Mathf.Abs(distanceToPC.z) > distance_combat.z)
            currentBehaviour = Behaviour_Chasing;
    }

    void Behaviour_HoldPosition()
    {
        RaycastHit[] hits = Physics.BoxCastAll(
            transform.position,
            distance_sense, 
            Vector3.right * pawn_owned.faceDirection, 
            transform.rotation, 
            0, 
            (int)(PP.LayerBit.CharacterEntity | PP.LayerBit.IgnoreProjectileOnly)
        );

        if (hits.Length > 0)
        { 
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                PP.Game.Pawn_Character pawn_char = hit.collider.gameObject.GetComponent<PP.Game.Pawn_Character>();
                if (pawn_char == null) continue;
                if (pawn_char.team != pawn_owned.team) target = pawn_char;
            }
        }

        if (target == null) return;

        Vector3 distanceToPC = (target.transform.position - pawn_owned.transform.position);
        if (Mathf.Abs(distanceToPC.x) < distance_sense.x || Mathf.Abs(distanceToPC.z) < distance_sense.z)
            currentBehaviour = Behaviour_Chasing;
    }
    void Behaviour_Chasing()
    {
        Vector3 distanceToPC = (target.transform.position - pawn_owned.transform.position);
        Vector3 toPC_plane = new Vector3(distanceToPC.x, 0, distanceToPC.z);

        float xDist = Mathf.Abs(distanceToPC.x);
        float zDist = Mathf.Abs(distanceToPC.z);

        pawn_owned.faceDirection = System.Math.Sign(distanceToPC.x);

        if (Mathf.Abs(distanceToPC.x) > distance_sense.x || Mathf.Abs(distanceToPC.z) > distance_sense.z)
            currentBehaviour = Behaviour_HoldPosition;
        else if (xDist < distance_combat.x && zDist < distance_combat.z)
            currentBehaviour = Behaviour_Combat;
        else
        {
            pawn_owned.inputVector_moving.x = (xDist > distance_combat.x) ? Mathf.Sign(toPC_plane.x) : 0;
            pawn_owned.inputVector_moving.z = (zDist > distance_combat.z) ? Mathf.Sign(toPC_plane.z) : 0;
        }
    }

}
