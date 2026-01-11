using UnityEngine;
using System.Collections.Generic;

namespace Q17pD.Frostwatch
{
    public class CursorHandler : MonoBehaviour
    {
        private Vector2 _hotSpot = Vector2.zero;
        private Dictionary<string, Texture2D> _cursors = new Dictionary<string, Texture2D>();

        private void Start() { SetCursor("Default"); }
        public void SetCursor(string cursor)
        {
            if (!_cursors.ContainsKey(cursor)) _cursors[cursor] = Resources.Load<Texture2D>("Cursors/" + cursor.ToString());
            Cursor.SetCursor(_cursors[cursor], _hotSpot, CursorMode.Auto);
        }
    }
}