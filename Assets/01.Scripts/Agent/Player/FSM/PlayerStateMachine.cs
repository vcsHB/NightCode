using System;
using System.Collections.Generic;
using Agents.Animate;
using UnityEngine;

namespace Agents.Players.FSM
{
    public class Channel
    {
        public void Invoke()
        {

        }
    }
    public class PlayerStateMachine
    {
        protected Dictionary<string, PlayerState> _stateDictionary = new();
        public PlayerState CurrentState { get; private set; }
        protected Player _player;
        public FeedbackEventController eventController;
        public PlayerRenderer playerRenderer;

        public PlayerStateMachine(Player player)
        {
            _player = player;
            playerRenderer = _player.GetCompo<PlayerRenderer>();
        }


        public virtual void Initialize(string firstState)
        {
            if (playerRenderer == null) Debug.Log("playerRenderer가 널임");
            AddState("Idle", "PlayerIdle", playerRenderer.IdleParam);
            AddState("Move", "PlayerMove", playerRenderer.MoveParam);
            AddState("Jump", "PlayerJump", playerRenderer.JumpParam);
            AddState("Fall", "PlayerFall", playerRenderer.FallParam);
            AddState("Hang", "PlayerHang", playerRenderer.HangParam);
            AddState("Grab", "PlayerGrab", playerRenderer.GrabParam);
            AddState("Pull", "PlayerPull", playerRenderer.PullParam);
            AddState("Swing", "PlayerSwing", playerRenderer.SwingParam);
            AddState("Enter", "PlayerTagEnter", playerRenderer.EnterParam);
            AddState("Exit", "PlayerTagExit", playerRenderer.ExitParam);

            AddState("SlideDown", "PlayerSlideDown", playerRenderer.SlideDownParam);
            AddState("HoldingWall", "PlayerHoldingWall", playerRenderer.HoldWallParam);
            AddState("Climb", "PlayerClimb", playerRenderer.ClimbParam);

            if (_stateDictionary.TryGetValue(firstState, out PlayerState state))
            {
                CurrentState = state;
            }
        }

        public void StartState()
        {
            CurrentState.Enter();

        }

        public void AddState(string id, string typeName, AnimParamSO animParam)
        {
            Type t = Type.GetType($"Agents.Players.FSM.{typeName}State");
            PlayerState state = Activator.CreateInstance(t, _player, this, animParam) as PlayerState;
            _stateDictionary.Add(id, state);
        }

        public void UpdateState()
        {
            CurrentState.UpdateState();
        }

        public void ChangeState(string name)
        {
            if (_stateDictionary.TryGetValue(name, out PlayerState state))
            {
                CurrentState.Exit();
                CurrentState = state;
                CurrentState.Enter();
            }
        }


    }
}