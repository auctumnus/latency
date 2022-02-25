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
            int index =
                Math.Max(Orchestrator.Instance.player.delay, Orchestrator.Instance.player.MinimumLatency(x, y)) +
                Orchestrator.Instance.colorBias;
            if (index >= Orchestrator.Instance.colors.Length)
            {
                index = Orchestrator.Instance.colors.Length - 1;
            }
            sr.color = Orchestrator.Instance.colors[index];
        }
    }
}