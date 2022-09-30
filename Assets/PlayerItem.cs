using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerItem : MonoBehaviour
{
    public Text playerName;

    Image backgroundImage;
    public Color highlightColor;
    public Gameobject leftArrowButton;
    public GameObject rightArrowButton;

    private void Start()
    {
        backgroundImage = GetComponent<Image>();
    }

    public void ApplyLocalChanges()
    {
        backgroundImage.color = highlightColor;
        leftArrowButton.SetActive(true);
        rightArrowButton.SetActive(true);
    }


    public void SetPlayerInfo(Player _player)
    {
        playerName.Text = _player.NickName;
    }
}
