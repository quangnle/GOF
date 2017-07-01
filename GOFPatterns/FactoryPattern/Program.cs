using System;

namespace FactoryPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Spawner spawner = null;

            var isDayTime = DateTime.Now.Hour >= 0 && DateTime.Now.Hour <= 11; // check the current time whether night or day
            if (isDayTime)
                spawner = new DaySpawner();
            else
                spawner = new NightSpawner();

            var monster = spawner.SpawnMonster();
            monster.Walk();
            monster.Attack();
        }
    }

    interface Spawner // Creator
    {
        IMonster SpawnMonster();
    }

    class DaySpawner : Spawner // Concrete spawner to create day time monster
    {
        public IMonster SpawnMonster()
        {
            return new DayMonster();
        }
    }

    class NightSpawner : Spawner // Concrete spawner to create night time monster
    {
        public IMonster SpawnMonster()
        {
            return new NightMonster();
        }
    }

    public interface IMonster // abstract definition of the outcome product
    {
        void Walk();
        void Attack();
}

    public class DayMonster : IMonster // concrete product
    {
        public void Attack()
        {
            Console.WriteLine("Day Monster is attacking");
        }

        public void Walk()
        {
            Console.WriteLine("Day Monster is walking");
        }
    }

    public class NightMonster : IMonster // concrete product
    {
        public void Attack()
        {
            Console.WriteLine("Night Monster is attacking");
        }

        public void Walk()
        {
            Console.WriteLine("Night Monster is walking");
        }
    }
}
