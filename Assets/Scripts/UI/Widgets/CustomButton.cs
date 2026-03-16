using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets
{
    public class CustomButton : Button
    {
        [SerializeField] private GameObject normal;
        [SerializeField] private GameObject pressed;
        [SerializeField] private GameObject highlighted;

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant); 
            
            if(!normal || !pressed || !highlighted) return;
            
            normal.SetActive(state != SelectionState.Pressed && state != SelectionState.Highlighted);
            highlighted.SetActive(state == SelectionState.Highlighted);
            pressed.SetActive(state == SelectionState.Pressed);
        }
    }
}