using UnityEngine;
using TMPro;

public class CrowdCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text crowdCounterText;
    [SerializeField] private Transform parent;

    private void Update()
    {
        RunnersCounter();
    }

    private void RunnersCounter()
    {
        crowdCounterText.text = parent.childCount.ToString();

        if (parent.childCount <= 0)
        {
            gameObject.gameObject.SetActive(false);
        }
        
    }
}
