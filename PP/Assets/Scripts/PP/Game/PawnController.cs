using UnityEngine;

namespace PP.Game.PawnController
{
    public class PawnController : MonoBehaviour
    {
        public IPawnCtrler_Behaviour currentBehaviour
        {
            get => _currentBehaviour;
            set
            {
                _currentBehaviour?.OnEnd();
                _currentBehaviour = value;
                _currentBehaviour?.OnBegin();
            }
        }
        protected IPawnCtrler_Behaviour _currentBehaviour = null;
    }
}