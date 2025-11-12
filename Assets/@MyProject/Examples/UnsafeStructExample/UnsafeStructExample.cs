using UnityEngine;

namespace Local.Editor.Examples.UnsafeStructExample
{
    public class UnsafeStructExample : MonoBehaviour
    {
        private void Start()
        {
            var data1 = new Data();
            data1.Type = EDataType.TypeA;
            ref var data1Ref = ref data1.GetDataRefAs<TypeA>();
            data1Ref.Id = 1;
            data1Ref.Value = 2;

            var data2 = new Data();
            data2.Type = EDataType.TypeB;
            ref var data2Ref = ref data2.GetDataRefAs<TypeB>();
            data2Ref.X = 1;
            data2Ref.Y = 2;
            data2Ref.Z = 3;
            Modify(ref data2Ref);

            unsafe
            {
                var data3 = new Data();
                data3.Type = EDataType.TypeA;
                var data3Ptr = data3.GetDataPtrAs<TypeA>();
                data3Ptr->Id = 10;
                data3Ptr->Value = 20;

                var data4 = new Data();
                data4.Type = EDataType.TypeB;
                var data4Ptr = data4.GetDataPtrAs<TypeB>();
                data4Ptr->X = 1;
                data4Ptr->Y = 2;
                data4Ptr->Z = 3;
                ModifyUsingPtr(data4Ptr);
            }
        }

        private void Modify(ref TypeB data)
        {
            data.X += 10;
            data.Y += 20;
            data.Z += 30;
        }

        private unsafe void ModifyUsingPtr(TypeB* data)
        {
            data->X += 10;
            data->Y += 20;
            data->Z += 30;
        }
    }

    public enum EDataType
    {
        TypeA,
        TypeB
    }

    public unsafe struct Data
    {
        public EDataType Type;
        public fixed byte Payload[16];
    }

    public struct TypeA
    {
        public int Id;
        public int Value;
    }

    public struct TypeB
    {
        public short X;
        public short Y;
        public short Z;
    }

    public static class TypeADataView
    {
        public static ref T GetDataRefAs<T>(this ref Data s) where T : unmanaged
        {
            unsafe
            {
                fixed (byte* p = s.Payload)
                    return ref *(T*)p;
            }
        }

        public static unsafe T* GetDataPtrAs<T>(this ref Data s) where T : unmanaged
        {
            unsafe
            {
                fixed (byte* p = s.Payload)
                    return (T*)p;
            }
        }
    }
}