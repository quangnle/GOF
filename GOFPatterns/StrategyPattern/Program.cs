using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            while (!game.IsGameEnd())
            {
                game.Update();
                game.Render();
                Thread.Sleep(1000);
            }
        }
    }

    class GameContext
    {
        public int NumHeroes { get; set; }
        public int NumMonsters { get; set; }
    }

    interface IGameStragegy
    {
        void Update(GameContext context);
    }

    class SpawnHeroStragegy : IGameStragegy
    {
        private Random _rnd;

        public SpawnHeroStragegy()
        {
            _rnd = new Random();
        }

        public void Update(GameContext context)
        {
            Console.WriteLine("[STRAGEGY] Spawning heroes...");

            var numSpawnedHeroes = 10 + _rnd.Next() % 10;
            context.NumHeroes += numSpawnedHeroes;
            Console.WriteLine("Spawned {0} heroes", numSpawnedHeroes);
        }
    }

    class KillAllMonsterStragegy : IGameStragegy
    {
        public void Update(GameContext context)
        {
            Console.WriteLine("[STRAGEGY] Killing all monsters...");

            var numDeadMonsters = Math.Min(context.NumHeroes, context.NumMonsters);
            context.NumHeroes -= numDeadMonsters;
            context.NumMonsters -= numDeadMonsters;
        }
    }

    class IdleStragegy : IGameStragegy
    {
        public void Update(GameContext context)
        {
            Console.WriteLine("[STRAGEGY] Zzz...");
        }
    }

    class Game
    {
        private Random _rnd;
        private GameContext _context;
        private IGameStragegy _stragegy;
        private IdleStragegy _idleStragegy;
        private SpawnHeroStragegy _spawnStragegy;
        private KillAllMonsterStragegy _killMonsterStragegy;

        public Game()
        {
            _rnd = new Random();
            _context = new GameContext();
            _idleStragegy = new IdleStragegy();
            _spawnStragegy = new SpawnHeroStragegy();
            _killMonsterStragegy = new KillAllMonsterStragegy();
        }

        public void ChangeStragegy(IGameStragegy stragegy)
        {
            _stragegy = stragegy;
        }

        public void Render()
        {
            Console.WriteLine("Heroes: {0}, monsters: {1}",
                _context.NumHeroes,
                _context.NumMonsters);
        }

        public void Update()
        {
            // execute stragegy
            _stragegy?.Update(_context);

            // spawn monsters
            var numSpawnedMonsters = _rnd.Next() % 10;
            _context.NumMonsters += numSpawnedMonsters;
            Console.WriteLine("Spawned {0} monsters", numSpawnedMonsters);

            // change stragegy
            if (_context.NumHeroes > _context.NumMonsters)
            {
                _stragegy = _killMonsterStragegy;
            }
            else if (_context.NumHeroes > _context.NumMonsters / 2)
            {
                _stragegy = _idleStragegy;
            }
            else
            {
                _stragegy = _spawnStragegy;
            }
        }

        public bool IsGameEnd()
        {
            return false;
        }
    }

}
