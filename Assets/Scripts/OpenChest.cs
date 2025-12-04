using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenChest : MonoBehaviour
{
    public static OpenChest instance;

    [Header("UI & Panel")]
    public GameObject chestOpenMenuPanel;
    public Image itemIconImage;
    public TextMeshProUGUI itemNameText;
    public Button button;

    [Header("3D Presentation")]
    public GameObject chest;
    public Animator anim;

    private Item selectedItem;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        chestOpenMenuPanel.SetActive(false);

        if (button.onClick.GetPersistentEventCount() == 0)
        {
            button.onClick.AddListener(OnOpenButtonClick);
        }
    }

    public void ShowAward(Item itemData)
    {
        AudioManager.instance.MuteMusicAndAddFilter();

        AudioManager.instance.PlaySFX(R.instance.foundChestSound);

        Time.timeScale = 0f;

        chestOpenMenuPanel.SetActive(true);

        anim.gameObject.SetActive(true);

        selectedItem = itemData;
        itemIconImage.sprite = itemData.icon;
        itemIconImage.gameObject.SetActive(false);

        button.gameObject.SetActive(true);

        StartCoroutine(ResetChestState());
    }

    private IEnumerator ResetChestState()
    {
        yield return null;

        anim.Play("ClosedChest", 0, 0f);
    }

    private IEnumerator OpenSequence(Item itemData)
    {
        anim.Play("open", 0, 0f);

        AudioManager.instance.PlaySFX(R.instance.chestOpeningSound);

        yield return new WaitForSecondsRealtime(1.0f);

        if (R.instance.lootEffect != null)
        {
            Transform glowRays = R.instance.lootEffect.transform.Find("TreasureChestGlowRays");
            if (glowRays != null)
            {
                glowRays.gameObject.SetActive(true);
                glowRays.GetComponent<ParticleSystem>().Play();
            }
        }

        itemIconImage.gameObject.SetActive(true);
        itemNameText.gameObject.SetActive(true);

        var imageUI = itemIconImage.rectTransform.DOScale(1.5f, 1.5f).From(0).SetEase(Ease.OutElastic).SetUpdate(true);

        itemNameText.text = itemData.name.ToString();
        var itemNameUI = itemNameText.gameObject.transform.DOScale(1f, 1.5f).From(0).SetEase(Ease.OutElastic).SetUpdate(true);

        yield return new WaitForSecondsRealtime(2.0f);

        if (R.instance.lootEffect != null)
        {
            foreach (Transform child in R.instance.lootEffect.transform)
            {
                if (child.name != "TreasureChestGlowRays")
                {
                    child.gameObject.SetActive(true);
                    child.GetComponent<ParticleSystem>().Play();
                }
            }
        }

        yield return new WaitForSecondsRealtime(2.0f);

        ItemInventory.instance.Add(itemData, 1);

        CloseReward();

        AudioManager.instance.RestoreMusicSettings();
    }

    public void CloseReward()
    {
        anim.gameObject.SetActive(false);
        chestOpenMenuPanel.SetActive(false);
        Time.timeScale = 1f;

        if (R.instance.lootEffect != null)
        {
            foreach (Transform child in R.instance.lootEffect.transform)
            {
                child.gameObject.SetActive(false);
                child.GetComponent<ParticleSystem>().Stop();
            }
        }
    }

    public void OnOpenButtonClick()
    {
        button.gameObject.SetActive(false);

        StartCoroutine(OpenSequence(selectedItem));
    }
}
