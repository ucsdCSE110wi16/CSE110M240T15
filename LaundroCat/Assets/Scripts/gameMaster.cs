using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameMaster : MonoBehaviour {

    public int laundryPoints = 0;
	  public Text laundryText;
    public Transform playerDeath_sound;
    public Transform playerHurt_sound;
    public Transform enemyDeath_sound;
    public Transform playerJump_sound;
    public Transform weaponUse_sound;


	// to make sure only one weapon spawns per level
	public static bool spawnWeapon = true;

    void Update()
    {
        laundryText.text = ("              " + PlayerPrefs.GetInt("socks"));
    }

    public void playerDeathSound()
    {
      Instantiate(playerDeath_sound, transform.position, transform.rotation);
    }

    public void playerHurtSound()
    {
      Instantiate(playerHurt_sound, transform.position, transform.rotation);
    }

    public void enemyDeathSound()
    {
      Instantiate(enemyDeath_sound, transform.position, transform.rotation);
    }

    public void playerJump()
    {
      Instantiate(playerJump_sound, transform.position, transform.rotation);
    }

    public void useWeapon()
    {
      Instantiate(weaponUse_sound, transform.position, transform.rotation);
    }
}
