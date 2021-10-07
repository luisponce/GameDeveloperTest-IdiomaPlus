using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Controls", menuName = "Control Preference", order = 1)] //make it show on the context menu when creating assets for easy creation
public class Controls : ScriptableObject
{
    public KeyCode forwardMovementKC;
    public KeyCode leftMovementKC;
    public KeyCode backMovementKC;
    public KeyCode rightMovementKC;

    public KeyCode attackCK;

    public KeyCode pauseMenuKC;

    public KeyCode hotbar1;
    public KeyCode hotbar2;
    public KeyCode hotbar3;
    public KeyCode hotbar4;
    public KeyCode hotbar5;
}
