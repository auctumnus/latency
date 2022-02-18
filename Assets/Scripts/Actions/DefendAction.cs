namespace Scenes.Actions
{
    public class DefendAction: Action
    {
        private int player;

        public DefendAction(int player)
        {
            this.player = player;
        }
        
        public override void Specify(int x, int y)
        {
        }
        public override void Fail(GridController gc)
        {
            
        }

        public override void Execute(Unit unit, GridController gc)
        {
            unit.isPrepared = 2;
        }
    }
}