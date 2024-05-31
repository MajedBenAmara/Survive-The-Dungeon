public class Goblin : Enemy
{
    protected override void Start()
    {
        base.Start();
    }
    private void Update()
    {
        Burn();
        ChasePlayer();
        FacePlayer();
    }

}
