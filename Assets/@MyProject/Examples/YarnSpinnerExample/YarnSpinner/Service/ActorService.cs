using System.Collections.Generic;
using MyProject.Examples.YarnSpinnerExample.YarnSpinner.Abstraction;
using UnityEngine;

namespace MyProject.Examples.YarnSpinnerExample.YarnSpinner.Service
{
    public class ActorService
    {
        private Dictionary<string, ICharActor> _charActors = new();

        public ICharActor GetActor(string charName) => _charActors[charName];

        private void CharCommand(string charName)
        {
            // char db에서 가져온 정보로 char 생성
        }

        private void BackgroundCommand(string backgroundName)
        {
        }

        private void CharPositionCommand(
            string charName,
            Vector2 position)
        {
            _charActors[charName].Position = position;
        }

        private void CharPositionFCommand(
            string charName,
            float x,
            float y)
        {
            _charActors[charName].Position = new Vector2(x, y);
        }

        private void CharDisplayName(
            string charName,
            string displayName)
        {
            _charActors[charName].DisplayName = displayName;
        }

        private void CharAppearanceCommand(
            string charName,
            string appearance,
            string pose)
        {
        }
    }
}