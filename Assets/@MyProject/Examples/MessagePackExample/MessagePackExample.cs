using UnityEngine;

namespace Examples.MessagePackExample
{
    public class MessagePackExample : MonoBehaviour
    {
        private void Start()
        {
            var data = new TestMasterData(1, "Test Data");
            var bytes = MessagePack.MessagePackSerializer.Serialize(data);
            var deserialized = MessagePack.MessagePackSerializer.Deserialize<TestMasterData>(bytes);
        }

        // private void Save()
        // {
        //     TestMasterData[] datas = new TestMasterData[10];
        //     for (int i = 0; i < datas.Length; i++)
        //     {
        //         datas[i] = new TestMasterData(i, "Test Data " + i);
        //     }
        //
        //     var bytes = MessagePack.MessagePackSerializer.Serialize(datas);
        //     System.IO.File.WriteAllBytes("test.dat", bytes);
        // }
    }
}