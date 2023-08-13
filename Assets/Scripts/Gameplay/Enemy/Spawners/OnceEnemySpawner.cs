namespace Gameplay.Enemy
{
    public class OnceEnemySpawner : EnemySpawner
    {
        protected void Start()
        {
            Spawn();
            Destroy(gameObject);
        }
    }
}