public enum SkillType
{
    A,
    B
}

public enum SkillGrade
{
    R,
    SR,
    SSR
}

public class SkillData
{
    // CsvHelper는 기본적으로 property를 찾아서 매핑하기 때문에, 모든 멤버를 property로 정의합니다.

    public int Id { get; set; }
    public SkillType Type { get; set; }
    public SkillGrade Grade { get; set; }
    public string Name { get; set; }
    public float Range { get; set; }
    public int Cost { get; set; }

    public override string ToString()
    {
        return
            $"Id: {Id}, Type: {Type}, Grade: {Grade}, Name: {Name}, Range: {Range}, Cost: {Cost}";
    }
}