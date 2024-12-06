using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : MonoBehaviour
{
    public static PlayerInventoryUI Instance;
    [SerializeField] private Image ItemImage;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetImage(Sprite sprite)
    {
        ItemImage.color = new Color(255, 255, 255, 1);
        ItemImage.sprite = sprite;
    }
    public void DeleteImage()
    {
        ItemImage.color = new Color(0, 0, 0, 0);
        ItemImage.sprite = null;
    }



}