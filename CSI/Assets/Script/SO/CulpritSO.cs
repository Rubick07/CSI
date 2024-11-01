using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Culprits", menuName = "Culprits")]
public class CulpritSO : ScriptableObject
{
    public string CulpritName;
    public Sprite CulpritPhoto;
    //Bukti
    public GameObject[] Bukti;

}
