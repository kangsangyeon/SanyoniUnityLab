using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private LoadAddressables addressables;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;

    private IEnumerator Start()
    {
        yield return StartCoroutine(addressables.Load());
        Set(addressables.opHandle.Result.GetComponent<SkillPrefab>());
    }

    public void Set(SkillPrefab _prefab)
    {
        text.text = _prefab.skillName;
        image.sprite = _prefab.sprite;
    }
}