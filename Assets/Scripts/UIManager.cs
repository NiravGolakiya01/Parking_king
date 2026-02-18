using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] LinesDrawer linesDrawer;

    [Space]
    [SerializeField] private CanvasGroup availableLineCanvasGroup;
    [SerializeField] private GameObject availableLineHolder;
    [SerializeField] private Image availableLineFill;
    private bool isAvailableLineUIAction = false;

    [Space]
    [SerializeField] Image fadePanel;
    [SerializeField] float fadeDuration;

    private Route activeRoute;

    private void Start()
    {
        fadePanel.DOFade(0f, fadeDuration).From(1f);

        availableLineCanvasGroup.alpha = 0f;

        linesDrawer.OnBingeDraw += OnBingeDrawHandler;
        linesDrawer.OnDraw      += OnDrawHandler;
        linesDrawer.OnEndDraw   += OnEndDrawHandler;
    }

    private void OnBingeDrawHandler(Route route)
    {
        activeRoute = route;

        availableLineFill.color = activeRoute.carColor;
        availableLineFill.fillAmount = 1f;
        availableLineCanvasGroup.DOFade(1f, .3f).From(0f);
        isAvailableLineUIAction = true;
    }

    private void OnDrawHandler()
    {
        if(isAvailableLineUIAction)
        {
            float maxLineLength = activeRoute.maxLineLength;
            float lineLength = activeRoute.line.length;

            availableLineFill.fillAmount = 1 - (lineLength/maxLineLength);
        }
    }

    private void OnEndDrawHandler()
    {
        if (isAvailableLineUIAction)
        {
            isAvailableLineUIAction = false;
            activeRoute = null;

            availableLineCanvasGroup.DOFade(0f, .3f).From(1f);
        }
    }
}

