using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dropdown_SelectLanguage : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    private void Start()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(
            I2.Loc.LocalizationManager.GetAllLanguages()
                .Select(x => new TMP_Dropdown.OptionData(x))
                .ToList());

        dropdown.onValueChanged.AddListener(x =>
        {
            string language = dropdown.options[x].text;
            I2.Loc.LocalizationManager.SetLanguageAndCode(language, language);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        dropdown.SetValueWithoutNotify(
            I2.Loc.LocalizationManager.GetAllLanguages()
                .IndexOf(I2.Loc.LocalizationManager.CurrentLanguage));
    }
}