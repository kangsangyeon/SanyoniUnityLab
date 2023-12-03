using System.IO;
using System.Linq;
using UnityEngine;
using TemplateTable;

public class TemplateTableJsonExample : MonoBehaviour
{
    private void Start()
    {
        using (var stream = File.Open(
                   $"{Application.streamingAssetsPath}/DataTable/CharacterDataTable.json", FileMode.Open))
        using (var reader = new StreamReader(stream))
        {
            var json = reader.ReadToEnd();
            var _characterTable = new TemplateTable<int, CharacterData>();
            _characterTable.Load(new TemplateTableJsonLoader<int, CharacterData>(json, false));
            _characterTable.Values.ToList().ForEach(x => Debug.Log(x.ToString()));
        }
    }
}