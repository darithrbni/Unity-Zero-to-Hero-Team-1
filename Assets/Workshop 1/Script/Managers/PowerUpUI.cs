using System.Collections;
using TMPro;
using UnityEngine;

public class PowerUpUI : MonoBehaviour
{
    public GameObject uiContainer;
    public TextMeshProUGUI textTimer;

    public GameObject iconSpread;
    public GameObject iconRapid;
    public GameObject iconGiant;

    private Coroutine uiCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        HideUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateUI(PowerUpAnimation.JenisPowerUP jenis, float duration)
    {
        if (uiCoroutine != null)
        {
            StopCoroutine(uiCoroutine);
        }

        if (iconSpread != null) iconSpread.SetActive(false);
        if (iconRapid != null) iconRapid.SetActive(false);
        if (iconGiant != null) iconGiant.SetActive(false);

        if (uiContainer != null) uiContainer.SetActive(true);

        switch (jenis)
        {
            case PowerUpAnimation.JenisPowerUP.SpreadShot:
                if (iconSpread != null) iconSpread.SetActive(true);
                break;
            case PowerUpAnimation.JenisPowerUP.RapidFire:
                if (iconRapid != null) iconRapid.SetActive(true);
                break;
            case PowerUpAnimation.JenisPowerUP.GiantBullet:
                if (iconGiant != null) iconGiant.SetActive(true);
                break;
        }

        uiCoroutine = StartCoroutine(CountdownRoutine(duration));
    }

    private IEnumerator CountdownRoutine(float duration)
    {
        float sisaWaktu = duration;

        while (sisaWaktu > 0)
        {
            textTimer.text = Mathf.Ceil(sisaWaktu).ToString() + "s";
            yield return null;
            sisaWaktu -= Time.deltaTime;
        }

        HideUI();
    }

    public void HideUI()
    {
        if (uiContainer != null) uiContainer.SetActive(false);
    }
}
