using System.Collections.Generic;
using UnityEngine;

namespace Infastructure.Character
{
    public class FinishTimeCalculator
    {
        private readonly float _levelTime;

        public FinishTimeCalculator(float levelTime) =>
            _levelTime = levelTime;

        public void CalculateTimeForAll(List<MoveAlongPath> moveCharacters)
        {
            for (int i = 0; i < moveCharacters.Count; i++)
            {
                float speed = GetLenght(moveCharacters[i]) / _levelTime;
                moveCharacters[i].MoveSpeed = speed;
            }

            foreach (MoveAlongPath moveCharacter in moveCharacters) 
                moveCharacter.StartMove();

        }

        private float GetLenght(MoveAlongPath moveCharacter)
        {
            float lenght = 0;

            for (int i = 0; i < moveCharacter.DrawPath.points.Count - 1; i++)
            {
                lenght += Vector2.Distance(moveCharacter.DrawPath.points[i],
                    moveCharacter.DrawPath.points[i + 1]);
            }

            return lenght;
        }
    }
}