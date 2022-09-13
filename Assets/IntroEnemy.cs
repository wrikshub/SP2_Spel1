using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEnemy : Health
{
    public override void Kill(DamageArgs d)
    {
        base.Kill(d);
        GameManager.Instance.StartGame();
    }
}
