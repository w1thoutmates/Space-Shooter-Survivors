using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OpenChest : MonoBehaviour
{
    public static OpenChest instance;

    [Header("UI & Panel")]
    public GameObject chestOpenMenuPanel;
    public Image itemIconImage;
    public Animator uiAnimator;
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
        Time.timeScale = 0f;

        chestOpenMenuPanel.SetActive(true);

        anim.gameObject.SetActive(true);

        selectedItem = itemData;
        itemIconImage.sprite = itemData.icon;
        itemIconImage.gameObject.SetActive(false);

        button.gameObject.SetActive(true);

        StartCoroutine(ResetChestState());
    }

    IEnumerator ResetChestState()
    {
        yield return null;

        anim.Play("ClosedChest", 0, 0f);
    }

    IEnumerator OpenSequence(Item itemData)
    {
        anim.Play("open", 0, 0f);

        yield return new WaitForSecondsRealtime(1.0f);

        itemIconImage.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(2.0f);

        // Партиклы 
        // Звуковой эффект Flash/Pop
        // Запустить Animator UI на itemIconImage (Trigger "ShowItem")

        ItemInventory.instance.Add(itemData, 1);

        CloseReward();

    }

    public void CloseReward()
    {
        anim.gameObject.SetActive(false);
        chestOpenMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnOpenButtonClick()
    {
        button.gameObject.SetActive(false);

        StartCoroutine(OpenSequence(selectedItem));
    }
}
