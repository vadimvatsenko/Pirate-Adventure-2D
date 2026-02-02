using Creatures.Interfaces;
using UnityEngine;

namespace Creatures.Weapons
{
    public class ProjectTile : BaseProjectTile
    {
        protected void Start()
        {
            Vector2 force = new Vector2(FacingDirection * speed, 0);
            Rigidbody.AddForce(force, ForceMode2D.Impulse);
        }
    }
}