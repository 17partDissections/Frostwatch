using UnityEngine;
using System.Collections.Generic;

namespace Q17pD.Frostwatch
{
    public class CursorHandler : MonoBehaviour
    {
        private Vector2 _hotSpot = new Vector2(13,0);
        private Dictionary<string, Texture2D> _cursors = new Dictionary<string, Texture2D>();

        private void Start() { SetCursor("Default"); }
        public void SetCursor(string cursor)
        {
            if (!_cursors.ContainsKey(cursor)) _cursors[cursor] = Resources.Load<Texture2D>("Cursors/" + cursor.ToString());
            Cursor.SetCursor(_cursors[cursor], _hotSpot, CursorMode.Auto);
        }
        public void LockCursorToggle(bool value)
        {
            Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !value;
        }
    }
}