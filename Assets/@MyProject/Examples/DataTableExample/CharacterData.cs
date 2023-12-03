public class CharacterData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Hp { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
    public int Critical { get; set; }
    public int CriticalDamage { get; set; }
    public int Accuracy { get; set; }
    public int Evasion { get; set; }
    public int Block { get; set; }
    public int Counter { get; set; }

    public override string ToString()
    {
        return
            $"Id: {Id}, Name: {Name}, Hp: {Hp}, Attack: {Attack}, Defense: {Defense}, Speed: {Speed}, Critical: {Critical}, CriticalDamage: {CriticalDamage}, Accuracy: {Accuracy}, Evasion: {Evasion}, Block: {Block}, Counter: {Counter}";
    }
}