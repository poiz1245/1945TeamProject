using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Lazor : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other != null)
            GameManagerSJ.Instance.player.Heart -= 1;
    }

}
