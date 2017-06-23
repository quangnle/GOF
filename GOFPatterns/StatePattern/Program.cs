using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StatePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var character = new Character();
            for (int i = 0; i < 20; i++)
            {
                character.Update();
                Thread.Sleep(500);
            }
        }
    }

    interface IState
    {
        void Execute(Character character);
    }

    class StandState : IState
    {
        public void Execute(Character character)
        {
            Console.WriteLine("Standing");
            character.ChangeState(new WalkState());
        }
    }

    class WalkState : IState
    {
        public void Execute(Character character)
        {
            Console.WriteLine("Walking");

            if(MeetStone())
                character.ChangeState(new JumpState());
            else
                character.ChangeState(new WalkState());
        }

        private bool MeetStone()
        {
            return (new Random()).NextDouble() < 0.3;
        }
    }

    class JumpState : IState
    {
        public void Execute(Character character)
        {
            Console.WriteLine("Jumping");
            character.ChangeState(new LandState());
        }
    }

    class LandState : IState
    {
        public void Execute(Character character)
        {
            Console.WriteLine("Landing");
            character.ChangeState(new WalkState());
        }
    }

    class Character
    {
        private IState _state;

        public Character()
        {
            _state = new StandState();
        }

        public void Update()
        {
            _state.Execute(this);
        }

        public void ChangeState(IState state)
        {
            _state = state;
        }
    }
}
