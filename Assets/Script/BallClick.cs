using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallClick : MonoBehaviour
{
    private void OnMouseUp()
    {
        GameManager.instance.BollSetUpPos(this.gameObject);
    }
}