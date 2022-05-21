using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShotProjectile : ProjectileBase
{
    public override void MoveTowardsReticle()
    {
        // Nothing. We don't want the projectile to go through reticle
    }
}
