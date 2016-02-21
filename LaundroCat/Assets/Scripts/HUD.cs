using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Sprite[] HeartSprites;
    public Image HeartUI;
    private Player player;
    public Sprite[] WeaponToggle;
    public Image PowerUpUI;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (player.currHealth <= 3 )
            HeartUI.sprite = HeartSprites[player.currHealth];
        
        // Weapon UI - Change array to each power-up
        if (player.weapon_beam)
        {
            PowerUpUI.sprite = WeaponToggle[1];
        }
        else
        {
            PowerUpUI.sprite = WeaponToggle[0];
        }
    }
}
