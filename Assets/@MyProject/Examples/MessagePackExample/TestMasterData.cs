using MessagePack;

namespace Examples.MessagePackExample
{
    [MessagePackObject]
    public record TestMasterData
    {
        [Key(0)] public readonly int Id;
        [Key(1)] public readonly string Name;
        // public int a { get; set; } // analyzer에 의해 에러 표시되어야 함.

        [SerializationConstructor]
        public TestMasterData(
            int id,
            string name)
        {
            Id = id;
            Name = name;
        }
    }
}