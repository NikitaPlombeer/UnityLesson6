using UnityEngine;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(Follow))]
    public class FollowEditor: UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Follow follow = (Follow) target;
            if (GUILayout.Button("Move"))
            {
                follow.ImmediatelyMove();
            }
        }
    }
}