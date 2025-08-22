using UnityEngine;

namespace MyProject.Examples.YarnSpinnerExample.YarnSpinner.Abstraction
{
    public interface ICharActorModel
    {
        string Name { get; }
        string DisplayName { get; set; }
        Color Color { get; }
        bool IsHighlighted { get; set; }
        Vector2 Position { get; set; }
        int SortOrder { get; set; }
    }

    public class CharActorModel : ICharActorModel
    {
        public string Name { get; }
        public string DisplayName { get; set; }
        public Color Color { get; }
        public bool IsHighlighted { get; set; }
        public Vector2 Position { get; set; }
        public int SortOrder { get; set; }
    }
}