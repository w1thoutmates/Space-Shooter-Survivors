using System.Collections;
using UnityEngine;

public class MagnetPickup : MonoBehaviour, IPickable
{
    public float megaMagnetTime = 5f;

    private float bonusValue = 150f;
    private Coroutine megaMagnetCoroutine;

    public void OnPickup(PlayerController player)
    {
        if (megaMagnetCoroutine != null)
        {
            player.StopCoroutine(megaMagnetCoroutine);

            megaMagnetCoroutine = null;
        }

        megaMagnetCoroutine = player.StartCoroutine(MegaMagnetCoroutine(player));

        Destroy(gameObject);
    }

    private IEnumerator MegaMagnetCoroutine(PlayerController player)
    {
        player.pickupMagnetBonus = bonusValue;
        player.UpdateMagnetArea();

        //Debug.Log($"Player magnet bonus (MegaMagnet Pickup is active!): {player.magnetBonus}");

        yield return new WaitForSeconds(megaMagnetTime);
        
        player.pickupMagnetBonus = 0;
        megaMagnetCoroutine = null;
        player.UpdateMagnetArea();

        //Debug.Log($"MegaMagnet Pickup is end.\nPlayer magnet bonus: {player.magnetBonus}");
    }

}
