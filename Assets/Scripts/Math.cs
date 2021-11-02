using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math : MonoBehaviour
{
    public static void AddVectors2(GameObject objectToMove, float x, float y)
    {
        objectToMove.transform.position = new Vector2(objectToMove.transform.position.x + x, objectToMove.transform.position.y + y);
    }
}
