using UnityEngine;

namespace Items.Traps
{
    public class SawBackForward : TrapSaw
    {
        protected override void SawMovement()
        {
            transform.position = 
                Vector3.MoveTowards(transform.position, 
                    WaypointsPositions[WaypointIndex], 
                    speed * Time.deltaTime);
            

            if (Vector3.Distance(transform.position, WaypointsPositions[WaypointIndex]) < 0.001f)
            {
                WaypointIndex++;
                StartCoroutine(CoolDown(cooldown));
                MovementDirection *= -1;
            }

            if (WaypointIndex >= waypoints.Length)
            {
                WaypointIndex = 0;
            }
        }

        /*protected override void Rotation()
        {
            transform.Rotate(0f,0f, (RotationAngle * MovementDirection) * Time.deltaTime);
        }*/
    }
}