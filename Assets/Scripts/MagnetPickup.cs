using DG.Tweening;
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

        AudioManager.instance.PlaySFX(R.instance.pickupSounds[Random.Range(0, R.instance.pickupSounds.Length)]);
        megaMagnetCoroutine = player.StartCoroutine(MegaMagnetCoroutine(player));

        Destroy(gameObject);
    }

    private IEnumerator MegaMagnetCoroutine(PlayerController player)
    {
        player.pickupMagnetBonus = bonusValue;
        player.UpdateMagnetArea();

        //Debug.Log($"Player magnet bonus (MegaMagnet Pickup is active!): {player.magnetBonus}");

        GameObject magnetVisual = Instantiate(R.instance.circle, player.transform.position, Quaternion.Euler(90f, 0f, 0f));
        magnetVisual.transform.DOScale(bonusValue, 1f).From(0).SetEase(Ease.OutCubic);
        magnetVisual.GetComponent<MeshRenderer>().material.DOFade(0f, 3f).From(1f);

        yield return new WaitForSeconds(megaMagnetTime);

        Destroy(magnetVisual.gameObject);
        
        player.pickupMagnetBonus = 0;
        megaMagnetCoroutine = null;
        player.UpdateMagnetArea();

        //Debug.Log($"MegaMagnet Pickup is end.\nPlayer magnet bonus: {player.magnetBonus}");
    }

}
