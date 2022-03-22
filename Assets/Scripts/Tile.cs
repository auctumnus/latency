using System;
using UnityEngine;

namespace Scenes
{
    public class Tile
    {
        public int x, y;
        public SpriteRenderer sr;

        public Tile(int x, int y, SpriteRenderer sr)
        {
            this.x = x;
            this.y = y;
            this.sr = sr;
        }
        public void Redraw()
        {
            sr.color = Orchestrator.Instance.GetColor(Math.Max(Orchestrator.Instance.player.delay, Orchestrator.Instance.player.MinimumLatency(x, y)));
        }
    }
}