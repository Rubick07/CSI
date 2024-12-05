using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Culprits", menuName = "Culprits")]
public class CulpritSO : ScriptableObject
{
    public string CulpritName;
    public Sprite CulpritPhoto;
    [TextArea]
    public string CulpritDesc;
    //Bukti
    public GameObject[] Bukti;

}
