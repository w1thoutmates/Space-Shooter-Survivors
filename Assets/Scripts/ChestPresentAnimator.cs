using DG.Tweening;
using UnityEngine;

public class ChestPresentAnimator : MonoBehaviour
{
    public void PlayAppearingAnimation()
    {
        transform.DOKill();

        transform
            .DORotate(new Vector3(0f, 25f, 5f), 0.3f)
            .From(new Vector3(0f, 25f, -25f))
            .SetEase(Ease.OutBack)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                transform
                    .DORotate(new Vector3(0f, 25f, 0f), 0.2f)
                    .SetEase(Ease.OutElastic, 0.5f, 0.8f)
                    .SetUpdate(true);
            });
    }
}
