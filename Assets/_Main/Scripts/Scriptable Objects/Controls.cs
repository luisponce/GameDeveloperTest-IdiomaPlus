using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Controls", menuName = "Control Preference", order = 1)]
public class Controls : ScriptableObject
{
    public KeyCode forwardMovementKC;
    public KeyCode leftMovementKC;
    public KeyCode backMovementKC;
    public KeyCode rightMovementKC;

    public KeyCode attackCK;
}
